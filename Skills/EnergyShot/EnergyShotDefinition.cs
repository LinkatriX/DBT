namespace DBT.Skills.EnergyShot
{
    public sealed class EnergyShotDefinition : SkillDefinition
    {
        public EnergyShotDefinition() : base("EnergyShot", "Energy Shot", "An enhanced version of a regular ki blast", new EnergyShotCharacteristics())
        {
        }
    }

    public sealed class EnergyShotCharacteristics : SkillCharacteristics
    {
        public EnergyShotCharacteristics() : base(new EnergyShotChargeCharacteristics(), 77, 1f, 20f, )
        {
        }
    }

    public sealed class EnergyShotChargeCharacteristics : SkillChargeCharacteristics
    {
        public EnergyShotChargeCharacteristics() : base()
        {
        }
    }
}
