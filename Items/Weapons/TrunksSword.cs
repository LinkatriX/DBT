using DBT.Items.KiStones;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Weapons
{
    public sealed class TrunksSword : DBTItem
    {
        public TrunksSword() : base("Trunks' Sword", "Has some armour penetration.", 74, 74, Item.buyPrice(gold: 2, silver: 80), 0, ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.damage = 74;
            item.melee = true;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 17;
            /*if (!Main.dedServ)
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/SwordSlash").WithPitchVariance(.3f);
            }*/
            item.autoReuse = true;
            item.useAnimation = 16;
            item.useTime = 16;
            item.reuseDelay = 16;
        }

        public override void HoldItem(Player player)
        {
            player.armorPenetration += 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddIngredient(mod, nameof(KiStoneT4));
            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
