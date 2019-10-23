using DBT.Items.Materials;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.SaiyanBattleArmor
{
    [AutoloadEquip(EquipType.Body)]
    public sealed class SaiyanBattleBreastplate : DBTArmorPiece
    {
        public SaiyanBattleBreastplate() : base("Saiyan Battle Breastplace",
            "10% Increased Ki Damage" +
            "\n6% Increased Ki Crit Chance" +
            "\n+100 Max ki",
            28, 18, value: Item.buyPrice(silver: 44), defense: 10, rarity: ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) =>
            base.IsArmorSet(head, body, legs) && legs.type == ModContent.ItemType<SaiyanBattleLeggings>();

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            player.setBonus = "Drastically increased pickup range for ki orbs.\n+500 max Ki and 8% reduced Ki usage";
            dbtPlayer.KiOrbGrabRange += 9;
            dbtPlayer.MaxKiModifier += 500;
            dbtPlayer.KiDrainMultiplier -= 0.08f;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.10f;
            dbtPlayer.KiCritAddition += 6;
            dbtPlayer.MaxKiModifier += 100;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddIngredient(mod, nameof(SkeletalEssence), 15);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}