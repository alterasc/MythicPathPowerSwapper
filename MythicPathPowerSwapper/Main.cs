using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnityModManagerNet;

namespace MythicPathPowerSwapper;

#if DEBUG
[EnableReloading]
#endif
static class Main
{
    public static bool Enabled;
    public static UnityModManager.ModEntry ModEntry;
    public static SettingsModMenu Settings;
    public static Harmony HarmonyInstance;
    static bool Load(UnityModManager.ModEntry modEntry)
    {
        Settings = new SettingsModMenu();
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        ModEntry = modEntry;
        modEntry.OnToggle = OnToggle;
#if DEBUG
        modEntry.OnUnload = OnUnload;
#endif
        HarmonyInstance.CreateClassProcessor(typeof(SettingsStarter)).Patch();
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


[HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
[HarmonyPriority(Priority.Last)]
internal static class SettingsStarter
{
    private static bool _initialized;

    [HarmonyPostfix]
    static void BlueprintsCache_Init_Patch()
    {
        if (_initialized) return;
        _initialized = true;
        Main.Settings.Initialize();
        PowerSwapper.Init();
        if (Main.Settings.IsLegendarification)
        {
            try
            {
                var patches = Main.HarmonyInstance.CreateClassProcessor(typeof(LegendarifyPatches)).Patch();
                Main.ModEntry.Logger.Log($"Applied {patches.Count} Legendarify patches successfully");
            }
            catch (System.Exception ex)
            {
                Main.ModEntry.Logger.LogException($"Exception when applying Legendarify patches: {ex.Message}", ex);
            }
        }
    }
}