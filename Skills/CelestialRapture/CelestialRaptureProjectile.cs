using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using System;

namespace DBT.Skills.CelestialRapture
{
    public sealed class CelestialRaptureProjectile : SkillProjectile
    {
        public CelestialRaptureProjectile() : base(SkillDefinitionManager.Instance.CelestialRapture, 20, 20)
        {
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.aiStyle = 1;
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;

            aiType = 14;
            projectile.timeLeft = 160;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
    }
}
