using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.KiBeam
{
    public sealed class KiBeamItem : SkillItem<KiBeamProjectile>
    {
        public KiBeamItem() : base(SkillDefinitionManager.Instance.KiBeam, 40, 40, ItemRarityID.Blue, true)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 24;
            item.useTime = 24;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 55f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }*/
    }
}
