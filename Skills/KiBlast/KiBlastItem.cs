using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.KiBlast
{
    public sealed class KiBlastItem : SkillItem<KiBlastProjectile>
    {
        public KiBlastItem() : base(SkillDefinitionManager.Instance.KiBlast, 20, 20, ItemRarityID.Blue, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 22;
            item.useTime = 22;
            item.useStyle = ItemUseStyleID.Stabbing;
        }

        public override bool Shoot(Player ply, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY + 2)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;

            return true;
        }
    }
}