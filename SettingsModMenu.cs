using Kingmaker.Localization;
using ModMenu.Settings;
using System;
using System.Collections.Generic;

namespace MythicPathPowerSwapper
{
    internal class SettingsModMenu
    {
        private static readonly string RootKey = "AlterAsc.MythicPathPowerSwapper".ToLower();

        public bool IsLegendarification
        {
            get
            {
                return ModMenu.ModMenu.GetSettingValue<int>(GetKey("mythictochange")) > 0 
                    && ModMenu.ModMenu.GetSettingValue<int>(GetKey("mythicsource")) == 9;
            }
        }

        public int MythicToChange => ModMenu.ModMenu.GetSettingValue<int>(GetKey("mythictochange"));
        public int MythicFrom => ModMenu.ModMenu.GetSettingValue<int>(GetKey("mythicsource"));
        public bool UnmythicHero => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("unmythichero"));
        public bool AddAivu => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("addaivu"));
        public bool UnmythicCompanions => ModMenu.ModMenu.GetSettingValue<bool>(GetKey("unmythiccompanions"));

        public Guid PatchedClass
        {
            get
            {
                return MythicToChange switch
                {                    
                    1 => PowerSwapper.AeonId,
                    2 => PowerSwapper.AngelId,
                    3 => PowerSwapper.AzataId,
                    4 => PowerSwapper.DemonId,
                    5 => PowerSwapper.LichId,
                    6 => PowerSwapper.TricksterId,
                    _ => Guid.Empty,
                };
            }
        }

        internal void Initialize()
        {
            List<LocalizedString> mythics = new()
            {
                CreateString("none", "No change"),
                CreateString("aeon", "Aeon"),
                CreateString("angel", "Angel"),
                CreateString("azata", "Azata"),
                CreateString("demon", "Demon"),
                CreateString("lich", "Lich"),
                CreateString("trickster", "Trickster")
            };

            var mythicSource = new List<LocalizedString>(mythics);
            mythicSource.Add(CreateString("partialunmythic", "Remove path powers"));
            mythicSource.Add(CreateString("unmythic", "Remove all mythic powers"));
            mythicSource.Add(CreateString("legendarify", "Legendarify path"));

            ModMenu.ModMenu.AddSettings(
              SettingsBuilder
                .New(GetKey("title"), CreateString("title", "MythicPathPowerSwapper"))
                .AddDropdownList(
                    DropdownList.New(
                        GetKey("mythictochange"),
                        defaultSelected: 0,
                        CreateString("mythictochange", "Mythic that will be changed"),
                        mythics)
                )
                .AddDropdownList(
                    DropdownList.New(
                        GetKey("mythicsource"),
                        defaultSelected: 0,
                        CreateString("mythicsource", "Change to"),
                        mythicSource)
                )
                .AddToggle(
                  Toggle
                    .New(GetKey("unmythichero"), defaultValue: false, CreateString("unmythichero", "Remove Mythic Hero powers"))
                )
                .AddToggle(
                  Toggle
                    .New(GetKey("unmythiccompanions"), defaultValue: false, CreateString("unmythiccompanions", "Remove Mythic Companion powers"))
                )
                .AddToggle(
                  Toggle
                    .New(GetKey("addaivu"), defaultValue: false, CreateString("addaivu", "Adds Aivu to your selected non-Azata path (DANGEROUS)"))
                    .WithLongDescription(CreateString("addaivu-desc", "Adds Aivu to path progression. If your game breaks unexpectedly or locks entirely," +
                    " you've been warned. I've never even tried to test this."))
                )
            );
        }

        private static LocalizedString CreateString(string partialKey, string text)
        {
            return CreateStringInner(GetKey(partialKey, "--"), text);
        }

        private static string GetKey(string partialKey, string separator = ".")
        {
            return $"{RootKey}{separator}{partialKey}";
        }

        private static LocalizedString CreateStringInner(string key, string value)
        {
            LocalizedString result = new()
            {
                m_Key = key
            };
            LocalizationManager.CurrentPack.PutString(key, value);
            return result;
        }
    }
}
