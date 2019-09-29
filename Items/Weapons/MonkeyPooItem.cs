using DBT.Projectiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DBT.Items.Weapons
{
    public sealed class MonkeyPooItem : DBTItem
    {
        public MonkeyPooItem() : base("Monkey Poo", "Literally just some monkey poo, why are you touching this?", 22, 32, Item.buyPrice(copper: 1), 0, ItemRarityID.Gray)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.thrown = true;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType<MonkeyPooProjectile>();
            item.shootSpeed = 20f;
            item.useTime = 12;
            item.damage = 9;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 8f;
            item.UseSound = SoundID.Item1;
            item.maxStack = 9999;
            item.consumable = true;
        }
    }
}
