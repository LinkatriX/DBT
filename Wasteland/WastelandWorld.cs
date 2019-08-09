using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.Wasteland.Tiles;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace DBT.Wasteland
{
    public class WastelandWorld : ModWorld
    {
        public static int wastelandTiles = 0;

        public override void TileCountsAvailable(int[] tileCounts)
        {
            wastelandTiles = tileCounts[mod.TileType(nameof(CoarseRock))];
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Grass Wall"));
            if (genIndex == -1)
            {
                return;
            }
            tasks.Insert(genIndex + 1, new PassLegacy("Wasteland", WastelandGen));
        }

        private void WastelandGen(GenerationProgress progress)
        {
            progress.Message = "Creating a barren wasteland.";
            progress.Set(0.20f);
            int startPositionX = WorldGen.genRand.Next(Main.maxTilesX / 2 - 1200, Main.maxTilesX / 2 - 400);
            int startPositionY = (int)Main.worldSurface - 300;
            Vector2 generationSize = new Vector2(0, 0);
            if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
            {
                generationSize = new Vector2(262, 18);
            }
            if (Main.maxTilesX == 6300 && Main.maxTilesY == 1800)
            {
                generationSize = new Vector2(380, 22);
            }
            if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
            {
                generationSize = new Vector2(608, 26);
            }

            var generationStartX = startPositionX;
            var generationStartY = RaycastDown(startPositionX, startPositionY);
            progress.Set(0.50f);

            for (int x = 0; x <= generationSize.X; x++)
            {
                for (int y = 0; y <= generationSize.Y; y++)
                {
                    int generationPositionX = generationStartX + x;
                    int generationPositionY = generationStartY + y;
                    if (Main.tile[generationPositionX, generationPositionY].active())
                    {
                        WorldGen.TileRunner(generationPositionX, generationPositionY, 5, WorldGen.genRand.Next(10, 20), (ushort)mod.TileType(nameof(CoarseRock)), false, 0f, 0f, true, true);

                        if (Main.tile[generationPositionX, generationPositionY].type == mod.TileType(nameof(CoarseRock)))
                        {
                            Main.tile[generationPositionX, generationPositionY].wall = (ushort)mod.WallType(nameof(CoarseRockWall));
                        }
                        progress.Set(0.70f);
                    }
                }
            }
        }
        public int RaycastDown(int x, int y)
        {
            while (!Main.tile[x, y].active())
            {
                y++;
            }
            return y;
        }
    }
}

