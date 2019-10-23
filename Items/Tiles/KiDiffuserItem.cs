using DBT.Items.KiStones;
using DBT.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tiles
{
    public class KiDiffuserItem : DBTItem
    {
        public KiDiffuserItem() : base("Ki Diffuser", "It radiates with the glow of ki energy.'\nGives slight ki regen when placed.", 22, 30, 4000, 0, ItemRarityID.Orange)
        {
        }

        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = ModContent.TileType<KiDiffuser>();
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, nameof(KiStoneT1), 5);
            recipe.AddIngredient(null, "ScrapMetal", 10);
            recipe.AddTile(null, "ZTableTile");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}