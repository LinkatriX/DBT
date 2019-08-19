using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.NPCs.Saibamen;
using DBT.Helpers;
using DBT.Items.Accessories.ArmCannons;
using DBT.Items.Bags;
using DBT.Items.Materials;
using DBT.Items.Weapons;
using DBT.NPCs.FriezaForce.Minions;
using DBT.Projectiles;
using DBT.Projectiles.FriezaForce;
using System.Linq;

namespace DBT.NPCs.Bosses.FriezaShip
{
	[AutoloadBossHead] //TODO: Work on teleportation dust.
	public class FriezaShip : ModNPC
	{
		public FriezaShip()
		{
			HoverDistance = new Vector2(0, 380);
			HyperPosition = new Vector2(0, 0);
			HoverCooldown = 400;
			XHoverTimer = 0;
			YHoverTimer = 0;
			SSDone = 0;
			DustScaleTimer = 0;
			SSDelay = 20;
			SlamCounter = 0;
			SlamBarrageSpin = 120;
			Random = 0;
			SelectHoverMP = 0;
			IterationCount = 0;
			MinionCount = 2;
			HyperSlamsDone = 0;
			ShieldDuration = 200;
			GalaxyDistance = (float)8.5 * 16f;
			GalaxyDistance2 = (float)8.5 * 16f;
			HyperSlamSpeed = -40f;
		}

		#region Stages Numbers.

		public const int
			STAGE_HOVER = 0,
			STAGE_SLAM = 1,
			STAGE_SHIELD = 2,
			STAGE_MINION = 3,
			STAGE_HYPER = 4,
			STAGE_WARP = 5,
			STAGE_GUNNING = 6,

			AI_STAGE_SLOT = 0,
			AI_TIMER_SLOT = 1;

		#endregion

		#region Defaults

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("A Frieza Force Ship");
			Main.npcFrameCount[npc.type] = 8;
			NPCID.Sets.TrailingMode[npc.type] = 2;
			NPCID.Sets.TrailCacheLength[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 220;
			npc.height = 120;
			npc.damage = 40;
			npc.defense = 8;
			npc.lifeMax = 3200;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = Item.buyPrice(0, 3, 25, 80);
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival");
			bossBag = mod.ItemType<FFShipBag>();
		}

		#endregion

		#region Drawing + Animation

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (AIStage == STAGE_SLAM)
			{
				float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
				Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width / 2,
					Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
				for (int k = 0; k < npc.oldPos.Length; k++)
				{
					Vector2 drawPos = new Vector2(
						npc.position.X - Main.screenPosition.X + npc.width / 2 -
						(float)Main.npcTexture[npc.type].Width * npc.scale / 2f + drawOrigin.X * npc.scale,
						npc.position.Y - Main.screenPosition.Y + npc.height -
						Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY +
						drawOrigin.Y * npc.scale + npc.gfxOffY);

					Color color = npc.GetAlpha(lightColor) *
								  ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
					spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin,
						npc.scale, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			if (AIStage == STAGE_SHIELD)
			{
				Texture2D texture = ModContent.GetTexture("DBT/NPCs/Bosses/FriezaShip/FFShield");

				ShieldFrame += .5;

				if (ShieldFrame >= 8)
					ShieldFrame = 0;

				int frameHeight = texture.Height / 8;

				Vector2 drawPos = npc.TopLeft - Main.screenPosition;
				Vector2 drawCenter = new Vector2(29f, 30f);

				Rectangle sourceRectangle = new Rectangle(0, frameHeight * (int)ShieldFrame, texture.Width, frameHeight);
				spriteBatch.Draw(texture, drawPos, sourceRectangle, Color.White, 0f, drawCenter, 1f, SpriteEffects.None, 0f);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (AIStage == STAGE_SHIELD)
				return new Color(30, 155, 40, 255);


			return new Color(lightColor.R, lightColor.G, lightColor.B, lightColor.A);
		}

		int ColorCount = 0;

		int _frame = 0;
		int _frameTimer = 0;
		int _frameRate = 1;

		public override void FindFrame(int frameHeight) //Spinning before the slam
		{
			if (AIStage == STAGE_SLAM && AITimer <= 180 || AIStage == STAGE_HYPER && AITimer < 300)
			{
				npc.frameCounter += _frameRate;
				_frameTimer++;
				if (_frameTimer > 30)
				{
					_frameRate = 2;
					if (_frameTimer > 50)
					{
						_frameRate = 3;
						if (_frameTimer > 80)
							_frameRate = 4;
					}
				}
			}
			else
			{
				npc.frameCounter++;
				_frameRate = 1;
				_frameTimer = 0;
			}


			if (npc.frameCounter > 4)
			{
				_frame++;
				npc.frameCounter = 0;
			}

			if (_frame > 7) //Make it 7 because 0 is counted as a frame, making it 8 frames. Genius.com
				_frame = 0;

			npc.frame.Y = frameHeight * _frame;
		}

		#endregion

		public override void AI()
		{
			AITimer++;

			if ((Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server) && SelectHoverMP == 1)
			{
				Random = Main.rand.Next(PlayerCount().Count);
				SelectHoverMP++;
			}

			Player player = Main.player[Random];

			#region Difficulty modification

			if (UnderEightyHealth)
			{
				SpeedAdd = 1f;
				npc.damage = 40;
				SSDelay = 15;
				ShieldDuration = 220;
				HoverCooldown = 350;
				HyperSlamSpeed = -30f;
			}

			if (UnderFiftyHealth)
			{
				MinionCount = 3;
				SpeedAdd = 2f;
				npc.damage = 50;
				ShieldDuration = 230;
				HoverCooldown = 300;
				HyperSlamSpeed = -40f;
			}

			if (UnderThirtyHealth)
			{
				MinionCount = 4;
				SpeedAdd = 4f;
				npc.damage = 55;
				SSDelay = 10;
				ShieldDuration = 240;
				ShieldLife = 2;
				HoverCooldown = 200;
				HyperSlamSpeed = -50f;
			}

			if (Main.expertMode && Under10Health)
			{
				MinionCount = 6;
				npc.damage = 60;
				SSDelay = 8;
				ShieldDuration = 250;
				ShieldLife = 3;
				HoverCooldown = 120;
				HyperSlamSpeed = -60f;
			}

			#endregion

			#region DisappearanceManager

			npc.TargetClosest(true);

			if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				if (!player.active || player.dead)
				{
					npc.velocity = new Vector2(0f, -10f);

					if (npc.timeLeft > 10)
						npc.timeLeft = 10;

					return;
				}
			}

			#endregion

			#region Fail prevention

			if (AIStage > 6)
				AIStage = STAGE_HOVER;

			if (AITimer > 550)
				AITimer = 0;

			if (SSDone > 9)
				SSDone = 3;

			#endregion

			#region Hovering

			if (AIStage == STAGE_HOVER)
			{
				npc.dontTakeDamage = false;
				//Y Hovering

				if (Main.player[npc.target].position.Y != npc.position.Y + HoverDistance.Y)
				{
					YHoverTimer++;

					if (YHoverTimer > 10)
					{
						//Thanks UncleDanny and Thorium  team for this <3
						if (Main.player[npc.target].position.Y < npc.position.Y + HoverDistance.Y)
							npc.velocity.Y -= npc.velocity.Y > 0f ? 1f : 0.15f;

						if (Main.player[npc.target].position.Y > npc.position.Y + HoverDistance.Y)
							npc.velocity.Y += npc.velocity.Y < 0f ? 1f : 0.15f;
					}
				}
				else
				{
					npc.velocity.Y = 0;
					YHoverTimer = 0;
				}

				if (Vector2.Distance(new Vector2(player.Center.X, 0), new Vector2(npc.Center.X, 0)) != HoverDistance.X)
				{
					XHoverTimer++;
					if (XHoverTimer > 30)
					{
						npc.velocity.X = 2.5f * npc.direction + SpeedAdd * npc.direction;
					}
				}
				else
				{
					npc.velocity.X = 0;
					XHoverTimer = 0;
				}

				if (AITimer >= HoverCooldown)
				{
					AdvanceStage(true);
					ResetValues(false);
					SelectHoverMP = 2;
				}

				npc.netUpdate = true;
			}

			#endregion // Starts at 0 ticks.

			#region Slam || MP compatible

			if (AIStage == STAGE_SLAM)
			{
				Slam();
			}

			#endregion

			#region Shield || Regen stage.

			if (AIStage == STAGE_SHIELD)
			{
				if (AITimer > 0)
				{
					if (AITimer == 1)
						SoundHelper.PlayCustomSound("Sounds/ShipShield", npc.Center, 2.5f);
					if (AITimer % 20 == 0)
						RandomShieldLines();

					npc.velocity.Y = -1f;
					npc.netUpdate = true;

					if (Vector2.Distance(player.Center, npc.Center) <= ShieldDistance + 2 * 16f)
					{
						player.Hurt(
							PlayerDeathReason.ByCustomReason(
								player.name + "has been cut in half by the Frieza Force Shield"), 40, 1);
						Dust dust = Main.dust[
							Dust.NewDust(player.Center, player.width, player.height, 56)]; //need to pick a new dust
						dust.noGravity = true;

						if (player.Center.X > npc.Center.X && player.Center.Y < npc.Center.Y) //4th qudrant
							player.velocity = new Vector2(16f, 16f);
						else if (player.Center.X < npc.Center.X && player.Center.Y < npc.Center.Y) //3rd quadrant
							player.velocity = new Vector2(-16f, 16f);
						else if (player.Center.X < npc.Center.X && player.Center.Y > npc.Center.Y) //2nd quadrant
							player.velocity = new Vector2(-16f, -16f);
						else if (player.Center.X > npc.Center.X && player.Center.Y > npc.Center.Y) //1st quadrant
							player.velocity = new Vector2(16f, -16f);
					}

					for (int i = 0; i <= Main.maxProjectiles; i++)
					{
						Projectile projectile = Main.projectile[i];

						if (Vector2.Distance(projectile.Center, npc.Center) <= ShieldDistance && projectile.active)
						{
							projectile.velocity *= -1f;

							for (int j = 0; j < 20; j++)
							{
								Dust dust = Main.dust[
									Dust.NewDust(projectile.position, projectile.width, projectile.height, 211)];
								dust.noGravity = true;
							}

							projectile.hostile = true;
							projectile.friendly = false;

							if (projectile.Center.X > player.Center.X * 0.5f
							) //Reference goes to Fargo for voring his epic code.
							{
								projectile.direction = 1;
								projectile.spriteDirection = 1;
							}
							else
							{
								projectile.direction = -1;
								projectile.spriteDirection = -1;
							}

							projectile.netUpdate = true;
							npc.netUpdate = true;
						}
						else if (Vector2.Distance(projectile.position, npc.Center) <= 6 * 16f)
						{
							projectile.Kill();
							Dust dust = Main.dust[
								Dust.NewDust(projectile.position, projectile.width, projectile.height, 211)];
							dust.noGravity = true;
						}
					}
				}

				if (npc.life <= npc.lifeMax)
					npc.life += ShieldLife;
				npc.netUpdate = true;

				if (AITimer >= ShieldDuration)
				{
					AdvanceStage(true);
					ResetValues(false);
				}
			}

			#endregion

			#region Minions

			if (AIStage == STAGE_MINION)
			{
				npc.velocity = new Vector2(0, -2f);
				if (AITimer == 0)
				{
					TileX = (int)(npc.position.X + Main.rand.NextFloat(-7f * 16, 6f * 16));
					TileY = (int)(npc.position.Y + Main.rand.NextFloat(-7f * 16, 6f * 16));
					SummonFFMinions();
					SummonSaibamen();
				}

				npc.netUpdate = true;

				if (AITimer == 60)
				{
					ResetValues(false);
					AdvanceStage(true);
				}
			}

			#endregion

			#region Hyper Stage

			if (AIStage == STAGE_HYPER)
			{
				npc.noTileCollide = true;

				if (HyperSlamsDone <= 4)
				{
					npc.dontTakeDamage = true;
					if (AITimer < 150)
					{
						DoChargeDust();
						npc.dontTakeDamage = true;
						npc.velocity = new Vector2(0, -0.3f);
						npc.netUpdate = true;
					}

					if (AITimer > 150 && HyperPosition == Vector2.Zero)
					{
						npc.dontTakeDamage = false;
						npc.velocity = Vector2.Zero;
						if (AITimer == 151)
						{
							CircularDust(30, npc, 133, 10f, 1);
							ChooseHyperPosition();
						}

						npc.netUpdate = true;

					}

					if (AITimer > 170 && HyperPosition != Vector2.Zero && npc.velocity == Vector2.Zero)
					{
						npc.dontTakeDamage = false;
						DoLineDust();
						npc.netUpdate = true;
					}

					if (AITimer == 160 && HyperPosition != Vector2.Zero)
					{
						npc.dontTakeDamage = false;
						TeleportRight();
						HyperSlamsDone++;
						npc.netUpdate = true;
					}

					if (AITimer >= 190 && HyperPosition != Vector2.Zero)
					{
						HorizontalSlam();
						Cache = 5;
					}
					npc.netUpdate = true;
				}
				else
				{
					HorizontalSlamTimer = 0;
					npc.dontTakeDamage = false;
					HyperSlamsDone = 0;
					AITimer = 0;
					ResetStage();
					npc.noTileCollide = false;
				}
			}

			#endregion

			#region Warp
			DoWarpSequence();
			#endregion

			#region Gunning
			PerformGunningSequence();
			#endregion

			#region Debugging Tools
			//Main.NewText("AiTimer is:" + AITimer, 255, 20, 20);
			//Main.NewText("Current stage is: " + AIStage, 20, 255, 20);
			//Main.NewText("SSDone: " + SSDone, 20, 20, 255);
			//Main.NewText("Player Count:" + PlayerCount().Count);
			//Main.NewText("");
			#endregion
		}

		#region Gunning Stage

		int count = 0;

		private void PerformGunningSequence() //Swoop in once on every player in the server.
		{
			if (AIStage == STAGE_GUNNING)
			{
				npc.noTileCollide = true;

				if (AITimer < 80)
				{
					DoChargeDust();
					npc.dontTakeDamage = true;
					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}
				if (AITimer == 80)
					TeleportToTheRightG();
				if (AITimer == 81)
					npc.velocity.X = 15f;
				if (AITimer == 220)
				{
					count++;
					npc.velocity = Vector2.Zero;
				}
				if (AITimer > 81 && AITimer < 230 && AITimer % 12 == 0)
					Projectile.NewProjectile(new Vector2(npc.BottomRight.X + 4f, npc.BottomRight.Y + 2 * 16f), new Vector2(13f, 13f).RotatedBy(50), mod.ProjectileType<FFShipGunningBlast>(), 20, 1f);
				if (count == PlayerCount().Count)
				{
					AdvanceStage(true);
					ResetValues(false);
					count = 0;
					npc.noTileCollide = false;
				}
			}
		}

		private void TeleportToTheRightG()
		{
			Player player = PlayerCount()[Random];

			npc.position = new Vector2(player.Center.X - 80 * 16f, player.Center.Y - 30 * 16f);

			Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(), 0, 0);
			SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
		}

		#endregion

		#region Warp Mechanics

		int warpCount = 0;
		int warpCountPlayer = 0;

		private void DoWarpSequence()
		{
			if (AIStage == STAGE_WARP)
			{
				npc.damage = 0;//damage dealt by Hurt() for nicer death message.

				if (AITimer < 100)
				{
					DecoDust(GalaxyDistance / 100);
					npc.dontTakeDamage = true;
					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}
				else if (AITimer == 100)
					Teleport();
				else if (AITimer == 101)
					DoSlamPerWarp();
				else if (AITimer == 115)
				{
					AITimer = 90;
					npc.noTileCollide = false;
					npc.netUpdate = true;
				}

				if (warpCount == PlayerCount().Count)
				{
					AdvanceStage(true);
					warpCount = 0;
					warpCountPlayer = 0;
					ResetValues(false);
					npc.damage = 50;
				}
			}
		}

		private void Teleport()
		{
			npc.position = GetWarpPositions(warpCountPlayer);
			Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(), 0, 0);
			SoundHelper.PlayCustomSound("Sounds/ShipTeleport");

			npc.netUpdate = true;
		}

		private void DoSlamPerWarp()
		{
			npc.noTileCollide = true;

			float velocity = 25f;

			Player player = PlayerCount()[warpCount];

			if (warpCountPlayer == 0)
			{
				npc.velocity.X = velocity;
				player.velocity = new Vector2(velocity * 3, -velocity / 2);
			}
			else if (warpCountPlayer == 1)
			{
				npc.velocity.X = -velocity;
				player.velocity = new Vector2(velocity * -3, -velocity / 2);
			}
			else if (warpCountPlayer == 2)
			{
				npc.velocity.Y = velocity;
				player.velocity = new Vector2(0f, velocity * 10);
				warpCountPlayer = 0;
				npc.velocity = Vector2.Zero;
				ExplodeEffect(player.Center);
				warpCount++;
			}

			player.Hurt(
			PlayerDeathReason.ByCustomReason(
			player.name + "has been flattened by the Frieza Force Ship"), 70, 1);
			ExplodeEffect(new Vector2(npc.Center.X, npc.Center.Y));
			SoundHelper.PlayCustomSound("Sounds/KiExplosion", npc.Center);
			npc.netUpdate = true;

			warpCountPlayer++;
		}

		private Vector2 GetWarpPositions(int fromWhere)
		{
			Player player = PlayerCount()[warpCount]; //Decide what player to Aim for;

			if (fromWhere == 0)
				return new Vector2(player.Center.X - 5 * 16f, player.Center.Y);
			else if (fromWhere == 1)
				return new Vector2(player.Center.X + 5 * 16f, player.Center.Y);
			else
				return new Vector2(player.Center.X, player.Center.Y - 5 * 16f);
		}
		#endregion

		#region Hyper Methods

		public void DoChargeDust()
		{
			if (Main.rand.NextFloat() < 5f)
			{
				Dust dust;
				Vector2 position = npc.position + new Vector2(10, 5);

				for (int i = 0; i < 10; i++)
				{
					dust = Main.dust[
						Dust.NewDust(position, 220, 120, 133, 0f, 0f, 0, new Color(255, 255, 255), 0.9236842f)];
					dust.noGravity = true;
				}
			}
		}

		public void ChooseHyperPosition()
		{
			Player targetPlayer = Main.player[Main.rand.Next(PlayerCount().Count)];
			HyperPosition = new Vector2(targetPlayer.Center.X + 500, targetPlayer.Center.Y + Main.rand.Next(10, 20));
			npc.netUpdate = true;
		}

		public void DoLineDust()
		{
			if (Main.rand.NextFloat() < 1.2f)
			{
				Dust dust;
				dust = Dust.NewDustPerfect(HyperPosition, 133, new Vector2(HyperSlamSpeed * 2f, 0), 0, new Color(255, 255, 255),
					1.052632f);
				dust.noGravity = true;
			}

			npc.netUpdate = true;
		}

		public void TeleportRight()
		{
			npc.position = HyperPosition + new Vector2(0, -4 * 16f);
			CircularDust(30, npc, 133, 10f, 1);
			npc.netUpdate = true;
		}

		private int HorizontalSlamTimer = 0;

		public void HorizontalSlam()
		{
			DoChargeDust();
			HorizontalSlamTimer++;
			npc.velocity = new Vector2(HyperSlamSpeed, 0f);

			if (HorizontalSlamTimer == 30)
			{
				npc.velocity = Vector2.Zero;
				npc.netUpdate = true;
				AITimer = 150;
				CircularDust(30, npc, 133, 10f, 1);
				HorizontalSlamTimer = 0;
				HyperPosition = Vector2.Zero;
			}

			npc.netUpdate = true;
		}

		#endregion

		#region Main Methods

		private void Slam()
		{
			if (IterationCount < PlayerCount().Count)
			{
				if (AITimer < 130)
				{
					DustScaleTimer++;
					npc.velocity = Vector2.Zero;
					CircularDust(10, npc, 133, 10f - DustScaleTimer / 20, 1);
				}
				else if (AITimer == 130)
				{
					TeleportAbove();
				}
				else if (AITimer > 130)
				{
					SlamCounter++;

					if (SlamCounter == SSDelay)
					{
						DoSlam();
						npc.netUpdate = true;
					}
					else if (SlamCounter == SSDelay + 13)
					{
						ExplodeEffect(new Vector2(npc.Center.X, npc.Center.Y));
						SoundHelper.PlayCustomSound("Sounds/KiExplosion", npc.Center);

						npc.velocity.Y = -8f;
						IterationCount++;
						Cache = 5;
						npc.netUpdate = true;
					}
					else if (SlamCounter == SSDelay + 60 && (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server))
					{
						ResetValues(false);
					}
				}
			}
			else if (IterationCount == PlayerCount().Count)
			{
				AdvanceStage(true);
				ResetValues(true);
			}
		}

		private void TeleportAbove()
		{
			Player player = PlayerCount()[IterationCount];
			Vector2 pos = player.Center + new Vector2(-100f + (player.velocity.X * 10), -HoverDistance.Y);
			npc.position = pos;
			Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(), 0, 0);
			SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
			npc.netUpdate = true;
		}

		public void DoSlam()
		{
			npc.velocity.Y = 25f;

			SSDone++;

			npc.netUpdate = true;
		}

		public int SummonSaibamen()
		{
			for (int i = 0; i <= MinionCount / 2; i++)
			{
				npc.netUpdate = true;

				switch (Main.rand.Next(0, 3))
				{
					case 0:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman1>());
					case 1:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman2>());
					case 2:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman3>());
					case 3:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman4>());
					default:
						return 0;
				}
			}

			return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman1>());
		}

		public int SummonFFMinions()
		{
			/*if (Collision.SolidCollision(new Vector2(TileX, TileY), 26, 36) && Main.tile[TileX, TileY].wall == 87)
                {
                    TileVariablesDefinition();
                }*/

			for (int i = 0; i <= MinionCount / 2; i++)
			{
				npc.netUpdate = true;
				switch (Main.rand.Next(0, 2))
				{
					case 0:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<FriezaForceMinion1>());
					case 1:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<FriezaForceMinion2>());
					case 2:
						return NPC.NewNPC(TileX, TileY, mod.NPCType<FriezaForceMinion3>());
					default:
						return 0;
				}
			}

			return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion1>());
		}

		private void RandomShieldLines()
		{
			float xStart = npc.Center.X + Main.rand.NextFloat(-8 * 16f, 8 * 16f);
			float yStart = npc.Center.Y + Main.rand.NextFloat(-8 * 16f, 8 * 16f);

			Dust dust = Main.dust[Dust.NewDust(new Vector2(xStart, yStart), 16, 16, 56)];
			dust.noGravity = true;
			dust.velocity = npc.velocity + new Vector2(0, 5 * 16f);
		}

		private void ResetValues(bool resetiter) //Does not require to reset the timer if anything consists of AdvanceStage(true).
		{
			SelectHoverMP = 0;
			DustScaleTimer = 0;
			SlamCounter = 0;
			AITimer = 0;
			GalaxyDistance = (float)8.5 * 16f;
			GalaxyDistance2 = (float)8.5 * 16f;
			GCounter = 0;
			if (resetiter)
				IterationCount = 0;
			npc.netUpdate = true;
			npc.noTileCollide = false;
		}

		private void ResetStage()
		{
			AIStage = STAGE_HOVER;
			SelectHoverMP = 0;
			AITimer = 0;
			npc.netUpdate = true;
		}

		private void AdvanceStage(bool resetTimer)
		{
			if (resetTimer)
				AITimer = 0;

			if (AIStage == STAGE_HOVER)
			{
				AIStage++;
				return;
			}
			else if (Main.expertMode && AIStage == STAGE_SLAM && (SSDone == 2 || SSDone == 5))
			{
				AIStage = STAGE_GUNNING;
				return;
			}
			else if (AIStage == STAGE_GUNNING)
			{
				AIStage = STAGE_SHIELD;
				return;
			}
			else if (AIStage == STAGE_SHIELD && (SSDone == 2 || SSDone == 5))
            {
				AIStage = STAGE_MINION;
				return;
			}
			else if (AIStage == STAGE_MINION && (SSDone == 2 || SSDone == 5))
            {
				AIStage = STAGE_HOVER;
				return;
			}
			else if (AIStage == STAGE_SLAM && (SSDone == 3 || SSDone == 9))
			{
				AIStage = STAGE_HYPER;
				return;
			}
			else if (AIStage == STAGE_HYPER && (SSDone == 3 || SSDone == 9))
			{
				AIStage = STAGE_SHIELD;
				return;
			}
            else if (AIStage == STAGE_SHIELD && (SSDone == 3 || SSDone == 9))
            {
                AIStage = STAGE_HOVER;
                return;
            }
            else if (AIStage == STAGE_SLAM && (SSDone == 4 || SSDone == 8))
			{
				AIStage = STAGE_MINION;
				return;
			}
            else if (AIStage == STAGE_MINION && (SSDone == 4 || SSDone == 8))
            {
				AIStage = STAGE_HOVER;
				return;
			}
            
            /*else if (AIStage == STAGE_SHIELD && SSDone == 4)
			{
				AIStage = STAGE_WARP;
				return;
			}*/
			else
			{
				npc.noTileCollide = false;
				ResetStage();
			}

			if (npc.velocity == Vector2.Zero)
				npc.noTileCollide = true;

			npc.netUpdate = true;
		}

		#endregion

		#region Misc Methods

		/// <summary>
		/// Returns a list of players. Use .count to find the Count of players present on the server.
		/// </summary>
		/// <returns></returns>
		public List<Player> PlayerCount() => Main.player.Where(player => player.active).ToList();

		//MAY BE NEEDED, do NOT remove;
        //public static double AngleBetweenVectors(Vector2 v1, Vector2 v2) => Math.Atan2((v1.X* v2.Y + v1.Y* v2.X), (v1.X* v2.X + v1.Y* v2.Y)) * (180 / MathHelper.Pi);

        public void CircularDust(int quantity, NPC target, short DustID, float radius, float scale)
        {
            for (int i = 0; i < quantity; i++)
            {
                Vector2 pos = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) + target.Center;
                float angle = Main.rand.NextFloat(-(float) Math.PI, (float) Math.PI);
                Dust dust = Dust.NewDustPerfect(pos, DustID,
                    new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) * radius, 255, default(Color),
                    scale);
                dust.noGravity = true;
            }
        }

        public void ExplodeEffect(Vector2 position) //Breakdown of original function: call the gore and spawn it twice.
        {
			for (int i = 0; i < 3; i++)
			{
				float scaleFactor = 3f;

				for (int j = 0; j <= 4; j++)
				{
					int numGore = Gore.NewGore(position, default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[numGore].velocity *= scaleFactor;

					Gore gore1 = Main.gore[numGore];
					gore1.velocity.X = gore1.velocity.X + 1f;

					Gore gore2 = Main.gore[numGore];
					gore2.velocity.Y = gore2.velocity.Y + 1f;
				}
			}
        }

        public override void NPCLoot()
        {

            if (Main.expertMode)
                npc.DropBossBags();
            else
            {
                int choice = Main.rand.Next(0, 1);

                if (choice == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType<BeamRifle>());

                /*if (choice == 1)
                    Item.NewItem(npc.getRect(), mod.ItemType<HenchBlast>());*/

                Item.NewItem(npc.getRect(), mod.ItemType<CyberneticParts>(), Main.rand.Next(7, 18));
                Item.NewItem(npc.getRect(), mod.ItemType<ArmCannonMK2>());

                //if (Main.rand.Next(10) == 0)
                //Item.NewItem(npc.getRect(), mod.ItemType<FFTrophy>());

            }

            if (!DBTWorld.DBTWorld.downedFriezaShip)
            {
                DBTWorld.DBTWorld.downedFriezaShip = true;

                if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
            }
        }

		private void DecoDust(float inter)
		{
			GCounter++;

			if (Deg <= 360)
			{
				Deg++;

				//To find the circumference you use formula: x = cX + r * cos(angle), where the x is the coordinate, cX is the center of the circle by X and r is radius.

				float CPosX = npc.Center.X + GalaxyDistance * (float)Math.Cos(Deg);
				float CPosY = npc.Center.Y + 16f + GalaxyDistance * (float)Math.Sin(Deg);

				GalaxyDistance -= inter;//decrease the Radius depending on the amount of ticks the function is called;

				for (int i = 0; i < 2; i++)
				{
					Dust dust = Main.dust[Dust.NewDust(new Vector2(CPosX, CPosY), 1, 1, 226)];
					dust.noGravity = true;
				}
			}

			if (Deg2 <= 360 && GCounter > 20)
			{
				Deg2++;

				float CPosX = npc.Center.X + GalaxyDistance2 * (float)Math.Cos(Deg);
				float CPosY = npc.Center.Y + 16f + GalaxyDistance2 * (float)Math.Sin(Deg);

				GalaxyDistance2 -= inter;//decrease the Radius depending on the amount of ticks the function is called;

				for (int i = 0; i < 2; i++)
				{
					Dust dust = Main.dust[Dust.NewDust(new Vector2(CPosX, CPosY), 1, 1, 226)];
					dust.noGravity = true;
				}
			}

			if (Deg == 360)
				Deg = 0;
			if (Deg2 == 360)
			{
				Deg2 = 0;
			}
		}

        #endregion

        #region Variables 

        /// <summary>
        /// Sets the normal hover distance between the player and the ship on the hovering stage.
        /// </summary>
        private Vector2 HoverDistance { get; set; }

        private Vector2 HyperPosition { get; set; }

        private const float ShieldDistance = (float)8.5 * 16f;
		private float GalaxyDistance;
		private float GalaxyDistance2;
        private int Deg = 0;
		private int Deg2 = 0;
		private int GCounter = 0;

		public float HyperSlamSpeed { get; private set; }
        public int SelectHoverMP { get; private set; }
        public int Random { get; private set; }
        public int SlamCounter { get; private set; }
        public int SSDelay { get; private set; }
        public int DustScaleTimer { get; private set; }
        public double SSDone { get; private set; }
        public int XHoverTimer { get; private set; }
        public float HoverCooldown { get; private set; }
        public int SlamBarrageSpin { get; private set; }
        public int IterationCount { get; private set; }
        public int MinionCount { get; private set; }
        public int YHoverTimer { get; private set; }
        private float SpeedAdd { get; set; }
        public int HyperSlamsDone { get; private set; }
        public float ShieldDuration { get; private set; }
        public int ShieldLife { get; private set; }
        public int TileX { get; private set; }
        public int TileY { get; private set; }
		public double ShieldFrame { get; private set; } = 0;
		public int Cache { get; private set; }


        public float AIStage
        {
            get { return npc.ai[AI_STAGE_SLOT]; }
            set { npc.ai[AI_STAGE_SLOT] = value; }
        }

        private float AITimer
        {
            get { return npc.ai[AI_TIMER_SLOT]; }
            set { npc.ai[AI_TIMER_SLOT] = value; }
        }

        private bool UnderEightyHealth
		{
			get { return npc.life <= npc.lifeMax * .8; }
		}

		private bool UnderFiftyHealth
		{
			get { return npc.life <= npc.lifeMax * .5; }
		}

        private bool UnderThirtyHealth
		{
			get { return npc.life <= npc.lifeMax * .3; }
		}

        private bool Under10Health
        {
            get { return npc.life <= npc.lifeMax * .1; }
        }

        #endregion
    }
}
