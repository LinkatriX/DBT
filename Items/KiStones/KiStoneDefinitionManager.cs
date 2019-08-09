using DBT.Dynamicity;
using DBT.Managers;

namespace DBT.Items.KiStones
{
    public sealed class KiStoneDefinitionManager : Manager<KiStoneDefinition>
    {
        private static KiStoneDefinitionManager _instance;

        internal override void DefaultInitialize()
        {
            KiStoneT1 = Add(new KiStoneDefinition(250, typeof(KiStoneT1)));
            KiStoneT2 = Add(new KiStoneDefinition(1000, typeof(KiStoneT2), KiStoneT1));
            KiStoneT3 = Add(new KiStoneDefinition(2500, typeof(KiStoneT3), KiStoneT2));
            KiStoneT4 = Add(new KiStoneDefinition(5000, typeof(KiStoneT4), KiStoneT3));
            KiStoneT5 = Add(new KiStoneDefinition(10000, typeof(KiStoneT5), KiStoneT4));
            KiStoneT6 = Add(new KiStoneDefinition(20000, typeof(KiStoneT6), KiStoneT5));

            Tree = new Tree<KiStoneDefinition>(byIndex);

            base.DefaultInitialize();
        }

        public KiStoneDefinition GetNearestKiStoneAbove(float ki)
        {
            KiStoneDefinition definition = null;

            foreach (KiStoneDefinition newDef in byIndex)
            {
                if (newDef.RequiredKi < ki) continue;

                if (definition == null)
                    definition = newDef;

                if (newDef.RequiredKi >= ki && definition.RequiredKi > newDef.RequiredKi)
                    definition = newDef;

            }

            return definition;
        }

        public KiStoneDefinition GetNearestKiStoneUnder(float ki)
        {
            KiStoneDefinition definition = null;

            foreach (KiStoneDefinition newDef in byIndex)
            {
                if (newDef.RequiredKi > ki) continue;

                if (definition == null)
                    definition = newDef;

                if (newDef.RequiredKi <= ki && definition.RequiredKi < newDef.RequiredKi)
                    definition = newDef;
            }

            return definition;
        }

        public KiStoneDefinition KiStoneT1 { get; private set; } // Stable
        public KiStoneDefinition KiStoneT2 { get; private set; } // Calm
        public KiStoneDefinition KiStoneT3 { get; private set; } // Prideful
        public KiStoneDefinition KiStoneT4 { get; private set; } // Angerful
        public KiStoneDefinition KiStoneT5 { get; private set; } // Pure
        public KiStoneDefinition KiStoneT6 { get; private set; }

        public Tree<KiStoneDefinition> Tree { get; private set; }

        public static KiStoneDefinitionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KiStoneDefinitionManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}