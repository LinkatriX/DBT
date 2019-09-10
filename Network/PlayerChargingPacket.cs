using System.IO;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Networking.Packets;

namespace DBT.Network
{
    public sealed class PlayerChargingPacket : ModPlayerNetworkPacket<DBTPlayer>
    {
        public bool IsCharging
        {
            get => ModPlayer.IsCharging;
            set => ModPlayer.IsCharging = value;
        }
    }
}
