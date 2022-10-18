using Kingmaker;
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
using static Kingmaker.Blueprints.Classes.BlueprintProgression;

namespace MythicPathPowerSwapper
{
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
        }

        internal static readonly Guid AeonId = Guid.Parse("15a85e67b7d69554cab9ed5830d0268e");
        internal static readonly Guid AngelId = Guid.Parse("a5a9fe8f663d701488bd1db8ea40484e");
        internal static readonly Guid AzataId = Guid.Parse("9a3b2c63afa79744cbca46bea0da9a16");
        internal static readonly Guid DemonId = Guid.Parse("8e19495ea576a8641964102d177e34b7");
        internal static readonly Guid LichId = Guid.Parse("5d501618a28bdc24c80007a5c937dcb7");
        internal static readonly Guid TricksterId = Guid.Parse("8df873a8c6e48294abdb78c45834aa0a");

        private MythicClass _aeon = new()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(AeonId),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("6091d66a2a9876b4891b989804cfbcb6"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("34b9484b0d5ce9340ae51d2bf9518bbe"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("f2305312ccccc2a46a49ba834ff7a092"),
            Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("b24d6185acea1f949b026c3b58e47947")
        };

        private MythicClass _angel = new()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(AngelId),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("015658ac45811b843b036e4ccc96c772"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("2f6fe889e91b6a645b055696c01e2f74"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("038090ec6b6a205418665f2489606534"),
            Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("2f45a11adb74b5a4a81f857a9886c5bd")
        };

        private MythicClass _azata = new()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(AzataId),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("b21b9f5e2831c2549a782d8128fb905b"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("9db53de4bf21b564ca1a90ff5bd16586"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("b172d5db251e3e0499671074ee15f7df"),
            Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("78cd50deada655e4cbe49765c0bbb7e4")
        };

        private MythicClass _demon = new()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(DemonId),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("e3daa889c72982e45a026f62cc84937d"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("285fe49f7df8587468f676aa49362213"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7764f540fbab8ab4c95492f1d8d4f04f"),
            Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("cdc95a3c4a74a874a895b3be61369564")
        };

        private MythicClass _lich = new()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(LichId),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("08a80074263809c4b9616aac05af90ae"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("ccec4e01b85bf5d46a3c3717471ba639"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7de9c45b07635e2418dffc185bd2eff4"),
            Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("1622ceba1d7829b4f9ee709b71bd6baf")
        };

        private MythicClass _trickster = new()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>(TricksterId),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("2ff51e0531ed8e545ab4cb35c32d40f4"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("cc64789b0cc5df14b90da1ffee7bbeea"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7c6a97566f7125c4c839720200311c3c"),
            Cloak = Utils.GetBlueprint<BlueprintItemEquipmentShoulders>("50b398d4630d9f244a5db288124ff181")
        };

        private BlueprintCharacterClass _mythicHeroClass = Utils.GetBlueprint<BlueprintCharacterClass>("247aa787806d5da4f89cfc3dff0b217f");
        private BlueprintCharacterClassReference _mythicHeroClassRef = Utils.GetBlueprintReference<BlueprintCharacterClassReference>("247aa787806d5da4f89cfc3dff0b217f");

        private BlueprintFeatureBaseReference _mythicAbilitySelection = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("ba0e5a900b775be4a99702f1ed08914d");
        private BlueprintFeatureBaseReference _mythicFeatSelection = Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("9ee0f6745f555484299b0a1563b99d81");
        private void Swap()
        {
            var mTo = Main.Settings.MythicToChange;
            var mFrom = Main.Settings.MythicFrom;
            PFLog.Default.Log("Replacing {0} with {1}", mTo, mFrom);
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
                        PFLog.Default.Log("Replacing {0} with {1}", mClassTo.MClass, mClassFrom.MClass);
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
                    AddAivu(mClassTo);
                }
            }
            if (Main.Settings.UnmythicHero)
            {
                UnMythicStarter();
            }

            if (Main.Settings.UnmythicCompanions)
            {
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

            mClassFrom.SummonProgression.m_Classes = new ClassWithLevel[] { new() { m_Class = classToRef } };
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
            lifeBondingFriendshipProgression.m_Classes = new ClassWithLevel[] { new() { m_Class = classToRef } };

            var marvelousEnduranceFastHealingProperty = Utils.GetBlueprint<BlueprintUnitProperty>("ca1b4b40b9f5407f904738b575bac1ca");
            marvelousEnduranceFastHealingProperty.GetComponents<SummClassLevelGetter>()
                .ForEach(x => x.m_Class = new BlueprintCharacterClassReference[] { classToRef, _mythicHeroClassRef });
        }

        private void LichFromUpdate(MythicClass classTo)
        {
            var classToRef = classTo.MClass.ToReference<BlueprintCharacterClassReference>();

            var deathOfElementsConsumingElementsResource = Utils.GetBlueprint<BlueprintAbilityResource>("7a558d186755620439e35817f174f749");
            var maxAm = deathOfElementsConsumingElementsResource.m_MaxAmount;
            maxAm.m_Class = new BlueprintCharacterClassReference[] { classToRef };
            maxAm.m_ClassDiv = new BlueprintCharacterClassReference[] { _mythicHeroClassRef, classToRef };

            var lichSkeletalUpgradeSelection = Utils.GetBlueprint<BlueprintFeatureSelection>("a434dddab8026e947bc16eb36d18a783");
            foreach (var feature in lichSkeletalUpgradeSelection.m_AllFeatures)
            {
                if (feature.GetBlueprint() is BlueprintProgression progression)
                {
                    progression.GetComponents<PrerequisiteClassLevel>()
                        .ForEach(x => x.m_CharacterClass = classToRef);
                    progression.m_Classes = new ClassWithLevel[] { new() { m_Class = classToRef } };
                }
            }
        }

        private void UnMythic(MythicClass classTo)
        {
            classTo.MClass.m_SignatureAbilities = new BlueprintFeatureReference[0];
            classTo.Progression.LevelEntries = Enumerable.Range(1, 8).Select(x => new LevelEntry() { Level = x }).ToArray();
            classTo.MClass.m_Spellbook = null;
            if (classTo.MClass == _azata.MClass)
            {
                AddAivu(classTo);
            }
        }

        private void UnMythicStarter()
        {
            var mythicStartingProgression = Utils.GetBlueprint<BlueprintProgression>("af4ee0acb9114e544bf02f39027966b0");
            mythicStartingProgression.LevelEntries[0].m_Features = new();
            mythicStartingProgression.LevelEntries[1].m_Features = new()
            {
                Utils.GetBlueprintReference<BlueprintFeatureBaseReference>("bef12729fa3f9b34488634f61751e295")
            };
        }
        private void PartialUnMythic(MythicClass classTo)
        {
            classTo.MClass.m_SignatureAbilities = new BlueprintFeatureReference[0];
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
                    return new LevelEntry() { Level = x, m_Features = features };
                }
                ).ToArray();
            classTo.MClass.m_Spellbook = null;
            if (classTo.MClass == _azata.MClass)
            {
                AddAivu(classTo);
            }
        }

        private void LegendarifyClass(MythicClass classTo)
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
                bp.AddComponent(new AddContextStatBonus()
                {
                    Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                    Stat = Kingmaker.EntitySystem.Stats.StatType.Strength,
                    Value = 1
                });
                bp.AddComponent(new AddContextStatBonus()
                {
                    Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                    Stat = Kingmaker.EntitySystem.Stats.StatType.Dexterity,
                    Value = 1
                });
                bp.AddComponent(new AddContextStatBonus()
                {
                    Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                    Stat = Kingmaker.EntitySystem.Stats.StatType.Constitution,
                    Value = 1
                });
                bp.AddComponent(new AddContextStatBonus()
                {
                    Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                    Stat = Kingmaker.EntitySystem.Stats.StatType.Intelligence,
                    Value = 1
                });
                bp.AddComponent(new AddContextStatBonus()
                {
                    Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                    Stat = Kingmaker.EntitySystem.Stats.StatType.Wisdom,
                    Value = 1
                });
                bp.AddComponent(new AddContextStatBonus()
                {
                    Descriptor = Kingmaker.Enums.ModifierDescriptor.None,
                    Stat = Kingmaker.EntitySystem.Stats.StatType.Charisma,
                    Value = 1
                });
                bp.m_DisplayName = Utils.CreateLocalizedString("8d3d9139-4256-43ad-8415-73d1e08b9a66", "Legendarification");
                bp.Ranks = 4;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });

            classTo.MClass.m_SignatureAbilities = new BlueprintFeatureReference[0];
            classTo.Progression.LevelEntries = Enumerable.Range(1, 8)
                .Select(x =>
                {
                    var features = new List<BlueprintFeatureBaseReference>();
                    if (x % 2 == 1)
                    {
                        features.Add(legendarificationStatBonus.ToReference<BlueprintFeatureBaseReference>());
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
}
