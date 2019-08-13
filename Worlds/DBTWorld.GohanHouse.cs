using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace DBT.Worlds
{
    public partial class DBTWorld
    {
        private bool _generateGohanHouse = false;
        private bool _isGohanHouseCleaned = false;
        public bool isDragonBallPlacementDone = false;
        public int gohanHouseStartPositionX = 0;
        public int gohanHouseStartPositionY = 0;

        #region Gohan House Bytes

        //18 tiles long, 16 tiles high (with dirt on the bottom)

        //0 = air, 1 = grey stucco, 2 = blue dynasty shingles, 3 = Smooth marble, 4 = dynasty wood, 5 = dirt
        private static readonly byte[,] _gohanHouseTiles =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,2,2,2,2,2,2,0,0,0,0,0,0},
            {0,0,0,0,0,2,2,2,2,2,2,2,2,0,0,0,0,0},
            {2,0,0,0,2,2,2,2,2,2,2,2,2,2,0,0,0,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0}
        };
        //0 = nothing, 1 = wood wall, 2 = living wood wall, 3 = grey stucco wall, 4 = glass wall
        private static readonly byte[,] _gohanHouseWalls =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,3,3,2,3,3,3,3,2,3,3,0,0,0,0},
            {0,0,0,0,3,3,2,4,4,4,4,2,3,3,0,0,0,0},
            {0,0,0,0,3,3,2,4,4,4,4,2,3,3,0,0,0,0},
            {0,0,0,0,3,3,2,4,4,4,4,2,3,3,0,0,0,0},
            {0,0,0,0,1,1,2,1,1,1,1,2,1,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

        //0=none, 1=bottom-right, 2=bottom-left, 3=top-left, 4=top-right, 5=half
        private static readonly byte[,] _gohanHouseSlopes =
{
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,2,0,0,0,0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0}
        };
        //0 = nothing, 1 = dynasty door, 2 = dynasty table, 3 = dynasty cup, 4 = large dynasty lantern, 5 = dynasty lantern, 6 = shadewood cabinet, 7 = 4 star dragon ball
        private static readonly byte[,] _gohanHouseObjects =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,4,0,0,0,0,0,5,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,2,0,0,0,6,0,0,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

        #endregion

        public void AddGohanHouse(GenerationProgress progress = null)
        {
            if (_generateGohanHouse)
                return;

            try
            {
                bool success = MakeGohanHouse(progress);
                if (success)
                {
                    _generateGohanHouse = true;
                }
            }
            catch (Exception exception)
            {
                Main.NewText("Oh no, an error happened [AddGohanHouse]! Report this to NuovaPrime or Skipping and send them the file Terraria/ModLoader/Logs/Logs.txt");
                ErrorLogger.Log(exception);
            }
        }

        public void CleanupGohanHouse(GenerationProgress progress = null)
        {
            // bail if the house never generated, something is wrong :(
            if (!_generateGohanHouse)
                return;

            // bail if already done.
            if (_isGohanHouseCleaned)
                return;

            try
            {
                var success = RunGohanCleanupRoutine(progress);
                if (success)
                {
                    _isGohanHouseCleaned = true;
                }
            }
            catch (Exception exception)
            {
                Main.NewText("Oh no, an error happened [CleanupGohanHouse]! Report this to NuovaPrime or Skipping and send them the file Terraria/ModLoader/Logs/Logs.txt");
                ErrorLogger.Log(exception);
            }
        }

        bool MakeGohanHouse(GenerationProgress progress)
        {
            string gohanHouseGen = "Creating the house of a legend.";
            if (progress != null)
            {
                progress.Message = gohanHouseGen;
                progress.Set(0.25f);
            }

            gohanHouseStartPositionX = WorldGen.genRand.Next(Main.maxTilesX / 2 - 70, Main.spawnTileX - 25);
            for (var attempts = 0; attempts < 10000; attempts++)
            {
                for (var i = 0; i < 25; i++)
                {
                    gohanHouseStartPositionY = 190;
                    do
                    {
                        gohanHouseStartPositionY++;
                    }
                    while ((!Main.tile[gohanHouseStartPositionX + i, gohanHouseStartPositionY].active() && gohanHouseStartPositionY < Main.worldSurface) || Main.tile[gohanHouseStartPositionX + i, gohanHouseStartPositionY].type == TileID.Trees || Main.tile[gohanHouseStartPositionX + i, gohanHouseStartPositionY].type == 27);
                    if (!Main.tile[gohanHouseStartPositionX, gohanHouseStartPositionY].active() || Main.tile[gohanHouseStartPositionX, gohanHouseStartPositionY].liquid > 0)
                    {
                        gohanHouseStartPositionX++;
                    }
                    if (Main.tile[gohanHouseStartPositionX + i, gohanHouseStartPositionY].active())
                    {
                        if (Main.tile[gohanHouseStartPositionX, gohanHouseStartPositionY].liquid > 0)
                        {
                            gohanHouseStartPositionX = WorldGen.genRand.Next(Main.maxTilesX / 2 - 70, Main.spawnTileX - 25);
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
            GenerateGohanStructureWithByteArrays(true);
            return true;
        }

        // flag tracking whether the initial house creation point has been offset by the building's height, should only occur once.
        public bool isGohanHouseOffsetSet = false;

        public void GenerateGohanStructureWithByteArrays(bool isFirstPass)
        {
            if (!isGohanHouseOffsetSet)
            {
                gohanHouseStartPositionY -= _gohanHouseTiles.GetLength(0);
                isGohanHouseOffsetSet = true;
            }

            // if we're here it means we are ready to generate our structure

            // tiles
            for (var x = 0; x < _gohanHouseTiles.GetLength(1); x++)
            {
                for (var y = 0; y < _gohanHouseTiles.GetLength(0); y++)
                {
                    var offsetX = gohanHouseStartPositionX + x;
                    var offsetY = gohanHouseStartPositionY + y;
                    var tile = Framing.GetTileSafely(offsetX, offsetY);
                    if (_gohanHouseTiles[y, x] == 0)
                    {
                        if (!isFirstPass) continue;
                        tile.type = 0;
                        tile.active(false);
                    }
                    else if (_gohanHouseTiles[y, x] == 1)
                    {
                        tile.type = TileID.GrayStucco;
                        tile.active(true);
                    }
                    else if (_gohanHouseTiles[y, x] == 2)
                    {
                        tile.type = TileID.BlueDynastyShingles;
                        tile.active(true);
                    }
                    else if (_gohanHouseTiles[y, x] == 3)
                    {
                        tile.type = TileID.MarbleBlock;
                        tile.active(true);
                    }
                    else if (_gohanHouseTiles[y, x] == 4)
                    {
                        tile.type = TileID.DynastyWood;
                        tile.active(true);
                    }
                    else if (_gohanHouseTiles[y, x] == 5)
                    {
                        tile.type = TileID.Grass;
                        tile.active(true);
                    }
                }
            }
            for (var x = 0; x < _gohanHouseSlopes.GetLength(1); x++)
            {
                for (var y = 0; y < _gohanHouseSlopes.GetLength(0); y++)
                {
                    var offsetX = gohanHouseStartPositionX + x;
                    var offsetY = gohanHouseStartPositionY + y;
                    var tile = Framing.GetTileSafely(offsetX, offsetY);
                    tile.slope(_gohanHouseSlopes[y, x]);
                    tile.halfBrick(false);
                }
            }
            // walls
            for (var x = 0; x < _gohanHouseWalls.GetLength(1); x++)
            {
                for (var y = 0; y < _gohanHouseWalls.GetLength(0); y++)
                {
                    var offsetX = gohanHouseStartPositionX + x;
                    var offsetY = gohanHouseStartPositionY + y;
                    var tile = Framing.GetTileSafely(offsetX, offsetY);
                    if (_gohanHouseWalls[y, x] == 0)
                        tile.wall = 0;
                    else if (_gohanHouseWalls[y, x] == 1)
                        tile.wall = WallID.Wood;
                    else if (_gohanHouseWalls[y, x] == 2)
                        tile.wall = WallID.LivingWood;
                    else if (_gohanHouseWalls[y, x] == 3)
                        tile.wall = WallID.Gray;
                    else if (_gohanHouseWalls[y, x] == 4) tile.wall = WallID.Glass;
                }
            }
            // Objects
            for (var x = 0; x < _gohanHouseObjects.GetLength(1); x++)
            {
                // house objects are different.. they go in reverse (ground up) so that the bottle placement actually works.
                for (var y = _gohanHouseObjects.GetLength(0) - 1; y >= 0; y--)
                {
                    var offsetX = gohanHouseStartPositionX + x;
                    var offsetY = gohanHouseStartPositionY + y;
                    var tile = Framing.GetTileSafely(offsetX, offsetY);
                    // break rocks!
                    if (tile.type == TileID.SmallPiles || tile.type == TileID.LargePiles || tile.type == TileID.LargePiles2 || tile.type == TileID.Dirt || tile.type == TileID.Stone)
                    {
                        // nullify tiles?
                        WorldGen.KillTile(offsetX, offsetY);
                        tile = Framing.GetTileSafely(offsetX, offsetY);
                    }

                    if (_gohanHouseObjects[y, x] == 0)
                    {
                    }
                    else if (_gohanHouseObjects[y, x] == 1)
                    {
                        WorldGen.PlaceObject(offsetX, offsetY, TileID.ClosedDoor, true, 28); // confirmed dynasty door
                    }
                    else if (_gohanHouseObjects[y, x] == 2)
                    {
                        WorldGen.PlaceObject(offsetX, offsetY, TileID.Tables, true, 25); // confirmed dynasty table
                    }
                    else if (_gohanHouseObjects[y, x] == 3)
                    {
                        WorldGen.PlaceObject(offsetX, offsetY, TileID.Bottles, true, 5); // confirmed dynasty cup
                    }
                    else if (_gohanHouseObjects[y, x] == 4)
                    {
                        WorldGen.PlaceObject(offsetX, offsetY, TileID.Chandeliers, true,
                            22); // confirmed large dynasty lantern
                        tile.color(28);
                    }
                    else if (_gohanHouseObjects[y, x] == 5)
                    {
                        WorldGen.PlaceObject(offsetX, offsetY, TileID.HangingLanterns, true,
                            26); // confirmed dynasty hanging lantern (small one)
                    }
                    else if (_gohanHouseObjects[y, x] == 6)
                    {
                        // WorldGen.PlaceObject(offsetX, offsetY, TileID.Dressers, true, 4); // confirmed shadewood dresser
                        WorldGen.PlaceChest(offsetX, offsetY, TileID.Dressers, false, 4);
                    }
                    else if (_gohanHouseObjects[y, x] == 7)
                    {
                        if (!isFirstPass)
                        {
                            TryPlacingDragonBall(4, offsetX, offsetY);
                        }
                    }
                }
            }

            // sample tiles at the origin (it's to the right, this isn't perfect)
            var sampleTile = Framing.GetTileSafely(gohanHouseStartPositionX, gohanHouseStartPositionY + 1);
            var isSnowBiome = sampleTile.type == TileID.SnowBlock || sampleTile.type == TileID.IceBlock;


            // experimental, also doesn't work when the tiles below are snow... which happens at spawn sometimes.
            // put dirt under the house and make sure gaps are filled. this might look weird.
            for (var y = 0; y < 5; y++)
            {
                for (var x = -1 - (y * 2); x < _gohanHouseTiles.GetLength(1) + 1 + (y * 2); x++)
                {
                    var offsetX = gohanHouseStartPositionX + x;
                    var offsetY = gohanHouseStartPositionY + _gohanHouseTiles.GetLength(0) + y;
                    var tile = Framing.GetTileSafely(offsetX, offsetY);
                    var isEdge = IsAnySideExposed(offsetX, offsetY);
                    tile.type = isSnowBiome ? TileID.SnowBlock : (isEdge ? TileID.Grass : TileID.Dirt);
                    // if it's a slope, remove slope. quit putting gaps in the ground terraria.
                    tile.slope(0);
                    tile.halfBrick(false);
                    tile.active(true);
                }
            }
        }

        private bool RunGohanCleanupRoutine(GenerationProgress progress)
        {
            // we already have the starting position, just cut straight to the build cleanup.
            const string gohanHouseGen = "Cleaning up Grandpa's House...";
            if (progress != null)
            {
                progress.Message = gohanHouseGen;
                progress.Set(0.50f);
            }

            GenerateGohanStructureWithByteArrays(false);
            return true;
        }

        public bool IsAnySideExposed(int startX, int startY)
        {
            for (var offX = -1; offX <= 1; offX++)
            {
                for (var offY = -1; offY <= 1; offY++)
                {
                    var tile = Framing.GetTileSafely(startX + offX, startY + offY);
                    if (!tile.active())
                        return true;
                }
            }
            return false;
        }
    }
}