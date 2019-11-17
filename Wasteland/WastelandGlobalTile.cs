using DBT.Wasteland.Tiles;
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
        public override void RandomUpdate(int i, int j, int type)
        {
            if (Main.tile[i, j].type == ModContent.TileType<CoarseRock>())
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
    }
}
