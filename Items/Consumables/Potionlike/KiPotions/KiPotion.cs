using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public abstract class KiPotion : DBTConsumable
    {
        protected KiPotion(string displayName, float percentKi, int value) : base(displayName, "Restores " + percentKi + "% of Ki", 16, 24, value, ItemRarityID.Orange, ItemUseStyleID.EatingUsing, true, SoundID.Item3, 12, 12)
        {
            PercentKi = percentKi;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 30;
        }

        public override bool CanUseItem(Player player) => base.CanUseItem(player) && !player.HasBuff(mod.BuffType(nameof(KiPotionSicknessDebuff)));

        public override bool UseItem(Player player)
        {
            float overallPercentRestored = player.GetModPlayer<DBTPlayer>().MaxKi * (PercentKi / 100);
            player.GetModPlayer<DBTPlayer>().ModifyKi(overallPercentRestored);

            player.AddBuff(mod.BuffType<KiPotionSicknessDebuff>(), 60 * Constants.TICKS_PER_SECOND);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), (int)overallPercentRestored, false, false);
            return true;
        }

        public float PercentKi { get; }
    }
}