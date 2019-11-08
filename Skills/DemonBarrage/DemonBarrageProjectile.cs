using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.DemonBarrage
{
    public sealed class DemonBarrageProjectile : SkillProjectile
    {
        public DemonBarrageProjectile() : base(SkillDefinitionManager.Instance.DemonBarrage, 18, 18)
        {
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.light = 0.7f;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netUpdate = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            aiType = 14;
            projectile.timeLeft = 200;
        }
        int dustType = 0;
        int dustPos = 0;
        int element = 0;
        bool decreasing = false;
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, Scale: 1.3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale -= 0.005f;
            }

            if (element == 0)
                element = Main.rand.Next(1, 5);

            projectile.ai[0]++;
            if (projectile.ai[0] == 2)
            {
                if (element == 1)
                    dustType = 90;
                if (element == 2)
                    dustType = 185;
                if (element == 3)
                    dustType = 74;
                if (element == 4)
                    dustType = 27;

                if (dustPos < 20 && !decreasing)
                    dustPos += 2;

                if (dustPos == 20)
                    decreasing = true;
                if (dustPos == 0)
                    decreasing = false;
                if (decreasing && dustPos != 0)
                    dustPos -= 2;

                int dust2 = Dust.NewDust(new Vector2(projectile.Center.X - 4, projectile.Center.Y - 4 - dustPos), 0, 0, dustType, Scale: 1f);
                Main.dust[dust2].velocity *= 0;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].scale -= 0.05f;

                int dust3 = Dust.NewDust(new Vector2(projectile.Center.X - 4, projectile.Center.Y - 4 + dustPos), 0, 0, dustType, Scale: 1f);
                Main.dust[dust3].velocity *= 0;
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].scale -= 0.05f;
                projectile.ai[0] = 0;
            }
        }
    }
}