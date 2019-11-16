using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Wasteland.Tiles
{
    public class CoarseRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("CoarseRockItem");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Coarse Rock");
            //AddMapEntry(new Color(242, 179, 70), name);
            AddMapEntry(new Color(252, 163, 108), name);
            dustType = 102;
        }
    }
}