﻿using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Projectiles.Overload
{
    public class CircleSpawner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Circle Spawner");
        }
        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 15;
            item.shootSpeed = 0f;
            item.useAnimation = 15;
            item.useStyle = 4;
            item.shoot = mod.ProjectileType<RitualGhostsSpawn>();
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
