﻿using DBT.Items.Materials.Metals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Weapons
{
    public sealed class FarmersShotgun : DBTItem
    {
        public FarmersShotgun() : base("Farmer's Shotgun", "'A weapon forged by the gods.'", 62, 20, Item.buyPrice(silver: 60), 0, ItemRarityID.Orange)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.damage = 9;
            item.ranged = true;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 8f;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 18f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 12);
            recipe.AddIngredient(ItemID.BorealWood, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}