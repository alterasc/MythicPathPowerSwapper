using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
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
        }

        private MythicClass _aeon = new MythicClass()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>("15a85e67b7d69554cab9ed5830d0268e"),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("6091d66a2a9876b4891b989804cfbcb6"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("34b9484b0d5ce9340ae51d2bf9518bbe"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("f2305312ccccc2a46a49ba834ff7a092")
        };

        private MythicClass _angel = new MythicClass()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>("a5a9fe8f663d701488bd1db8ea40484e"),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("015658ac45811b843b036e4ccc96c772"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("2f6fe889e91b6a645b055696c01e2f74"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("038090ec6b6a205418665f2489606534")
        };

        private MythicClass _azata = new MythicClass()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>("9a3b2c63afa79744cbca46bea0da9a16"),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("b21b9f5e2831c2549a782d8128fb905b"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("9db53de4bf21b564ca1a90ff5bd16586"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("b172d5db251e3e0499671074ee15f7df")
        };

        private MythicClass _demon = new MythicClass()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>("8e19495ea576a8641964102d177e34b7"),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("e3daa889c72982e45a026f62cc84937d"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("285fe49f7df8587468f676aa49362213"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7764f540fbab8ab4c95492f1d8d4f04f")
        };

        private MythicClass _lich = new MythicClass()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>("5d501618a28bdc24c80007a5c937dcb7"),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("08a80074263809c4b9616aac05af90ae"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("ccec4e01b85bf5d46a3c3717471ba639"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7de9c45b07635e2418dffc185bd2eff4")
        };

        private MythicClass _trickster = new MythicClass()
        {
            MClass = Utils.GetBlueprint<BlueprintCharacterClass>("8df873a8c6e48294abdb78c45834aa0a"),
            Spellbook = Utils.GetBlueprint<BlueprintSpellbook>("2ff51e0531ed8e545ab4cb35c32d40f4"),
            Progression = Utils.GetBlueprint<BlueprintProgression>("cc64789b0cc5df14b90da1ffee7bbeea"),
            SummonProgression = Utils.GetBlueprint<BlueprintProgression>("7c6a97566f7125c4c839720200311c3c")
        };

        private BlueprintCharacterClass _mythicHeroClass = Utils.GetBlueprint<BlueprintCharacterClass>("247aa787806d5da4f89cfc3dff0b217f");
        private void Swap()
        {
            //BasicReplace(mClassFrom: _aeon, mClassTo: _angel);

            //AeonFromUpdate(_angel);
        }

        private void BasicReplace(MythicClass mClassFrom, MythicClass mClassTo)
        {
            var classToRef = mClassTo.MClass.ToReference<BlueprintCharacterClassReference>();

            mClassTo.Progression.LevelEntries = mClassFrom.Progression.LevelEntries;
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
        }

        private void AeonFromUpdate(MythicClass classTo)
        {
            var classToRef = classTo.MClass.ToReference<BlueprintCharacterClassReference>();

            var aeonGazeResource = Utils.GetBlueprint<BlueprintAbilityResource>("905722fe39d87474aa6d41bffa327ff3");
            var classes = aeonGazeResource.m_MaxAmount.m_Class.Where(x => x != _aeon.MClass.ToReference<BlueprintCharacterClassReference>()).ToList();
            classes.Add(classToRef);
            aeonGazeResource.m_MaxAmount.m_Class = classes.ToArray();
        }
    }
}
