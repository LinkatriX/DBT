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
        public static int undergroundWastelandTiles = 0;
        public override void TileCountsAvailable(int[] tileCounts)
        {
            wastelandTiles = tileCounts[mod.TileType(nameof(CoarseRock))];
            undergroundWastelandTiles = tileCounts[mod.TileType(nameof(HardenedRock))];
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Granite"));
            if (genIndex == -1)
            {
                return;
            }
            tasks.Insert(genIndex + 1, new PassLegacy("Wasteland", WastelandGen));
            
            int genIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (genIndex2 == -1)
            {
                return;
            }
            tasks.Insert(genIndex2 + 1, new PassLegacy("Saibaman Nest", NestGen));
        }

        private void WastelandGen(GenerationProgress progress)
        {
            progress.Message = "Creating a barren wasteland.";
            progress.Set(0.20f);

            int startPositionX = Main.maxTilesX / 2 - 1300;
            int startPositionY = (int)Main.worldSurface - 180;

            Vector2 generationSize = new Vector2(0, 0);
            if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
            {
                generationSize = new Vector2(262, 240);
                startPositionX = Main.maxTilesX / 2 - 1300;
            }
            else if (Main.maxTilesX == 6300 && Main.maxTilesY == 1800)
            {
                generationSize = new Vector2(390, 350);
                startPositionX = Main.maxTilesX / 2 - 1900;
            }
            else if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
            {
                generationSize = new Vector2(508, 540);
                startPositionX = Main.maxTilesX / 2 - 2500;
            }
            else
                generationSize = new Vector2(262, 210);

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
                        if (Main.tile[generationPositionX, generationPositionY].type == TileID.SnowBlock || Main.tile[generationPositionX, generationPositionY].type == TileID.Mud)
                            generationPositionX += 200;

                        if (Main.tile[generationPositionX, generationPositionY - 1].type != (ushort)mod.TileType(nameof(CoarseRock)) && Main.tile[generationPositionX, generationPositionY - 1].active())
                            generationPositionY--;

                        else
                        {
                            WorldGen.TileRunner(generationPositionX, generationPositionY, 5, WorldGen.genRand.Next(20, 30), (ushort)mod.TileType(nameof(CoarseRock)), false, 0f, 0f, false, true);
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

        private void NestGen(GenerationProgress progress)
        {
            progress.Message = "Forming a nest.";
            progress.Set(0.20f);
            GenerateSaibaNest();
        }

        public void GenerateSaibaNest()
        {
            Point origin = new Point(Main.maxTilesX / 2 - 1300 + 40, RaycastDown(Main.maxTilesX / 2 - 1700, (int)Main.worldSurface - 180));
            SaibaNest saiba = new SaibaNest();
            saiba.Place(origin, WorldGen.structures);
        }
    }

    

    public class SaibaNest : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = DBTMod.Instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(152, 74, 0)] = ModContent.TileType<HardenedRock>(),
                [new Color(255, 181, 81)] = ModContent.TileType<CoarseRock>(),
                [new Color(255, 212, 171)] = -2,
                [Color.Black] = -1
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(103, 50, 0)] = ModContent.WallType<CoarseRockWall>(),
                [Color.Black] = -2
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Generation/SaibaNestTiles"), colorToTile, mod.GetTexture("Generation/SaibaNestWalls"), colorToWall, null, mod.GetTexture("Generation/SaibaNestSlopes"));

            gen.Generate(origin.X, origin.Y, true, true);

            return true;
        }
    }
}


