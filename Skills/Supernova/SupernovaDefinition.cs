namespace DBT.Skills.Supernova
{
    public sealed class SupernovaDefinition : SkillDefinition
    {
        public SupernovaDefinition(params SkillDefinition[] parents) : base("Supernova", "Supernova", "A massive blast attack powerful enough to destroy a planet.", typeof(SupernovaItem), new SupernovaCharacteristics(), parents: parents)
        {
        }
    }

    public sealed class SupernovaCharacteristics : SkillCharacteristics
    {
        public SupernovaCharacteristics() : base(new SupernovaChargeCharacteristics(), 140, 140 / Constants.TICKS_PER_SECOND, 4f, 4f, 3f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class SupernovaChargeCharacteristics : SkillChargeCharacteristics
    {
        public SupernovaChargeCharacteristics() : base(100, 8, 350, 350 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}