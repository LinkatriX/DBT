using DBT.Items.KiStones;
using DBT.Items.Materials;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.ITTomes
{
    public sealed class ITTomeVolPuraFaciliorisTransmissus : ITTomeVol
    {
        public ITTomeVolPuraFaciliorisTransmissus() : base("I.T. Tome Vol 3 - Pura Facilioris Transmissus", "'It holds an alien power, bending space to your will with ease.'", ItemRarityID.LightPurple)
        {
        }

        public override bool CanUseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            return base.CanUseItem(player) && dbtPlayer.ITBeaconsUnlocked && !dbtPlayer.ITTargetUnlocked;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().ITTargetUnlocked = true;

            if (player.whoAmI == Main.myPlayer)
                Main.NewText("You have unlocked Instant Transmission Lv3.\nInstant Transmission free roaming costs\nare significantly reduced.");

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT5), 20);
            recipe.AddIngredient(mod, nameof(GammaFragment));
            recipe.AddIngredient(ItemID.FragmentNebula);
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.AddIngredient(ItemID.FragmentStardust);
            recipe.AddIngredient(ItemID.FragmentVortex);
            recipe.AddIngredient(ItemID.LunarBar, 4);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}