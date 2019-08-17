using DBT.Items.KiStones;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Weapons
{
    public sealed class YajirobesKatana : DBTItem
    {
        public YajirobesKatana() : base("Yajirobe's Katana", "", 76, 82, Item.buyPrice(silver: 6), 0, ItemRarityID.Green) 
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.damage = 32;
            item.melee = true;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.melee = true; //so the item's animation doesn't do damage
            item.knockBack = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 31f;
            item.useAnimation = 12;
            item.useTime = 15;
            item.reuseDelay = 14;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddIngredient(mod, nameof(KiStoneT1));
            recipe.AddIngredient(mod, nameof(KiStoneT2));
            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}