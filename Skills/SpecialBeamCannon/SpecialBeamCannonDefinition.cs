using Microsoft.Xna.Framework;

namespace DBT.Skills.SpecialBeamCannon
{//Needs armor piercing added.
    public sealed class SpecialBeamCannonDefinition : SkillDefinition
    {
        public SpecialBeamCannonDefinition(params SkillDefinition[] parents) : base("SpecialBeamCannon", "Special Beam Cannon", "A concentrated beam of energy that pierces through foes.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(SpecialBeamCannonItem), new SpecialBeamCannonCharacteristics(), new Vector2(306, 322), parents: parents)
        {
        }
    }

    public sealed class SpecialBeamCannonCharacteristics : SkillCharacteristics
    {
        public SpecialBeamCannonCharacteristics() : base(new SpecialBeamCannonChargeCharacteristics(), 114, 114f / Constants.TICKS_PER_SECOND, 0f, 3f, 1f)
        {
        }
    }

    public sealed class SpecialBeamCannonChargeCharacteristics : SkillChargeCharacteristics
    {
        public SpecialBeamCannonChargeCharacteristics() : base(60, 6, 220, 220 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
