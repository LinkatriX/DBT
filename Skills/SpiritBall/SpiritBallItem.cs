﻿using DBT.Skills.KiBlast;
using Terraria.ID;

namespace DBT.Skills.SpiritBall
{
    public sealed class SpiritBallItem : SkillItem<KiBlastProjectile>
    {
        public SpiritBallItem() : base(SkillDefinitionManager.Instance.SpiritBall, 20, 20, ItemRarityID.Lime, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = ItemUseStyleID.Stabbing;
        }

        /*public override void UseStyle(Player player)
        {
            player.itemLocation.X = player.position.X + (float)player.width * 0.5f;// - (float)Main.itemTexture[item.type].Width * 0.5f;// - (float)(player.direction * 2);
            player.itemLocation.Y = player.MountedCenter.Y + player.gravDir * (float)Main.itemTexture[item.type].Height * 0.5f;
            float relativeX = (float)Main.mouseX + Main.screenPosition.X - player.Center.X;
            float relativeY = (float)Main.mouseY + Main.screenPosition.Y - player.Center.Y;
            if (player.gravDir == -1f)
                relativeY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - player.Center.Y;
            if (relativeX - relativeY > 0)
            {
                if (relativeX + relativeY > 0)
                {
                    player.itemRotation = 0;
                }
                else
                {
                    player.itemRotation = player.direction * -MathHelper.Pi / 2;
                    player.itemLocation.X += player.direction * 2;
                    player.itemLocation.Y -= 10;
                }
            }
            else
            {
                if (relativeX + relativeY > 0)
                {
                    player.itemRotation = player.direction * MathHelper.Pi / 2;
                    player.itemLocation.X += player.direction * 2;
                    Main.rand.Next(0, 100);
                }
                else
                {
                    player.itemRotation = 0;
                }
            }
        }*/
    }
}