using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.DisgustingGoops
{
    public sealed class DisgustingGoop : DBTConsumable
    {
        public DisgustingGoop() : base("Disguting Goop", "Stablizes Ki but tastes disgusting.", 16, 20, Item.buyPrice(silver: 48), ItemRarityID.Orange, ItemUseStyleID.EatingUsing, true, SoundID.Item3, 17, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.buffType = ModContent.BuffType<DisgustingGoopBuff>();
            item.buffTime = 5400;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disgusting Goop");
            Tooltip.SetDefault("Stablizes Ki but tastes disgusting.");
        }
    }
}