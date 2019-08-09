using System.Collections.Generic;
using DBT.Commons.Items;
using DBT.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items
{
    public abstract class DBTItem : ModItem, IHasValue, IHasDefense, IHasRarity
    {
        public const string 
            TOOLTIP_PATREON_LINE_NAME = "DBT_ToolTip_Patreon",
            TOOLTIP_GUARDIAN_LINE_NAME = "DBT_ToolTip_Guardian",
            TERRARIA_DESCRIPTION_TOOLTIP = "Tooltip0";

        private readonly string _displayName, _tooltip;
        private readonly int _width, _height;

        protected DBTItem(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White)
        {
            _displayName = displayName;
            _tooltip = tooltip;

            _width = width;
            _height = height;

            Value = value;
            Defense = defense;
            Rarity = rarity;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault(_displayName);
            Tooltip.SetDefault(_tooltip);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = _width;
            item.height = _height;

            item.value = Value;
            item.defense = Defense;
            item.rare = Rarity;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //Tooltip.SetDefault(_tooltip);

            PostModifyTooltips(tooltips);
            base.ModifyTooltips(tooltips);
        }

        public virtual void PostModifyTooltips(List<TooltipLine> tooltips)
        {
            IIsGuardianItem guardianItem = this as IIsGuardianItem;

            if (guardianItem != null)
                tooltips.Add(new TooltipLine(mod, TOOLTIP_GUARDIAN_LINE_NAME, "- Guardian -")
                {
                    overrideColor = Color.LimeGreen
                });


            IIsPatreonLocked patreonLocked = this as IIsPatreonLocked;

            if (patreonLocked != null)
            {
                if (patreonLocked.IsDonator())
                {
                    tooltips.Add(new TooltipLine(mod, TOOLTIP_PATREON_LINE_NAME, "You donated for this item!")
                    {
                        overrideColor = Color.Orange
                    });
                }
                else
                {
                    bool endsWithS = patreonLocked.Donator.DisplayName.ToLower()[patreonLocked.Donator.DisplayName.Length - 1] == 's';

                    tooltips.Add(new TooltipLine(mod, TOOLTIP_PATREON_LINE_NAME, patreonLocked.Donator.DisplayName + (endsWithS ? " " : "'s ") + "Patreon item")
                    {
                        overrideColor = Color.Orange
                    });
                }
            }
        }

        public int Value { get; }
        public int Defense { get; }
        public int Rarity { get; }
    }
}
