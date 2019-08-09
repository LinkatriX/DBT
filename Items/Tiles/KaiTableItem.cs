using DBT.Items.KiStones;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tiles
{
    public sealed class KaiTableItem : DBTItem
    {
        public KaiTableItem() : base("Kai Table", "It pulses with divine pressure, it seems to entrance you.", 24, 26, value: Item.buyPrice(gold: 2, silver: 40), rarity: ItemRarityID.Lime)
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

            item.createTile = mod.TileType(nameof(KaiTableTile));
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ZTableItem));
            recipe.AddIngredient(mod, nameof(KiStoneT5), 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.Pearlwood, 20);
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}