using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles
{
    public class TransmissionLinesProjectile : ModProjectile
    {
        public bool isInitialized = false;
        private const int MAX_FRAMES = 18;
        public float scaleExtra;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = MAX_FRAMES;
        }

        public override void SetDefaults()
        {
            Player player = Main.player[projectile.owner];
            projectile.width = 30;
            projectile.height = 49;
            projectile.aiStyle = 0;
            projectile.timeLeft = 10;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.damage = 0;
            projectile.alpha = 50;
            scaleExtra = 0.1f;
            projectile.light = 0f;
        }

        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public void InitializeVanishState()
        {
            isInitialized = true;
        }

        public override void AI()
        {
            if (!isInitialized)
            {
                InitializeVanishState();
            }
            projectile.netUpdate = true;
            projectile.netUpdate2 = true;
            projectile.netImportant = true;

            projectile.frameCounter++;
            if (projectile.frameCounter > projectile.frame)
            {
                projectile.frame++;
            }
            if (projectile.frame >= MAX_FRAMES)
            {
                projectile.frame = 0;
            }

            projectile.scale = 1.0f + scaleExtra;
        }
    }
}
