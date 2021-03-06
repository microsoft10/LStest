﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;


namespace MasterYiByPrunes
{
    class Program
    {
        public static Obj_AI_Base Player = ObjectManager.Player;
        public static readonly int[] RedMachete = { 3715, 3718, 3717, 3716, 3714 };
        public static readonly int[] BlueMachete = { 3706, 3710, 3709, 3708, 3707 };
		public static readonly int[] PurpleMachete = { 3723, 3724, 3726, 3725, 3713 };
        public static readonly string[] SmiteNames = {"s5_summonersmiteplayerganker", "s5_summonersmiteduel", "itemsmiteaoe"};
        public static Menu Config;
        public static SpellSlot smiteSlot = SpellSlot.Unknown;
        public static Spell smite;
        
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {	
			if (ObjectManager.Player.GetSpellSlot("summonersmite") == SpellSlot.Unknown)
            {
				if (ObjectManager.Player.GetSpellSlot("s5_summonersmiteplayerganker") == SpellSlot.Unknown)
				{
					if (ObjectManager.Player.GetSpellSlot("s5_summonersmiteduel") == SpellSlot.Unknown)
					{
						if (ObjectManager.Player.GetSpellSlot("itemsmiteaoe") == SpellSlot.Unknown)
						{
				                return;
						}
					}
				}
            }
            Config = new Menu("SG Smite Combo", "SG Smite Combo", true);
            var ts = new Menu("Target Selector", "Target Selector");  
            TargetSelector.AddToMenu(ts);
            Config.AddSubMenu(ts);			
            Config.AddSubMenu(new Menu("Combo", "Combo"));			
            Config.SubMenu("Combo").AddItem(new MenuItem("useSmite", "Use smite in combo?").SetValue(new KeyBind("Z".ToCharArray()[0], KeyBindType.Toggle)));
			Config.SubMenu("Combo").AddItem(new MenuItem("rSmite", "Range use (Red, Blue)").SetValue(new Slider(300, 0, 500)));
			Config.SubMenu("Combo").AddItem(new MenuItem("ComboActive", "Combo").SetValue(new KeyBind(32, KeyBindType.Press)));
			
			Config.SubMenu("Save").AddItem(new MenuItem("sSmite", "Save me with monsters?").SetValue(true));
            Config.SubMenu("Save").AddItem(new MenuItem("hSmite", "HP use (Purple)").SetValue(new Slider(20, 0, 100)));
			Config.SubMenu("Save").AddItem(new MenuItem("mSmite", "MP use (Purple)").SetValue(new Slider(10, 0, 100)));
			
			Config.SubMenu("KS").AddItem(new MenuItem("ks", "LOL, u are junger -.-' u want ks ur team ?").SetValue(true));
            
			            
            Config.AddToMainMenu();
            SmiteSlot();
            Game.OnUpdate += Game_OnGameUpdate;
            Game.PrintChat("<font color='#000FFF'>SG Simple Smite Combo</font>");
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            if (Config.Item("ComboActive").GetValue<KeyBind>().Active && Config.Item("useSmite").GetValue<KeyBind>().Active)
            {
                Combo();
            }
		/* 	if (Config.Item("sSmite").GetValue<KeyBind>().Active)
            {
                Save();
            } */
			
			var enemys = ObjectManager.Get<Obj_AI_Hero>().Where(h => !h.IsAlly && !h.IsDead && Player.Distance(h, false) <= 500);
            if (enemys == null || !Config.Item("ks").GetValue<KeyBind>().Active)
                return;
            float dmgb = DamageB();
			float dmgr = DamageR();
            foreach (var enemy in enemys)
            {
                if (smitetype() == "s5_summonersmiteplayerganker" && enemy.Health <= dmgb)
                {
                    SmiteSlot();
                ObjectManager.Player.Spellbook.CastSpell(smiteSlot, enemy);
                }
				if (smitetype() == "s5_summonersmiteduel" && enemy.Health <= dmgr)
                {
                    SmiteSlot();
                ObjectManager.Player.Spellbook.CastSpell(smiteSlot, enemy);
                }

            }
			
			var target = TargetSelector.GetTarget(1000, TargetSelector.DamageType.Physical);
			var minion = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, 500, MinionTypes.All, MinionTeam.Neutral).FirstOrDefault();; 
			
            if (!Config.Item("sSmite").GetValue<KeyBind>().Active) return;
			             			
			if (minion != null && target != null && (Player.HealthPercent <= Config.Item("hSmite").GetValue<Slider>().Value || Player.ManaPercent <= Config.Item("mSmite").GetValue<Slider>().Value) &&
                ObjectManager.Player.Spellbook.CanUseSpell((smiteSlot)) == SpellState.Ready && smitetype() == "itemsmiteaoe")
            {
                SmiteSlot();
                ObjectManager.Player.Spellbook.CastSpell(smiteSlot, minion);
            }
			
        }

      

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(500, TargetSelector.DamageType.Physical);
			
            if (target == null) return;
			   
            if (target.IsValidTarget(Config.Item("rSmite").GetValue<Slider>().Value) &&
                ObjectManager.Player.Spellbook.CanUseSpell((smiteSlot)) == SpellState.Ready && (smitetype() == "s5_summonersmiteplayerganker" || smitetype() == "s5_summonersmiteduel"))
            {
                SmiteSlot();
                ObjectManager.Player.Spellbook.CastSpell(smiteSlot, target);
            }
			
			
			
        }
		
		/* public static void Save()
        {
            var target = TargetSelector.GetTarget(1000, TargetSelector.DamageType.Physical);
			var minion = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, 500, MinionTypes.All, MinionTeam.Neutral).FirstOrDefault();; 
			
            if (target == null) return;
			             			
			if (minion != null && (Player.HealthPercent <= Config.Item("hSmite").GetValue<Slider>().Value || Player.ManaPercent <= Config.Item("mSmite").GetValue<Slider>().Value) &&
                ObjectManager.Player.Spellbook.CanUseSpell((smiteSlot)) == SpellState.Ready && smitetype() == "itemsmiteaoe")
            {
                SmiteSlot();
                ObjectManager.Player.Spellbook.CastSpell(smiteSlot, minion);
            }
			
        } */
		
	
		

        public static string smitetype()
        {
            if (BlueMachete.Any(id => Items.HasItem(id)))
            {
                return "s5_summonersmiteplayerganker";
            }
            if (RedMachete.Any(id => Items.HasItem(id)))
            {
                return "s5_summonersmiteduel";
            }
			if (PurpleMachete.Any(id => Items.HasItem(id)))
            {
                return "itemsmiteaoe";
            }
            return "summonersmite";
        }
		
		public static void SmiteSlot()
        {
            foreach (var spell in ObjectManager.Player.Spellbook.Spells.Where(spell => String.Equals(spell.Name, smitetype(), StringComparison.CurrentCultureIgnoreCase)))
            {
                smiteSlot = spell.Slot;
                smite = new Spell(smiteSlot, 500);
                return;
            }
        }
		
		 private static float DamageB()
        {
            int lvlb = ObjectManager.Player.Level;
            int damageb = (20 + 8 * lvlb);

            return damageb;
        }
		
		 private static float DamageR()
        {
            int lvlr = ObjectManager.Player.Level;
            int damager = (54 + 6 * lvlr);

            return damager;
        }
		
    }
}
