using DBT.Items;
using DBT.Wasteland.Tiles;
using Terraria.ID;

namespace DBT.Wasteland.Items
{
    public class CoarseRockWallItem : DBTItem
    {
        public CoarseRockWallItem() : base("Coarse Rock Wall", null, 12, 12)
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
            item.createWall = mod.WallType(nameof(CoarseRockWall));
        }
    }
}