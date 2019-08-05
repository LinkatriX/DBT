using DBT.Items.KiStones;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public sealed class StandardKiPotion : KiPotion
    {
        public StandardKiPotion() : base("Ki Potion", 15, 30)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT2));
            recipe.AddIngredient(mod, nameof(LesserKiPotion), 2);
            recipe.AddTile(TileID.Bottles);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
