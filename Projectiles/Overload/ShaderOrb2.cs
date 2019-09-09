using System;
using DBT.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace DBT.Projectiles.Overload
{
    public class ShaderOrb2 : ModProjectile
    {
        private int _shockwaveSpeed = 18;
        private float _scaletime;
        public override void SetDefaults()
        {
            projectile.width = 1214;
            projectile.height = 1214;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.aiStyle = 101;
            projectile.light = 0f;
            projectile.stepSpeed = 13;
            projectile.netUpdate = true;
            projectile.damage = 0;
			projectile.alpha = 255;
            projectile.scale = 0;
        }
		public override Color? GetAlpha(Color lightColor)
        {
			return new Color(255, 255, 255, projectile.alpha);
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center + new Vector2(-590 + projectile.scale * 590, 0);
            projectile.alpha -= 5;
            if (!Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene.Activate("Shockwave", player.Center).GetShader().UseColor(1, 5, _shockwaveSpeed).UseTargetPosition(player.Center);
            }
            float progress = (600f - projectile.timeLeft) / 60f;
            Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(100f * (1 - progress / 3f));
            if (projectile.scale < 2.5f)
            {
                if (projectile.scale < .5f)
                    _shockwaveSpeed = 22;
                projectile.scale += 0.03f;
            }
            else
                projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            Filters.Scene["Shockwave"].Deactivate();
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType<ShaderOrb3>(), 0, 0, projectile.owner);
            SoundHelper.PlayCustomSound("Sounds/Overload/Overloadcircle", Main.player[projectile.owner], 0.3f);
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