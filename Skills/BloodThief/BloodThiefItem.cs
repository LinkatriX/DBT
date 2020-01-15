using DBT.Skills.KiBlast;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.BloodThief
{
    public sealed class BloodThiefItem : SkillItem<BloodThiefProjectile>
    {
        public BloodThiefItem() : base(SkillDefinitionManager.Instance.BloodThief, 20, 20, ItemRarityID.Green, false)
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
            type = mod.ProjectileType("BloodThiefProjectile");
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}