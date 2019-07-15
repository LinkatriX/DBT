namespace DBT.Skills.DoubleSunday
{
    public sealed class DoubleSundayDefinition : SkillDefinition
    {
        public DoubleSundayDefinition() : base("DoubleSunday", "Double Sunday", "A twin beam attack fired from both hands.", new DoubleSundayCharacteristics())
        {
        }
    }

    public sealed class DoubleSundayCharacteristics : SkillCharacteristics
    {
        public DoubleSundayCharacteristics() : base(new DoubleSundayChargeCharacteristics(), 28, 1.5f, 70f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class DoubleSundayChargeCharacteristics : SkillChargeCharacteristics
    {
        public DoubleSundayChargeCharacteristics() : base(60, 4, 55, 55)//Since beams aren't fully implemented these values are omegaPepega
        {
        }
    }
}
