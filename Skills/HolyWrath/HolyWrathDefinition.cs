using Microsoft.Xna.Framework;

namespace DBT.Skills.HolyWrath
{
    public sealed class HolyWrathDefinition : SkillDefinition
    {
        public HolyWrathDefinition(params SkillDefinition[] parents) : base("HolyWrath", "Holy Wrath", "A massive blast formed with divine energy.", typeof(HolyWrathItem), new HolyWrathCharacteristics(), new Vector2(256, 22), parents: parents)
        {
        }
    }

    public sealed class HolyWrathCharacteristics : SkillCharacteristics
    {
        public HolyWrathCharacteristics() : base(new HolyWrathChargeCharacteristics(), 200, 200 / Constants.TICKS_PER_SECOND, 6f, 12f, 1f)
        {
        }
    }

    public sealed class HolyWrathChargeCharacteristics : SkillChargeCharacteristics
    {
        public HolyWrathChargeCharacteristics() : base(150, 8, 500, 500 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
