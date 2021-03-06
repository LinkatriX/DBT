﻿using Microsoft.Xna.Framework;

namespace DBT.Skills.CandyLaser
{
    public sealed class CandyLaserDefinition : SkillDefinition
    {
        public CandyLaserDefinition(params SkillDefinition[] parents) : base("CandyLaser", "Candy Laser", "Fires a beam of energy that transforms your enemy into candy. Doesn't change bosses.", typeof(CandyLaserItem), new CandyLaserCharacteristics(), new Vector2(376, 352), parents: parents)
        {
        }
    }

    public sealed class CandyLaserCharacteristics : SkillCharacteristics
    {
        public CandyLaserCharacteristics() : base(new CandyLaserChargeCharacteristics(), 142, 1f, 14f, 4f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class CandyLaserChargeCharacteristics : SkillChargeCharacteristics
    {
        public CandyLaserChargeCharacteristics() : base(0, 0, 300, 0)
        {
        }
    }
}
