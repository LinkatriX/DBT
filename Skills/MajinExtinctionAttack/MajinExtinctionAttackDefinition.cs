﻿using Microsoft.Xna.Framework;

namespace DBT.Skills.MajinExtinctionAttack
{
    public sealed class MajinExtinctionAttackDefinition : SkillDefinition
    {
        public MajinExtinctionAttackDefinition(params SkillDefinition[] parents) : base(
            "MajinExtinctionAttack", "Majin Extinction Attack", "'Human Extinction...'", typeof(MajinExtinctionAttackItem),
            new MajinExtinctionAttackCharacteristics(), new Vector2(366, 22), parents: parents)
        {
        }
    }

    public sealed class MajinExtinctionAttackCharacteristics : SkillCharacteristics
    {
        public MajinExtinctionAttackCharacteristics() : base(new MajinExtinctionAttackChargeCharacteristics(), 1, 1f, 20f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class MajinExtinctionAttackChargeCharacteristics : SkillChargeCharacteristics
    {
        public MajinExtinctionAttackChargeCharacteristics() : base(0, 0, 5, 0)
        {
        }
    }
}
