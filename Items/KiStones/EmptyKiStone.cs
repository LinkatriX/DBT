using DBT.Buffs;
using DBT.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.KiStones
{
    public class EmptyKiStone : DBTItem
    {
        public const int VALUE = 2 * Constants.SILVER_VALUE_MULTIPLIER;

        public EmptyKiStone() : base("Empty Ki Stone", "This ancient stone looks like it can be charged.", VALUE, 0, ItemRarityID.White)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 24;
            item.height = 24;
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            if (dbtPlayer.Charging)
            {
                if (dbtPlayer.Ki <= 1)
                {
                    ChargingInTry = false;
                    player.AddBuff(ModContent.BuffType<KiDegenerationBuff>(), 10 * 60);
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

                float kiPerTick = NextTier.RequiredKi / 60 / 2;

                dbtPlayer.KiChargeRateModifier = -kiPerTick;
                CurrentKiForTier += kiPerTick;

                if (CurrentKiForTier >= NextTier.RequiredKi)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        CircularDust(20, dbtPlayer.player, 156, NextTier.RequiredKi / 100, 0.8f);
                    }
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
                recipe.AddTile(TileID.Anvils);

                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        public void CircularDust(int quantity, Player target, short dustID, float radius, float scale)
        {
            for (int i = 0; i < quantity; i++)
            {
                Vector2 pos = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) + target.Center;
                float angle = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(pos, dustID,
                    new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * radius, 255, default(Color),
                    scale);
                dust.noGravity = true;
            }
        }

        public int CurrentTry { get; protected set; }
        public bool ChargingInTry { get; protected set; }

        public KiStoneDefinition NextTier { get; protected set; }
        public KiStoneDefinition TierOnRelease { get; protected set; }

        public float CurrentKiForTier { get; protected set; }
    }
}