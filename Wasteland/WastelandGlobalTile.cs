using DBT.Helpers;
using DBT.Wasteland.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Wasteland
{
    public class WastelandGlobalTile : GlobalTile
    {
        private int[] undergroundTiles = new int[] { ModContent.TileType<HardenedRock>() };
        public override void RandomUpdate(int i, int j, int type)
        {
            if (Main.tile[i, j].type == ModContent.TileType<CoarseRock>() && !Main.tile[i, j - 1].active())
            {
                if (Main.rand.Next(30) == 0)
                {
                    WorldGen.PlaceTile(i, j - 2, ModContent.TileType<WastelandRocks>(), true, true, -1, Main.rand.Next(0, 2));
                }
            }
            if (Main.tile[i, j].type == ModContent.TileType<CoarseRock>())
            {
                if (Main.tile[i, j - 1].type == TileID.Plants || Main.tile[i, j - 1].type == TileID.Plants2)
                    WorldGen.KillTile(i, j - 1);
            }
        }
        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            if (!Main.tile[i, j].active() && Main.tile[i, j].liquid > 0 && BaseTile.GetTileCount(new Vector2(i, j), undergroundTiles, 50) >= 30)
            {
                if (Main.tile[i, j - 1].liquid == 0 || !Main.tile[i, j - 1].active())
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(BaseUtility.TileToPos(new Vector2(i, j)), 4, 4, 27, Main.rand.Next(-1, 1), Main.rand.Next(-15, -30), 0, default, 1.5f);
                        SoundHelper.PlayCustomSound("Sounds/AcidSizzle", BaseUtility.TileToPos(new Vector2(i, j)), 0.6f, 0.1f);
                    }
                }
            }
        }
    }
}
