using Microsoft.Xna.Framework;

namespace DBT.Skills.EnergyWave
{
    public sealed class EnergyWaveDefinition : SkillDefinition
    {
        public EnergyWaveDefinition(params SkillDefinition[] parents) : base("EnergyWave", "Energy Wave", "Fires a concentrated\nbeam of ki.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(EnergyWaveItem), new EnergyWaveCharacteristics(), new Vector2(430, 202), "Unlocked after hitting 800 max ki.", parents: parents)
        {
        }
    }

    public sealed class EnergyWaveCharacteristics : SkillCharacteristics
    {
        public EnergyWaveCharacteristics() : base(new EnergyWaveChargeCharacteristics(), 32, 32f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f)
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
