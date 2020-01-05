using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.KiBeam
{
    public sealed class KiBeamProjectile : SkillProjectile
    {
        public KiBeamProjectile() : base(SkillDefinitionManager.Instance.KiBeam, 6, 174)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 6;
            projectile.height = 6;

            projectile.aiStyle = 1;
            projectile.light = 1f;
            projectile.friendly = true;
            projectile.alpha = 220;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = true;
            projectile.penetrate = 3;

            aiType = 14;
            projectile.timeLeft = 60;

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override bool PreAI()
        {
            //Tile currTile = Main.tile[(int)projectile.position.X * 16, (int)projectile.position.Y * 16];
            if (hasCollided) 
            {
                projectile.timeLeft -= 3;
            }
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = oldVelocity;
            projectile.tileCollide = false;
            hasCollided = true;
            return false;
        }

        public bool hasCollided { get; set; } = false;
    }
}
