using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskProjectile : SkillProjectile
    {
        public DestructoDiskProjectile() : base(SkillDefinitionManager.Instance.DestructoDisk, 124, 60)
        {
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = 56; //Perfect ai for gravless and rotation, useful for disks
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = false;
            projectile.stepSpeed = 13;
            projectile.penetrate = 200;
            aiType = 14;
            projectile.timeLeft = 140;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 100);
        }

        public override void OnChargeAttack()
        {
            Main.projFrames[projectile.type] = 12;
            ChargeOverrideTexture = mod.GetTexture("Skills/DestructoDisk/DestructoDiskCharge");
            projectile.ai[0]++;
            if (projectile.ai[0] >= Definition.Characteristics.ChargeCharacteristics.BaseChargeTimer / 12)
            {
                if (projectile.frame < 12)
                {
                    projectile.frame++;
                }
                projectile.ai[0] = 0;
            }
                
        }

        public override void OnFireAttack()
        {
            Main.projFrames[projectile.type] = 3;
            ChargeOverrideTexture = mod.GetTexture("Skills/DestructoDisk/DestructoDiskProjectile");
            projectile.velocity = Vector2.Normalize(Main.MouseWorld - projectile.position) * 13;
        }

        public override void PostAI()
        {
            channelingOffset = new Vector2(0f, -40f);
            if (IsFired)
            {
                for (int d = 0; d < 1; d++)
                {
                    if (Main.rand.NextFloat() < 1f)
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, 72, 72, 169, 0f, 0f, 0, new Color(255, 255, 255), 1.5f);
                        dust.noGravity = true;
                    }

                }

                projectile.frameCounter++;
                if (projectile.frameCounter > 3)
                {
                    projectile.frame++;
                }
                if (projectile.frame >= 3)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}