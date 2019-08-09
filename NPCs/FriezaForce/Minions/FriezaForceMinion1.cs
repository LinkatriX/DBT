using DBT.Projectiles.FriezaForce;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.FriezaForce.Minions
{
	public class FriezaForceMinion1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frieza Force Henchman");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 52;
			npc.height = 71;
			npc.damage = 26;
			npc.defense = 2;
			npc.lifeMax = 120;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath3;
			npc.value = 60f;
			npc.knockBackResist = 0f;
			npc.aiStyle = 3;
		}

		public override void AI()
		{
			Player player = Main.player[npc.target];
			npc.TargetClosest(true);

			//Thanks UncleDanny for this <3
			if (Main.player[npc.target].position.Y < npc.position.Y + 260)
			{
				npc.velocity.Y -= npc.velocity.Y > 0f ? 2f : .5f;
			}
			if (Main.player[npc.target].position.Y > npc.position.Y + 260)
			{
				npc.velocity.Y += npc.velocity.Y < 0f ? 2f : .5f;
			}

			if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) > 100)
			{
				npc.velocity.X = 1.5f * npc.direction;
			}
			shootTimer++;
			if (shootTimer > 180)
			{
				Vector2 newVelocity = Vector2.Normalize(Main.player[npc.target].Center + Main.player[npc.target].velocity - npc.Center) * 18;
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, newVelocity.X, newVelocity.Y, mod.ProjectileType<FFMinionBlast>(), npc.damage / 3, 3f, Main.myPlayer);
				shootTimer = 0;
			}
		}

		int frame = 0;
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 1;
			if (npc.frameCounter > 4)
			{
				frame++;
				npc.frameCounter = 0;
			}
			if (frame > 2)
			{
				frame = 0;
			}
			npc.spriteDirection = npc.direction;
			npc.frame.Y = frameHeight * frame;
		}

		public int shootTimer = 0;
		public int YHoverTimer = 0;
		public bool assignedTexture = false;
	}
}

