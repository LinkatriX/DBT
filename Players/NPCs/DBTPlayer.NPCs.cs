using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.NPCs.Bosses.FriezaShip;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WebmilioCommons.Tinq;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private List<NPC> _aliveBosses;

        private void UpdateNPCs()
        {
            if (FriendshipCooldown > 0)
                FriendshipCooldown--;
        }

        // The ?? operator returns the first value if its not null, otherwise the second. The '=' following assigns the right hand value to the left if the left is null.
        public List<NPC> AliveBosses => _aliveBosses ??= Main.npc.WhereActive(n => n.boss);

        public Dictionary<int, int> AliveTownNPCs { get; set; } = new Dictionary<int, int>();
        public int FriendshipCooldown { get; set; } = 0;

        private void CheckFriezaShipSpawn()
        {
            int spawnTimer = 0;
            spawnTimer++;
            if (spawnTimer == 5400)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<FriezaShip>());
                Main.PlaySound(SoundID.Roar, player.position, 0);
                Main.NewText("The Frieza Force has arrived!", Color.OrangeRed);
                if (Main.netMode != 2)
                {
                    Main.NewText("The Frieza Force has arrived!", Color.OrangeRed);
                }
                else
                {
                    NetworkText text3 = NetworkText.FromLiteral("The Frieza Force has arrived!");
                    NetMessage.BroadcastChatMessage(text3, Color.OrangeRed);
                }
                Worlds.DBTWorld.friezaShipTriggered = false;
            }
        }
    }
}