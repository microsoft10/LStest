using System;
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
        public static readonly string[] SmiteNames = {"s5_summonersmiteplayerganker", "s5_summonersmiteduel"};
        public static Menu Config;
        public static SpellSlot smiteSlot = SpellSlot.Unknown;
        public static Spell smite;
        
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            Config = new Menu("SG Smite Combo", "SG Smite Combo", true);
            var ts = new Menu("Target Selector", "Target Selector");  
            TargetSelector.AddToMenu(ts);
            Config.AddSubMenu(ts);			
            Config.AddSubMenu(new Menu("Combo", "Combo"));			
            Config.SubMenu("Combo").AddItem(new MenuItem("useSmite", "Use smite in combo?").SetValue(true));
			Config.SubMenu("Combo").AddItem(new MenuItem("rSmite", "Range use").SetValue(new Slider(500, 760, 0)));
            Config.SubMenu("Combo").AddItem(new MenuItem("ComboActive", "Combo").SetValue(new KeyBind(32, KeyBindType.Press)));
            Config.AddToMainMenu();
            SmiteSlot();
            Game.OnUpdate += Game_OnGameUpdate;
            Game.PrintChat("<font color='#000FFF'>SG Simple Smite Combo</font>");
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            if (Config.Item("ComboActive").GetValue<KeyBind>().Active)
            {
                Combo();
            }
        }

      

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Config.Item("rSmite").GetValue<Slider>().Value, TargetSelector.DamageType.Physical);
            if (target == null) return;
            if (target.IsValidTarget(Config.Item("rSmite").GetValue<Slider>().Value) &&
                ObjectManager.Player.Spellbook.CanUseSpell((smiteSlot)) == SpellState.Ready && (smitetype() == "s5_summonersmiteplayerganker" || smitetype() == "s5_summonersmiteduel") && Config.Item("useSmite").GetValue<bool>())
            {
                SmiteSlot();
                ObjectManager.Player.Spellbook.CastSpell(smiteSlot, target);
            }
        }

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
            return "summonersmite";
        }


        public static void SmiteSlot()
        {
            foreach (var spell in ObjectManager.Player.Spellbook.Spells.Where(spell => String.Equals(spell.Name, smitetype(), StringComparison.CurrentCultureIgnoreCase)))
            {
                smiteSlot = spell.Slot;
                smite = new Spell(smiteSlot, Config.Item("rSmite").GetValue<Slider>().Value);
                return;
            }
        }
    }
}
