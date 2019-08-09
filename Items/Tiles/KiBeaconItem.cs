using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tiles
{
    public class KiBeaconItem : DBTItem
    {
        public KiBeaconItem() : base("Ki Beacon", "Radiates Ki you can lock on to with Instant Transmission.", 24, 26, value: Item.buyPrice(gold: 12), rarity: ItemRarityID.Lime)
        {
        }

        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("KiBeaconTile");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Teleporter, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 3);
            recipe.AddIngredient(null, "AstralEssentia", 1);
            recipe.AddTile(null, "ZTableTile");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}