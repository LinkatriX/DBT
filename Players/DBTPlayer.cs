using System;
using DBT.Network;
using DBT.Transformations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer : ModPlayer
    {
        public void OnKilledNPC(NPC npc)
        {
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerKilledNPC(this, npc));
            ForAllActiveTransformations(t => t.OnActivePlayerKilledNPC(this, npc));
        }

        public override void OnEnterWorld(Player player)
        {
            HandleTransformationsOnEnterWorld(player);
        }

        
        public bool PlayerInitialized { get; private set; }
        public float HealthDrainMultiplier { get; set; }
    }
}
