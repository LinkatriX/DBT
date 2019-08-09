using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.EliteSaiyan
{
    [AutoloadEquip(EquipType.Legs)]
    public sealed class EliteSaiyanLeggings : DBTArmorPiece
    {
        public EliteSaiyanLeggings() : base("Elite Saiyan Leggings", 
            "22% Increased Ki Damage" +
            "\n18% Increased Ki crit chance" +
            "\n+750 Max Ki" +
            "\nIncreased Ki regen" +
            "\n22% Increased movement speed", 
            28, 18, value: Item.buyPrice(silver: 88), defense: 20, rarity: ItemRarityID.Cyan)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.22f;
            dbtPlayer.KiCritAddition += 18;
            dbtPlayer.MaxKiModifier += 750;
            dbtPlayer.ExternalKiRegenerationModifier += 2;

            player.moveSpeed += 0.22f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(EliteSaiyanLeggings));

            // TODO Add back
            //recipe.AddIngredient(mod, nameof(KatchinScale), 12);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}