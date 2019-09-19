using Microsoft.Xna.Framework;

namespace DBT.Skills.KiBeam
{
    public sealed class KiBeamDefinition : SkillDefinition
    {
        public KiBeamDefinition(params SkillDefinition[] parents) : base("KiBeam", "Ki Beam", "A quick firing beam of\nKi that pierces enemies.", typeof(KiBeamItem), new KiBeamCharacteristics(), new Vector2(326, 252), "Unlocked after hitting 650 max ki.", parents: parents)
        {
        }

        public sealed class KiBeamCharacteristics : SkillCharacteristics
        {
            public KiBeamCharacteristics() : base(new KiBeamChargeCharacteristics(), 17, 1f, 70f, 3f, 1f, 0.05f, 1f, 2f, 1f)
            {
            }
        }

        public sealed class KiBeamChargeCharacteristics : SkillChargeCharacteristics
        {
            public KiBeamChargeCharacteristics() : base(0, 0, 29, 0)
            {
            }
        }
    }
}
