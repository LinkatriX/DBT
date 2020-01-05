﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.BloodThief
{
    public sealed class BloodThiefProjectile : SkillProjectile
    {
        public BloodThiefProjectile() : base(SkillDefinitionManager.Instance.BloodThief, 22, 40)
        {
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 40;
            projectile.aiStyle = 1;
            projectile.light = 0.7f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            aiType = 14;
            projectile.timeLeft = 100;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public int collisionTimer = 5;
        public bool alphaTicking = false;
        public Vector2 originalVelocity = default(Vector2);

        bool _init = false;
        public void Initialize()
        {
            if (projectile.position == default(Vector2)) return;
            originalVelocity = projectile.velocity;
            _init = true;
        }

        public override void AI()
        {
            if (!_init) Initialize();
            if (alphaTicking)
            {
                if (projectile.tileCollide) projectile.velocity = originalVelocity;
                projectile.velocity *= 0.9f;
                projectile.alpha = Math.Min(255, projectile.alpha + 10);
                if (Main.myPlayer == projectile.owner && projectile.alpha >= 255) projectile.Kill();
            }
            else
            if (projectile.alpha > 0) { projectile.alpha = Math.Max(0, projectile.alpha - 10); }
            collisionTimer = Math.Max(0, collisionTimer - 1);
            projectile.tileCollide = !alphaTicking && collisionTimer == 0;
            if (Main.netMode != 2)
            {
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            alphaTicking = true;
            return false;
        }

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            Player player = Main.LocalPlayer;
            player.statLife += 5;
            player.HealEffect(5);
            projectile.Kill();
            base.OnHitNPC(npc, damage, knockback, crit);
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

        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.timeLeft < 85)
            {
                byte b2 = (byte)(projectile.timeLeft * 3);
                byte a2 = (byte)(100f * ((float)b2 / 255f));
                return new Color((int)b2, (int)b2, (int)b2, (int)a2);
            }
            return new Color(255, 255, 250, 400);
        }
    }
}
