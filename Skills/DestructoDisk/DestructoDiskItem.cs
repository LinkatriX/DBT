using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskItem : SkillItem<DestructoDiskProjectile>
    {
        public DestructoDiskItem() : base(SkillDefinitionManager.Instance.DestructoDisk, 20, 20, ItemRarityID.Blue, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 22;
            item.useTime = 22;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.channel = true;
        }
    }
}
