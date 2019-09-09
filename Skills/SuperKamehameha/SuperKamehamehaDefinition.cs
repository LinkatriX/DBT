namespace DBT.Skills.SuperKamehameha
{
    public sealed class SuperKamehamehaDefinition : SkillDefinition
    {
        public SuperKamehamehaDefinition(params SkillDefinition[] parents) : base("SuperKamehameha", "Super Kamehameha", "A superior version of the Kamehameha wave.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(SuperKamehamehaItem), new SuperkamehamehaCharacteristics(), parents: parents)
        {
        }
    }

    public sealed class SuperkamehamehaCharacteristics : SkillCharacteristics
    {
        public SuperkamehamehaCharacteristics() : base(new SuperKamehamehaChargeCharacteristics(), 118, 118f / Constants.TICKS_PER_SECOND, 0f, 7f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class SuperKamehamehaChargeCharacteristics : SkillChargeCharacteristics
    {
        public SuperKamehamehaChargeCharacteristics() : base(60, 8, 150, 150 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
