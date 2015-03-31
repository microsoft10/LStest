using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LeagueSharp;
using SharpDX;
using Color = System.Drawing.Color;

/*
    Copyright (C) 2014 Nikita Bernthaler

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace JungleTimers
{
    internal class JungleTimers
    {
        private readonly List<JungleCamp> _jungleCamps = new List<JungleCamp>
        {
            new JungleCamp //Baron
            {
                SpawnTime = TimeSpan.FromSeconds(1200),
                RespawnTimer = TimeSpan.FromSeconds(420),
                Position = new Vector3(4549.126f, 10126.66f, -63.11666f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Baron12.1.1")
                }
            },
            new JungleCamp //Dragon
            {
                SpawnTime = TimeSpan.FromSeconds(150),
                RespawnTimer = TimeSpan.FromSeconds(360),
                Position = new Vector3(9606.835f, 4210.494f, -60.30991f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Dragon6.1.1")
                }
            },
            //Order
            new JungleCamp //Wight
            {
                SpawnTime = TimeSpan.FromSeconds(125),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(2072.131f, 8450.272f, 51.92376f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Gromp13.1.1")
                }
            },
            new JungleCamp //Blue
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(300),
                Position = new Vector3(3820.156f, 7920.175f, 52.21874f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Blue1.1.1"),
                    new JungleMinion("SRU_BlueMini1.1.2"),
                    new JungleMinion("SRU_BlueMini21.1.3")
                }
            },
            new JungleCamp //Wolfs
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(3842.77f, 6462.637f, 52.60973f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Murkwolf2.1.1"),
                    new JungleMinion("SRU_MurkwolfMini2.1.2"),
                    new JungleMinion("SRU_MurkwolfMini2.1.3")
                }
            },
            new JungleCamp //Wraith
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(6926.0f, 5400.0f, 51.0f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Razorbeak3.1.1"),
                    new JungleMinion("SRU_RazorbeakMini3.1.2"),
                    new JungleMinion("SRU_RazorbeakMini3.1.3"),
                    new JungleMinion("SRU_RazorbeakMini3.1.4")
                }
            },
            new JungleCamp //Red
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(300),
                Position = new Vector3(7772.412f, 4108.053f, 53.867f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Red4.1.1"),
                    new JungleMinion("SRU_RedMini4.1.2"),
                    new JungleMinion("SRU_RedMini4.1.3")
                }
            },
            new JungleCamp //Golems
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(8404.148f, 2726.269f, 51.2764f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Krug5.1.2"),
                    new JungleMinion("SRU_KrugMini5.1.1")
                }
            },
            //Chaos
            new JungleCamp //Golems
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(6424.0f, 12156.0f, 56.62551f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Krug11.1.2"),
                    new JungleMinion("SRU_KrugMini11.1.1")
                }
            },
            new JungleCamp //Red
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(300),
                Position = new Vector3(7086.157f, 10866.92f, 56.63499f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Red10.1.1"),
                    new JungleMinion("SRU_RedMini10.1.2"),
                    new JungleMinion("SRU_RedMini10.1.3")
                }
            },
            new JungleCamp //Wraith
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(7970.319f, 9410.513f, 52.50048f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Razorbeak9.1.1"),
                    new JungleMinion("SRU_RazorbeakMini9.1.2"),
                    new JungleMinion("SRU_RazorbeakMini9.1.3"),
                    new JungleMinion("SRU_RazorbeakMini9.1.4")
                }
            },
            new JungleCamp //Wolfs
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(10972.0f, 8306.0f, 62.5235f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Murkwolf8.1.1"),
                    new JungleMinion("SRU_MurkwolfMini8.1.2"),
                    new JungleMinion("SRU_MurkwolfMini8.1.3")
                }
            },
            new JungleCamp //Blue
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(300),
                Position = new Vector3(10938.95f, 7000.918f, 51.8691f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Blue7.1.1"),
                    new JungleMinion("SRU_BlueMini7.1.2"),
                    new JungleMinion("SRU_BlueMini27.1.3")
                }
            },
            new JungleCamp //Wight
            {
                SpawnTime = TimeSpan.FromSeconds(115),
                RespawnTimer = TimeSpan.FromSeconds(100),
                Position = new Vector3(12770.0f, 6468.0f, 51.84151f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("SRU_Gromp14.1.1")
                }
            },
             new JungleCamp //Crab
            {
                SpawnTime = TimeSpan.FromSeconds(150),
                RespawnTimer = TimeSpan.FromSeconds(180),
                Position = new Vector3(10218.0f, 5296.0f, -62.84151f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("Sru_Crab15.1.1")
                }
            },
             new JungleCamp //Crab
            {
                SpawnTime = TimeSpan.FromSeconds(150),
                RespawnTimer = TimeSpan.FromSeconds(180),
                Position = new Vector3(5118.0f, 9200.0f, -71.84151f),
                Minions = new List<JungleMinion>
                {
                    new JungleMinion("Sru_Crab16.1.1")
                }
            }
        };

        private readonly Action _onLoadAction;

        public JungleTimers()
        {
            _onLoadAction = new CallOnce().A(OnLoad);
            Game.OnUpdate += OnGameUpdate;
        }

        private void OnLoad()
        {
            GameObject.OnCreate += ObjectOnCreate;
            GameObject.OnDelete += ObjectOnDelete;
            Drawing.OnDraw += OnDraw;
            Game.PrintChat(
                string.Format(
                    "<font color='#F7A100'>{0} v{1} loaded.</font>",
                    Assembly.GetExecutingAssembly().GetName().Name,
                    Assembly.GetExecutingAssembly().GetName().Version
                    )
                );
        }

        private void OnGameUpdate(EventArgs args)
        {
            try
            {
                _onLoadAction();
                UpdateCamps();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void OnDraw(EventArgs args)
        {
            try
            {
                foreach (JungleCamp minionCamp in _jungleCamps)
                {
                    if (minionCamp.State == JungleCampState.Dead)
                    {
                        float delta = Game.Time - minionCamp.ClearTick;
                        if (delta < minionCamp.RespawnTimer.TotalSeconds)
                        {
                            TimeSpan time = TimeSpan.FromSeconds(minionCamp.RespawnTimer.TotalSeconds - delta);
                            Vector2 pos = Drawing.WorldToMinimap(minionCamp.Position);
                            string display = string.Format("{0}:{1:D2}", time.Minutes, time.Seconds);
                            Drawing.DrawText(pos.X - display.Length*3, pos.Y - 5, Color.Yellow, display);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ObjectOnDelete(GameObject sender, EventArgs args)
        {
            try
            {
                if (sender.Type != GameObjectType.obj_AI_Minion)
                    return;

                var neutral = (Obj_AI_Minion) sender;
                if (neutral.Name.Contains("Minion") || !neutral.IsValid)
                    return;

                foreach (
                    JungleMinion minion in
                        from camp in _jungleCamps
                        from minion in camp.Minions
                        where minion.Name == neutral.Name
                        select minion)
                {
                    minion.Dead = neutral.IsDead;
                    minion.Unit = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ObjectOnCreate(GameObject sender, EventArgs args)
        {
            try
            {
                if (sender.Type != GameObjectType.obj_AI_Minion)
                    return;

                var neutral = (Obj_AI_Minion) sender;

                if (neutral.Name.Contains("Minion") || !neutral.IsValid)
                    return;

                foreach (
                    JungleMinion minion in
                        from camp in _jungleCamps
                        from minion in camp.Minions
                        where minion.Name == neutral.Name
                        select minion)
                {
                    minion.Unit = neutral;
                    minion.Dead = neutral.IsDead;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void UpdateCamps()
        {
            foreach (JungleCamp camp in _jungleCamps)
            {
                bool allAlive = true;
                bool allDead = true;

                foreach (JungleMinion minion in camp.Minions)
                {
                    if (minion.Unit != null)
                        minion.Dead = minion.Unit.IsDead;

                    if (minion.Dead)
                        allAlive = false;
                    else
                        allDead = false;
                }

                switch (camp.State)
                {
                    case JungleCampState.Unknown:
                        if (allAlive)
                        {
                            camp.State = JungleCampState.Alive;
                            camp.ClearTick = 0.0f;
                        }
                        break;
                    case JungleCampState.Dead:
                        if (allAlive)
                        {
                            camp.State = JungleCampState.Alive;
                            camp.ClearTick = 0.0f;
                        }
                        break;
                    case JungleCampState.Alive:
                        if (allDead)
                        {
                            camp.State = JungleCampState.Dead;
                            camp.ClearTick = Game.Time;
                        }
                        break;
                }
            }
        }
    }
}
