using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tiles
{
    public class GammaFragmentBlockItem : DBTItem
    {
        public GammaFragmentBlockItem() : base("Gamma Fragment Block", null, 12, 12, 500)
        {
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.consumable = true;
            item.useTime = 10;
            item.useStyle = 1;
            item.createTile = mod.TileType("GammaFragmentBlock");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.AddIngredient(null, "GammaFragment");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}