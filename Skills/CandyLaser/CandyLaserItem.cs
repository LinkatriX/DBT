using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.CandyLaser
{
    public sealed class CandyLaserItem : SkillItem<CandyLaserProjectile>
    {
        public CandyLaserItem() : base(SkillDefinitionManager.Instance.CandyLaser, 20, 20, ItemRarityID.Lime, false)
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
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 20f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType(nameof(CandyLaserProjectile))] > 1)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
    }
}