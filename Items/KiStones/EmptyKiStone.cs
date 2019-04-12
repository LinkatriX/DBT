﻿using System.Collections.Generic;
using DBT.Buffs;
using DBT.Commons.Items;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.KiStones
{
    public class EmptyKiStone : DBTItem, IHasRarity
    {
        public const int VALUE = (int) 2 * Constants.SILVER_VALUE_MULTIPLIER;

        private static Dictionary<float, KiStoneDefinition> _requiredKiPerStone;

        public EmptyKiStone() : base("Empty Ki Stone", "This ancient stone looks like it can be charged.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 24;
            item.height = 24;
            item.rare = Rarity;
            item.value = VALUE;
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            if (dbtPlayer.IsCharging)
            {
                if (dbtPlayer.Ki == 0)
                {
                    ChargingInTry = false;
                    player.AddBuff(mod.BuffType<KiDegenerationBuff>(), 10 * 60);
                    return;
                }

                if (!ChargingInTry)
                {
                    CurrentTry++;
                    ChargingInTry = true;
                }

                if (NextTier == null)
                {
                    NextTier = KiStoneDefinitionManager.Instance.GetNearestKiStoneAbove(CurrentKiForTier);
                }

                float kiPerTick = NextTier.RequiredKi / 60;

                dbtPlayer.KiChargeRate = -kiPerTick;
                CurrentKiForTier += kiPerTick;

                if (CurrentKiForTier >= NextTier.RequiredKi)
                {
                    TierOnRelease = NextTier;

                    NextTier = KiStoneDefinitionManager.Instance.GetNearestKiStoneUnder(CurrentKiForTier);
                }
            }
            else if (TierOnRelease != null)
            {

                item.TurnToAir();
                player.PutItemInInventory(mod.ItemType(TierOnRelease.ItemType.Name));
            }
            else if (CurrentTry > 0)
                item.TurnToAir();
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            int[] gemIDs = new int[] { ItemID.Amethyst, ItemID.Diamond, ItemID.Emerald, ItemID.Ruby, ItemID.Sapphire, ItemID.Topaz };

            for (int i = 0; i < gemIDs.Length; i++)
            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(ItemID.StoneBlock, 25);
                recipe.AddIngredient(gemIDs[i]);
                recipe.AddTile(mod, nameof(ZTableTile));

                recipe.SetResult(this);

                recipe.AddRecipe();
            }
        }

        public int Rarity => ItemRarityID.White;

        public int CurrentTry { get; protected set; }
        public bool ChargingInTry { get; protected set; }

        public KiStoneDefinition NextTier { get; protected set; }
        public KiStoneDefinition TierOnRelease { get; protected set; }

        public float CurrentKiForTier { get; protected set; }
    }
}