namespace DBT.Skills.KiBlast
{
    public sealed class KiBlastDefinition : SkillDefinition
    {
        public KiBlastDefinition() : base("KiBlast", "Ki Blast", "A small Ki blast that damages enemies.", new KiBlastCharacteristics())
        {
        }
    }

    public sealed class KiBlastCharacteristics : SkillCharacteristics
    {
        public KiBlastCharacteristics() : base(new KiBlastChargeCharacteristics(), 19, 1f, 15f, 3f, 1f, 0.05f, 1, 2, 1)
        {
        }
    }

    public sealed class KiBlastChargeCharacteristics : SkillChargeCharacteristics
    {
        public KiBlastChargeCharacteristics() : base(0, 0, 15, 0)
        {
        }
    }
}