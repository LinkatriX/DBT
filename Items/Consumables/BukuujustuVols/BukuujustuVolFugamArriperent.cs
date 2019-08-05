using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.BukuujustuVols
{
    public sealed class BukuujustuVolFugamArriperent : BukuujustuVol
    {
        public BukuujustuVolFugamArriperent() : base("Bukuujutsu Guide Vol. 3 - Fugam Arriperent", "'It has an ancient technique inscribed in it, holding it makes your ki feel calmer.'", ItemRarityID.LightPurple)
        {
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().FlightT3 = true;

            if (player.whoAmI == Main.myPlayer)
                Main.NewText("Your flight now takes barely any Ki.\nYou also now have no fall damage permanently.");

            return true;
        }

        public override bool CanUseItem(Player player) => !player.GetModPlayer<DBTPlayer>().FlightT3;

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 10);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell);
            recipe.AddIngredient(ItemID.TurtleShell);
            recipe.AddIngredient(ItemID.Book, 3);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}