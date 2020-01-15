using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills.SuperEnergyBarrage
{
    public sealed class SuperEnergyBarrageProjectile : SkillProjectile
    {
        public SuperEnergyBarrageProjectile() : base(SkillDefinitionManager.Instance.SuperEnergyBarrage, 28, 176) 
        {
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 60;
            projectile.aiStyle = 1;
            aiType = 14;
            projectile.netUpdate = true;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 4)
            {
                projectile.frame = 0;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.timeLeft < 85)
            {
                byte b2 = (byte)(projectile.timeLeft * 3);
                byte a2 = (byte)(100f * (b2 / 255f));
                return new Color(b2, b2, b2, a2);
            }
            return new Color(255, 255, 255, 100);
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 0, 0, 0, mod.ProjectileType(nameof(SuperEnergyBarrageExplosion)), projectile.damage, 4f, projectile.owner, 0, projectile.rotation);
        }
    }

    public sealed class SuperEnergyBarrageExplosion : ModProjectile
    {
        private float _sizeTimer;
        public override void SetDefaults()
        {
            projectile.width = 62;
            projectile.height = 62;
            projectile.aiStyle = 0;
            projectile.alpha = 70;
            projectile.timeLeft = 120;
            projectile.light = 1f;
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
                byte a2 = (byte)(100f * (b2 / 255f));
                return new Color(b2, b2, b2, a2);
            }
            return new Color(255, 255, 255, 100);
        }

        public override void AI()
        {
            if (_sizeTimer > 0)
            {
                projectile.scale = (_sizeTimer / 120f) * 2;
                _sizeTimer--;
            }
            else
            {
                projectile.scale = 0.5f;
            }
        }
    }
}
