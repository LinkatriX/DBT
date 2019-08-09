using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.KiBeam
{
    public sealed class KiBeamProjectile : SkillProjectile
    {
        bool hasCollided = false;

        public KiBeamProjectile() : base(SkillDefinitionManager.Instance.KiBeam, 6, 6)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.aiStyle = 1;
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.alpha = 220;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;

            aiType = 14;
            projectile.timeLeft = 60;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return this.hasCollided = true;
        }

        public override void PostAI()
        {
            if (this.hasCollided == true)
            {
                projectile.timeLeft -= 3;
            }
        }

        /*public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft -= 3;

            return base.OnTileCollide(oldVelocity);
        }*/

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }*/
    }
}
