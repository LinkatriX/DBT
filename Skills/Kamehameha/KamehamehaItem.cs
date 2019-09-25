using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills.Kamehameha
{
    //Old way we structured the item for reference.
    public class KamehamehaItem : ModItem
    {
        public override void SetDefaults()
        {
            item.shoot = mod.ProjectileType("KamehamehaCharge");
            item.shootSpeed = 0f;
            item.damage = 88;
            item.knockBack = 2f;
            item.useStyle = 5;
            item.UseSound = SoundID.Item12;
            item.channel = true;
            item.useAnimation = 100;
            item.useTime = 100;
            item.width = 40;
            item.noUseGraphic = true;
            item.height = 40;
            item.autoReuse = false;
            item.value = 10500;
            item.rare = 3;
        }
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Maximum Charges = 6\nHold Right Click to Charge\nHold Left Click to Fire");
            DisplayName.SetDefault("Kamehameha");
        }

        public override void UseStyle(Player player)
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
        }
    }

    /*public sealed class KamehamehaItem : SkillItem<KamehamehaProjectile>
    {
        public KamehamehaItem() : base(SkillDefinitionManager.Instance.Kamehameha, 20, 20, ItemRarityID.Lime, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 16;
            item.useTime = 16;
            item.useStyle = ItemUseStyleID.Stabbing;
        }

        public override void UseStyle(Player player)
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
        }
    }*/
}