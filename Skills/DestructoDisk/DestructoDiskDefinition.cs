using Microsoft.Xna.Framework;

namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskDefinition : SkillDefinition
    {//Add armor piercing.
        public DestructoDiskDefinition(params SkillDefinition[] parents) : base("DestructoDisk", "Destructo Disk", "Fires a disk that is capable\nof cutting through enemies. \nCharge to unleash a more\npowerful version.", typeof(DestructoDiskItem), new DestructoDiskCharacteristics(), new Vector2(276, 282), "Given through Master Roshi's 5th quest.", parents: parents)
        {
        }
    }

    public sealed class DestructoDiskCharacteristics : SkillCharacteristics
    {
        public DestructoDiskCharacteristics() : base(new DestructoDiskChargeCharacteristics(), 42, 1.5f, 20f, 3f, 1f, 0.05f, 1.5f, 2f, 1f)
        {
        }
    }

    public sealed class DestructoDiskChargeCharacteristics : SkillChargeCharacteristics
    {
        public DestructoDiskChargeCharacteristics() : base(180, 1, 40, 40 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}