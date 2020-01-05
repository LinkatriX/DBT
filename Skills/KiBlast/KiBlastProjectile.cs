using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.KiBlast
{
    public sealed class KiBlastProjectile : SkillProjectile
    {
        public KiBlastProjectile() : base(SkillDefinitionManager.Instance.KiBlast, 12, 140)
        {
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 16;
            projectile.height = 16;

            projectile.aiStyle = 1;
            projectile.light = 0.7f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;

            aiType = 14;
            projectile.timeLeft = 80;
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

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 26, 26, 86, projectile.velocity.X, projectile.velocity.Y)];
                dust.color = new Color(158, 239, 255);
                dust.noGravity = true;
            }
        }
    }
}