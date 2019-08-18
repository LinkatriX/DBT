using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Projectiles.FriezaForce
{
	public sealed class FFShipGunningBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frieza Force Ship Cannon Projectile");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 89;
			projectile.timeLeft = 240;
			projectile.penetrate = 1;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.friendly = false;
			projectile.magic = true;
			projectile.hostile = true;
			projectile.aiStyle = 101;
			projectile.light = 1f;
			projectile.stepSpeed = 13;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			projectile.netUpdate = true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.position, 26, 26, 86, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 255, 255), 1.3f)];
				dust.noGravity = true;
			}

			for (int i = 0; i < 3; i++)
			{
				float scaleFactor = 3f;

				for (int j = 0; j <= 4; j++)
				{
					int numGore = Gore.NewGore(projectile.oldPosition, default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[numGore].velocity *= scaleFactor;

					Gore gore1 = Main.gore[numGore];
					gore1.velocity.X = gore1.velocity.X + 1f;

					Gore gore2 = Main.gore[numGore];
					gore2.velocity.Y = gore2.velocity.Y + 1f;
				}
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 110);
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