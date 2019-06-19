using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using DBT.NPCs.Saibas;
using DBT.Helpers;
using DBT.Items.Accessories.ArmCannons;
using DBT.Items.Bags;
using DBT.Items.Materials;
using DBT.Items.Weapons;
using DBT.NPCs.FriezaForce.Minions;
using DBT.NPCs.Saibamen;
using DBT.Projectiles;

namespace DBT.NPCs.Bosses.FriezaShip
{
    [AutoloadBossHead]
    public class FriezaShip : ModNPC
    {
		public const int
			STAGE_HOVER = 0,
			STAGE_SLAM = 1,
			STAGE_SLAMBARRAGE = 2,
			STAGE_MINION = 3,
			STAGE_HYPER = 4,

			AI_STAGE_SLOT = 0,
            AI_TIMER_SLOT = 1;

        public FriezaShip()
        {
            HoverDistance = new Vector2(0, 320);
            HyperPosition = new Vector2(0, 0);
            HoverCooldown = 500;
            SlamDelay = 50;
            SlamTimer = 0;
            SlamBarrageCount = 0;
            MinionCount = 2;
            HasDoneExplodeEffect = false;
			SlamsDone = 0;
            HyperSlamsDone = 0;
            DustScaleTimer = 0;
        }

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
            npc.damage = 36;
            npc.defense = 10;
            npc.lifeMax = 3600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = Item.buyPrice(0, 3, 25, 80);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival");
            bossBag = mod.ItemType<FFShipBag>();
        }

        //To-Do: Add the rest of the stages to the AI. Make green saibaman code.
        //Make the speed of the ship's movements increase with less health, increase the speed of the projectiles, increase how fast the ship goes down on the dash, finish dash afterimage, make the homing projectiles move faster if the player is flying.
        //Boss loot: Drops Undecided material that's used to create a guardian class armor set (frieza cyborg set). Alternates drops between a weapon and accessory, accessory is arm cannon mk2, weapon is a frieza force beam rifle. Expert item is the mechanical amplifier.
        //Spawn condition: Near the ocean you can find a frieza henchmen, if he runs away then you'll get an indicator saying the ship will be coming the next morning.

        
            //AI Rundown: Hovers for 400 ticks then stops in place, spinning up for 190 ticks, then teleports above the player and slams down then flies back up. After every other normal slam and below 70% health it swaps to a barrage of slams, which does 3 slams against 1 player in singleplayer, and 1 for each player in multiplayer.
            //After every 6 basic slams and when below 50% health the ship summons a handful of saibamen and frieza force henchmen.
            //After every 5 basic slams and when below 30% health the ship stops, becomes invulnurable and starts emitting yellow dust, after 180 ticks the ship creates lines of dust from right to left indicating the path it will take to attack you, then after 20 ticks it will teleport to the right and move to the left quickly, following the dust line. It does this attack 4 times.

        public override void AI()
        {
			AITimer++;
            
            #region Base targetting
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            //Runaway if no players are alive
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

            #region Random Checks           

            //Make sure the stages loop back around
            if (AIStage > 4)
                AIStage = STAGE_HOVER;

            if (SlamsDone > 5)
                SlamsDone = 0;
            
            //Speed between stages and general movement speed drastically increased with health lost
            if (npc.life < npc.lifeMax * 0.80f)
            {
                HoverCooldown = 400;
                SpeedAdd = 1f;
                SlamDelay = 45;
                if (npc.life < npc.lifeMax * 0.50f)
                {
                    HoverCooldown = 320;
                    SpeedAdd = 2f;
                    SlamDelay = 38;
                }
                if (npc.life < npc.lifeMax * 0.2f)
                {
                    HoverCooldown = 240;
                    SpeedAdd = 4f;
                    SlamDelay = 32;
                    MinionCount = 4;
                }
            }
            #endregion

            #region Hovering
            if (AIStage == STAGE_HOVER)
            {
                //Y Hovering

                if (Main.player[npc.target].position.Y != npc.position.Y + HoverDistance.Y)
                {
                    YHoverTimer++;

                    if (YHoverTimer > 10)
                    {
                        //Thanks UncleDanny for this <3
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

                //X Hovering, To-Do: Make the ship not just center itself on the player, have some left and right alternating movement?
                if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) != HoverDistance.X)
                {
                    //float hoverSpeedY = (-2f + Main.rand.NextFloat(-3, -8));
                    XHoverTimer++;
                    if (XHoverTimer > 30)
                    {
                        npc.velocity.X = 2.5f * npc.direction + SpeedAdd * npc.direction;
                        if (AITimer > HoverCooldown / 1.2)
                        {
                            npc.velocity.X = 7f * npc.direction + SpeedAdd * 2 * npc.direction;
                        }

                    }
                }
                else
                {
                    npc.velocity.X = 0;
                    XHoverTimer = 0;
                }

                //Next Stage
                AITimer++;
                if (AITimer > HoverCooldown)
                {
                    StageAdvance();
                    AITimer = 0;
                }

            }
			#endregion

			#region Slam Barrage
			if (AIStage == STAGE_SLAMBARRAGE)
			{
                if (SlamBarrageCount <= 3)
                {
                    if (AITimer < 130)
                    {
                        if (SlamBarrageCount >= 1)
                            DustScaleTimer += 8;
                        else
                            DustScaleTimer++;
                        npc.velocity = Vector2.Zero;
                        CircularDust(10, npc, 134, 8f - DustScaleTimer / 18, 1);
                    }

                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        if (AITimer == 130)
                            TeleportAbove();


                        if (AITimer > 130)
                        {
                            SlamTimer++;
                            if (SlamTimer == SlamDelay)
                            {
                                DustScaleTimer = 0;
                                DoSlam();
                            }
                                
                            if (SlamTimer > SlamDelay)
                                CheckCollision(SlamTimer);
                        }
                    }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        if (AITimer == 130 && IterationSlamAmount <= PlayerAmountMP().Count)
                            TeleportAboveAll();


                        if (AITimer > 130)
                        {
                            SlamTimer++;
                            if (SlamTimer == SlamDelay)
							{
								DoSlam();
								IterationSlamAmount++;
							}
                        }

						if (SlamTimer > SlamDelayMP)
							CheckCollision(SlamTimer);
					}
                }
                else
                {
                    ResetStage();
                    ResetAllVariables();
                }
            }
            #endregion

            //To-Do: Make the teleport dust get larger the closer it is to teleporting
            #region Basic Slam
            if (AIStage == STAGE_SLAM)
            {
				if (AITimer < 180)
                {
                    DustScaleTimer++;
                    npc.velocity = Vector2.Zero;
                    CircularDust(10, npc, 133, 10f - DustScaleTimer / 20, 1);
                }
                    
                if (AITimer > 180)
                {
                    if(AITimer == 190)
					{
						TeleportAbove();
					}

                    SlamTimer++;
                    if (SlamTimer == SlamDelay)
						DoSlam();

                    if (SlamTimer >= SlamDelay + 15)//If the bottom of the ship touches a tile, nullify speed and do dust particles
                    {
                        npc.velocity.Y = -8f;

						if (!HasDoneExplodeEffect)
                        {
                            ExplodeEffect();
                            SoundHelper.PlayCustomSound("Sounds/KiExplosion", npc.position, 1.0f);
                        }
                        npc.netUpdate = true;
                    }

                }

				if (SlamTimer >= 100)
                {
                    StageAdvance();
                    ResetAllVariables();
                }

            }
			#endregion
			// Hyper visuals rundown: When it is spinning up it slowly rises up and emits dust from the center of the ship outwards, then becomes invisible until it teleports.
			//A line of dust goes horizontally in the direction the ship is going to teleport and move through, the ship then teleports to the beginning of that line with a puff of dust where it lands.
			//While the ship is moving horizontally it is emitting yellow dust that has no gravity, to simulate movement. Once it reaches the end of the movement it dissapears again with a puff of dust.
			#region Hyper

            if (AIStage == STAGE_HYPER)
            {
				if (HyperSlamsDone <= 4)
                {
                    npc.dontTakeDamage = true;
                    if (AITimer < 300)
                    {
                        DoChargeDust();
                        npc.velocity = new Vector2(0, -0.3f);
                    }
                    if (AITimer > 300 && HyperPosition == Vector2.Zero)
                    {
                        npc.alpha = 255;
                        npc.netUpdate = true;
                        npc.velocity = Vector2.Zero;
                        if (AITimer == 301)
                        {
                            CircularDust(30, npc, 133, 10f, 1);
                            ChooseHyperPosition();
                        }
                            
                    }
                    if (AITimer > 320 && HyperPosition != Vector2.Zero && npc.velocity == Vector2.Zero)
                        DoLineDust();
                    if (AITimer == 310 && HyperPosition != Vector2.Zero)
                    {
                        TeleportRight();
                        HyperSlamsDone++;
                    }
                    if (AITimer >= 350 && HyperPosition != Vector2.Zero)
                        HorizontalSlam();
                }
                else
                {
                    HorizontalSlamTimer = 0;
                    npc.dontTakeDamage = false;
                    HyperSlamsDone = 0;
                    AITimer = 0;
					ResetStage();
                }
            }

			#endregion

			//Main.NewText("Slams done: " + SlamsDone);
            Main.NewText("Slam barrages done: " + SlamBarrageCount);
            Main.NewText("Ai Stage is:" + AIStage);
			Main.NewText("Amount of players:" + PlayerAmountMP().Count);
            Main.NewText("Slam Timer is:" + SlamTimer);
            Main.NewText("Ai Timer is:" + AITimer);

            #region Minion Spawning
            if (Main.netMode != 1 && AIStage == STAGE_MINION)
            {
                if (AITimer == 0)
                {
                    SummonSaibaman();
                    SummonFFMinions();
                    npc.netUpdate = true;
                }

                if (AITimer > 60)
                {
					AIStage++;
                    AITimer = 0;
				}
            }

            //Main.NewText(AIStage);
        }

        public int SummonSaibaman()
        {
            for (int i = 0; i < MinionCount; i++)
            {
                npc.netUpdate = true;

                switch (Main.rand.Next(0, 3))
                {
                    case 0:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman1>());
                    case 1:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman2>());
                    case 2:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman3>());
                    case 3:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman4>());
                    default:
                        return 0;
                }
            }
            return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman1>());
        }

        public int SummonFFMinions()
        {
            for (int i = 0; i < MinionCount; i++)
            {
                npc.netUpdate = true;
                switch (Main.rand.Next(0, 2))
                {
                    case 0:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion1>());
                    case 1:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion2>());
                    case 2:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion3>());
                    default:
                        return 0;
                }
            }
            return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion1>());
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
                    dust = Main.dust[Dust.NewDust(position, 220, 120, 133, 0f, 0f, 0, new Color(255, 255, 255), 0.9236842f)];
                    dust.noGravity = true;
                }
            }
        }
        public void ChooseHyperPosition()
        {
            Player targetPlayer = Main.player[npc.target];
            HyperPosition = new Vector2(targetPlayer.position.X + 500, targetPlayer.position.Y + Main.rand.Next(10, 20));
        }
        public void DoLineDust()
        {
            if (Main.rand.NextFloat() < 1.2f)
            {
                Dust dust;
                dust = Dust.NewDustPerfect(HyperPosition, 133, new Vector2(-70f, 0f), 0, new Color(255, 255, 255), 1.052632f);
                dust.noGravity = true;
            }

        }
        public void TeleportRight()
        {
            npc.position = HyperPosition + new Vector2(0, -10);
            npc.alpha = 0;
            npc.netUpdate = true;
            CircularDust(30, npc, 133, 10f, 1);
        }
        private int HorizontalSlamTimer = 0;
		public void HorizontalSlam()
		{
            DoChargeDust();
            HorizontalSlamTimer++;
            npc.velocity = new Vector2(-40f, 0f);

            if (HorizontalSlamTimer == 30)
			{
                npc.velocity = Vector2.Zero;
                npc.alpha = 255;
                npc.netUpdate = true;
                AITimer = 300;
                CircularDust(30, npc, 133, 10f, 1);
                HorizontalSlamTimer = 0;
                HyperPosition = Vector2.Zero;
            }
		}
		#endregion

		#region Slam + Teleporting
		public int IterationSlamAmount { get; set; }
		public bool SlamDone = true;
		public int callArray = -1;

        
		public void DoTeleportDust(int dustType, int dustRate)
        {
            if (Main.rand.NextFloat() < 2f)
            {
                for (int i = 0; i < dustRate; i++)
                {
                    DustScaleTimer++;
                    DustScaleTimer /= 10;
                    Dust dust;
                    Vector2 position = Main.player[npc.target].position + new Vector2(-50f, -220);
                    dust = Main.dust[Dust.NewDust(position, 100, 57, dustType, 0f, 0f, 0, new Color(255, 255, 255), 0.7236842f + DustScaleTimer)];
                    dust.noGravity = true;
                }
            }

        }


        public void TeleportAbove()//For singleplayer
        {
			npc.alpha = 255;

			Vector2 pos = Main.player[npc.target].position + new Vector2(-100f + (Main.player[npc.target].velocity.X * 10), -HoverDistance.Y);

			int tileX = (int)(pos.X / 16); //because every tile in Terraria is 16;
			int tileY = (int)(pos.Y / 16);

			if ((Main.tile[tileX, tileY] != null && Main.tile[tileX, tileY].wall != 87) || (double)tileY <= Main.worldSurface && !Collision.SolidCollision(pos, Main.player[callArray].width, Main.player[callArray].height))
			{
				npc.noTileCollide = true;
			}
			else
			{
				npc.noTileCollide = false;
			}

			npc.position = new Vector2(pos.X, -HoverDistance.Y);
			npc.position = new Vector2(pos.X, -HoverDistance.Y);
			npc.position = pos;
            npc.netUpdate = true;
            Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(), 0, 0);
            SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
            npc.alpha = 0;
        }

		public void TeleportAboveAll() //For multiplayer
		{
			callArray++;

			if (callArray < PlayerAmountMP().Count)
			{
				npc.alpha = 255;
				Vector2 pos = Main.player[callArray].position + new Vector2(-100f + (Main.player[npc.target].velocity.X * 10));

				int tileX = (int)(pos.X / 16); //because every tile in Terraria is 16;
				int tileY = (int)(pos.Y / 16);

				if ((Main.tile[tileX, tileY] != null && Main.tile[tileX, tileY].wall != 87) || (double)tileY <= Main.worldSurface && !Collision.SolidCollision(pos, Main.player[callArray].width, Main.player[callArray].height))
				{
					npc.noTileCollide = true;
				}
				else
				{
					npc.noTileCollide = false;
				}

				npc.position = new Vector2(pos.X, -255);
				npc.netUpdate = true;
				Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(), 0, 0);
				SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
				npc.alpha = 0;
			}
		}

		public void DoSlam()
		{
			if (npc.life <= npc.lifeMax * 0.50)//Double the contact damage if below 50% health
				npc.damage *= 2;

			npc.noTileCollide = false;
			npc.velocity.Y = 25f;
            npc.netUpdate = true;

			if (AIStage == STAGE_SLAM)
				SlamsDone++;

			if (AIStage == STAGE_SLAMBARRAGE)
				SlamBarrageCount++;
		}

		public void CheckCollision(int SlamTimerV) //Gives the slamTimer 10 ticks before going back up, but shouldn't be considered since most of it is wasted on slam movement.
		{
            if (SlamTimerV >= SlamDelay + 15)//If the bottom of the ship touches a tile, nullify speed and do dust particles
            {
                npc.velocity.Y = -8f;

				if (!HasDoneExplodeEffect)
				{
					ExplodeEffect();
					SoundHelper.PlayCustomSound("Sounds/KiExplosion", npc.position, 1.0f);
				}

				if (SlamTimerV >= 50)
				{
					SlamTimer = 0;
					AITimer = 90;
                    HasDoneExplodeEffect = false;
                    npc.netUpdate = true;
                }
			}
		}

		#endregion

		#region Misc Methods

		public List<Player> array = new List<Player>();

		public List<Player> PlayerAmountMP()
		{
			for (int i = 0; i < Main.player.Length; i++)
			{
				if (Main.player[i].active)
				{
					if (array.Contains(Main.player[i]))
					{
						break;
					}

					array.Add(Main.player[i]);
				}
				else if (!Main.player[i].active)
				{
					break;
				}
			}
			return array;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (AIStage == STAGE_SLAM)
            {
                float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Vector2 drawPos = new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + drawOrigin.X * npc.scale,
                        npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + drawOrigin.Y * npc.scale + npc.gfxOffY);

                    Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        
        private void StageAdvance()
        {
			if (AIStage == STAGE_HOVER)
			{
				AIStage++;
				return;
			}
            else if (AIStage == STAGE_SLAM && SlamsDone == 5 && npc.life <= npc.lifeMax * .5)
            {
                AIStage = STAGE_MINION;
                return;
            }
            else if (AIStage == STAGE_SLAM && (SlamsDone == 1 || SlamsDone == 3) && npc.life <= npc.lifeMax * .7)
			{
				AIStage++;
				return;
			}
			else if (AIStage == STAGE_SLAM && SlamsDone == 4 && npc.life <= npc.lifeMax * .3)
			{
				AIStage = STAGE_HYPER;
				return;
			}
			else
				ResetStage();
            npc.netUpdate = true;
        }

        private void ResetStage()
        {
            AIStage = STAGE_HOVER;
            npc.netUpdate = true;
        }

        private void ResetAllVariables()
        {
            AITimer = 0;
            DustScaleTimer = 0;
            SlamBarrageCount = 0;
            SlamTimer = 0;
            npc.velocity.Y = 0;
            npc.noTileCollide = true;
            HasDoneExplodeEffect = false;

            if (npc.life <= npc.lifeMax * 0.50)//Reset the damage back to its normal amount
                npc.damage /= 2;
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

            if (!DBTWorld.downedFriezaShip)
            {
                DBTWorld.downedFriezaShip = true;

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
            }
        }

        public void ExplodeEffect()
        {
            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 3f;

                if (num619 == 1)
                    scaleFactor9 = 3f;

                int num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;

                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;

                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;

                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;

                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;

                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;

                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;

                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
            }

            HasDoneExplodeEffect = true;
        }

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

        #endregion

        #region Animations
        int _frame = 0;
        int _frameTimer = 0;
        int _frameRate = 1;
        public override void FindFrame(int frameHeight)
        {
            if (AIStage == STAGE_SLAM && AITimer < 180 || AIStage == STAGE_SLAMBARRAGE && AITimer < 120 || AIStage == STAGE_HYPER && AITimer < 300)
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
                        {
                            _frameRate = 4;
                        }
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

            if (_frame > 7) //Make it 7 because 0 is counted as a frame, making it 8 frames
                _frame = 0;

            npc.frame.Y = frameHeight * _frame;
        }
		#endregion

		#region Variables

		public Vector2 HoverDistance { get; set; }
        public Vector2 HyperPosition { get; set; }

		private int sDelayMP;


		public float HoverCooldown { get; set; }
        public int SlamDelay { get; set; }
        public int SlamBarrageCount { get; set; }
        public int SlamsDone { get; set; }
        public int SlamTimer { get; set; }
        public int SlamCoolDownTimer { get; set; }
        public int MinionCount { get; set; }
        public bool HasDoneExplodeEffect { get; set; }
        public float SpeedAdd { get; set; }
        public int HyperSlamsDone { get; set; }
        public int YHoverTimer { get; set; }
        public int XHoverTimer { get; set; }
        public float DustScaleTimer { get; set; }

        public float AIStage
        {
            get { return npc.ai[AI_STAGE_SLOT]; }
            set { npc.ai[AI_STAGE_SLOT] = value; }
        }

        public float AITimer
        {
            get { return npc.ai[AI_TIMER_SLOT]; }
            set { npc.ai[AI_TIMER_SLOT] = value; }
        }
	
		public int SlamDelayMP
		{
			get { return sDelayMP; }
			set 
			{
				if (npc.life <= npc.lifeMax * .5)
				{
					value = (int)(SlamDelay * 1.2 / PlayerAmountMP().Count);
					sDelayMP = value;
				}
				else
				{
					value = (int)(SlamDelay * 1.5 / PlayerAmountMP().Count);
					sDelayMP = value;
				} 
			}
		}
		#endregion
	}
}
