using Microsoft.Xna.Framework;

namespace DBT.Skills.DestructoDiskAssault
{/*This move's previous code is pretty broken and will need to be mostly rewritten. Epic.
   Uncharged the move should fire off (2-3?) Destructo disks, while charged it should fire (5-6?). -Skipping*/
   //Add armor piercing.
    public sealed class DestructoDiskAssaultDefinition : SkillDefinition
    {
        public DestructoDiskAssaultDefinition(params SkillDefinition[] parents) : base("DestructoDiskAssault", "Destructo Disk Assault", "Fires a barrage of Destructo Disks. Charge to unleash a larger assault.", typeof(DestructoDiskAssaultItem), new DestructoDiskAssaultCharacteristics(), new Vector2(226, 282), parents: parents)
        {
        }
    }

    public sealed class DestructoDiskAssaultCharacteristics : SkillCharacteristics
    {
        public DestructoDiskAssaultCharacteristics() : base(new DestructoDiskAssaultChargeCharacteristics(), 90, 1f, 20f, 5f, 1f)
        {
        }
    }

    public sealed class DestructoDiskAssaultChargeCharacteristics : SkillChargeCharacteristics
    {
        public DestructoDiskAssaultChargeCharacteristics() : base(100, 4, 140, 140 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
