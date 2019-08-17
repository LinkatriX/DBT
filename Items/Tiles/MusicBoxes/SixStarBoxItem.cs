using Terraria;
using Terraria.ID;

namespace DBT.Items.Tiles.MusicBoxes
{
    public sealed class SixStarBoxItem : DBTItem
    {
        public SixStarBoxItem() : base("Music Box (Lost Courage - Tenkaichi 2)", null,
            36, 22, Item.buyPrice(gold: 1), 0, ItemRarityID.Blue)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("SixStarBoxTile");
            item.accessory = true;
        }
    }
}