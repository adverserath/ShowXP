//using Microsoft.Xna.Framework;
//using Netcode;
//using StardewModdingAPI.Events;
//using StardewValley;
//using StardewValley.Buildings;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using xTile.Dimensions;

//namespace ShowXP
//{
//    public class SkillGains
//    {
//        public int Mining { get; private set; }
//        public int Farming { get; private set; }
//        public int Fishing { get; private set; }
//        public int Foraging { get; private set; }
//        public int Combat { get; private set; }
//        public int Luck { get; private set; }

//        public void Reset()
//        {
//            Mining = 0;
//            Farming = 0;
//            Fishing = 0;
//            Foraging = 0;
//            Combat = 0;
//            Luck = 0;
//        }

//        public void AddExperience(int skill, int xp)
//        {
//            switch (skill)
//            {
//                case 0:
//                    Farming += xp;
//                    break;
//                case 3:
//                    Mining += xp;
//                    break;
//                case 1:
//                    Fishing += xp;
//                    break;
//                case 2:
//                    Foraging += xp;
//                    break;
//                case 5:
//                    Luck += xp;
//                    break;
//                case 4:
//                    Combat += xp;
//                    break;
//            }
//        }
//    }
//    public class ShowXP
//    {
//        static Dictionary<Farmer, SkillGains> AllSkillGains = new Dictionary<Farmer, SkillGains>();

//        internal static void ShowDebris(Farmer farmer, int value, Color color)
//        {
//            if (farmer == null)
//                return;
//            Debris xpDebris = new Debris(value, new Vector2(farmer.getStandingX() + 40, farmer.getStandingY()), color, 1f, farmer);
//            xpDebris.Chunks[0].xVelocity.Value = 0;
//            xpDebris.Chunks[0].yVelocity.Value = 0;
//            farmer.currentLocation.debris.Add(xpDebris);
//        }
//        internal static void DisplayXp(object sender, OneSecondUpdateTickedEventArgs e)
//        {

//            foreach (KeyValuePair<Farmer, SkillGains> farmerSkill in AllSkillGains)
//            {
//                if(farmerSkill.Value.Mining > 0)
//                {
//                    ShowDebris(farmerSkill.Key, farmerSkill.Value.Mining, Color.LightGray);
////                    Game1.addHUDMessage(new HUDMessage("Mining:"+farmerSkill.Value.Mining));
//                }
//                if (farmerSkill.Value.Farming > 0)
//                {
//                    ShowDebris(farmerSkill.Key, farmerSkill.Value.Farming, Color.Green);
//                }
//                if (farmerSkill.Value.Fishing > 0)
//                {
//                    ShowDebris(farmerSkill.Key, farmerSkill.Value.Fishing, Color.LightSkyBlue);
//                }
//                if (farmerSkill.Value.Foraging > 0)
//                {
//                    ShowDebris(farmerSkill.Key, farmerSkill.Value.Foraging, Color.YellowGreen);
//                }
//                if (farmerSkill.Value.Combat > 0)
//                {
//                    ShowDebris(farmerSkill.Key, farmerSkill.Value.Combat, Color.Orange);
//                    ShowDebris(farmerSkill.Key, farmerSkill.Value.Combat, Color.Orange);
//                }
//                farmerSkill.Value.Reset();
//            }
            
//        }
//        internal static void gainExperience(int which, int howMuch, Farmer __instance)
//        {
//            if (!AllSkillGains.ContainsKey(__instance))
//            {
//                AllSkillGains.Add(__instance, new SkillGains());
//            }
//                    if (howMuch == 0)
//                        return;

//            AllSkillGains[__instance].AddExperience(which, howMuch);

//        }

//        internal static void LevelUp(object sender, LevelChangedEventArgs e)
//        {
//            e.Player.currentLocation.playSound("achievement");
//            HUDMessage hm = new HUDMessage($"{e.Player.Name} increased {e.Skill.ToString()} to level {e.NewLevel}",1);
//            Game1.addHUDMessage(hm);
//        }

//    }
//}