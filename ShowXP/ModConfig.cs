using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using System;
using System.Drawing;
using System.Reflection;

namespace ShowXP
{
    class ModConfig
    {

        private IModHelper helper;

       // public String MiningColor { get; set; } = "Gray";

        public bool DisplayXp { get; set; } = true;
        public bool DisplayLevels { get; set; } = true;

        internal void Reset()
        {
            //MiningColor = Color.Gray.Name;
            DisplayXp  = true;
            DisplayLevels = true;
        }
        internal void Apply()
        {
            helper.WriteConfig(this);
        }
        internal void Register(GMCMAPI gmcm, IModHelper helper, IManifest manifest)
        {
            this.helper = helper;
            
            gmcm.Register(manifest, Reset, Apply);
            //AddOption(gmcm, manifest, nameof(MiningColor));
            AddOption(gmcm, manifest, nameof(DisplayXp));
            AddOption(gmcm, manifest, nameof(DisplayLevels));

        }

        private void AddOption(GMCMAPI gmcm, IManifest manifest, string field)
        {
            PropertyInfo prop = typeof(ModConfig).GetProperty(field);
            if (prop is not null && prop.CanWrite && prop.CanRead)
            {
                Delegate getter = Delegate.CreateDelegate(typeof(Func<>).MakeGenericType(prop.PropertyType), this, prop.GetMethod);
                Delegate setter = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(prop.PropertyType), this, prop.SetMethod);
                string trans() => helper.Translation.Get($"config.{prop.Name.ToLowerInvariant()}");

                switch (prop.GetValue(this))
                {
                    case bool:
                        gmcm.AddBoolOption(manifest, getter as Func<bool>, setter as Action<bool>, trans); break;
                    case float:
                        gmcm.AddNumberOption(manifest, getter as Func<float>, setter as Action<float>, trans); break;
                    case KeybindList:
                        gmcm.AddKeybindList(manifest, getter as Func<KeybindList>, setter as Action<KeybindList>, trans); break;

                }
            }
        }
    }
}
