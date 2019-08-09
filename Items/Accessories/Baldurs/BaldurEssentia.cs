using Terraria.ID;
using Terraria;

namespace DBT.Items.Accessories.Baldurs
{
    public sealed class BaldurEssentia : BaldurItem
    {
        public BaldurEssentia() : base("Baldur Essentia", "'The essence of strong defense.'\nCharging grants a protective barrier that grants drastically increased defense",
            Item.buyPrice(silver: 3), 6, ItemRarityID.LightRed, 0.3f)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }
    }
}