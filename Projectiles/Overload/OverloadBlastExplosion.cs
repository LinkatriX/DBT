using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles.Overload
{
    public class OverloadBlastExplosion : ModProjectile
    {
        private float _sizeTimer;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            projectile.width = 62;
            projectile.height = 62;
            projectile.aiStyle = 0;
            projectile.alpha = 70;
            projectile.timeLeft = 120;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            _sizeTimer = 120;
        }
		
		public override Color? GetAlpha(Color lightColor)
        {
			if (projectile.timeLeft < 85) 
			{
				byte b2 = (byte)(projectile.timeLeft * 3);
				byte a2 = (byte)(100f * ((float)b2 / 255f));
				return new Color((int)b2, (int)b2, (int)b2, (int)a2);
			}
			return new Color(255, 255, 255, 100);
        }
		
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.frameCounter++;
            if (projectile.frameCounter > 1)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 1)
            {
                projectile.frame = 0;
            }
            if (_sizeTimer > 0)
            {
                projectile.scale = (_sizeTimer / 120f) * 1.5f;
                _sizeTimer--;
            }
            else
            {
                projectile.scale = 1f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            DBTMod.circle.ApplyShader(radius);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}