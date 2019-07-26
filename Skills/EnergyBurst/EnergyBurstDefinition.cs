namespace DBT.Skills.EnergyBurst
{
    public sealed class EnergyBurstDefinition : GuardianDefinition
    {
        public EnergyBurstDefinition() : base("EnergyBurst", "Energy Burst", "A condensed mass of ki that can restore the ki of allies.", new EnergyBurstCharacteristics())
        {
        }
    }

    public sealed class EnergyBurstCharacteristics : GuardianCharacteristics
    {
        public EnergyBurstCharacteristics() : base(new EnergyBurstChargeCharacteristics(), 0, 0f, 15f, 0, 0)
        {
        }
    }

    public sealed class EnergyBurstChargeCharacteristics : SkillChargeCharacteristics
    {
        public EnergyBurstChargeCharacteristics() : base(0, 0, 0, 0)
        {
        }
    }
}