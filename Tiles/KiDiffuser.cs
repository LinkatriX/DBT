using DBT.Items.Tiles;
using DBT.Players;
using Microsoft.Xna.Framework;
 using Terraria;
using Terraria.DataStructures;
 using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DBT.Tiles
{
    public class KiDiffuser : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(75, 139, 166));
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Ki Diffuser");
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType<KiDiffuserItem>());
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>(mod);
                modPlayer.KiDiffuser = true;
            }
        }        
    }
}