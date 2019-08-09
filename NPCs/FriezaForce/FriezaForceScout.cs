using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DBT.NPCs.FriezaForce
{
    public class FriezaForceScout : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frieza Force Scout");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 52;
            npc.height = 71;
            npc.damage = 26;
            npc.defense = 12;
            npc.lifeMax = 180;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = 60f;
            npc.knockBackResist = 0.3f;
            npc.aiStyle = 3;
            aiType = NPCID.GoblinScout;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!DBTWorld.friezaShipTriggered && !NPC.AnyNPCs(mod.NPCType("FriezaForceScout")) && NPC.downedBoss2)
            {
                return spawnInfo.player.GetModPlayer<DBTPlayer>().zoneWasteland ? 10f : 0f;
            }
            else
            {
                return 0f;
            }

        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);
            if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) < 200 && npc.life < npc.lifeMax * 0.50)
            {
                Alerted = true;
            }
            if (Alerted)
            {
                //Thanks UncleDanny for this <3
                if (Main.player[npc.target].position.Y < npc.position.Y + 320)
                {
                    npc.velocity.Y -= npc.velocity.Y > 0f ? 1.2f : 0.20f;
                }
                if (Main.player[npc.target].position.Y > npc.position.Y + 320)
                {
                    npc.velocity.Y += npc.velocity.Y < 0f ? 1.2f : 0.20f;
                }
                
                npc.velocity.X = -4f * npc.direction;

            }
            else
            {
                npc.velocity.X = 1.4f * npc.direction;
            }
        }
        public override void PostAI()
        {
            if(npc.timeLeft == 1 && Alerted)
            {
                DBTWorld.friezaShipTriggered = true;
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
                
                if (Main.netMode != 2)
                {
                    Main.NewText("If I tell lord frieza of this then I'll surely get promoted...", Color.Red);
                    Main.NewText("The Frieza Force is coming...", Color.OrangeRed);
                }
                else
                {
                    NetworkText text2 = NetworkText.FromLiteral("If I tell lord frieza of this then I'll surely get promoted...");
                    NetworkText text3 = NetworkText.FromLiteral("The Frieza Force is coming...");
                    NetMessage.BroadcastChatMessage(text2, Color.Red);
                    NetMessage.BroadcastChatMessage(text3, Color.OrangeRed);
                }
            }
        }

        int frame = 0;

        public override void FindFrame(int frameHeight)
        {
            if(!Alerted)
            {
                frame = 0;
                npc.spriteDirection = npc.direction * -1;
            }
            else
            {
                npc.frameCounter += 1;
                if (npc.frameCounter > 4)
                {
                    frame++;
                    npc.frameCounter = 0;
                }
                if (frame > 3)
                {
                    frame = 1;
                }
                npc.spriteDirection = npc.direction;
            }
            
            npc.frame.Y = frameHeight * frame;
        }

        public int YHoverTimer { get; private set; }

        public bool Alerted { get; private set; }
    }
}
