using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.SaiyanScout
{
    [AutoloadEquip(EquipType.Body)]
    public sealed class SaiyanScoutScalemail : DBTArmorPiece
    {
        public SaiyanScoutScalemail() : base("Saiyan Scout Scalemail",
            "3% Increased Ki Damage" +
            "\n2% Increased Ki Crit Chance" +
            "\n5% reduced ki usage",
            28, 18, value: Item.buyPrice(silver: 16), defense: 4, rarity: ItemRarityID.Green)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) =>
            base.IsArmorSet(head, body, legs) && legs.type == ModContent.ItemType<SaiyanScoutPants>();

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "+100 max Ki";
            player.GetModPlayer<DBTPlayer>().MaxKiModifier += 100;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.03f;
            dbtPlayer.KiCritAddition += 2;
            dbtPlayer.KiDrainMultiplier -= 0.05f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Silk, 18);
            recipe.AddIngredient(ItemID.CopperBar, 8);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();


            recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Silk, 18);
            recipe.AddIngredient(ItemID.TinBar, 8);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}