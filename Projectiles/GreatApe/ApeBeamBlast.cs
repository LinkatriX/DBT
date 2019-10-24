using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Projectiles.GreatApe
{
    public class ApeBeamBlast : ModProjectile
    {
        public bool tailSpawned = false;
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chou Makouhou");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 168;
            projectile.height = 144;
			projectile.light = 1f;
            projectile.knockBack = 1.0f;
            projectile.alpha = 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.netUpdate = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
			projectile.aiStyle = 1;
            aiType = 14;
			projectile.tileCollide = false;

			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void AI()
        {
            if (projectile.alpha < 255)
            {
                projectile.ai[0]++;
                if (projectile.ai[0] == 2)
                {
                    projectile.alpha++;
                    projectile.ai[0] = 0;
                }
                    

            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            projectile.ai[1]++;
            if (projectile.ai[1] == 4)
            {
                if (tailSpawned)
                    Projectile.NewProjectile(projectile.getRect().Right, projectile.Center.Y - 0, 0, 0, mod.ProjectileType("ApeBeamTrail"), projectile.damage / 2, 4f, projectile.owner, 0, projectile.rotation);
                else if (!tailSpawned)
                {
                    Projectile.NewProjectile(projectile.getRect().Right, projectile.Center.Y - 0, 0, 0, mod.ProjectileType("ApeBeamTail"), projectile.damage / 2, 4f, projectile.owner, 0, projectile.rotation);
                    tailSpawned = true;
                }
                    

                projectile.ai[1] = 0;
                projectile.netUpdate = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

			return true;
        }
    }
}