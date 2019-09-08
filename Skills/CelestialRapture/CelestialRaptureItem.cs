using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.CelestialRapture
{
    public sealed class CelestialRaptureItem : SkillItem<CelestialRaptureProjectile>
    {
        public CelestialRaptureItem() : base(SkillDefinitionManager.Instance.CelestialRapture, 28, 30, ItemRarityID.Expert, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 12;
            item.useTime = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        //Couldn't figure out how to improve this code :ech:
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 30f * 0.0174f;
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle = 1;
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(position.X, position.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 4f), type, damage, knockBack, Main.myPlayer);
                Projectile.NewProjectile(position.X, position.Y, (float)(-Math.Sin(offsetAngle) * 3f), (float)(-Math.Cos(offsetAngle) * 3f), type, damage, knockBack, Main.myPlayer);
                Projectile.NewProjectile(position.X, position.Y, (float)(Math.Sin(offsetAngle) * 2f), (float)(Math.Cos(offsetAngle) * 2f), type, damage, knockBack, Main.myPlayer);
                Projectile.NewProjectile(position.X, position.Y, (float)(-Math.Sin(offsetAngle) * 6f), (float)(-Math.Cos(offsetAngle) * 6f), type, damage, knockBack, Main.myPlayer);
                Projectile.NewProjectile(position.X, position.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), type, damage, knockBack, Main.myPlayer);
                Projectile.NewProjectile(position.X, position.Y, (float)(-Math.Sin(offsetAngle) * 1f), (float)(-Math.Cos(offsetAngle) * 1f), type, damage, knockBack, Main.myPlayer);
            }
            return false;
        }
    }
}
