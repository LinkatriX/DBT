using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Baldurs//TODO: Still needs to increase ki charging rate. -Skipping
{
    public sealed class BuldariumSigmite : BaldurItem
    {
        public BuldariumSigmite() : base("Buldarium Sigmite", "'A fragment of the God of Defense's soul.'\nCharging grants a protective barrier that grants a 50% increase to defense\nCharging also grants effects of shiny stone\nIncreased Ki charge rate",
            Item.buyPrice(gold: 1, silver: 80), 10, ItemRarityID.Yellow, 0.5f)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            if (player.GetModPlayer<DBTPlayer>().IsCharging)
            {
                dbtPlayer.KiChargeRateMultiplierLimit += 1;
                player.shinyStone = true;
            }
                
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(BaldurEssentia));
            recipe.AddIngredient(mod, nameof(KiStoneT3));
            recipe.AddIngredient(ItemID.ShinyStone);

            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
