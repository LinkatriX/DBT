using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Scouters
{
    public sealed class ScouterMK1 : Scouter
    {
        public ScouterMK1() : base("Green Scouter", "A Piece of equipment used for scanning power levels\n5% Increased Ki Damage and Hunter effect", Item.buyPrice(silver: 80), ItemRarityID.Orange, 0.05f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(CalmKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 15);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}