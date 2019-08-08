using DBT.Items.DragonBalls;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DBT.Tiles.DragonBalls
{
    public abstract class DragonBallTile : ModTile
    {
        //private readonly string _displayName, _itemName;
        protected DragonBallTile(string displayName, string itemName)
        {
            /*_displayName = displayName;
            _itemName = itemName;*/
        }

        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileShine[Type] = 1150;
            Main.tileShine2[Type] = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);

            /*ModTranslation name;
            name = CreateMapEntryName();
            name.SetDefault(_displayName);
            drop = mod.ItemType(_itemName);
            AddMapEntry(new Color(249, 193, 49), name);*/
            disableSmartCursor = true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType(DragonBall.GetDragonBallItemTypeFromNumber(DragonBall.GetDragonBallNumber(player)));
        }

        //The four star is special, it has its own drop method
        public override bool Drop(int i, int j)
        {
            Player player = Main.player[Player.FindClosest(new Vector2(i * 16f, j * 16f), 1, 1)];
            if (player == null)
                return true;
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>(mod);
            if (modPlayer.firstDragonBallPickup)
                return true;
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("DragonBallNote"));
            modPlayer.firstDragonBallPickup = true;
            return true;
        }
    }

    public class OneStarDBTile : DragonBallTile
    {
        public OneStarDBTile() : base("1 Star Dragon Ball", "OneStarDragonBall")
        {
            base.SetDefaults();
        }
    }

    public class TwoStarDBTile : DragonBallTile
    {
        public TwoStarDBTile() : base("2 Star Dragon Ball", "TwoStarDragonBall")
        {
            base.SetDefaults();
        }
    }

    public class ThreeStarDBTile : DragonBallTile
    {
        public ThreeStarDBTile() : base("3 Star Dragon Ball", "ThreeStarDragonBall")
        {
            base.SetDefaults();
        }
    }

    public class FourStarDBTile : DragonBallTile
    {
        public FourStarDBTile() : base("4 Star Dragon Ball", "FourStarDragonBall")
        {
            base.SetDefaults();
        }
    }

    public class FiveStarDBTile : DragonBallTile
    {
        public FiveStarDBTile() : base("5 Star Dragon Ball", "FiveStarDragonBall")
        {
            base.SetDefaults();
        }
    }

    public class SixStarDBTile : DragonBallTile
    {
        public SixStarDBTile() : base("6 Star Dragon Ball", "SixStarDragonBall")
        {
            base.SetDefaults();
        }
    }

    public class SevenStarDBTile : DragonBallTile
    {
        public SevenStarDBTile() : base("7 Star Dragon Ball", "SevenStarDragonBall")
        {
            base.SetDefaults();
        }
    }
}
