using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;

namespace DBT.Worlds
{
    public partial class DBTWorld
    {
        public static bool downedFriezaShip = false;
        public static bool friezaShipTriggered = false;

        public override void Initialize()
        {
            downedFriezaShip = false;
        }

        public override TagCompound Save()
        {
            SaveDownedBools();
            SaveExtras();

            var dbtWorldTag = new TagCompound
            {
                {"KiBeacons", kiBeacons}
            };
            for (var i = 0; i < 7; i++)
            {
                var dbCache = GetCachedDragonBallLocation(i + 1);
                var cacheKeyNameX = $"DragonBall{i + 1}LocationX";
                var cacheKeyNameY = $"DragonBall{i + 1}LocationY";
                dbtWorldTag.Add(cacheKeyNameX, dbCache.X);
                dbtWorldTag.Add(cacheKeyNameY, dbCache.Y);
            }
            return dbtWorldTag;
        }

        public TagCompound SaveDownedBools()
        {
            List<string> downed = new List<string>();

            if (downedFriezaShip) downed.Add("friezaShip");

            return new TagCompound { { "downed", downed } };
        }

        public TagCompound SaveExtras()
        {
            List<string> tag = new List<string>();

            if (repairedGravModule) tag.Add("repairedGravModule");

            return new TagCompound { { "tag", tag } };
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();

            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedFriezaShip = flags[0];
            }
        }
    }
}