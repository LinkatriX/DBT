﻿using DBT.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.MajinExtinctionAttack
{
    public sealed class MajinExtinctionAttackItem : SkillItem<MajinExtinctionAttackProjectile>
    {
        public MajinExtinctionAttackItem() : base(SkillDefinitionManager.Instance.MajinExtinctionAttack, 28, 30, ItemRarityID.Lime, true)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useAnimation = 16;
            item.useTime = 5;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Random randVelocityX = new Random();

            Projectile.NewProjectile(position.X - 4, position.Y - 12, (float)(randVelocityX.NextDouble() * 8f - 4.2f), 0f, type, damage, knockBack, Owner: Main.myPlayer); //6f - 3.2f
            return false;
        }
    }
}