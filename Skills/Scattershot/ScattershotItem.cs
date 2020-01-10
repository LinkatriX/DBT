using DBT.Skills.KiBlast;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.Scattershot
{
    public sealed class ScattershotItem : SkillItem<ScattershotProjectile>
    {
        public ScattershotItem() : base(SkillDefinitionManager.Instance.Scattershot, 20, 20, ItemRarityID.Lime, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = ItemUseStyleID.Stabbing;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25));
                // If you want to randomize the speed to stagger the projectiles
                float scale = 1f - (Main.rand.NextFloat() * .4f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 12f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
    }
}