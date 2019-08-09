using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles
{
    public abstract class DBTProjectile : ModProjectile
    {
        private readonly int _width, _height;

        protected DBTProjectile(int damage, float knockback, int width, int height)
        {
            _width = width;
            _height = height;

            Damage = damage;
            Knockback = knockback;
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = _width;
            projectile.height = _height;

            projectile.damage = Damage;
            projectile.knockBack = Knockback;
        }

        public override bool PreAI()
        {
            if (Owner == null)
                Owner = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();

            projectile.netUpdate = true;

            return base.PreAI();
        }

        
        public DBTPlayer Owner { get; protected set; }

        public override bool CloneNewInstances => true;

        public int Damage { get; }
        public float Knockback { get; }
    }
}