using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using System;
using System.Collections.Generic;

namespace MythicPathPowerSwapper;

internal class Patches
{

    internal static bool IsPatchedMythic(ClassData classData)
    {
        if (classData == null) return false;
        var guid = classData.CharacterClass.AssetGuid.m_Guid;
        if (Main.Settings.PatchedClass == guid) return true;
        return false;
    }

    [HarmonyPatch(typeof(AdvanceUnitLevel), nameof(AdvanceUnitLevel.RunAction))]
    static class AdvanceUnitLevel_RunAction_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(ref AdvanceUnitLevel __instance)
        {
            UnitEntityData unitEntityData = __instance.Unit.GetValue();
            if (!unitEntityData.IsMainCharacter) return true;
            if (!Main.Settings.IsLegendarification) return true;
            var mythicClass = unitEntityData.Progression.GetCurrentMythicClass();
            if (!IsPatchedMythic(mythicClass)) return true;

            var xpTbl = Legendarify.XPTableDict[unitEntityData.Progression.MythicLevel];
            unitEntityData.Descriptor.Progression.AdvanceExperienceTo(xpTbl[Math.Min(xpTbl.Length - 1, __instance.Level.GetValue())], false);
            return false;
        }
    }

    [HarmonyPatch(typeof(LevelUpUnit), nameof(LevelUpUnit.RunAction))]
    static class LevelUpUnit_RunAction_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(ref LevelUpUnit __instance)
        {
            UnitEntityData unitEntityData = __instance.Unit.GetValue();
            if (!unitEntityData.IsMainCharacter) return true;
            if (!Main.Settings.IsLegendarification) return true;
            var mythicClass = unitEntityData.Progression.GetCurrentMythicClass();
            if (!IsPatchedMythic(mythicClass)) return true;

            var xpTbl = Legendarify.XPTableDict[unitEntityData.Progression.MythicLevel];
            unitEntityData.Descriptor.Progression.AdvanceExperienceTo(xpTbl[__instance.TargetLevel.GetValue()]);
            return false;
        }
    }

    [HarmonyPatch(typeof(UnitExperienceForLevel), nameof(UnitExperienceForLevel.GetValueInternal))]
    static class UnitExperienceForLevel_GetValueInternal_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(ref UnitExperienceForLevel __instance, ref int __result)
        {
            UnitEntityData unitEntityData;
            if (__instance.Unit == null || !__instance.Unit.CanEvaluate() || !__instance.Unit.TryGetValue(out unitEntityData))
            {
                __result = 0;
                return false;
            }
            if (__instance.Level == null || !__instance.Level.CanEvaluate() || !__instance.Level.TryGetValue(out int level))
            {
                __result = 0;
                return false;
            }
            if (!unitEntityData.IsMainCharacter) return true;
            if (!Main.Settings.IsLegendarification) return true;
            var mythicClass = unitEntityData.Progression.GetCurrentMythicClass();
            if (!IsPatchedMythic(mythicClass)) return true;

            var xpTbl = Legendarify.XPTableDict[unitEntityData.Progression.MythicLevel];
            __result = xpTbl[level];
            return false;
        }
    }

    [HarmonyPatch(typeof(LevelUpController), nameof(LevelUpController.GetEffectiveLevel))]
    static class LevelUpController_GetEffectiveLevel_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(ref int __result, UnitEntityData unit)
        {
            UnitEntityData unitEntityData = unit;
            if (unitEntityData is null)
                unitEntityData = Game.Instance.Player.MainCharacter.Value;
            unit = unitEntityData;
            if (unit == null)
            {
                __result = 1;
                return false;
            }
            if (!unitEntityData.IsMainCharacter) return true;
            if (!Main.Settings.IsLegendarification) return true;
            var mythicClass = unitEntityData.Progression.GetCurrentMythicClass();
            if (!IsPatchedMythic(mythicClass)) return true;

            int characterLevel = unit.Progression.CharacterLevel;
            int experience = unit.Progression.Experience;
            var xpTbl = Legendarify.XPTableDict[unitEntityData.Progression.MythicLevel];
            while (characterLevel < 20 && xpTbl[characterLevel + 1] <= experience)
                ++characterLevel;
            __result = characterLevel;
            return false;
        }
    }

    [HarmonyPatch(typeof(UnitProgressionData), nameof(UnitProgressionData.ExperienceTable), MethodType.Getter)]
    static class UnitProgressionData_ExperienceTable_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(ref BlueprintStatProgression __result, UnitProgressionData __instance)
        {
            var unitEntityData = __instance.Owner;
            if (!unitEntityData.IsMainCharacter) return true;
            if (!Main.Settings.IsLegendarification) return true;
            var mythicClass = unitEntityData.Progression.GetCurrentMythicClass();
            if (!IsPatchedMythic(mythicClass)) return true;

            __result = Utils.GetBlueprint<BlueprintStatProgression>(Legendarify.GuidDict[unitEntityData.Progression.MythicLevel]);
            return false;
        }
    }

    [HarmonyPatch(typeof(UnitProgressionData), nameof(UnitProgressionData.MaxCharacterLevel), MethodType.Getter)]
    static class UnitProgressionData_MaxCharacterLevel_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(ref int __result, UnitProgressionData __instance)
        {
            var unitEntityData = __instance.Owner;
            if (!unitEntityData.IsMainCharacter) return true;
            if (!Main.Settings.IsLegendarification) return true;
            var mythicClass = unitEntityData.Progression.GetCurrentMythicClass();
            if (!IsPatchedMythic(mythicClass)) return true;

            __result = 40;
            return false;
        }
    }

}

internal class Legendarify
{
    public const string m3id = "21189474-06c1-4c12-9ed4-0fcb6239651d";
    public const string m4id = "b1e1756e-142e-4f1b-81d0-8906a923a077";
    public const string m5id = "92000e45-241d-4d6e-a376-05451c084025";
    public const string m6id = "0601c7bc-cd20-4d0d-a40e-666096370df3";
    public const string m7id = "019871b7-6e8c-41cb-b9e5-e9e9c0cad75a";
    public const string m8id = "44b20c8e-f137-4b0f-abc0-c59ef9aac324";
    public const string m9id = "f81c03bb-989d-4822-9493-e45fa4128800";
    public static readonly Dictionary<int, string> GuidDict = new()
    {
        { 3, m3id },
        { 4, m4id },
        { 5, m5id },
        { 6, m6id },
        { 7, m7id },
        { 8, m8id },
        { 9, m9id },
        { 10, m9id }
    };

    public static readonly int[] MR3XPTable = new int[]
    {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        69000,
        94000,
        125000,
        170000,
        235000,
        315000,
        430000,
        580000,
        785000,
        1050000,
        1450000,
        1950000,
        2650000,
        3600000,
        4900000,
        6600000,
        8950000,
        12000000,
        16500000,
        22500000,
        30500000,
        41000000,
        55500000,
        75500000,
        100000000,
        140000000,
        190000000,
        255000000,
        345000000,
        465000000,
        635000000,
        860000000
    };
    public static readonly int[] MR4XPTable = new int[]
    {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        67000,
        89000,
        120000,
        155000,
        205000,
        270000,
        360000,
        475000,
        625000,
        830000,
        1100000,
        1450000,
        1900000,
        2550000,
        3350000,
        4400000,
        5850000,
        7700000,
        10000000,
        13500000,
        18000000,
        23500000,
        31000000,
        41000000,
        54000000,
        71500000,
        94500000,
        125000000,
        165000000,
        220000000,
        290000000,
        380000000
    };
    public static readonly int[] MR5XPTable = new int[] {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        66000,
        85000,
        110000,
        140000,
        180000,
        235000,
        300000,
        385000,
        500000,
        645000,
        830000,
        1050000,
        1400000,
        1750000,
        2300000,
        2950000,
        3800000,
        4900000,
        6300000,
        8100000,
        10500000,
        13500000,
        17500000,
        22500000,
        29000000,
        37000000,
        48000000,
        61500000,
        79500000,
        100000000,
        130000000,
        170000000
    };
    public static readonly int[] MR6XPTable = new int[] {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        64000,
        80000,
        100000,
        125000,
        160000,
        200000,
        250000,
        315000,
        395000,
        500000,
        625000,
        790000,
        990000,
        1250000,
        1550000,
        1950000,
        2450000,
        3100000,
        3900000,
        4900000,
        6150000,
        7700000,
        9700000,
        12000000,
        15500000,
        19000000,
        24000000,
        30500000,
        38000000,
        48000000,
        60000000,
        75500000
    };
    public static readonly int[] MR7XPTable = new int[] {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        62000,
        77000,
        94000,
        115000,
        140000,
        170000,
        210000,
        260000,
        315000,
        390000,
        475000,
        580000,
        710000,
        875000,
        1050000,
        1300000,
        1600000,
        1950000,
        2400000,
        2950000,
        3600000,
        4400000,
        5400000,
        6650000,
        8100000,
        9950000,
        12000000,
        15000000,
        18500000,
        22500000,
        27500000,
        33500000
    };
    public static readonly int[] MR8XPTable = new int[] {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        61000,
        73000,
        87000,
        105000,
        125000,
        150000,
        175000,
        210000,
        250000,
        300000,
        360000,
        430000,
        515000,
        610000,
        730000,
        875000,
        1050000,
        1250000,
        1500000,
        1800000,
        2100000,
        2550000,
        3000000,
        3600000,
        4300000,
        5150000,
        6150000,
        7350000,
        8750000,
        10500000,
        12500000,
        15000000
    };
    public static readonly int[] MR9XPTable = new int[] {
        0,
        0,
        2000,
        5000,
        9000,
        15000,
        23000,
        35000,
        51000,
        59000,
        69000,
        81000,
        94000,
        110000,
        125000,
        150000,
        170000,
        200000,
        235000,
        270000,
        315000,
        370000,
        430000,
        500000,
        580000,
        680000,
        790000,
        920000,
        1050000,
        1250000,
        1450000,
        1700000,
        1950000,
        2300000,
        2650000,
        3100000,
        3600000,
        4200000,
        4900000,
        5700000,
        6650000
    };
    public static readonly Dictionary<int, int[]> XPTableDict = new()
    {
        { 3, MR3XPTable },
        { 4, MR4XPTable },
        { 5, MR5XPTable },
        { 6, MR6XPTable },
        { 7, MR7XPTable },
        { 8, MR8XPTable },
        { 9, MR9XPTable },
        { 10, MR9XPTable }
    };
}
