
using DBT.Items;
using DBT.Players;
using DBTMod.Skills;
using Terraria;
using Terraria.ID;

namespace DBT.Skills
{
    public abstract class BaseBeamItem : KiItem
    {
        public override void SetDefaults()
        {
            item.shootSpeed = 0f;
            item.damage = 0;
            item.knockBack = 2f;
            item.useStyle = 5;
            item.channel = true;
            item.useAnimation = 2;
            item.useTime = 2;
            item.width = 40;
            item.noUseGraphic = true;
            item.height = 40;
            item.autoReuse = false;
            item.value = 120000;
            item.rare = 8;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Abstract Beam Item");
            DisplayName.SetDefault("Base Beam Item");
        }

        public override void HoldItem(Player player)
        {
            // set the ki weapon held var
            player.GetModPlayer<DBTPlayer>().isHoldingKiWeapon = true;
        }

        // this is important, don't let the player left click, basically.
        public override bool CanUseItem(Player player)
        {
            player.channel = true;
            if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == player.whoAmI)
            {
                float weaponDamage = item.damage;
                ModifyWeaponDamage(player, ref weaponDamage, ref weaponDamage, ref weaponDamage);
                Projectile.NewProjectileDirect(player.position, player.position, item.shoot, (int)weaponDamage, item.knockBack, player.whoAmI);
            }

            return false;
        }
    }
}
