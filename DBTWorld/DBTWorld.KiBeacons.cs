using DBT.Players;
using DBT.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace DBT.DBTWorld
{
    public partial class DBTWorld
    {
        public List<Vector2> kiBeacons = new List<Vector2>();

        public override void ResetNearbyTileEffects()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>(mod);
            modPlayer.KiDiffuser = false;
        }

        public void CleanupKiBeaconList()
        {
            var listToRemove = new List<Vector2>();
            foreach (var location in kiBeacons)
            {
                var tile = Framing.GetTileSafely((int)location.X / 16, (int)location.Y / 16);
                if (tile.type == mod.TileType<KiBeaconTile>())
                    continue;
                listToRemove.Add(location);
            }
            kiBeacons = kiBeacons.Except(listToRemove).ToList();
        }

        private bool _kiBeaconCleanupCheck = false;
    }
}