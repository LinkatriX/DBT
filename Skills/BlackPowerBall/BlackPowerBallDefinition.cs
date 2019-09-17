using Microsoft.Xna.Framework;

namespace DBT.Skills.BlackPowerBall
{
    public sealed class BlackPowerBallDefinition : SkillDefinition
    {
        public BlackPowerBallDefinition(params SkillDefinition[] parents) : base("BlackPowerBall", "Black Power Ball", "A powerful blast attack that can also be rapidly fired as a barrage.", typeof(BlackPowerBallItem), new BlackPowerBallCharacteristics(), new Vector2(166, 248), parents: parents)
        {
        }
    }

    public sealed class BlackPowerBallCharacteristics : SkillCharacteristics
    {
        public BlackPowerBallCharacteristics() : base(new BlackPowerBallChargeCharacteristics(), 130, 0.65f, 10f, 5f, 0.65f, 0.05f, 1.02f, 2f, 1f)
        {
        }
    }

    public sealed class BlackPowerBallChargeCharacteristics : SkillChargeCharacteristics
    {
        public BlackPowerBallChargeCharacteristics() : base(70, 6, 100, 100 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
