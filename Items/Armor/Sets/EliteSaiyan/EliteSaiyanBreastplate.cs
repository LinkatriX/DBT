using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.EliteSaiyan
{
    [AutoloadEquip(EquipType.Body)]
    public sealed class EliteSaiyanBreastplate : DBTArmorPiece
    {
        public EliteSaiyanBreastplate() : base("Elite Saiyan Breastplate", 
            "26% Increased Ki Damage" +
            "\n24% Increased Ki crit chance" +
            "\n+1000 Max Ki" +
            "\n+3 Maximum Charges" +
            "\nIncreased Ki regen", 
            28, 18, value: Item.buyPrice(gold: 1, silver: 44), defense: 30, rarity: ItemRarityID.Cyan)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && legs.type == mod.ItemType<EliteSaiyanLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "Taking fatal damage will instead restore you to max life with x2 damage for a short time,\n1 minute cooldown.\n+150 Max Life";
            player.statLifeMax2 += 150;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.26f;
            dbtPlayer.KiCritAddition += 24;
            dbtPlayer.MaxKiModifier += 1000;
            dbtPlayer.ExternalKiRegenerationModifier += 2;
            dbtPlayer.KiChargeRateMultiplierLimit += 3;
        }
    }
}