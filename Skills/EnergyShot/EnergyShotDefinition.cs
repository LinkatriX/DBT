using Microsoft.Xna.Framework;

namespace DBT.Skills.EnergyShot
{
    public sealed class EnergyShotDefinition : SkillDefinition
    {
        public EnergyShotDefinition(params SkillDefinition[] parents) : base("EnergyShot", "Energy Shot", "An enhanced version of a regular ki blast", typeof(EnergyShotItem), new EnergyShotCharacteristics(), new Vector2(266, 202), parents: parents)
        {
        }
    }

    public sealed class EnergyShotCharacteristics : SkillCharacteristics
    {
        public EnergyShotCharacteristics() : base(new EnergyShotChargeCharacteristics(), 77, 1f, 20f, 6f, 1.05f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class EnergyShotChargeCharacteristics : SkillChargeCharacteristics
    {
        public EnergyShotChargeCharacteristics() : base(60, 3, 105, 50)
        {
        }
    }
}