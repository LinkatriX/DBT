using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.Helpers;
using DBT.Items.Bags;
using DBT.Items.Materials;
using DBT.Items.Weapons;
using DBT.Projectiles;
using System.Linq;

namespace DBT.NPCs.Bosses.GreatApe
{
    [AutoloadBossHead]
    public class GreatApeBoss : ModNPC
    {
        #region Defaults
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Great Ape");
            Main.npcFrameCount[npc.type] = 29;
        }

        public override void SetDefaults()
        {
            npc.width = 220;
            npc.height = 180;
            npc.damage = 30;
            npc.defense = 6;
            npc.lifeMax = 2200;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 2, 50, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = false;
            npc.noGravity = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival");
            bossBag = mod.ItemType<FFShipBag>();
        }

        public const int
            STAGE_WALK = 0,
            STAGE_LEAP = 1,
            STAGE_BLAST = 2,
            STAGE_BEAM = 3,
            STAGE_PUNCH = 4,
            STAGE_MEGAROAR = 5;
        #endregion

        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

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

            if (!HasDoneStartingStuff)
            {
                ChangeStage();
            }
            
            if (AIStage == STAGE_WALK)
            {
                
            }

            if (AIStage == STAGE_LEAP)
            {
                if (!HasDoneStartingStuff && !IsRoaring)
                {
                    if (!HasDoneStartingPosition)
                    {
                        npc.position = player.position + new Vector2(600, -800);
                        npc.velocity.X -= 0.2f;
                        HasDoneStartingPosition = true;
                    }
                    if (npc.velocity.Y == 0 && npc.velocity.X != 0)
                        npc.velocity.X = 0;
                    if (_frame == 0)
                        if (DBTMod.IsTickRateElapsed(30))
                            DoRoar();

                }
                
            }
            if (AIStage == STAGE_BEAM)
            {
                AITimer1++;
                if (_frame != 0)
                {
                    _frame = 0;
                }
                if (AITimer1 == 20)
                    DoRoar();

                if (!IsRoaring)
                {
                    IsBeaming = true;
                }
                
                if (IsRoaringAnimation)
                {
                    //Do Beam Blast here, using old style
                }
                    

            }
            //Main.NewText("Has done starting position? " + HasDoneStartingPosition);
            //Main.NewText("Has done starting stuff? " + HasDoneStartingStuff);
            //Main.NewText("Y velocity is: " + npc.velocity.Y);
        }

        public void ChangeStage()
        {
            if (!HasDoneStartingStuff)
                AIStage = STAGE_LEAP;

            if (AIStage == STAGE_LEAP && HasDoneStartingStuff)
                AIStage = STAGE_BEAM;
        }

        public void DoRoar()
        {
            IsRoaring = true;
        }

        int _frame = 0;
        int _frameTimer = 0;
        int _frameRate = 1;

        public override void FindFrame(int frameHeight)
        {
            if (AIStage == STAGE_LEAP)
            {
                if (npc.velocity.Y < 0)
                    _frame = 12;
                if (npc.velocity.Y > 0)
                    _frame = 16;
                if (npc.velocity.Y == 0 && (_frame == 16))
                    _frame = 17;
                if (_frame == 17)
                {
                    _frameTimer++;
                    if (_frameTimer == 8)
                    {
                        _frameTimer = 0;
                        _frame = 0;
                    }
                        

                    npc.spriteDirection = -1;
                }

                if (IsRoaring)
                {
                    _frameTimer++;

                    if (_frameTimer > 10 && _frame == 0 && !IsRoaringAnimation)
                    {
                        _frame = 18;
                        _frameTimer = 0;
                    }

                    if (_frameTimer > 10 && _frame != 0 && _frame < 22 && !IsRoaringAnimation)
                    {
                        _frame++;
                        _frameTimer = 0;
                    }
                    if (_frame == 22 || IsRoaringAnimation)
                    {
                        IsRoaringAnimation = true;
                        if (_frameTimer == 1)
                            SoundHelper.PlayCustomSound("Sounds/GreatApe/ApeRoar");
                        if (_frameTimer > 60)
                        {
                            _frame = 18;
                            if (_frameTimer > 68)
                            {
                                _frame = 0;
                                IsRoaring = false;
                                _frameTimer = 0;
                                if (!HasDoneStartingStuff)
                                {
                                    HasDoneStartingStuff = true;
                                    ChangeStage();
                                }
                            }
                        }
                    }

                }
            }

            if (AIStage == STAGE_BEAM)
            {
                if (IsBeaming)
                {
                    _frameTimer++;

                    if (_frameTimer > 10 && _frame == 0 && !IsRoaringAnimation)
                    {
                        _frame = 18;
                        _frameTimer = 0;
                    }

                    if (_frameTimer > 10 && _frame != 0 && _frame < 22 && !IsRoaringAnimation)
                    {
                        _frame++;
                        _frameTimer = 0;
                    }

                    if (_frame == 22 || IsRoaringAnimation)
                    {
                        IsRoaringAnimation = true;
                        if (_frameTimer == 1)
                            SoundHelper.PlayCustomSound("Sounds/GreatApe/ApeBeam");

                        if (HasFinishedFiringBeam)
                        {
                            _frame = 18;
                            AITimer2++;
                            if (AITimer2 > 10)
                            {
                                _frame = 0;
                                AITimer2 = 0;
                            }
                        }

                    }
                }
                        
            }

            

            npc.frame.Y = frameHeight * _frame;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = ModContent.GetTexture("DBT/NPCs/Bosses/GreatApe/GreatApeBoss");

            int frameHeight = texture.Height / 29;

            Vector2 drawPos = npc.TopLeft - Main.screenPosition;
            Vector2 drawCenter = new Vector2(30f, 42f);
            SpriteEffects effects;

            if (npc.direction == -1)
                effects = SpriteEffects.FlipHorizontally;
            else
                effects = SpriteEffects.None;

            Rectangle sourceRectangle = new Rectangle(0, frameHeight * _frame, texture.Width, frameHeight);
            spriteBatch.Draw(texture, drawPos, sourceRectangle, Color.White, 0f, drawCenter, 1f, effects, 0f);

            return false;
        }

        public bool Jumping { get; set; } = false;
        public bool HasDoneStartingPosition { get; set; } = false;
        public bool HasDoneStartingStuff { get; set; } = false;
        public bool HasFinishedFiringBeam { get; set; } = false;
        public bool IsBeaming { get; set; } = false;
        public bool IsRoaring { get; set; } = false;
        public bool IsRoaringAnimation { get; set; } = false;
        public int AITimer1 { get; set; }
        public int AITimer2 { get; set; }

        public int AIStage { get; set; }
    }
}
