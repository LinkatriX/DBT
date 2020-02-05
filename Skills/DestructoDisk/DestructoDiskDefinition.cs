using Microsoft.Xna.Framework;

namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskDefinition : SkillDefinition
    {//Add armor piercing.
        public DestructoDiskDefinition(params SkillDefinition[] parents) : base("DestructoDisk", "Destructo Disk", "Fires a disk that is capable\nof cutting through enemies.", typeof(DestructoDiskItem), new DestructoDiskCharacteristics(), new Vector2(276, 282), "Given through Master Roshi's 5th quest.", parents: parents)
        {
        }
    }

    public sealed class DestructoDiskCharacteristics : SkillCharacteristics
    {
        public DestructoDiskCharacteristics() : base(new DestructoDiskChargeCharacteristics(), 42, 1f, 20f, 3f, 1f)
        {
        }
    }

    public sealed class DestructoDiskChargeCharacteristics : SkillChargeCharacteristics
    {
        public DestructoDiskChargeCharacteristics() : base(130, 1, 40, 40 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}