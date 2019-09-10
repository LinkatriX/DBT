using Terraria.ID;

namespace DBT.Items.Materials
{
    public class KatchinScale : DBTItem
    {
        public KatchinScale() : base("Katchin Scale", "A scale with incredible durability ripped from a massive fish.", 14, 16, 2500, 0, ItemRarityID.Lime)
        {
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Katchin Scale");
            Tooltip.SetDefault("'A scale with incredible durability ripped from a massive fish.'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 9999;
        }
    }
}