using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class DragonGemNecklace : DBTItem
    {
        public DragonGemNecklace() : base("Dragon Gem Necklace", "Infused with the essence of the dragon.\nAll effects of the previous necklaces, some enhanced.", Item.buyPrice(gold: 6, silver: 40), 2, ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 34;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.MaxKiModifier += 250;

            player.endurance += 0.07f;
            player.allDamage += 0.07f;

            player.meleeSpeed += 0.07f;

            player.magicCrit += 7;

            player.rangedCrit += 7;

            player.lifeRegen += 2;

            player.maxMinions += 2;

            player.statDefense += 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(AmberGemNecklace));
            recipe.AddIngredient(mod, nameof(AmethystGemNecklace));
            recipe.AddIngredient(mod, nameof(DiamondGemNecklace));
            recipe.AddIngredient(mod, nameof(EmeraldGemNecklace));
            recipe.AddIngredient(mod, nameof(RubyGemNecklace));
            recipe.AddIngredient(mod, nameof(SapphireGemNecklace));
            recipe.AddIngredient(mod, nameof(TopazGemNecklace));

            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}