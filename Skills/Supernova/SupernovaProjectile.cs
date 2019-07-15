using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills.Supernova
{
    public sealed class SupernovaProjectile : SkillProjectile
    {
        public SupernovaProjectile() : base(SkillDefinitionManager.Instance.Supernova, 226, 226)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.light = 1f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public float HeldTime
        {
            get
            {
                return projectile.ai[0];
            }
            set
            {
                projectile.ai[0] = value;
            }
        }

        //private bool _isInitialized = false;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            ModPlayer modPlayer = player.GetModPlayer<ModPlayer>();

            /*if (!_isInitialized)
            {
                modPlayer.isMassiveBlastCharging = true;
                //modPlayer.isMassiveBlastInUse = true;
                HeldTime = 1;
                _isInitialized = true;
            }*/

            // cancel channeling if the projectile is maxed
            if (player.channel && projectile.scale > 2.5)
            {
                player.channel = false;
            }

            if (player.channel)
            {
                projectile.scale = BASE_SCALE + SCALE_INCREASE * HeldTime;
                Vector2 projectileOffset = new Vector2(-projectile.width * 0.5f, -projectile.height * 0.5f);
                projectileOffset += new Vector2(0, -(80 + projectile.scale * 115f));
                projectile.position = player.Center + projectileOffset;
                HeldTime++;

                //Rock effect
                projectile.ai[1]++;
                if (projectile.ai[1] % 7 == 0)
                    Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloat(-500, 600), projectile.Center.Y + 1000, 0, -10, mod.ProjectileType("StoneBlockDestruction"), projectile.damage, 0f, projectile.owner);
                Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloat(-500, 600), projectile.Center.Y + 1000, 0, -10, mod.ProjectileType("DirtBlockDestruction"), projectile.damage, 0f, projectile.owner);

                if (projectile.timeLeft < 399)
                {
                    projectile.timeLeft = 400;
                }

                MyPlayer.ModPlayer(player).AddKi(-2, true, false);
                player.ApplyChannelingSlowdown();

                // depleted check, release the ball
                if (MyPlayer.ModPlayer(player).IsKiDepleted())
                {
                    player.channel = false;
                }
                if (_soundtimer == 0)
                {
                    _soundInfo = SoundHelper.PlayCustomSound("Sounds/SuperNovaCharge", player, 0.6f);
                }
                _soundtimer++;
                if (_soundtimer > 120)
                {
                    _soundtimer = 0;
                }
            }
            else if (modPlayer.isMassiveBlastCharging)
            {
                modPlayer.isMassiveBlastCharging = false;
                float projectileWidthFactor = projectile.width * projectile.scale / TRAVEL_SPEED_COEFFICIENT;
                projectile.timeLeft = (int)Math.Ceiling(projectileWidthFactor) + 180;
                projectile.velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * TRAVEL_SPEED_COEFFICIENT;
                projectile.tileCollide = false;
                projectile.damage *= (int)Math.Ceiling(projectile.scale / 3f);
                _soundInfo = SoundHelper.KillTrackedSound(_soundInfo);
                SoundHelper.PlayCustomSound("Sounds/SuperNovaThrow", player, 0.6f);
            }
            projectile.netUpdate = true;
            projectile.netUpdate2 = true;
        }

        public override void OnHitNPC(NPC npc, int damage, float knockback, bool crit)
        {
            projectile.scale -= 0.025f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float radius = (float)projectile.width * projectile.scale / 2f;
            float rSquared = radius * radius;

            return rSquared > Vector2.DistanceSquared(Vector2.Clamp(projectile.Center, targetHitbox.TopLeft(), targetHitbox.BottomRight()), projectile.Center);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            int radius = (int)Math.Ceiling(projectile.width / 2f * projectile.scale);
            DBZMOD.circle.ApplyShader(radius);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}
