using DBT.Items.DragonBalls;
using DBT.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DBT.Tiles.DragonBalls
{
    public abstract class DragonBallTile : ModTile
    {
        private readonly string _displayName, _itemName;
        private readonly int _whichDragonBallAmI;

        protected DragonBallTile(string displayName, string itemName, int whichDragonBallAmI)
        {
            _displayName = displayName;
            _itemName = itemName;
            _whichDragonBallAmI = whichDragonBallAmI;
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
            ModTranslation name;
            name = CreateMapEntryName();
            name.SetDefault(_displayName);
            drop = mod.ItemType(_itemName);
            AddMapEntry(new Color(249, 193, 49), name);
            disableSmartCursor = true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType(DragonBall.GetDragonBallItemTypeFromNumber(_whichDragonBallAmI));
        }

        //The four star is special, it has its own drop method
        public override bool Drop(int i, int j)
        {
            Player player = Main.player[Player.FindClosest(new Vector2(i * 16f, j * 16f), 1, 1)];
            if (player == null)
                return true;
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>(mod);
            if (modPlayer.FirstDragonBallPickup)
                return true;
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("FourStarDragonBall"));
            modPlayer.FirstDragonBallPickup = true;
            return true;
        }
    }

    public class OneStarDragonBallTile : DragonBallTile
    {
        public OneStarDragonBallTile() : base("1 Star Dragon Ball", "OneStarDragonBallTile", 1)
        {
        }
    }

    public class TwoStarDragonBallTile : DragonBallTile
    {
        public TwoStarDragonBallTile() : base("2 Star Dragon Ball", "TwoStarDragonBallTile", 2)
        {
        }
    }

    public class ThreeStarDragonBallTile : DragonBallTile
    {
        public ThreeStarDragonBallTile() : base("3 Star Dragon Ball", "ThreeStarDragonBallTile", 3)
        {
        }
    }

    public class FourStarDragonBallTile : DragonBallTile
    {
        public FourStarDragonBallTile() : base("4 Star Dragon Ball", "FourStarDragonBallTile", 4)
        {
        }
    }

    public class FiveStarDragonBallTile : DragonBallTile
    {
        public FiveStarDragonBallTile() : base("5 Star Dragon Ball", "FiveStarDragonBallTile", 5)
        {
        }
    }

    public class SixStarDragonBallTile : DragonBallTile
    {
        public SixStarDragonBallTile() : base("6 Star Dragon Ball", "SixStarDragonBallTile", 6)
        {
        }
    }

    public class SevenStarDragonBallTile : DragonBallTile
    {
        public SevenStarDragonBallTile() : base("7 Star Dragon Ball", "SevenStarDragonBallTile", 7)
        {
        }
    }
}
