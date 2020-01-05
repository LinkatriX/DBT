using DBT.Skills.KiBlast;
using Terraria.ID;

namespace DBT.Skills.SpiritBall
{
    public sealed class SpiritBallItem : SkillItem<SpiritBallProjectile>
    {
        public SpiritBallItem() : base(SkillDefinitionManager.Instance.SpiritBall, 20, 20, ItemRarityID.Lime, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = ItemUseStyleID.Stabbing;
        }
    }
}