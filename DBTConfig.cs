using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DBT
{
    public class DBTConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Friendship Gain Cooldown")]
        [Tooltip("Configure the cooldown on gaining friendship with npcs.")]
        [DefaultValue(30)]
        public int FriendshipCooldownConfig { get; set; }


    }
    public class DBTConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

    }
}
