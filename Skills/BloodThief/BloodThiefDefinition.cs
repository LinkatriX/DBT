using Microsoft.Xna.Framework;

namespace DBT.Skills.BloodThief
{
    public sealed class BloodThiefDefinition : SkillDefinition
    {
        public BloodThiefDefinition(params SkillDefinition[] parents) : base("BloodThief", "Blood Thief", "Fires life stealing ki blasts.", typeof(BloodThiefItem) , new BloodThiefCharacteristics(), new Vector2(416, 282), "Kill 50 enemies using ki blast\nduring a blood moon.", parents: parents)
        {
        }
    }

    public sealed class BloodThiefCharacteristics : SkillCharacteristics
    {
        public BloodThiefCharacteristics() : base(new BloodThiefChargeCharacteristics(), 51, 1f, 16f, 4f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class BloodThiefChargeCharacteristics : SkillChargeCharacteristics
    {
        public BloodThiefChargeCharacteristics() : base(0, 0, 75, 0)
        {
        }
    }
}
