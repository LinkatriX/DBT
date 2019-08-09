using DBT.Items.KiStones;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public sealed class SuperKiPotion : KiPotion
    {
        public SuperKiPotion() : base("Super Ki Potion", 2180, 0)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4));
            recipe.AddIngredient(mod, nameof(GreaterKiPotion), 4);
            recipe.AddTile(TileID.Bottles);

            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}
