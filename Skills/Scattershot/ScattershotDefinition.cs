namespace DBT.Skills.Scattershot
{
    public sealed class ScattershotDefinition : SkillDefinition
    {
        public ScattershotDefinition() : base("Scattershot", "Scattershot", "Fires an array of seeking ki blasts.", typeof(ScattershotItem), new ScattershotCharacteristics())
        {
        }
    }

    public sealed class ScattershotCharacteristics : SkillCharacteristics
    {
        public ScattershotCharacteristics() : base(new ScattershotChargeCharacteristics(), 72, 1f, 17f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class ScattershotChargeCharacteristics : SkillChargeCharacteristics
    {
        public ScattershotChargeCharacteristics() : base(0, 0, 110, 0)
        {
        }
    }
}
