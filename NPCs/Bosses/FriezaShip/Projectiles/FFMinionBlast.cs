using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using DBT.Utilities;
using DBT.Helpers;

namespace DBT.NPCs.Bosses.FriezaShip.Projectiles
{
    public class FFMinionBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 24;
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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frieza Force Minion Blast");
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