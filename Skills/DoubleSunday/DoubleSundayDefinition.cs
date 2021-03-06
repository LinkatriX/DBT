﻿using Microsoft.Xna.Framework;

namespace DBT.Skills.DoubleSunday
{
    public sealed class DoubleSundayDefinition : SkillDefinition
    {
        public DoubleSundayDefinition(params SkillDefinition[] parents) : base("DoubleSunday", "Double Sunday", "A twin beam attack\nfired from both hands.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(DoubleSundayItem), new DoubleSundayCharacteristics(), new Vector2(458, 152), "Kill 70 enemies in the snow biome\nusing energy wave.", parents: parents)
        {
        }
    }

    public sealed class DoubleSundayCharacteristics : SkillCharacteristics
    {
        public DoubleSundayCharacteristics() : base(new DoubleSundayChargeCharacteristics(), 28, 28f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class DoubleSundayChargeCharacteristics : SkillChargeCharacteristics
    {
        public DoubleSundayChargeCharacteristics() : base(60, 4, 55, 55 / Constants.TICKS_PER_SECOND)//Since beams aren't fully implemented these values are omegaPepega
        {
        }
    }
}
