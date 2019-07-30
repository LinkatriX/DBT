using DBT.Managers;
using DBT.Skills.EnergyBurst;

namespace DBT.Skills
{
    public sealed class GuardianDefinitionManager : SingletonManager<GuardianDefinitionManager, GuardianDefinition>
    {
        internal override void DefaultInitialize()
        {
            #region Guardian Skills

            EnergyBurst = Add(new EnergyBurstDefinition()) as EnergyBurstDefinition;

            #endregion

            base.DefaultInitialize();
        }

        #region Skill Properties
        public EnergyBurstDefinition EnergyBurst { get; private set; }

        #endregion
    }
}