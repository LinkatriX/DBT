using Microsoft.Xna.Framework;

namespace DBT.Skills.BigBangAttack
{
    public sealed class BigBangAttackDefinition : SkillDefinition
    {
        public BigBangAttackDefinition(params SkillDefinition[] parents) : base("BigBangAttack", "Big Bang Attack", "A blast attack capable of being charged for greater damage.", typeof(BigBangAttackItem) , new BigBangAttackCharacteristics(), new Vector2(226, 142), parents: parents)
        {
        }
    }
    public sealed class BigBangAttackCharacteristics : SkillCharacteristics
    {
        public BigBangAttackCharacteristics() : base(new BigBangAttackChargeCharacteristics(), 66, 1.5f, 25f, 6f, 1.2f, 0.05f, 1.02f, 2f, 1f)
        {
        }
    }

    public sealed class BigBangAttackChargeCharacteristics : SkillChargeCharacteristics
    {
        public BigBangAttackChargeCharacteristics() : base(120, 5, 100, 100 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
