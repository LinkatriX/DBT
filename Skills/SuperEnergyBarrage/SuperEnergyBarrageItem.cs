using DBT.Skills.KiBlast;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.SuperEnergyBarrage
{
    public sealed class SuperEnergyBarrageItem : SkillItem<SuperEnergyBarrageProjectile>
    {
        public SuperEnergyBarrageItem() : base(SkillDefinitionManager.Instance.SuperEnergyBarrage, 20, 20, ItemRarityID.Lime, true)
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
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }
}