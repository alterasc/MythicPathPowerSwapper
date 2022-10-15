using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnityModManagerNet;

namespace MythicPathPowerSwapper
{
#if DEBUG
    [EnableReloading]
#endif
    static class Main
    {
        public static bool Enabled;
        public static UnityModManager.ModEntry ModEntry;
        public static SettingsModMenu Settings;
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Settings = new SettingsModMenu();
            var harmony = new Harmony(modEntry.Info.Id);
            ModEntry = modEntry;
            modEntry.OnToggle = OnToggle;
#if DEBUG
            modEntry.OnUnload = OnUnload;
#endif
            harmony.PatchAll();
            return true;
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        static bool OnUnload(UnityModManager.ModEntry modEntry)
        {
            return true;
        }
    }

    internal class SettingsStarter
    {
        [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
        internal static class BlueprintsCache_Init_Patch
        {
            private static bool _initialized;

            [HarmonyPostfix]
            static void Postfix()
            {
                if (_initialized) return;
                _initialized = true;
                Main.Settings.Initialize();
                PowerSwapper.Init();
            }
        }
    }
}
