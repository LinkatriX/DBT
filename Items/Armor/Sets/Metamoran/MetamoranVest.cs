using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Metamoran
{
    [AutoloadEquip(EquipType.Body)]
    public sealed class MetamoranVest : DBTArmorPiece
    {
        public MetamoranVest() : base("Metamoran Vest",
            "15% Increased Ki damage" +
            "\nIncreased Ki regen",
            28, 18, value: Item.buyPrice(silver: 36), defense: 8, rarity: ItemRarityID.Yellow)
        {
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            base.DrawHands(ref drawHands, ref drawArms);

            drawArms = true;
            drawHands = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) =>
            base.IsArmorSet(head, body, legs) && legs.type == ModContent.ItemType<MetamoranPants>();

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            player.setBonus = "8% reduced ki usage and +400 Max Ki\n5% chance to do double damage.";
            dbtPlayer.KiDamageMultiplier -= 0.08f;
            dbtPlayer.MaxKiModifier += 200;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.15f;
            dbtPlayer.ExternalKiRegenerationModifier += 1f;
        }
    }
}