using DBT.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            projectile.tileCollide = true;
            projectile.aiStyle = 0;
            projectile.light = 1f;
            projectile.timeLeft = 175;
            aiType = 0;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.netUpdate = true;
            projectile.alpha = 50;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void AI()
        {
            projectile.velocity.Y = -20;

            foreach (NPC target in Main.npc)
            {
                if (projectile.Hitbox.Intersects(target.getRect()))
                {
                    HasCollidedNPC = true;
                }
            }

            if (projectile.timeLeft >= 140)
            {
                if (projectile.velocity.X < 4.2f && projectile.velocity.X > 0)
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
                if (!HasCollidedNPC)
                {
                    projectile.DoHoming(1000f, 20f, false);
                    IsHoming = true;
                }
                else
                {
                    IsHoming = false;
                    projectile.velocity = projectile.oldVelocity;
                    projectile.timeLeft -= 4;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (IsHoming || HasCollidedNPC)
            {
                projectile.tileCollide = false;
            }
            else
            {
                projectile.Kill();
            }
            if (HasCollidedNPC) 
            {
                projectile.velocity = projectile.oldVelocity;
                projectile.timeLeft -= 4;
            }
            return false;
        }

        public override void PerChargeLevel()
        {
            Definition.Characteristics.ChargeCharacteristics.BaseCastKiDrain += 100;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {//This code creates the trails made by the projectile.
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / projectile.oldPos.Length);
                color.A = 50;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public bool HasCollidedNPC { get; set; } = false;
        public bool IsHoming { get; set; } = false;
    }
}