using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles.Overload.Rings
{
    public abstract class GreenRing : ModProjectile
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
            projectile.damage = 0;
            projectile.scale = 2f;
            projectile.aiStyle = -1;
            projectile.light = 0f;
            projectile.stepSpeed = 13;
            projectile.netUpdate = true;
			projectile.alpha = 255;
            projectile.rotation = 90;
        }
		public override Color? GetAlpha(Color lightColor)
        {
			return new Color(255, 255, 255, projectile.alpha);
        }
		
		public override void AI()
		{
            projectile.rotation = projectile.ai[0];
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center + new Vector2(-170, 0 + projectile.scale * 220);

            if (projectile.scale > 0f)
            {
                projectile.scale -= 0.06f;
            }
            else
                projectile.Kill();
        }
		
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            //DBTMod.circle.ApplyShader(radius);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }

    public class GreenRing1 : GreenRing
    {
    }

    public class GreenRing2 : GreenRing
    {
    }

    public class GreenRing3 : GreenRing
    {
    }

    public class GreenRing4 : GreenRing
    {
    }
}