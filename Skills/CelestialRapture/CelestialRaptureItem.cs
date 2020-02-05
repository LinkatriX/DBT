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
    }
}
