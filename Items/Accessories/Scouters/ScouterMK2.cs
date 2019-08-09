using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Scouters
{
    public sealed class ScouterMK2 : Scouter
    {
        public ScouterMK2() : base("Blue Scouter", "A Piece of equipment used for scanning power levels\n8% Increased Ki Damage and Hunter effect", Item.buyPrice(gold: 1, silver: 40), ItemRarityID.LightRed, 0.08f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            // TODO Rework recipe.
            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(PridefulKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(ScouterMK1));
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}