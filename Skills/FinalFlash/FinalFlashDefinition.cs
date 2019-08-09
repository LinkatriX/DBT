namespace DBT.Skills.FinalFlash
{
    public sealed class FinalFlashDefinition : SkillDefinition
    {
        public FinalFlashDefinition() : base("FinalFlash", "Final Flash", "A full powered beam attack.\n" + DEFAULT_BEAM_INSTRUCTIONS, new FinalFlashCharacteristics())
        {
        }
    }

    public sealed class FinalFlashCharacteristics : SkillCharacteristics
    {
        public FinalFlashCharacteristics() : base(new FinalFlashChargeCharacteristics(), 144, 144f / Constants.TICKS_PER_SECOND, 0f, 3f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class FinalFlashChargeCharacteristics : SkillChargeCharacteristics
    {
        public FinalFlashChargeCharacteristics() : base(100, 9, 220, 220 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
