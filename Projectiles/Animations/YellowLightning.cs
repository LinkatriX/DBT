using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles.Animations
{
    public class YellowLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 12;
        }

        public override void SetDefaults()
        {
            Player player = Main.player[projectile.owner];
            projectile.width = 90;
            projectile.height = 120;
            projectile.aiStyle = -1;
            projectile.timeLeft = 50;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.damage = 0;
            projectile.alpha = 30;
            projectile.light = 1f;
        }

        public override void AI()
        {
            projectile.velocity = new Vector2(0, 10);
            projectile.netUpdate = true;
            projectile.netUpdate2 = true;
            projectile.netImportant = true;

            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 12)
            {
                projectile.frame = 0;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}
