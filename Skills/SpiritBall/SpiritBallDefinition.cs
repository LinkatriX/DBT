namespace DBT.Skills.SpiritBall
{
    public sealed class SpiritBallDefinition : SkillDefinition
    {
        public SpiritBallDefinition() : base("SpiritBall", "Spirit Ball", "The user creates a controllable, concentrated orb of ki.", typeof(SpiritBallItem), new SpiritBallCharacteristics())
        {
        }
    }

    public sealed class SpiritBallCharacteristics : SkillCharacteristics
    {
        public SpiritBallCharacteristics() : base(new SpiritBallChargeCharacteristics(), 32, 1f, 35f, 8f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class SpiritBallChargeCharacteristics : SkillChargeCharacteristics
    {
        public SpiritBallChargeCharacteristics() : base(0, 0, 60, 0)
        {
        }
    }
}
