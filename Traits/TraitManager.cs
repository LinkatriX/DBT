using WebmilioCommons.Managers;

namespace DBT.Traits
{
    public sealed class TraitManager : SingletonManager<TraitManager, TraitDefinition>
    {
        public override void DefaultInitialize()
        {


            base.DefaultInitialize();
        }

        public TraitDefinition Prodigy { get; private set; }

        public TraitDefinition Peaceful { get; private set; }

        public TraitDefinition Legendary { get; private set; }

        public TraitDefinition Primal { get; private set; }
    }
}
