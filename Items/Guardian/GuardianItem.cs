using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.Commons.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Guardian
{
    public abstract class GuardianItem : DBTItem, IIsGuardianItem
    {
        protected GuardianItem(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White) : base(displayName, tooltip, width, height)
        {
        }

        public override void PostModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, TOOLTIP_GUARDIAN_LINE_NAME, "- Guardian -") { overrideColor = new Color(69, 255, 56) };
            tooltips.Add(line);

            base.ModifyTooltips(tooltips);
        }

        public override bool CloneNewInstances => true;
    }
}
