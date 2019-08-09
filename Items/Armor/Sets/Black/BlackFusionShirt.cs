using DBT.Commons.Players;
using DBT.Items.Materials;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Black
{
    [AutoloadEquip(EquipType.Body)]
    public sealed class BlackFusionShirt : DBTArmorPiece, IHandleOnPlayerPostUpdate
    {
        private int _timer = 0;
        private float _multiplier = 1f;

        public BlackFusionShirt() : base("Black Fusion Shirt",
            "24% Increased Ki Damage" +
            "\n20% Increased Ki Crit Chance" +
            "\n+1000 Max Ki" +
            "\n+2 Maximum Charges" +
            "\nIncreased Ki Charge Rate",
            28, 18, Item.buyPrice(gold: 1, silver: 28), 24, ItemRarityID.Cyan)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && legs.type == mod.ItemType<BlackFusionPants>();

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "All damage is slowly increased while a boss is alive, limits at 300%." +
                              "\n+100 Max Life";

            player.statLifeMax2 += 100;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.24f;
            dbtPlayer.KiCritAddition += 20;
            dbtPlayer.MaxKiModifier += 1000;
            dbtPlayer.KiChargeRateModifier += 2;
            dbtPlayer.KiChargeRateMultiplierLimit += 2;
        }

        public void OnPlayerPostUpdate(DBTPlayer dbtPlayer)
        {
            bool isSet = IsArmorSet(dbtPlayer.player);

            if (!isSet || dbtPlayer.AliveBosses.Count == 0)
            {
                _timer = 0;
                _multiplier = 1;
            }
            else if (dbtPlayer.AliveBosses.Count > 0)
            {
                _timer++;

                if (_multiplier <= 3f && _timer > 300)
                {
                    _multiplier += 0.05f;
                    _timer = 0;
                }
            }

            if (_multiplier != 1f)
            {
                dbtPlayer.player.meleeDamage *= _multiplier;
                dbtPlayer.player.thrownDamage *= _multiplier;
                dbtPlayer.player.rangedDamage *= _multiplier;
                dbtPlayer.player.magicDamage *= _multiplier;
                dbtPlayer.player.minionDamage *= _multiplier;
                dbtPlayer.player.statDefense = (int) (dbtPlayer.player.statDefense  * _multiplier);

                dbtPlayer.KiDamageMultiplier += _multiplier;
            }
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(DivineThread), 22);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}