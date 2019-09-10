using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;

namespace DBT.Worlds
{
    public partial class DBTWorld
    {
        public bool TryPlacingDragonBall(int whichDragonBall, int offsetX, int offsetY)
        {
            // dragon ball already exists, bail out.
            if (IsExistingDragonBall(whichDragonBall))
                return true;
            TileObject tileObjectOut;
            int dragonBallType = GetDbType(whichDragonBall);
            if (!TileObject.CanPlace(offsetX, offsetY, dragonBallType, 0, -1, out tileObjectOut, true,
                false)) return false;

            CacheDragonBallLocation(whichDragonBall, new Point(offsetX, offsetY));

            WorldGen.PlaceObject(offsetX, offsetY, dragonBallType, true);
            NetMessage.SendTileSquare(-1, offsetX, offsetY, 1, TileChangeType.None);
            if (Main.netMode == NetmodeID.Server)
            {
                //NetworkHelper.playerSync.SendAllDragonBallLocations();
            }

            return true;
        }

        public bool AttemptToPlaceDragonBallsInWorld(GenerationProgress progress = null)
        {
            const string placingDragonBalls = "Placing Dragon Balls";
            if (progress != null)
            {
                progress.Message = placingDragonBalls;
                progress.Set(0.25f);
            }

            for (var i = 0; i < 7; i++)
            {
                TryPlacingDragonBall(i + 1);
            }
            return true;
        }

        public void TryPlacingDragonBall(int whichDragonBall)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;
            var isAttemptingToSpawnDragonBall = !IsExistingDragonBall(whichDragonBall);

            if (!isAttemptingToSpawnDragonBall) return;
            var safeCoordinates = GetSafeDragonBallCoordinates();
            while (!TryPlacingDragonBall(whichDragonBall, safeCoordinates.X, safeCoordinates.Y))
            {
                safeCoordinates = GetSafeDragonBallCoordinates();
            }
        }

        public void HandleRetrogradeCleanup(Player ignorePlayer = null)
        {
            // only fire this server side or single player.
            if (Main.netMode != NetmodeID.MultiplayerClient)
                CleanupAndRegenerateDragonBalls(true);
        }

        public void CleanupAndRegenerateDragonBalls(bool isCleanupNeeded)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;
            Point[] dragonBallFirstFound = Enumerable.Repeat(Point.Zero, 7).ToArray();

            if (isCleanupNeeded)
            {
                // DebugHelper.Log("Server is running cleanup routine.");
                // destroy all but the first located dragon ball of any given type in the world, with no items.
                for (var i = 0; i < Main.maxTilesX; i++)
                {
                    for (var j = 0; j < Main.maxTilesY; j++)
                    {
                        var tile = Main.tile[i, j];
                        // var tile = Framing.GetTileSafely(i, j);
                        if (tile == null)
                            continue;
                        if (!tile.active())
                            continue;
                        var dbNum = GetDragonBallNumberFromType(tile.type);
                        if (dbNum == 0)
                            continue;
                        var thisDragonBallLocation = new Point(i, j);
                        if (dragonBallFirstFound[dbNum - 1].Equals(Point.Zero))
                        {
                            dragonBallFirstFound[dbNum - 1] = thisDragonBallLocation;
                        }
                        else
                        {
                            WorldGen.KillTile(i, j, false, false, true);
                        }
                    }
                }
            }
            else
            {
                dragonBallFirstFound = CachedDragonBallLocations;
                // DebugHelper.Log("Server is running dragon ball confirmation routine.");
            }

            // figure out if new dragon balls need to be spawned (are any missing?)
            for (var i = 0; i < dragonBallFirstFound.Length; i++)
            {
                // check that the found locations are still valid
                Point testLocation = dragonBallFirstFound[i];
                if (!IsDragonBallLocation(testLocation.X, testLocation.Y))
                {
                    // DebugHelper.Log($"Server thinks dragon ball {i + 1} is missing.");
                    // if this isn't a dragon ball, erase it.
                    dragonBallFirstFound[i] = Point.Zero;
                    CacheDragonBallLocation(i + 1, Point.Zero);
                }

                if (dragonBallFirstFound[i].Equals(Point.Zero))
                {
                    TryPlacingDragonBall(i + 1);
                }
            }
        }

        public string GetDragonBallNumberName(int whichDragonBall)
        {
            switch (whichDragonBall)
            {
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                default:
                    return string.Empty;
            }
        }

        public bool IsExistingDragonBall(int whichDragonBall)
        {
            var existingLocation = GetCachedDragonBallLocation(whichDragonBall);
            if (existingLocation.Equals(Point.Zero))
                return false;
            return true;
        }

        public Point[] CachedDragonBallLocations { get; } = Enumerable.Repeat(Point.Zero, 7).ToArray();

        public Point GetCachedDragonBallLocation(int whichDragonBall)
        {
            return CachedDragonBallLocations[whichDragonBall - 1];
        }

        public void CacheDragonBallLocation(int whichDragonBall, Point location)
        {
            CachedDragonBallLocations[whichDragonBall - 1] = location;
        }

        public bool IsDragonBallLocation(int i, int j)
        {
            var tile = Main.tile[i, j];
            if (tile == null)
                return false;
            if (!tile.active())
                return false;
            for (var d = 0; d < 7; d++)
            {
                bool isDragonBall = GetDbType(d + 1) == tile.type;
                if (isDragonBall)
                    return true;
            }
            return false;
        }

        public int?[] dbTypes = Enumerable.Repeat((int?)null, 7).ToArray();

        public int GetDbType(int whichDragonBall)
        {
            if (dbTypes[whichDragonBall - 1].HasValue)
                return dbTypes[whichDragonBall - 1].Value;
            var dragonBallWord = GetDragonBallNumberName(whichDragonBall);
            dbTypes[whichDragonBall - 1] = DBTMod.Instance?.TileType($"{dragonBallWord}StarDragonBallTile");
            if (dbTypes[whichDragonBall - 1].HasValue)
                return dbTypes[whichDragonBall - 1].Value;
            return 0;
        }

        public int GetDragonBallNumberFromType(int type)
        {
            for (int i = 1; i <= 7; i++)
            {
                if (type == GetDbType(i))
                {
                    return i;
                }
            }

            return 0;
        }

        // the following walls are *natural* [not placed] dungeon walls and the lihzard temple wall, respectively.
        // these prevent the dragon ball from spawning.
        private static readonly int[] _invalidDragonBallWalls = new int[] { 7, 9, 94, 95, 96, 97, 98, 99, 87 };
        public static bool IsInvalidTileForDragonBallPlacement(Tile tile)
        {
            // quick wall check
            if (_invalidDragonBallWalls.Contains(tile.wall))
                return false;
            return tile.type == TileID.SmallPiles
                || tile.type == TileID.LargePiles
                || tile.type == TileID.LargePiles2
                || tile.type == TileID.Containers
                || tile.type == TileID.Containers2
                || tile.type == TileID.FakeContainers
                || tile.type == TileID.FakeContainers;
        }

        public static Point GetSafeDragonBallCoordinates()
        {
            var underworldHeight = Main.maxTilesY - 220;

            var surfaceHeight = (int)Math.Floor(Main.worldSurface * 0.30f);

            while (true)
            {
                int thresholdX = (int)Math.Floor(Main.maxTilesX * 0.1f);
                var randX = Main.rand.Next(thresholdX, Main.maxTilesX - thresholdX);
                var randY = Main.rand.Next(surfaceHeight, underworldHeight);
                var tile = Framing.GetTileSafely(randX, randY);
                if (IsInvalidTileForDragonBallPlacement(tile))
                {
                    continue;
                }
                var tileAbove = Framing.GetTileSafely(randX, randY - 1);
                if (tile.active() && (Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type]) && !tileAbove.active())
                {
                    return new Point(randX, randY - 1);
                }
            }
        }
    }
}