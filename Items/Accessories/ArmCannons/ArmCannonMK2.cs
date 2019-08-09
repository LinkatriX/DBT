using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.ArmCannons
{
    public sealed class ArmCannonMK2 : DBTItem
    {
        public ArmCannonMK2() : base("Arm Cannon MK2", "A high-tier arm blaster used by the elite soldiers in the frieza force.\nUsing ki attacks occasionally causes you to fire out a blast of energy towards the cursor.\n16% Reduced Ki usage\nIncreased charge speed\nIncreased ki regen",
            18, 30, value: Item.buyPrice(gold:2), rarity: ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.expert = true;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.GetModPlayer<DBTPlayer>().KiChargeRateModifier += 1;
        }
    }
}