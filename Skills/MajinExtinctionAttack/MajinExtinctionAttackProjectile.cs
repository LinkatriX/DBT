using DBT.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.MajinExtinctionAttack
{
    public class MajinExtinctionAttackProjectile : SkillProjectile
    {
        public MajinExtinctionAttackProjectile() : base(SkillDefinitionManager.Instance.MajinExtinctionAttack, 24, 152) 
        {
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.aiStyle = 0;
            projectile.light = 1f;
            projectile.timeLeft = 175;
            aiType = 0;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.netUpdate = true;
            projectile.alpha = 50;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;//9
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        /*int dustType = 0;
        int dustPos = 0;
        int element = 0;
        bool decreasing = false;
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, Scale: 1.3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale -= 0.005f;
            }

            if (element == 0)
                element = Main.rand.Next(1, 5);

            projectile.ai[0]++;
            if (projectile.ai[0] == 2)
            {
                if (element == 1)
                    dustType = 90;
                if (element == 2)
                    dustType = 185;
                if (element == 3)
                    dustType = 74;
                if (element == 4)
                    dustType = 27;

                if (dustPos < 20 && !decreasing)
                    dustPos += 2;

                if (dustPos == 20)
                    decreasing = true;
                if (dustPos == 0)
                    decreasing = false;
                if (decreasing && dustPos != 0)
                    dustPos -= 2;

                int dust2 = Dust.NewDust(new Vector2(projectile.Center.X - 4, projectile.Center.Y - 4 - dustPos), 0, 0, dustType, Scale: 1f);
                Main.dust[dust2].velocity *= 0;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale -= 0.05f;

                int dust3 = Dust.NewDust(new Vector2(projectile.Center.X - 4, projectile.Center.Y - 4 + dustPos), 0, 0, dustType, Scale: 1f);
                Main.dust[dust3].velocity *= 0;
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].scale -= 0.05f;
                projectile.ai[0] = 0;
            }
        }*/

        public override void AI() 
        {
            Player player = Main.player[projectile.owner];

            //Random randVelocityX = new Random();
            projectile.velocity.Y = -22;//-15

            if (projectile.velocity.X != 0 && !hasCollided)
            {
                if (projectile.timeLeft >= 140f)//120
                {
                    if (projectile.velocity.X < 4.2f && projectile.velocity.X > 0)//3.2
                    {
                        projectile.velocity.X -= 0.05f;
                    }
                    if (projectile.velocity.X > -4.2f && projectile.velocity.X < 0)
                    {
                        projectile.velocity.X += 0.2f;
                    }
                }
                else
                {
                    if (!hasCollided)
                    {
                        projectile.DoHoming(1000f, 20f, false);
                    }
                    else 
                    {
                        projectile.velocity = projectile.oldVelocity;
                        projectile.timeLeft -= 4;
                    }
                }
            }
            else //Secondary check incase X velocity == 0
            {
                if (!hasCollided)
                {
                    projectile.DoHoming(1000f, 20f, false);
                }
                else
                {
                    projectile.velocity = projectile.oldVelocity;
                    projectile.timeLeft -= 4;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Main.player[projectile.owner].position, 32, 50, 86, 0, -5f)];
                dust.noGravity = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            hasCollided = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                color.A = 50;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        
        public bool hasCollided { get; set; } = false;
    }
}