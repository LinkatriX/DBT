namespace DBT.Skills.Supernova
{
    public sealed class SupernovaDefinition : SkillDefinition
    {
        public SupernovaDefinition() : base("Supernova", "Supernova", "A massive blast attack powerful enough to destroy a planet.", new SupernovaCharacteristics())
        {
        }
    }

    public sealed class SupernovaCharacteristics : SkillCharacteristics
    {
        public SupernovaCharacteristics() : base(new SupernovaChargeCharacteristics(), 50, 1.7f, 4f, 4f, 3f, 0.05f, 1.5f, 2f, 1f)
        {
        }
    }

    public sealed class SupernovaChargeCharacteristics : SkillChargeCharacteristics
    {
        public SupernovaChargeCharacteristics() : base(180, 3, 350, 350)
        {
        }
    }
}