using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.Wasteland.Tiles;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.ID;

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
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Granite"));
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
            int startPositionX = Main.maxTilesX / 2 - 1000;
            int startPositionY = (int)Main.worldSurface - 180;
            Vector2 generationSize = new Vector2(0, 0);
            if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
            {
                generationSize = new Vector2(262, 210);
            }
            if (Main.maxTilesX == 6300 && Main.maxTilesY == 1800)
            {
                generationSize = new Vector2(390, 320);
            }
            if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
            {
                generationSize = new Vector2(508, 510);
            }

            var generationStartX = startPositionX;
            var generationStartY = RaycastDown(startPositionX, startPositionY);
            progress.Set(0.50f);

            for (int x = 0; x <= generationSize.X; x++)
            {
                for (int y = 0; y <= generationSize.Y; y++)
                {
                    int generationPositionX = generationStartX + x + Main.rand.Next(1, 3);
                    int generationPositionY = generationStartY + y;
                    if (Main.tile[generationPositionX, generationPositionY].active())
                    {
                        if (Main.tile[generationPositionX, generationPositionY].type == TileID.SnowBlock)
                            generationPositionX += 100;

                        if (Main.tile[generationPositionX, generationPositionY - 1].type != (ushort)mod.TileType(nameof(CoarseRock)) && Main.tile[generationPositionX, generationPositionY - 1].active())
                            generationPositionY--;

                        else
                        {
                            WorldGen.TileRunner(generationPositionX, generationPositionY, 5, WorldGen.genRand.Next(20, 30), (ushort)mod.TileType(nameof(CoarseRock)), false, 0f, 0f, true, true);
                            progress.Set(0.70f);
                        }
                    }
                }
            }
        }
        public int RaycastDown(int x, int y)
        {
            while (!Framing.GetTileSafely(x, y).active())
            {
                y++;
            }
            return y;
        }
    }
}

