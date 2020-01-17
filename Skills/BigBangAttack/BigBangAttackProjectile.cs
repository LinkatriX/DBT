using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.BigBangAttack
{
    public sealed class BigBangAttackProjectile : SkillProjectile
    {
        public BigBangAttackProjectile() : base(SkillDefinitionManager.Instance.BigBangAttack, 120, 120)
        {
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = 1;
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = false;
            projectile.stepSpeed = 13;
            projectile.penetrate = 1;
            aiType = 14;
            projectile.timeLeft = 200;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 100);
        }
        public override void OnChargeAttack()
        {
            projectile.timeLeft = 200;
            projectile.position = GetChargeBallPosition();
            Main.player[projectile.owner].velocity = Vector2.Zero;
            projectile.scale = 1f * (projectile.scale * ChargeTimer / 120);
            Dust dust = Main.dust[Dust.NewDust(projectile.position, 8, 8, 92, projectile.velocity.X, projectile.velocity.Y)];
            dust.noGravity = true;

        }
        public override void OnFireAttack()
        {
            projectile.velocity = Vector2.Normalize(Main.MouseWorld - projectile.position) * Definition.Characteristics.BaseShootSpeed;
            projectile.timeLeft = 200;
            projectile.ai[1] = 1;
            IsFired = true;
            projectile.width *= (int)projectile.scale;
            projectile.height *= (int)projectile.scale;
        }
        public override void PostAI()
        {
            channelingOffset = new Vector2(-40f, -90f);
            
            if (IsFired)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, 8, 8, 92, projectile.velocity.X, projectile.velocity.Y)];
                    dust.noGravity = true;
                }
            }
        }
    }
}