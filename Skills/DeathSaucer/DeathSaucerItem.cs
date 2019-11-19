using Terraria.ID;

namespace DBT.Skills.DeathSaucer
{
    public sealed class DeathSaucerItem : SkillItem<DeathSaucerProjectile>
    {
        public DeathSaucerItem() : base(SkillDefinitionManager.Instance.DeathSaucer, 44, 18, ItemRarityID.LightRed, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }
    }
}