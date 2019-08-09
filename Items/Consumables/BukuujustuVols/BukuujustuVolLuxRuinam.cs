using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.BukuujustuVols
{
    public sealed class BukuujustuVolLuxRuinam : BukuujustuVol
    {
        public BukuujustuVolLuxRuinam() : base("Bukuujutsu Guide Vol. 2 - Lux Ruinam", "'It has an ancient technique inscribed in it, holding it makes your feet feel softer.'", ItemRarityID.Pink)
        {
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().FlightDampenedFall = true;

            if (player.whoAmI == Main.myPlayer)
                Main.NewText("Your flight now takes barely any Ki.");

            return true;
        }

        public override bool CanUseItem(Player player) => !player.GetModPlayer<DBTPlayer>().FlightDampenedFall;

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT3), 25);
            recipe.AddIngredient(ItemID.SoulofFright, 8);
            recipe.AddIngredient(ItemID.SoulofSight, 8);
            recipe.AddIngredient(ItemID.SoulofMight, 8);
            recipe.AddIngredient(ItemID.Book, 3);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}