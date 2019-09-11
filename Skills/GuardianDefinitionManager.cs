using DBT.Skills.EnergyBurst;
using WebmilioCommons.Managers;

namespace DBT.Skills
{
    public sealed class GuardianDefinitionManager : SingletonManager<GuardianDefinitionManager, GuardianDefinition>
    {
        public override void DefaultInitialize()
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