using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class WornGloves : DBTItem
    {
        public WornGloves() : base("Worn Gloves", "10% Increased Ki cast speed\n6% Increased Ki damage", 
            22, 16, value: Item.buyPrice(silver: 20), defense: 0, rarity: ItemRarityID.Orange)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.06f;
        }

        public override void AddRecipes()
        {
            // TODO Rework recipe.
            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(CalmKiCrystal), 15);
            recipe.AddIngredient(ItemID.Silk, 20);

            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);

            recipe.AddRecipe();*/
        }
    }
}