using Microsoft.Xna.Framework;

namespace DBT.Skills.Scattershot
{
    public sealed class ScattershotDefinition : SkillDefinition
    {
        public ScattershotDefinition(params SkillDefinition[] parents) : base("Scattershot", "Scattershot", "Fires an array of seeking ki blasts.", typeof(ScattershotItem), new ScattershotCharacteristics(), new Vector2(456, 62), parents: parents)
        {
        }
    }

    public sealed class ScattershotCharacteristics : SkillCharacteristics
    {
        public ScattershotCharacteristics() : base(new ScattershotChargeCharacteristics(), 72, 1f, 17f, 2f, 1f)
        {
        }
    }

    public sealed class ScattershotChargeCharacteristics : SkillChargeCharacteristics
    {
        public ScattershotChargeCharacteristics() : base(0, 0, 110, 0)
        {
        }
    }
}
