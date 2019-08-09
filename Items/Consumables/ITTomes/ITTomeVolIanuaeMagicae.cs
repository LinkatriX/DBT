using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.ITTomes
{
    public sealed class ITTomeVolIanuaeMagicae : ITTomeVol
    {
        public ITTomeVolIanuaeMagicae() : base("I.T. Tome Vol 2 - Ianuae Magicae", "'It holds an alien power, bending space to your will.'", ItemRarityID.Pink)
        {
        }

        public override bool CanUseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            return base.CanUseItem(player) && dbtPlayer.ITUnlocked && !dbtPlayer.ITBeaconsUnlocked;
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().ITBeaconsUnlocked = true;

            if (player.whoAmI == Main.myPlayer)
                Main.NewText("You have unlocked Instant Transmission Lv2 (Supreme Kai Teleportation).\nUse your Instant Transmission hotkey and cursor to seek a remote destination.");

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 10);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.RodofDiscord);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}