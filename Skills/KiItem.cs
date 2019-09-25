using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DBT.Players;
using DBT.Skills;

namespace DBTMod.Skills
{
    public abstract class KiItem : ModItem
    {
        internal Player player;
        public bool isFistWeapon;
        public bool canUseHeavyHit;
        public float kiDrain;
        public string weaponType;

        public override void SetDefaults()
        {
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }

        public override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback = knockback + player.GetModPlayer<DBTPlayer>().kiKbAddition;
            //base.GetWeaponKnockback(player, ref knockback);
        }

        public override void ModifyWeaponDamage(Player player, ref float damage, ref float mult, ref float flat)
        {
            damage = (int)Math.Ceiling(damage * player.GetModPlayer<DBTPlayer>().kiDamage);
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            if (this is BaseBeamItem)
            {
                return false;
            }
            return base.CanHitNPC(player, target);
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.GetModPlayer<DBTPlayer>().kiCrit;
        }

        public override float UseTimeMultiplier(Player player)
        {
            return player.GetModPlayer<DBTPlayer>().kiSpeedAddition;
        }

        public int RealKiDrain(Player player)
        {
            return (int)(kiDrain * player.GetModPlayer<DBTPlayer>().kiDrainMulti);
        }

        public override bool CanUseItem(Player player)
        {
            return true;//Temporary for testing.
        }

        public override bool UseItem(Player player)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine indicate = new TooltipLine(mod, "", "");
            string[] text = indicate.text.Split(' ');
            indicate.text = "Consumes " + RealKiDrain(Main.LocalPlayer) + " Ki ";
            indicate.overrideColor = new Color(34, 232, 222);
            tooltips.Add(indicate);
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.text = damageValue + " ki " + damageWord;
            }
            TooltipLine indicate2 = new TooltipLine(mod, "", "");
            string[] text2 = indicate.text.Split(' ');
            indicate2.text = weaponType + " Technique ";
            indicate2.overrideColor = new Color(232, 202, 34);
            tooltips.Add(indicate2);
            if (item.damage > 0)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.mod == "Terraria" && line.Name == "Tooltip")
                    {
                        line.overrideColor = Color.Cyan;
                    }
                }
            }
        }
    }
}
