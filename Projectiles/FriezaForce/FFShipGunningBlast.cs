using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

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
			projectile.damage = 40;
			projectile.width = 10;
			projectile.height = 89;
			projectile.timeLeft = 120;
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
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 110);
		}
	}
}