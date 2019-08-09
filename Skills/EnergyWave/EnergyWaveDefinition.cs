namespace DBT.Skills.EnergyWave
{
    public sealed class EnergyWaveDefinition : SkillDefinition
    {
        public EnergyWaveDefinition() : base("EnergyWave", "Energy Wave", "Fires a concentrated beam of ki.\n" + DEFAULT_BEAM_INSTRUCTIONS, new EnergyWaveCharacteristics())
        {
        }
    }

    public sealed class EnergyWaveCharacteristics : SkillCharacteristics
    {
        public EnergyWaveCharacteristics() : base(new EnergyWaveChargeCharacteristics(), 32, 32f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class EnergyWaveChargeCharacteristics : SkillChargeCharacteristics
    {
        public EnergyWaveChargeCharacteristics() : base(60, 3, 40, 40 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
