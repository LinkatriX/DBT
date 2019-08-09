using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.NPCs.Bosses.FriezaShip;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private List<NPC> _aliveBosses;

        public List<NPC> AliveBosses
        {
            get
            {
                if (_aliveBosses == null)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                        if (Main.npc[i].boss && Main.npc[i].active)
                            _aliveBosses.Add(Main.npc[i]);
                }

                return _aliveBosses;
            }
        }

        private void CheckFriezaShipSpawn()
        {
            int spawnTimer = 0;
            spawnTimer++;
            if (spawnTimer == 5400)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<FriezaShip>());
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
                DBTWorld.friezaShipTriggered = false;
            }
        }
    }
}