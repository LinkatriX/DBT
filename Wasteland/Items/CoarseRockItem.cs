using DBT.Items;
using DBT.Wasteland.Tiles;
using Terraria.ID;

namespace DBT.Wasteland.Items
{
    public class CoarseRockItem : DBTItem
    {
        public CoarseRockItem() : base("Coarse Rock", "A dried out and compressed piece of sand.", 12, 12)
        {
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;

            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.consumable = true;

            item.rare = ItemRarityID.White;
            item.value = 0;
            item.createTile = mod.TileType(nameof(CoarseRock));
        }
    }
}