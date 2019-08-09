﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskProjectile : SkillProjectile
    {
        public DestructoDiskProjectile() : base(SkillDefinitionManager.Instance.DestructoDisk, 74, 74)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.aiStyle = 56; //Perfect ai for gravless and rotation, useful for disks
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = false;
            projectile.stepSpeed = 13;
            projectile.penetrate = 200;

            aiType = 14;
            projectile.timeLeft = 80;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 100);
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
        public override void PostAI()
        {
            for (int d = 0; d < 1; d++)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust = Dust.NewDustDirect(projectile.position, 72, 72, 169, 0f, 0f, 0, new Color(255, 255, 255), 1.5f);
                    dust.noGravity = true;
                }

            }
        }
    }
}