using DBT.Items.KiStones;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.Potionlike.KiStimulants
{
    public sealed class KiStimulant : DBTConsumable
    {
        public KiStimulant() : base("Ki Stimulant", "Stimulates your body into enhancing ki.", 32, 32, Item.buyPrice(silver: 5), ItemRarityID.Orange, ItemUseStyleID.EatingUsing, true, SoundID.Item1, 17, 17)
        {
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            item.maxStack = 99;
            item.buffType = ModContent.BuffType<KiStimulantBuff>();
            item.buffTime = 120 * Constants.TICKS_PER_SECOND;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Waterleaf, 5);
            recipe.AddIngredient(mod, nameof(KiStoneT2), 10);
            recipe.AddTile(TileID.Bottles);

            recipe.alchemy = true;
            recipe.SetResult(this, 3);

            recipe.AddRecipe();
        }
    }
}