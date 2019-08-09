using DBT.Items.KiStones;
using DBT.Items.Materials.Metals;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tiles
{
    public sealed class ZTableItem : DBTItem
    {
        public ZTableItem() : base("Z-Table", "A device capable of bending the essence of Ki itself.", 24, 26, value: Item.buyPrice(silver: 6), rarity: ItemRarityID.Green)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 99;

            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.consumable = true;

            item.createTile = mod.TileType(nameof(ZTableTile));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ScrapMetal), 15);
            recipe.AddIngredient(mod, nameof(KiStoneT1), 2);
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
