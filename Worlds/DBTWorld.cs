using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria.ID;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using System;

namespace DBT.Worlds
{
    public partial class DBTWorld : ModWorld
    {
        public override void Load(TagCompound tag)
        {
            IList<string> downed = tag.GetList<string>("downed");
            downedFriezaShip = downed.Contains("friezaShip");

            kiBeacons = tag.ContainsKey("KiBeacons") ? (List<Vector2>)tag.GetList<Vector2>("KiBeacons") : new List<Vector2>();

            // cleanup ki beacon list, not sure why this is necessary.
            CleanupKiBeaconList();

            for (var i = 0; i < 7; i++)
            {
                var cacheKeyNameX = $"DragonBall{i + 1}LocationX";
                var cacheKeyNameY = $"DragonBall{i + 1}LocationY";
                if (tag.ContainsKey(cacheKeyNameX) && tag.ContainsKey(cacheKeyNameY))
                {
                    var dbX = tag.GetInt(cacheKeyNameX);
                    var dbY = tag.GetInt(cacheKeyNameY);
                    var dbLocation = new Point(dbX, dbY);
                    CacheDragonBallLocation(i + 1, dbLocation);
                }
            }
        }

        // helper utility method for snagging the currently loaded world.
        public static DBTWorld GetWorld()
        {
            return DBTMod.Instance.GetModWorld("DBTWorld") as DBTWorld;
        }

        public override void PostWorldGen()
        {
            base.PostWorldGen();
            _generateGohanHouse = false;
            _isGohanHouseCleaned = false;
            isDragonBallPlacementDone = false;
            isGohanHouseOffsetSet = false;
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            // useful debug tool type thing. Help me find the names of tasks to decide where to put this one.
            var taskNames = tasks.Select(x => x.Name).ToList();
            // I've tried injecting the task before "Piles" and "Spreading Grass", "Piles" can cause furniture interference.
            // Neither works. So far "Planting Trees" is the only one I can get to work.
            var index = tasks.FindIndex(x => x.Name == "Planting Trees");
            if (index != -1)
            {
                tasks.Insert(index, new PassLegacy("[DBT] Gohan House", AddGohanHouse));
                tasks.Insert(index, new PassLegacy("[DBT] Gravity Generator", AddGravGenerator));
            }

            // insert a cleanup task
            index = tasks.FindIndex(x => x.Name == "Micro Biomes");
            if (index != -1)
            {
                tasks.Insert(index, new PassLegacy("[DBT] Gohan House Validation", CleanupGohanHouse));
            }

            tasks.Insert(tasks.Count - 1, new PassLegacy("[DBT] Placing dragon balls", PlaceDragonBalls));
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedFriezaShip;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedFriezaShip = flags[0];
        }

        public void PlaceDragonBalls(GenerationProgress progress = null)
        {
            try
            {
                var success = AttemptToPlaceDragonBallsInWorld(progress);
                if (success)
                {
                    isDragonBallPlacementDone = true;
                }
            }
            catch (Exception exception)
            {
                Main.NewText("Oh no, an error happened [PlacingDragonBalls]! Report this to NuovaPrime or Skipping and send them the file Terraria/ModLoader/Logs/Logs.txt");
                ErrorLogger.Log(exception);
            }
        }

        public override void PreUpdate()
        {
            if (!_kiBeaconCleanupCheck)
            {
                _kiBeaconCleanupCheck = true;
            }
        }

        // handle retrograde cleanup immediately after the first update tick.
        private bool _initialized;
        public override void PostUpdate()
        {
            if (!_initialized)
            {
                _initialized = true;
                HandleRetrogradeCleanup();
            }
            else
            {
                // every 10 seconds, check dragon ball locations, but don't run the full cleanup detail.
                if (Main.netMode != NetmodeID.MultiplayerClient && DBTMod.IsTickRateElapsed(600))
                    CleanupAndRegenerateDragonBalls(false);
            }
        }
    }
}