﻿using DBT.Items.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills.CandyLaser
{
    public sealed class CandyLaserProjectile : SkillProjectile
    {
        public CandyLaserProjectile() : base(SkillDefinitionManager.Instance.CandyLaser, 24, 28) 
        {
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SwordBeam);
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.aiStyle = 1;
            projectile.light = 1f;
            projectile.timeLeft = 220;
            aiType = 14;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.netUpdate = true;
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

        public override void AI()
        {
            projectile.ai[1]++;
            if (projectile.ai[1] == 2)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 0, 0, 0, mod.ProjectileType(nameof(CandyLaserTrail)), projectile.damage / 3, 4f, projectile.owner, 0, projectile.rotation);
                projectile.ai[1] = 0;
                projectile.netUpdate = true;
            }
        }

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            if (!npc.boss && !(npc.type == NPCID.DungeonGuardian) && npc.life > 1)
            {
                npc.life = 0;
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ChocoSelect(), 1);
                base.OnHitNPC(npc, damage, knockback, crit);
                projectile.Kill();
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.statLife = 0;
            Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, ChocoSelect(), 1);
            projectile.Kill();
        }

        private int ChocoSelect()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return mod.ItemType(nameof(Choco1));
                case 1:
                    return mod.ItemType(nameof(Choco2));
                case 2:
                    return mod.ItemType(nameof(Choco3));
                case 3:
                    return mod.ItemType(nameof(CoffeeCandy));
                default:
                    return 0;

            }
        }
    }

    public class CandyLaserTrail : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SwordBeam);
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.width = 16;
            projectile.height = 28;
            projectile.aiStyle = 1;
            projectile.light = 1f;
            projectile.timeLeft = 60;
            aiType = 14;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            projectile.netUpdate = true;
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

        public override void AI()
        {
            projectile.rotation = projectile.ai[1];
            projectile.localAI[0] += 1f;
            projectile.alpha = (int)projectile.localAI[0] * 2;

            if (projectile.localAI[0] > 130f)
            {
                projectile.Kill();
            }
        }
    }
}
