using Microsoft.Xna.Framework;
using SpaceCore;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;

namespace ShowXP
{
    public class ModEntry : Mod
    {
        private ModConfig Config;


        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            
            helper.Events.GameLoop.OneSecondUpdateTicked += this.DisplayXp;
            helper.Events.Player.LevelChanged += this.LevelUp;
            helper.Events.GameLoop.GameLaunched += this.OnLaunch;
        }

        static Dictionary<Farmer, SkillGains> AllSkillGains = new Dictionary<Farmer, SkillGains>();
        private GMCMAPI GMCM;

        internal void DisplayXp(object sender, OneSecondUpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
            {
                if (AllSkillGains.Count > 0)
                    AllSkillGains.Clear();
                return;
            }

            foreach (Farmer farmer in Game1.getAllFarmers())
            {
                if (!AllSkillGains.ContainsKey(farmer))
                {
                    AllSkillGains.Add(farmer, new SkillGains(farmer));
                }

            }
            foreach (SkillGains skills in AllSkillGains.Values)
            {
                skills.Update(this.Config.DisplayXp);
            }

        }

        internal void LevelUp(object sender, LevelChangedEventArgs e)
        {
            if (this.Config.DisplayLevels)
            {
                e.Player.currentLocation.playSound("achievement");
                HUDMessage hm = new HUDMessage($"{e.Player.Name} increased {e.Skill.ToString()} to level {e.NewLevel}", 1);
                Game1.addHUDMessage(hm);
            }
        }

        internal void OnLaunch(object sender, GameLaunchedEventArgs e)
        {
            if (Helper.ModRegistry.IsLoaded("spacechase0.GenericModConfigMenu"))
            {
                if (Helper.ModRegistry.Get("spacechase0.GenericModConfigMenu").Manifest.Version.IsOlderThan("1.5.0"))
                {
                    Monitor.Log(Helper.Translation.Get("config.warn", new { v = "1.5.0" }), LogLevel.Warn);
                }
                else
                {
                    GMCM = Helper.ModRegistry.GetApi<GMCMAPI>("spacechase0.GenericModConfigMenu");
                    Config.Register(GMCM, Helper, ModManifest);
                }
            }
        }
    }
    public class SkillGains
    {
        private Farmer _farmer;
        public SkillGains(Farmer farmer)
        {
            _farmer = farmer;
        }
        public bool DisplayXp { get; set; }
        private int? mining;
        public int Mining
        {
            get { return mining.GetValueOrDefault(0); }
            set
            {
                if (!mining.HasValue)
                {
                    mining = value;
                    return;
                }
                if (mining < value)
                {
                    int difference = value - mining.GetValueOrDefault(0);
                    ShowDebris(_farmer, difference, Color.LightGray);
                    mining = value;
                }
            }
        }

        private int? farming;
        public int Farming
        {
            get { return farming.GetValueOrDefault(0); }
            set
            {
                if (!farming.HasValue)
                {
                    farming = value;
                    return;
                }
                if (farming < value)
                {
                    int difference = value - farming.GetValueOrDefault(0);
                    ShowDebris(_farmer, difference, Color.Green);
                    farming = value;
                }
            }
        }
        private int? fishing;
        public int Fishing
        {
            get { return fishing.GetValueOrDefault(0); }
            set
            {
                if (!fishing.HasValue)
                {
                    fishing = value;
                    return;
                }
                if (fishing < value)
                {
                    int difference = value - fishing.GetValueOrDefault(0);
                    ShowDebris(_farmer, difference, Color.LightSkyBlue);
                    fishing = value;
                }
            }
        }
        private int? foraging;
        public int Foraging
        {
            get { return foraging.GetValueOrDefault(0); }
            set
            {
                if (!foraging.HasValue)
                {
                    foraging = value;
                    return;
                }
                if (foraging < value)
                {
                    int difference = value - foraging.GetValueOrDefault(0);
                    ShowDebris(_farmer, difference, Color.GreenYellow);
                    foraging = value;
                }
            }
        }

        private int? combat;
        public int Combat
        {
            get { return combat.GetValueOrDefault(0); }
            set
            {
                if (!combat.HasValue)
                {
                    combat = value;
                    return;
                }
                if (combat < value)
                {
                    int difference = value - combat.GetValueOrDefault(0);
                    ShowDebris(_farmer, difference, Color.Orange);
                    combat = value;
                }
            }
        }
        public void Update(bool displayXP)
        {
            if (_farmer.IsMainPlayer || _farmer.IsLocalPlayer)
            {
                foreach (var s in Skills.GetSkillList())
                {
                    int lvl = Skills.GetExperienceFor(_farmer, s);
                    int lastIndex = s.LastIndexOf('.');
                    if (lastIndex >= 0 && lastIndex < s.Length - 1)
                    {
                        string trimmed = s.Substring(lastIndex + 1);
                    }
                }
                
                Farming = _farmer.experiencePoints[0];
                Fishing = _farmer.experiencePoints[1];
                Foraging = _farmer.experiencePoints[2];
                Mining = _farmer.experiencePoints[3];
                Combat = _farmer.experiencePoints[4];
            }
            DisplayXp = displayXP;
        }

        internal void ShowDebris(Farmer farmer, int value, Color color)
        {
            if (farmer == null || !DisplayXp)
                return;

            Debris xpDebris = new Debris(value, new Vector2(farmer.getStandingPosition().X + 40, farmer.getStandingPosition().Y), color, 1f, farmer);
            xpDebris.Chunks[0].xVelocity.Value = 0;
            xpDebris.Chunks[0].yVelocity.Value = 0;
            farmer.currentLocation.debris.Add(xpDebris);
        }
    }
}
