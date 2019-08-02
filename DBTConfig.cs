using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

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
