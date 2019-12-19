using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Wasteland.Tiles
{
    public class HardenedRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("HardenedRockItem");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Hardened Rock");
            //AddMapEntry(new Color(242, 179, 70), name);
            AddMapEntry(new Color(189, 114, 68), name);
            dustType = 102;
            minPick = 200;
        }
    }
}