using DBT;
using DBT.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace DBT.Worlds
{
    public partial class DBTWorld
    {
        public bool generateGravStructure = false;
        public bool placedGravModule = false;
        public bool repairedGravModule = false;
        public int gravGeneratorStartPositionX = 0;
        public int gravGeneratorStartPositionY = 0;

        public void AddGravGenerator(GenerationProgress progress = null)
        {
            if (generateGravStructure)
                return;

            try
            {
                bool success = MakeGravGenerator(progress);
                if (success)
                {
                    generateGravStructure = true;
                }
            }
            catch (Exception exception)
            {
                Main.NewText("Oh no, an error happened [AddGohanHouse]! Report this to NuovaPrime or Skipping and send them the file Terraria/ModLoader/Logs/Logs.txt");
                ErrorLogger.Log(exception);
            }
        }

        bool MakeGravGenerator(GenerationProgress progress)
        {
            string gravGeneratorGen = "Placing some high tech doodads.";
            if (progress != null)
            {
                progress.Message = gravGeneratorGen;
                progress.Set(0.25f);
            }

            gravGeneratorStartPositionX = WorldGen.genRand.Next(Main.maxTilesX - 500, Main.maxTilesX - 200);
            for (var attempts = 0; attempts < 10000; attempts++)
            {
                for (var i = 0; i < 25; i++)
                {
                    gravGeneratorStartPositionY = 190;
                    do
                    {
                        gravGeneratorStartPositionY++;
                    }
                    while ((!Main.tile[gravGeneratorStartPositionX + i, gravGeneratorStartPositionY].active() && gravGeneratorStartPositionX < Main.worldSurface) || Main.tile[gravGeneratorStartPositionX + i, gravGeneratorStartPositionY].type == TileID.Trees || Main.tile[gravGeneratorStartPositionX + i, gravGeneratorStartPositionY].type == 27);
                    if (!Main.tile[gravGeneratorStartPositionX, gravGeneratorStartPositionY].active() || Main.tile[gravGeneratorStartPositionX, gravGeneratorStartPositionY].liquid > 0)
                    {
                        gravGeneratorStartPositionX--;
                    }
                    if (Main.tile[gravGeneratorStartPositionX + i, gravGeneratorStartPositionY].active())
                    {
                        if (Main.tile[gravGeneratorStartPositionX, gravGeneratorStartPositionY].liquid > 0)
                        {
                            gravGeneratorStartPositionX = WorldGen.genRand.Next(Main.maxTilesX - 200, Main.maxTilesX - 500);
                        }
                        else
                        {
                            goto GenerateBuild;
                        }
                    }
                }
            }
            goto GenerateBuild;

        GenerateBuild:
            GenerateGravGenerator();
            return true;
        }

        public bool isGravGeneratorOffsetSet = false;

        public void GenerateGravGenerator()
        {
            if (!isGravGeneratorOffsetSet)
            {
                gravGeneratorStartPositionY -= _gohanHouseTiles.GetLength(0);
                isGravGeneratorOffsetSet = true;
            }

            Point origin = new Point((int)gravGeneratorStartPositionX, gravGeneratorStartPositionY - 14);
            GravGenerator grav = new GravGenerator();
            grav.Place(origin, WorldGen.structures);
            WorldGen.PlaceObject(gravGeneratorStartPositionX + 16, gravGeneratorStartPositionY + 1, mod.TileType<GravityGenerator>());

        }
    }
}

public class GravGenerator : MicroBiome
{
    public override bool Place(Point origin, StructureMap structures)
    {
        Mod mod = DBTMod.Instance;

        Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
        {
            [new Color(54, 50, 255)] = TileID.Titanstone,
            [Color.Black] = -1
        };

        Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
        {
            [new Color(255, 229, 0)] = WallID.DiamondGemspark,
            [new Color(255, 97, 0)] = WallID.DiamondGemsparkOff,
            [new Color(242, 0, 255)] = WallID.SapphireGemsparkOff,
            [new Color(148, 255, 48)] = WallID.Cloud,
            [Color.Black] = -1
        };

        TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Generation/GravChamberTiles"), colorToTile, mod.GetTexture("Generation/GravChamberWalls"), colorToWall, null, mod.GetTexture("Generation/GravChamberSlopes"));

        gen.Generate(origin.X, origin.Y, true, true);

        return true;
    }
}
