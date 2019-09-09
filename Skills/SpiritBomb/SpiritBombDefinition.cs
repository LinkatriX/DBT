namespace DBT.Skills.SpiritBomb
{
    public sealed class SpiritBombDefinition : SkillDefinition
    {
        public SpiritBombDefinition(params SkillDefinition[] parents) : base("SpiritBomb", "Spirit Bomb", "The user draws energy from surrounding life to create a powerful blast attack.", typeof(SpiritBombItem), new SpiritBombCharacteristics(), parents: parents)
        {
        }
    }

    public sealed class SpiritBombCharacteristics : SkillCharacteristics
    {
        public SpiritBombCharacteristics() : base(new SpiritBombChargeCharacteristics(), 32, 32f / Constants.TICKS_PER_SECOND, 35f, 8f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class SpiritBombChargeCharacteristics : SkillChargeCharacteristics
    {
        public SpiritBombChargeCharacteristics() : base(100, 7, 60, 60 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
