using Terraria;
using Terraria.ID;

namespace DBT.Items.Tiles.MusicBoxes
{
    public sealed class BabidisMagicBoxItem : DBTItem
    {
        public BabidisMagicBoxItem() : base("Music Box (Burst Limit)", null,
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
            item.createTile = mod.TileType("BabidisMagicBoxTile");
            item.accessory = true;
        }
    }
}