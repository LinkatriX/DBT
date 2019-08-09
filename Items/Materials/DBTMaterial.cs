namespace DBT.Items.Materials
{
    public class DBTMaterial : DBTItem
    {
        protected DBTMaterial(string displayName, string tooltip, int width, int height, int value, int rarity) : base(displayName, tooltip, width, height, value, rarity: rarity)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 9999;
        }
    }
}