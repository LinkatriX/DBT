using Terraria;
using Terraria.ID;

namespace DBT.Items.Tiles.MusicBoxes
{
    public sealed class WastelandBoxItem : DBTItem
    {
        public WastelandBoxItem() : base("Music Box (Wastelands)", null,
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
            item.createTile = mod.TileType("WastelandBoxTile");
            item.accessory = true;
        }
    }
}