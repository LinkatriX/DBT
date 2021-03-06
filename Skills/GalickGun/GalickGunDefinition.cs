﻿using Microsoft.Xna.Framework;

namespace DBT.Skills.GalickGun
{
    public sealed class GalickGunDefinition : SkillDefinition
    {
        public GalickGunDefinition(params SkillDefinition[] parents) : base("GalickGun", "Galick Gun", "No it doesn't smell like garlic.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(GalickGunItem), new GalickGunCharacteristics(), new Vector2(522, 162), "PRODIGY TRAIT REQUIRED\nUnlocked after hitting 2500 max ki\nand after mastering energy wave.", parents: parents)
        {
        }
    }

    public sealed class GalickGunCharacteristics : SkillCharacteristics
    {
        public GalickGunCharacteristics() : base(new GalickGunChargeCharacteristics(), 94, 94f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class GalickGunChargeCharacteristics : SkillChargeCharacteristics
    {
        public GalickGunChargeCharacteristics() : base(60, 9, 80, 80 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
