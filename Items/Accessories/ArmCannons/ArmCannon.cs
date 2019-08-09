using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.ArmCannons
{
    public sealed class ArmCannon : DBTItem
    {
        public ArmCannon() : base("Arm Cannon", "An old arm blaster used by many soldiers.\n10% Reduced Ki usage\nIncreased charge speed",
            18, 30, value: Item.buyPrice(gold:1, silver: 20), rarity: ItemRarityID.LightRed)
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

            player.GetModPlayer<DBTPlayer>().KiChargeRateModifier += 1;
        }
    }
}