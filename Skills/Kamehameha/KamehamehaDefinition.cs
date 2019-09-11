﻿using Microsoft.Xna.Framework;

namespace DBT.Skills.Kamehameha
{
    public sealed class KamehamehaDefinition : SkillDefinition
    {
        public KamehamehaDefinition(params SkillDefinition[] parents) : base("Kamehameha", "Kamehameha", "The user draws the latent energy of their body forwards, and releases it all at once.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(KamehamehaItem), new DefinitionCharacteristics(), new Vector2(522, 232), parents: parents)
        {
        }

        public sealed class DefinitionCharacteristics : SkillCharacteristics
        {
            public DefinitionCharacteristics() : base(new DefinitionChargeCharacteristics(), 88, 88f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f, 0.15f, 1f, 2f, 1f)
            {
                Channel = true;
                BaseSkillCooldown = 5 * Constants.TICKS_PER_SECOND;
            }

            public sealed class DefinitionChargeCharacteristics : SkillChargeCharacteristics
            {
                public DefinitionChargeCharacteristics() : base(60, 6, 80, 80 / Constants.TICKS_PER_SECOND)
                {
                }
            }
        }
    }
}