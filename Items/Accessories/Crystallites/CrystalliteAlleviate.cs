using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Crystallites
{
    public sealed class CrystalliteAlleviate : CrystalliteItem
    {
        public CrystalliteAlleviate() : base("Aspera Crystallite", "'The essence of pure energy lives within the crystal.'\nDrastically Increased speed while charging\n+2500 Max ki", 
            22, 34, Item.buyPrice(gold:14, silver: 40), ItemRarityID.Cyan, 2500)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            //recipe.AddIngredient(mod, nameof(RadiantFragment));
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(mod, nameof(CrystalliteFlow));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}