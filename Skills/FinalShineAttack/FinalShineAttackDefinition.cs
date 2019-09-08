namespace DBT.Skills.FinalShineAttack
{
    public sealed class FinalShineAttackDefinition : SkillDefinition
    {
        public FinalShineAttackDefinition() : base("FinalShineAttack", "Final Shine Attack", "An evolved version of the Final Flash technique.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(FinalShineAttackItem), new FinalShineAttackCharacteristics())
        {
        }
    }

    public sealed class FinalShineAttackCharacteristics : SkillCharacteristics
    {
        public FinalShineAttackCharacteristics() : base(new FinalShineChargeAttackCharacteristics(), 184, 184f / Constants.TICKS_PER_SECOND, 0f, 3f, 1f, 0.05f, 1f, 2f,1f)
        {
        }
    }

    public sealed class FinalShineChargeAttackCharacteristics : SkillChargeCharacteristics
    {
        public FinalShineChargeAttackCharacteristics() : base(100, 12, 250, 250 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
