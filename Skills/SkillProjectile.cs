using DBT.Extensions;
using DBT.Players;
using DBT.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills
{
    public abstract class SkillProjectile : KiProjectile
    {
        protected SkillProjectile(SkillDefinition definition, int width, int height) : base(definition.Characteristics.BaseDamage, definition.Characteristics.BaseKnockback, width, height) 
        {
            Definition = definition;
            Width = width;
            Height = height;
        }

        private bool ShouldFireSkill(DBTPlayer modPlayer)
        {
            if (!modPlayer.MouseLeftHeld) 
            {
                return true;
            }
            return false;
        }

        public override void SetDefaults()
        {
            projectile.width = Width;
            projectile.height = Height;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.velocity = Vector2.Zero;
            base.SetDefaults();
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            DBTPlayer dbtPlayer = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();
            if (IsFired && UsesChargeBall)
                return false;
            if (dbtPlayer.MouseLeftHeld)
                HandleCharging();

            if (!UsesChargeBall)
            {
                if (dbtPlayer.MouseLeftHeld)
                {
                    if (ChargeOverrideTexture != null)
                        DrawSkillCharge(spriteBatch, Main.projectileTexture[projectile.type], 0f, projectile.scale, Color.White);
                    else
                        DrawSkillCharge(spriteBatch, ChargeOverrideTexture, 0f, projectile.scale, Color.White);
                }
            }
            return true;
        }*/

        //The core function for drawing a skill's charge.
        public void DrawSkillCharge(SpriteBatch spriteBatch, Texture2D texture, float rotation = 0f, float scale = 1f, Color color = default(Color))
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            //int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            //DBTMod.circle.ApplyShader(radius);
            spriteBatch.Draw(texture, GetChargeBallPosition() - Main.screenPosition,
                new Rectangle(0, 0, Width, Height), color, rotation, new Vector2(Width * .5f, Height / Main.projFrames[projectile.type] * .5f), scale, 0, 0.99f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public Vector2 GetChargeBallPosition()
        {
            return Main.player[projectile.owner].Center + channelingOffset;
        }
        // vector to reposition the charge ball if it feels too low or too high on the character sprite
        public Vector2 channelingOffset = new Vector2(0, 4f);

        public void HandleCharging()
        {
            DBTPlayer dbtPlayer = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();
            projectile.timeLeft = 999;
            projectile.velocity = Vector2.Zero;
            if (Definition.Characteristics.ChargeCharacteristics.CurrentCharge < Definition.Characteristics.ChargeCharacteristics.BaseMaxChargeLevel)
            {
                projectile.ai[1]++;
                if (projectile.ai[1] >= Definition.Characteristics.ChargeCharacteristics.BaseChargeTimer)
                {
                    Definition.Characteristics.ChargeCharacteristics.CurrentCharge++;
                    PerChargeLevel();
                    projectile.ai[1] = 0;
                }
                OnChargeAttack();
            }
            if (!dbtPlayer.MouseLeftHeld)
            {
                if (RequiresFullCharge)
                {
                    if (Definition.Characteristics.ChargeCharacteristics.CurrentCharge >= Definition.Characteristics.ChargeCharacteristics.BaseMaxChargeLevel)
                        HandleFiring(Main.player[projectile.owner]);
                }
                else
                {
                    if (Definition.Characteristics.ChargeCharacteristics.CurrentCharge > 0)
                        HandleFiring(Main.player[projectile.owner]);
                }
            }
        }

        public void HandleFiring(Player player)
        {
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            
            if (ShouldFireSkill(modPlayer))
            {
                OnFireAttack();
                
                IsFired = true;

                // kill the charge sound if we're firing
                //chargeSoundSlotId = SoundHelper.KillTrackedSound(chargeSoundSlotId);

                if (UsesChargeBall)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == player.whoAmI)
                    {
                        // fire the laser!
                        //fire
                        KillCharge();
                    }
                }

                // beam is no longer sustainable, and neither is the charge ball
                /*if (Charge <= 0f)
                {
                    KillCharge();
                }*/
            }
        }

        /// <summary>A hook that is called per tick the skill is being charged.</summary>
        /// <returns></returns>
        public virtual void OnChargeAttack()
        {
        }
        /// <summary>A hook that is called whenever the skill is released.</summary>
        /// <returns></returns>
        public virtual void OnFireAttack()
        {
        }
        /// <summary>A hook that is called for every time the attack hits the next charge level.</summary>
        /// <returns></returns>
        public virtual void PerChargeLevel()
        {
        }

        public override bool PreAI()
        {
            // pre AI of the charge ball is responsible for telling us if the weapon has changed or the projectile should otherwise die

            bool isPassingPreAi = base.PreAI();
            if (!isPassingPreAi && MyProjectile != null)
            {
                MyProjectile.StartKillRoutine();
            }
            return isPassingPreAi;
        }

        public void KillCharge()
        {
            // kill the charge ball
            projectile.Kill();
        }

        public Projectile MyProjectile { get; set; } = null;
        public bool IsFired { get; set; } = false;
        public SkillDefinition Definition { get; }
        public int Width { get; }
        public int Height { get; }
        public bool UsesChargeBall { get; set; } = false;
        public bool RequiresFullCharge { get; set; } = false;
        public Texture2D ChargeOverrideTexture { get; set; } = null;
        
    }
}