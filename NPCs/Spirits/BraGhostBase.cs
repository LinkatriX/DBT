using DBT.Projectiles.FriezaForce;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Spirits
{
	public class BraGhostBase : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bra");
			Main.npcFrameCount[npc.type] = 15;
		}

		public override void SetDefaults()
		{
			npc.width = 46;
			npc.height = 58;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 1;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.immortal = true;
            npc.noGravity = true;
		}

		public override void AI()
		{
			Player player = Main.player[npc.target];
			npc.TargetClosest(true);

            npc.ai[0]++;
            npc.ai[1]++;
            if (npc.velocity.Y == 0 || (npc.velocity.Y == 0.5f && npc.ai[0] == 20))
            {
                npc.velocity.Y -= 0.5f;
                npc.ai[0] = 0;
            }
            if (npc.velocity.Y == -0.5f && npc.ai[0] == 20)
            {
                npc.velocity.Y = 0.5f;
                npc.ai[0] = 0;
            }
		}

		int _frame = 0;
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 1;
			if (npc.frameCounter > 4)
			{
				_frame++;
				npc.frameCounter = 0;
			}
			if (_frame == 15)
			{
				_frame = 0;
			}
			npc.spriteDirection = npc.direction;
			npc.frame.Y = frameHeight * _frame;
		}

		public int shootTimer = 0;
		public int yHoverTimer = 0;
		public bool assignedTexture = false;
	}
}

