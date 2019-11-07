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
        protected SkillProjectile(SkillDefinition definition, int width = 0, int height = 0) : 
            base(definition.Characteristics.BaseDamage, definition.Characteristics.BaseKnockback, width, height)
        {
            Definition = definition;
        }

        public SkillDefinition Definition { get; }
    }

    public abstract class SkillChargeProjectile : ModProjectile 
    {
        protected SkillChargeProjectile(SkillDefinition definition, ref int width, ref int height) : base() 
        {
            Definition = definition;
            Width = width;
            Height = height;
        }

        private bool ShouldFireSkill(DBTPlayer modPlayer)
        {
            if (!modPlayer.MouseLeftHeld || IsFired) 
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
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Don't draw the charge when firing.
            if (IsFired)
                return false;
            float originalRotation = -1.57f;
            DrawSkillCharge(spriteBatch, Main.projectileTexture[projectile.type], originalRotation, projectile.scale, Color.White);
            return false;
        }

        //The core function for drawing a skill's charge.
        public void DrawSkillCharge(SpriteBatch spriteBatch, Texture2D texture, float rotation = 0f, float scale = 1f, Color color = default(Color))
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            //int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            //DBTMod.circle.ApplyShader(radius);
            float projRotation = projectile.velocity.ToRotation() + rotation;
            spriteBatch.Draw(texture, GetChargeBallPosition() - Main.screenPosition,
                new Rectangle(0, 0, Width, Height), color, projRotation, new Vector2(Width * .5f, Height * .5f), scale, 0, 0.99f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public Vector2 GetChargeBallPosition()
        {
            Vector2 positionOffset = channelingOffset + projectile.velocity * ChargeBallHeldDistance;
            return Main.player[projectile.owner].Center + positionOffset;
        }

        public float ChargeBallHeldDistance
        {
            get
            {
                return (Height / 2f) + 10f;
            }
        }

        // vector to reposition the charge ball if it feels too low or too high on the character sprite
        public Vector2 channelingOffset = new Vector2(0, 4f);

        public void HandleFiring(Player player, Vector2 mouseVector)
        {
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            bool temp = true;
            
            // minimum charge level is required to fire in the first place, but once you fire, you can keep firing.
            if (ShouldFireSkill(modPlayer))
            {
                // once fired, there's no going back.
                IsFired = true;

                // kill the charge sound if we're firing
                //chargeSoundSlotId = SoundHelper.KillTrackedSound(chargeSoundSlotId);

                if (MyProjectile == null)
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
    }
}