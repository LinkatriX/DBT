using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.BukuujustuVols
{
    public sealed class BukuujustuVolChorusAeria : BukuujustuVol
    {
        public BukuujustuVolChorusAeria() : base("Bukuujutsu Guide Vol. 1 - Chorus Aeria", "'It has an ancient technique inscribed in it, holding it makes you feel lighter.'", ItemRarityID.Orange)
        {
        }

        public override bool CanUseItem(Player player) => !player.GetModPlayer<DBTPlayer>().FlightUnlocked;

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().FlightUnlocked = true;

            if (player.whoAmI == Main.myPlayer)
                Main.NewText("You have unlocked flight.\nBind a key to flight toggle to use it.");

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT3), 10);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}