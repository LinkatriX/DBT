using System;
using DBT.Players;
using DBT.Projectiles.Overload.Rings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles.Overload
{
    public class AuraOrb : ModProjectile
    {
        private float _sizeTimer;
        private float _blastTimer;
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 999000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.aiStyle = -1;
            projectile.scale = 2f;
            projectile.light = 0f;
            projectile.netUpdate = true;
            projectile.damage = 0;
			projectile.alpha = 0;
            projectile.scale = 0;
        }
		public override Color? GetAlpha(Color lightColor)
        {
			return new Color(255, 255, 255, projectile.alpha);
        }
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            projectile.position.X = player.Center.X;
            projectile.position.Y = player.Center.Y;
            projectile.Center = player.Center + new Vector2(-50, -15 - projectile.scale * 45);
            if (modPlayer.IsOverloading && modPlayer.IsTransformed())
            {
                if (_sizeTimer < 500)
                {
                    projectile.scale = _sizeTimer / 300f * 4;
                    _sizeTimer++;

                    projectile.ai[0]++;
                    if (projectile.ai[0] >= 10)
                    {
                        int rotation = Main.rand.Next(60, 120);
                        Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, GetRingStyle(), 0, 0, projectile.owner, rotation);
                        projectile.ai[0] = 0;
                    }

                    player.position.Y = player.oldPosition.Y - 0.45f;
                    player.velocity.X = 0;
                    player.immuneNoBlink = true;
                }
                if (_sizeTimer == 500)
                {
                    projectile.scale = 0f;
                    _sizeTimer += 1;
                }

                if (player.dead)
                    projectile.Kill();

                else
                {
                    if (modPlayer.IsCharging && _sizeTimer > 500)
                    {

                        if (projectile.scale > 2.5f)
                        {
                            player.position.Y = player.oldPosition.Y;
                            player.velocity.X = 0;
                            projectile.ai[1]++;
                            if (projectile.ai[1] > 60)
                            {
                                _blastTimer++;
                                if (_blastTimer > 2)
                                {
                                    int blastDamage = (int)modPlayer.KiDamageMultiplier * modPlayer.MaxKi / 60;
                                    Vector2 velocity = Vector2.UnitY.RotateRandom(MathHelper.TwoPi) * 30;
                                    Projectile.NewProjectile(player.Center.X, player.Center.Y, velocity.X, velocity.Y, mod.ProjectileType<OverloadBlast>(), blastDamage, 2f, projectile.owner);
                                    _blastTimer = 0;
                                }
                            }
                        }
                        else
                        {
                            projectile.scale += 0.02f;
                            player.position.Y = player.oldPosition.Y - 2f;
                            player.velocity.X = 0;
                        }

                    }
                    if (DBTMod.Instance.energyChargeKey.JustReleased && projectile.scale != 0)
                        projectile.scale = 0f;
                }
            }
            else
                projectile.Kill();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            DBTMod.circle.ApplyShader(radius);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public int GetRingStyle()
        {
            switch (Main.rand.Next(0, 3))
            {
                case 0:
                    return mod.ProjectileType<GreenRing1>();

                case 1:
                    return mod.ProjectileType<GreenRing2>();

                case 2:
                    return mod.ProjectileType<GreenRing3>();

                case 3:
                    return mod.ProjectileType<GreenRing4>();

                default:
                    return mod.ProjectileType<GreenRing1>();
            }
        }
    }
}