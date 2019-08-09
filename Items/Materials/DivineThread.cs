using DBT.Items.KiStones;
using DBT.Tiles.Stations;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Materials
{
    public sealed class DivineThread : DBTMaterial
    {
        // TODO Set sell price.
        public DivineThread() : base("Divine Threads", "'A unbelievably soft material that radiates a divine-like energy.'",
            26, 28, 0, ItemRarityID.Yellow)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT5));
            recipe.AddIngredient(ItemID.Ectoplasm, 3);
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this, 6);
            recipe.AddRecipe();
        }
    }
}