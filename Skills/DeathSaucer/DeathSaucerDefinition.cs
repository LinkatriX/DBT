using Microsoft.Xna.Framework;

namespace DBT.Skills.DeathSaucer
{
    public sealed class DeathSaucerDefinition : SkillDefinition
    {//Values are extremely WIP.
        public DeathSaucerDefinition(params SkillDefinition[] parents) : base(
            "DeathSaucer", "Death Saucer", "The user charges a disk of energy that follows the user's cursor.", typeof(DeathSaucerItem),
            new DeathSaucerCharacteristics(), new Vector2(27, 27),
            "Unlock through the ritual sacrifice of cat.", parents: parents)
        {
        }
    }

    public sealed class DeathSaucerCharacteristics : SkillCharacteristics
    {
        public DeathSaucerCharacteristics() : base(new DeathSaucerChargeCharacteristics(), 10, 10f, 16f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class DeathSaucerChargeCharacteristics : SkillChargeCharacteristics
    {
        public DeathSaucerChargeCharacteristics() : base(5, 5, 35, 35)
        {
        }
    }
}
