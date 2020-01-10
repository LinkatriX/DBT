using DBT.Skills.KiBlast;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.HellzoneGrenade
{
    public sealed class HellzoneGrenadeItem : SkillItem<HellzoneGrenadeProjectile>
    {
        public HellzoneGrenadeItem() : base(SkillDefinitionManager.Instance.HellzoneGrenade, 20, 20, ItemRarityID.Lime, true)
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
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(35));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }
}