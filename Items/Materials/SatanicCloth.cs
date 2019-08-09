using DBT.Items.KiStones;
using DBT.Tiles.Stations;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Materials
{
    public sealed class SatanicCloth : DBTMaterial
    {
        // TODO Set sell price.
        public SatanicCloth() : base("Satanic Cloth", "'The cloth radiates with animosity.'",
            26, 28, 0, ItemRarityID.Pink)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4));
            recipe.AddIngredient(ItemID.SoulofFright, 3);
            recipe.AddIngredient(ItemID.SoulofMight, 3);
            recipe.AddIngredient(ItemID.SoulofSight, 3);
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this, 6);
            recipe.AddRecipe();
        }
    }
}