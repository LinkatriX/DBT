namespace DBT.Items.Accessories.Necklaces
{
    public abstract class Necklace : DBTItem
    {
        protected Necklace(string displayName, string tooltip, int width, int height, int value, int defense, int rare) : base(displayName, tooltip, width, height, value, defense, rare)
        {
            Width = width;
            Height = height;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.accessory = true;
        }

        public int Width { get; }

        public int Height { get; }
    }
}
