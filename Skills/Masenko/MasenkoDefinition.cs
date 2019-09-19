using Microsoft.Xna.Framework;

namespace DBT.Skills.Masenko
{
    public sealed class MasenkoDefinition : SkillDefinition
    {
        public MasenkoDefinition(params SkillDefinition[] parents) : base("Masenko", "Masenko", "A technique created by the\nson of the demon king Piccolo.\n" + DEFAULT_BEAM_INSTRUCTIONS, typeof(MasenkoItem), new MasenkoCharacteristics(), new Vector2(458, 242), "Unlocked after hitting 1250 max ki.", parents: parents)
        {
        }
    }

    public sealed class MasenkoCharacteristics : SkillCharacteristics
    {
        public MasenkoCharacteristics() : base(new MasenkoChargeCharacteristics(), 72, 72f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class MasenkoChargeCharacteristics : SkillChargeCharacteristics
    {
        public MasenkoChargeCharacteristics() : base(60, 5, 60, 60 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
