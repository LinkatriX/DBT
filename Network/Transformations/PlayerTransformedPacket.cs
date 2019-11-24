using System.IO;
using DBT.Players;
using DBT.Transformations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Networking.Packets;

namespace DBT.Network.Transformations
{
    public sealed class PlayerTransformedPacket : ModPlayerNetworkPacket<DBTPlayer>
    {
        private TransformationDefinition _transformation;

        public PlayerTransformedPacket()
        {
        }

        public PlayerTransformedPacket(TransformationDefinition transformation)
        {
            _transformation = transformation;
        }


        protected override bool PostReceive(BinaryReader reader, int fromWho)
        {
            ModPlayer.AcquireAndTransform(_transformation);

            return base.PostReceive(reader, fromWho);
        }


        public string Transformation
        {
            get => _transformation.UnlocalizedName;
            set => _transformation = TransformationDefinitionManager.Instance[value];
        }
    }
}
