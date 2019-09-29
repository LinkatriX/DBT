using WebmilioCommons.Networking.Packets;

namespace DBT.Players
{
    public class PlayerFlightStateChangedPacket : ModPlayerNetworkPacket<DBTPlayer>
    {
        public bool Flying
        {
            get => ModPlayer.Flying;
            set => ModPlayer.Flying = value;
        }
    }
}