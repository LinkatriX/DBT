namespace DBT.Skills.Kamehamehax10
{
    public sealed class Kamehamehax10Definition : SkillDefinition
    {
        public Kamehamehax10Definition() : base("Kamehamehax10", "Kamehameha x10", "A kamehameha wave boosted x10 by the power of a great ape.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(Kamehamehax10Item), new Kamehamehax10Characteristics())
        {
        }
    }

    public sealed class Kamehamehax10Characteristics : SkillCharacteristics
    {
        public Kamehamehax10Characteristics() : base(new Kamehamehax10ChargeCharacteristics(), 156, 156f / Constants.TICKS_PER_SECOND, 0f, 10f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class Kamehamehax10ChargeCharacteristics : SkillChargeCharacteristics
    {
        public Kamehamehax10ChargeCharacteristics() : base(60, 8, 120, 120 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
