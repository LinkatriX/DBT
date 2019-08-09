using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.ITTomes
{
    public sealed class ITTomeVolDissipatiPeribunt : ITTomeVol
    {
        public ITTomeVolDissipatiPeribunt() : base("I.T. Tome Vol 1 - Dissipati Peribunt", "'It holds an alien power, bending space to seek beacons of Ki.'", ItemRarityID.LightRed)
        {
        }

        public override bool CanUseItem(Player player) => base.CanUseItem(player) && !player.GetModPlayer<DBTPlayer>().ITUnlocked;

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().ITUnlocked = true;

            if (player.whoAmI == Main.myPlayer)
                Main.NewText("You have unlocked Instant Transmission Lv1. Open your map to learn how to use it.");

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT1), 20);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}