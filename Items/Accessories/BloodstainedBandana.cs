using Terraria.ID;
using Terraria;

namespace DBT.Items.Accessories
{
    public sealed class BloodstainedBandana : DBTItem
    {
        public BloodstainedBandana() : base("Bloodstained Bandana", "'Change the future'\n14% Increased Ki damage\nThorns effect", 
            18, 30, value: Item.buyPrice(gold: 2, silver: 80), defense: 2, rarity: ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }
    }
}