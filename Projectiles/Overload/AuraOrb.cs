using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles.Overload
{
    public class AuraOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 2000;
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
			projectile.Center = player.Center + new Vector2(-30 - projectile.scale * 80, -30 - projectile.scale * 60);

            if (projectile.scale < 5f)
            {
                projectile.scale += 0.01f;
                projectile.ai[1]++;
            }
            else
                projectile.Kill();

            projectile.ai[0]++;
            if (projectile.ai[0] >= 15)
            {
                int rotation = Main.rand.Next(60, 120);
                Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType<GreenRing>(), 0, 0, projectile.whoAmI, rotation);
                projectile.ai[0] = 0;
            }

            if (projectile.ai[1] < 2000)
            {
                player.position.Y -= 0.4f;
                player.velocity.X = 0;
            }
                
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
    }
}