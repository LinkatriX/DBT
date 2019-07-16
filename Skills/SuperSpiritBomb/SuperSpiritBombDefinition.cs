namespace DBT.Skills.SuperSpiritBomb
{
    public sealed class SuperSpiritBombDefinition : SkillDefinition
    {
        public SuperSpiritBombDefinition() : base("SuperSpiritBomb", "Super Spirit Bomb", "A stronger version of the Spirit Bomb.", new SuperSpiritBombCharacteristics())
        {
        }
    }

    public sealed class SuperSpiritBombCharacteristics : SkillCharacteristics
    {
        public SuperSpiritBombCharacteristics() : base(new SuperSpiritBombChargeCharacteristics(), 200, 200 / Constants.TICKS_PER_SECOND, 6f, 12f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class SuperSpiritBombChargeCharacteristics : SkillChargeCharacteristics
    {
        public SuperSpiritBombChargeCharacteristics() : base(100, 12, 500, 500 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
