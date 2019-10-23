using DBT.Items.Materials;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Demon
{
    [AutoloadEquip(EquipType.Body)]
    public sealed class DemonShirt : DBTArmorPiece
    {
        public DemonShirt() : base("Demon Shirt",
            "18% Increased Ki Damage" +
            "\n14% Increased Ki Crit Chance" +
            "\n+700 Max Ki" +
            "\n+1 Maximum Charges",
            28, 18, Item.buyPrice(silver: 60), 20, ItemRarityID.Cyan)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && legs.type == ModContent.ItemType<DemonLeggings>();

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.18f;
            dbtPlayer.KiCritAddition += 14;
            dbtPlayer.MaxKiModifier += 700;
            dbtPlayer.KiChargeRateMultiplierLimit += 1;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            base.DrawHands(ref drawHands, ref drawArms);

            drawArms = true;
            drawHands = true;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(SatanicCloth), 16);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}