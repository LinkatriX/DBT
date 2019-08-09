using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Earthens
{
    public sealed class EarthenArcanium : EarthenItem
    {
        public EarthenArcanium() : base("Earthen Arcanium",
            "'A core of the pure energy of the earth'" +
            "\n10% Increased ki damage" +
            "\nIncreased ki regen" +
            "\nReduced flight ki usage" +
            "\n+1 Max Charges" +
            "\n10% Increased flight speed" +
            "\nThe longer you charge the more ki you charge, limits at +500%",
            18, 30, Item.buyPrice(gold: 7, silver: 20), 0, ItemRarityID.LightPurple)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.10f;
            dbtPlayer.FlightSpeedModifier += 0.1f;
            dbtPlayer.ExternalKiRegenerationModifier += 1;
            dbtPlayer.FlightKiUsageModifier += 1;
            dbtPlayer.KiChargeRateMultiplierLimit += 1;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(EarthenScarab));
            recipe.AddIngredient(mod, nameof(EarthenSigil));
            recipe.AddIngredient(mod, nameof(KiStoneT3), 3);
            
            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}