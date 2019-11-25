using System.IO;
using DBT.Players;
using DBT.Transformations;
using Terraria.ModLoader;
using WebmilioCommons.Networking.Packets;

namespace DBT.Network.Transformations
{
    public class PlayerUntransformedPacket : ModPlayerNetworkPacket<DBTPlayer>
    {
        private TransformationDefinition _transformation;

        public PlayerUntransformedPacket()
        {
        }

        public PlayerUntransformedPacket(TransformationDefinition transformation)
        {
            _transformation = transformation;
        }


        protected override bool PostReceive(BinaryReader reader, int fromWho)
        {
            ModPlayer.Untransform(_transformation);

            return base.PostReceive(reader, fromWho);
        }


        public string Transformation
        {
            get => _transformation.UnlocalizedName;
            set => _transformation = TransformationDefinitionManager.Instance[value];
        }
    }
}