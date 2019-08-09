namespace DBT.Skills.KiBeam
{
    public sealed class KiBeamDefinition : SkillDefinition
    {
        public KiBeamDefinition() : base("KiBeam", "Ki Beam", "A quick firing beam of Ki that pierces enemies.", new KiBeamCharacteristics())
        {
        }

        public sealed class KiBeamCharacteristics : SkillCharacteristics
        {
            public KiBeamCharacteristics() : base(new KiBeamChargeCharacteristics(), 17, 1f, 70f, 3f, 1f, 0.05f, 1f, 2f, 1f)
            {
            }
        }

        public sealed class KiBeamChargeCharacteristics : SkillChargeCharacteristics
        {
            public KiBeamChargeCharacteristics() : base(0, 0, 29, 0)
            {
            }
        }
    }
}
