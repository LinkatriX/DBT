using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills.TrapShooter
{
    public sealed class TrapShooterProjectile : SkillProjectile
    {
        public TrapShooterProjectile() : base(SkillDefinitionManager.Instance.TrapShooter, 16, 16) 
        {
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 17;
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 120;
            projectile.aiStyle = 1;
            aiType = 14;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.netUpdate = true;
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

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 0, 0, 0, mod.ProjectileType(nameof(TrapShooterExplosion)), projectile.damage, 4f, projectile.owner, 0, projectile.rotation);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }

    public sealed class TrapShooterExplosion : ModProjectile
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
            projectile.tileCollide = false;
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
                projectile.scale = (_sizeTimer / 120f) * 2;
                _sizeTimer--;
            }
            else
            {
                projectile.scale = 1f;
            }
        }
    }
}
