using DBT.Items.KiStones;
using DBT.Items.Materials.Metals;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Infusers
{
    [AutoloadEquip(EquipType.Neck)]
    public abstract class Infuser : DBTItem
    {
        private const int GEM_COUNT = 5;

        protected Infuser(string displayName, string tooltip, int value, int gemID, int rarity = ItemRarityID.Pink) : base(displayName, tooltip, value, 0, rarity)
        {
            GemID = gemID;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 18;
            item.height = 30;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 2);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 12);
            recipe.AddIngredient(GemID, GEM_COUNT);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);

            recipe.AddRecipe();
        }

        public int GemID { get; }
    }
}