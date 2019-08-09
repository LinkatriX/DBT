using DBT.Items.Accessories.ArmCannons;
using DBT.Items.Accessories.Scouters;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    public sealed class BattleKit : DBTItem
    {
        public BattleKit() : base("Battle Kit", "The essence of strong defense.\nCharging grants a protective barrier that grants drastically increased defense.", 
            18, 30, value: Item.buyPrice(silver: 40), rarity: ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            player.detectCreature = true;

            dbtPlayer.KiDamageMultiplier += 0.06f;
            dbtPlayer.KiChargeRateModifier += 1f;
            dbtPlayer.KiSpeedAddition += 1;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ScouterMK1));
            recipe.AddIngredient(mod, nameof(WornGloves));
            recipe.AddIngredient(mod, nameof(ArmCannon));
            
            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}