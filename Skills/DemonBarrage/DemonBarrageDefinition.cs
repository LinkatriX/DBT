using Microsoft.Xna.Framework;

namespace DBT.Skills.DemonBarrage
{
    public sealed class DemonBarrageDefinition : SkillDefinition
    {
        public DemonBarrageDefinition(params SkillDefinition[] parents) : base("DemonBarrage", "Demon Barrage", "Fires a multitude of elemental\nattacks fueled by terraria itself.", typeof(DemonBarrageItem), new DemonBarrageCharacteristics(), new Vector2(466, 282), "Unlocked after having\nthe other 4 elemental attacks\nand 2000 max ki.", parents: parents)
        {
        }
    }

    public sealed class DemonBarrageCharacteristics : SkillCharacteristics
    {
        public DemonBarrageCharacteristics() : base(new DemonBarrageChargeCharacteristics(), 39, 1f, 5f, 4f, 1f)
        {
        }
    }

    public sealed class DemonBarrageChargeCharacteristics : SkillChargeCharacteristics
    {
        public DemonBarrageChargeCharacteristics() : base(0, 0, 15, 0)
        {
        }
    }
}