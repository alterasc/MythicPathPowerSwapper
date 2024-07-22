using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MythicPathPowerSwapper;

internal class PowerSwapper
{
    internal static void Init()
    {
        new PowerSwapper().Swap();
    }

    private class MythicClass
    {
        public BlueprintCharacterClass MClass;
        public BlueprintSpellbook Spellbook;
        public BlueprintProgression Progression;
        public BlueprintProgression SummonProgression;
        public BlueprintItemEquipmentShoulders Cloak;

        public MythicClass(Guid classGuid)
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(classGuid);
            Spellbook = MClass.Spellbook;
            Progression = MClass.Progression;
        }
    }

    internal static readonly Guid AeonId = Guid.Parse("15a85e67b7d69554cab9ed5830d0268e");
    internal static readonly Guid AngelId = Guid.Parse("a5a9fe8f663d701488bd1db8ea40484e");
    internal static readonly Guid AzataId = Guid.Parse("9a3b2c63afa79744cbca46bea0da9a16");
    internal static readonly Guid DemonId = Guid.Parse("8e19495ea576a8641964102d177e34b7");
    internal static readonly Guid LichId = Guid.Parse("5d501618a28bdc24c80007a5c937dcb7");
    internal static readonly Guid TricksterId = Guid.Parse("8df873a8c6e48294abdb78c45834aa0a");

    private readonly MythicClass _aeon = new(AeonId)
    {
        SummonProgression = Utils.GetBlueprint<BlueprintProgression>("f2305312ccccc2a46a49ba834ff7a092"),
        Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("b24d6185acea1f949b026c3b58e47947")
    };

    private readonly MythicClass _angel = new(AngelId)
    {
        SummonProgression = Utils.GetBlueprint<BlueprintProgression>("038090ec6b6a205418665f2489606534"),
        Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("2f45a11adb74b5a4a81f857a9886c5bd")
    };

    private readonly MythicClass _azata = new(AzataId)
    {
        SummonProgression = Utils.GetBlueprint<BlueprintProgression>("b172d5db251e3e0499671074ee15f7df"),
        Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("78cd50deada655e4cbe49765c0bbb7e4")
    };

    private readonly MythicClass _demon = new(DemonId)
    {
        SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7764f540fbab8ab4c95492f1d8d4f04f"),
        Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("cdc95a3c4a74a874a895b3be61369564")
    };

    private readonly MythicClass _lich = new(LichId)
    {
        SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7de9c45b07635e2418dffc185bd2eff4"),
        Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("1622ceba1d7829b4f9ee709b71bd6baf")
    };

    private readonly MythicClass _trickster = new(TricksterId)
    {
        SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7c6a97566f7125c4c839720200311c3c"),
        Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("50b398d4630d9f244a5db288124ff181")
    };

    private readonly BlueprintCharacterClassReference _mythicHeroClassRef = Utils.GetBlueprintReference<BlueprintCharacterClassReference>("247aa787806d5da4f89cfc3dff0b217f");

    private readonly BlueprintFeatureBaseReference _mythicAbilitySelection = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("ba0e5a900b775be4a99702f1ed08914d");
    private readonly BlueprintFeatureBaseReference _mythicFeatSelection = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("9ee0f6745f555484299b0a1563b99d81");
    private void Swap()
    {
        CreateLegendarifyBlueprints();

        var mTo = Main.Settings.MythicToChange;
        var mFrom = Main.Settings.MythicFrom;
        Main.ModEntry.Logger.Log($"Changing {mTo} to {mFrom}");
        if (mTo != 0)
        {
            Dictionary<int, MythicClass> m = new()
            {
                { 1, _aeon },
                { 2, _angel },
                { 3, _azata },
                { 4, _demon },
                { 5, _lich },
                { 6, _trickster }
            };
            var mClassTo = m[mTo];
            if (mFrom > 0)
            {
                if (mFrom <= 6)
                {
                    var mClassFrom = m[mFrom];
                    Main.ModEntry.Logger.Log($"Replacing {mClassTo.MClass} with {mClassFrom.MClass}");
                    BasicReplace(mClassFrom, mClassTo);
                    if (mClassFrom.MClass == _aeon.MClass)
                    {
                        AeonFromUpdate(mClassTo);
                    }
                    else if (mClassFrom.MClass == _azata.MClass)
                    {
                        AzataFromUpdate(mClassTo);
                    }
                    else if (mClassFrom.MClass == _lich.MClass)
                    {
                        LichFromUpdate(mClassTo);
                    }
                }
                else if (mFrom == 7)
                {
                    PartialUnMythic(mClassTo);
                }
                else if (mFrom == 8)
                {
                    UnMythic(mClassTo);
                }
                else if (mFrom == 9)
                {
                    LegendarifyClass(mClassTo);
                }
            }
            if (Main.Settings.AddAivu && mClassTo.MClass != _azata.MClass)
            {
                Main.ModEntry.Logger.Log($"Adding Aivu to {mClassTo}");
                AddAivu(mClassTo);
            }
        }
        if (Main.Settings.UnmythicHero)
        {
            Main.ModEntry.Logger.Log($"Removing mythic powers from Mythic Hero");
            UnMythicStarter();
        }

        if (Main.Settings.UnmythicCompanions)
        {
            Main.ModEntry.Logger.Log($"Removing mythic powers from Mythic Companion");
            var mythicCompanionProgression = Utils.GetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
            mythicCompanionProgression.LevelEntries = Enumerable.Range(1, 10).Select(x => new LevelEntry() { Level = x }).ToArray();
        }
    }

    private void BasicReplace(MythicClass mClassFrom, MythicClass mClassTo)
    {
        var classToRef = mClassTo.MClass.ToReference<BlueprintCharacterClassReference>();

        if (mClassFrom.MClass == _azata.MClass)
        {
            var aivuFeatures = new BlueprintFeatureBaseReference[] {
                Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("2780764bf33c46745b11f0e1d2d20092"),
                Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("cf36f23d60987224696f03be70351928"),
                Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("4d9785fa28ab443289497ccb05e49fe2"),
                Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("1bfc72ee31e349ab91991d14e1db471e"),
                Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("e0cd072417ac444a99e83eae51eea8df")
            };
            mClassTo.Progression.LevelEntries = mClassFrom.Progression.LevelEntries.Select(x =>
            {
                var filtered = x.m_Features.Where(f => !aivuFeatures.Contains(f)).ToList();
                return new LevelEntry() { Level = x.Level, m_Features = filtered };
            }).ToArray();
        }
        else
        {
            mClassTo.Progression.LevelEntries = mClassFrom.Progression.LevelEntries;
        }
        if (mClassTo.MClass == _azata.MClass)
        {
            AddAivu(mClassTo);
        }
        mClassTo.Progression.UIGroups = mClassFrom.Progression.UIGroups;
        mClassTo.MClass.m_Spellbook = mClassFrom.Spellbook.ToReference<BlueprintSpellbookReference>();
        mClassFrom.Spellbook.m_CharacterClass = classToRef;
        mClassTo.MClass.m_SignatureAbilities = mClassFrom.MClass.m_SignatureAbilities;

        mClassFrom.SummonProgression.m_Classes = [new() { m_Class = classToRef }];
        foreach (var entry in mClassFrom.SummonProgression.LevelEntries)
        {
            foreach (var feature in entry.m_Features)
            {
                var bp = feature.GetBlueprint() as BlueprintFeature;
                foreach (var comp in bp.GetComponents<AddFeatureOnClassLevel>())
                {
                    comp.m_Class = classToRef;
                }
            }
        }

        mClassTo.Cloak.m_Enchantments = mClassFrom.Cloak.m_Enchantments;
    }

    private void AeonFromUpdate(MythicClass classTo)
    {
        var classToRef = classTo.MClass.ToReference<BlueprintCharacterClassReference>();

        var aeonGazeResource = Utils.GetBlueprint<BlueprintAbilityResource>("905722fe39d87474aa6d41bffa327ff3");
        var classes = aeonGazeResource.m_MaxAmount.m_Class.Where(x => x != _aeon.MClass.ToReference<BlueprintCharacterClassReference>()).ToList();
        classes.Add(classToRef);
        aeonGazeResource.m_MaxAmount.m_Class = classes.ToArray();
    }

    private void AzataFromUpdate(MythicClass classTo)
    {
        var classToRef = classTo.MClass.ToReference<BlueprintCharacterClassReference>();

        var azataPerfomanceResource = Utils.GetBlueprint<BlueprintFeature>("02c96331ed2d87d43a4a3509142678b8");
        azataPerfomanceResource.GetComponents<IncreaseResourcesByClass>()
            .Where(x => x.m_CharacterClass == _azata.MClass.ToReference<BlueprintCharacterClassReference>())
            .ForEach(x => x.m_CharacterClass = classToRef);

        var lifeBondingFriendshipProgression = Utils.GetBlueprint<BlueprintProgression>("6c85301c50c6621409b42b83ce9cc6d9");
        lifeBondingFriendshipProgression.m_Classes = [new() { m_Class = classToRef }];

        var marvelousEnduranceFastHealingProperty = Utils.GetBlueprint<BlueprintUnitProperty>("ca1b4b40b9f5407f904738b575bac1ca");
        marvelousEnduranceFastHealingProperty.GetComponents<SummClassLevelGetter>()
            .ForEach(x => x.m_Class = [classToRef, _mythicHeroClassRef]);
    }

    private void LichFromUpdate(MythicClass classTo)
    {
        var classToRef = classTo.MClass.ToReference<BlueprintCharacterClassReference>();

        var deathOfElementsConsumingElementsResource = Utils.GetBlueprint<BlueprintAbilityResource>("7a558d186755620439e35817f174f749");
        var maxAm = deathOfElementsConsumingElementsResource.m_MaxAmount;
        maxAm.m_Class = [classToRef];
        maxAm.m_ClassDiv = [_mythicHeroClassRef, classToRef];

        var lichSkeletalUpgradeSelection = Utils.GetBlueprint<BlueprintFeatureSelection>("a434dddab8026e947bc16eb36d18a783");
        foreach (var feature in lichSkeletalUpgradeSelection.m_AllFeatures)
        {
            if (feature.GetBlueprint() is BlueprintProgression progression)
            {
                progression.GetComponents<PrerequisiteClassLevel>()
                    .ForEach(x => x.m_CharacterClass = classToRef);
                progression.m_Classes = [new() { m_Class = classToRef }];
            }
        }
    }

    private void UnMythic(MythicClass classTo)
    {
        classTo.MClass.m_SignatureAbilities = [];
        classTo.Progression.LevelEntries = Enumerable.Range(1, 8).Select(x => new LevelEntry() { Level = x }).ToArray();
        classTo.MClass.m_Spellbook = null;
        if (classTo.MClass == _azata.MClass)
        {
            AddAivu(classTo);
        }
        var mythicStartingProgression = Utils.GetBlueprint<BlueprintProgression>("af4ee0acb9114e544bf02f39027966b0");
        mythicStartingProgression.LevelEntries.Where(x => x.Level > 2).ForEach(x => x.m_Features = []);
    }

    private void UnMythicStarter()
    {
        var mythicStartingProgression = Utils.GetBlueprint<BlueprintProgression>("af4ee0acb9114e544bf02f39027966b0");
        mythicStartingProgression.LevelEntries[0].m_Features = [];
        mythicStartingProgression.LevelEntries[1].m_Features =
        [
            Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("bef12729fa3f9b34488634f61751e295")
        ];
    }
    private void PartialUnMythic(MythicClass classTo)
    {
        classTo.MClass.m_SignatureAbilities = [];
        classTo.Progression.LevelEntries = Enumerable.Range(1, 8)
            .Select(x =>
            {
                var features = new List<BlueprintFeatureBaseReference>();
                if (x % 2 == 1)
                {
                    features.Add(_mythicAbilitySelection);
                }
                else
                {
                    features.Add(_mythicFeatSelection);
                }
                if (x == 1)
                {
                    features.Add(Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("24e78475f0a243e1a810452d14d0a1bd"));
                }
                return new LevelEntry() { Level = x, m_Features = features };
            }
            ).ToArray();
        classTo.MClass.m_Spellbook = null;
        if (classTo.MClass == _azata.MClass)
        {
            AddAivu(classTo);
        }
    }

    /// <summary>
    /// Creates Legendarify blueprints
    /// </summary>
    private void CreateLegendarifyBlueprints()
    {
        for (int i = 3; i < 10; i++)
        {
            Utils.CreateBlueprint<BlueprintStatProgression>($"LegendarifiedMR{i}XPTable", Legendarify.GuidDict[i], bp =>
            {
                bp.Bonuses = Legendarify.XPTableDict[i];
            });
        }
        var legendarificationStatBonus = Utils.CreateBlueprint<BlueprintFeature>($"LegendarifyStatBonus", "6e052ad3-ac5e-483e-ba4d-f693629919af", bp =>
        {
            bp.AddComponent(new AddStatBonus()
            {
                Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                Stat = Kingmaker.EntitySystem.Stats.StatType.Strength,
                Value = 1
            });
            bp.AddComponent(new AddStatBonus()
            {
                Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                Stat = Kingmaker.EntitySystem.Stats.StatType.Dexterity,
                Value = 1
            });
            bp.AddComponent(new AddStatBonus()
            {
                Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                Stat = Kingmaker.EntitySystem.Stats.StatType.Constitution,
                Value = 1
            });
            bp.AddComponent(new AddStatBonus()
            {
                Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                Stat = Kingmaker.EntitySystem.Stats.StatType.Intelligence,
                Value = 1
            });
            bp.AddComponent(new AddStatBonus()
            {
                Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                Stat = Kingmaker.EntitySystem.Stats.StatType.Wisdom,
                Value = 1
            });
            bp.AddComponent(new AddStatBonus()
            {
                Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                Stat = Kingmaker.EntitySystem.Stats.StatType.Charisma,
                Value = 1
            });
            bp.AddComponent(new DisableClassAdditionalVisualSettings());
            bp.m_DisplayName = Utils.CreateLocalizedString("8d3d9139-4256-43ad-8415-73d1e08b9a66", "Legendarification");
            bp.m_Description = Utils.CreateLocalizedString("803e8380-3d2a-47da-945d-0e2871b51b37", "At odd mythic ranks you receive stacking bonus +1 to all stats.");
            bp.Ranks = 4;
            bp.ReapplyOnLevelUp = false;
            bp.IsClassFeature = true;
        });
    }

    private void LegendarifyClass(MythicClass classTo)
    {
        var legendarificationStatBonusRef = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("6e052ad3-ac5e-483e-ba4d-f693629919af");
        classTo.MClass.m_SignatureAbilities = [];
        classTo.Progression.LevelEntries = Enumerable.Range(1, 8)
            .Select(x =>
            {
                var features = new List<BlueprintFeatureBaseReference>();
                if (x % 2 == 1)
                {
                    features.Add(legendarificationStatBonusRef);
                }
                if (x == 1)
                {
                    features.Add(Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("24e78475f0a243e1a810452d14d0a1bd"));
                }
                if (x == 7)
                {
                    features.Add(Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("27327f8954cd4f91ab3deaebf1f8cfa7"));
                }
                return new LevelEntry() { Level = x, m_Features = features };
            }
            ).ToArray();
        classTo.MClass.m_Spellbook = null;
        if (classTo.MClass == _azata.MClass)
        {
            AddAivu(classTo);
        }
        var mythicStartingProgression = Utils.GetBlueprint<BlueprintProgression>("af4ee0acb9114e544bf02f39027966b0");
        mythicStartingProgression.LevelEntries.Where(x => x.Level > 2).ForEach(x => x.m_Features = []);
    }

    private void AddAivu(MythicClass classTo)
    {
        var entries = classTo.Progression.LevelEntries;

        var aivuRank = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("2780764bf33c46745b11f0e1d2d20092");

        var aivuFeature = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("cf36f23d60987224696f03be70351928");
        var aivuTier2 = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("4d9785fa28ab443289497ccb05e49fe2");
        var aivuTier3 = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("1bfc72ee31e349ab91991d14e1db471e");
        var aivuTier4 = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("e0cd072417ac444a99e83eae51eea8df");
        if (!entries[0].m_Features.Contains(aivuFeature))
        {
            entries[0].m_Features.Add(aivuFeature);
        }
        if (!entries[2].m_Features.Contains(aivuTier2))
        {
            entries[2].m_Features.Add(aivuTier2);
        }
        if (!entries[4].m_Features.Contains(aivuTier3))
        {
            entries[4].m_Features.Add(aivuTier3);
        }
        if (!entries[6].m_Features.Contains(aivuTier4))
        {
            entries[6].m_Features.Add(aivuTier4);
        }
        foreach (var entry in entries)
        {
            if (!entry.m_Features.Contains(aivuRank))
            {
                entry.m_Features.Add(aivuRank);
            }
        }
    }
}
