using System.Collections.Generic;
using DBT.Items;
using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Skills
{
    public abstract class SkillItem<TProjectile> : DBTItem where TProjectile : SkillProjectile
    {
        public const string 
            TOOLTIP_KI_CAST_CONSUMPTION_LINE_NAME = "DBT_ToolTip_Ki_Cast_Consumption",
            TOOLTIP_KI_CHARGE_CONSUMPTION_LINE_NAME = "DBT_Tooltip_Ki_Charge_Consumption";

        protected SkillItem(SkillDefinition definition, int width, int height, int rarity, bool autoReuse) : base(definition.DisplayName, definition.Description, width, height, rarity: rarity)
        {
            Definition = definition;

            AutoReuse = autoReuse;
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.noUseGraphic = true;

            item.shoot = mod.ProjectileType<TProjectile>();
            item.shootSpeed = Definition.Characteristics.BaseShootSpeed;

            item.damage = (int) Definition.Characteristics.BaseDamage;
            item.autoReuse = AutoReuse;

            item.knockBack = Definition.Characteristics.BaseKnockback;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            int tooltipIndex = tooltips.IndexOf(tooltips.Find(t => t.Name == TERRARIA_DESCRIPTION_TOOLTIP)) - 1;

            float castKiDrain = Definition.Characteristics.ChargeCharacteristics.GetCastKiDrain(dbtPlayer);

            if (castKiDrain > 0f)
                tooltips.Insert(++tooltipIndex, new TooltipLine(mod, TOOLTIP_KI_CAST_CONSUMPTION_LINE_NAME, "Uses " + castKiDrain + " Ki"));

            float chargeKiDrain = Definition.Characteristics.ChargeCharacteristics.GetChargeTickKiDrain(dbtPlayer);
            if (chargeKiDrain > 0)
                tooltips.Insert(++tooltipIndex, new TooltipLine(mod, TOOLTIP_KI_CHARGE_CONSUMPTION_LINE_NAME, "Consumes " + chargeKiDrain * Constants.TICKS_PER_SECOND + "/s while charging"));
                
            base.ModifyTooltips(tooltips);
        }


        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            // TODO Add charges.
            item.shootSpeed = Definition.Characteristics.GetShootSpeed(dbtPlayer, 1);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult)
        {
            add = Definition.Characteristics.BaseDamage - Definition.Characteristics.GetDamage(player.GetModPlayer<DBTPlayer>(), 0);

            base.ModifyWeaponDamage(player, ref add, ref mult);
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            Definition.Characteristics.GetKnockback(player.GetModPlayer<DBTPlayer>(), ref knockback, 1);

            base.GetWeaponKnockback(player, ref knockback);
        }


        public override bool CanUseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            // TODO Add check for skill unlocked.

            bool hasKi = dbtPlayer.Ki >= Definition.Characteristics.ChargeCharacteristics.GetCastKiDrain(dbtPlayer);
            if (hasKi)
                dbtPlayer.ModifyKi(-Definition.Characteristics.ChargeCharacteristics.GetCastKiDrain(dbtPlayer), Definition.Characteristics.ChargeCharacteristics.GetBaseKiRegenerationHaltedForDuration(dbtPlayer));

            return base.CanUseItem(player) && hasKi;
        }


        public SkillDefinition Definition { get; }

        public bool AutoReuse { get; }
    }
}