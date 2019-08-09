using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public abstract class KiPotion : DBTConsumable
    {
        protected KiPotion(string displayName, int restoredKi, int value) : base(displayName, "Restores " + restoredKi + " Ki", 16, 24, value, ItemRarityID.Orange, ItemUseStyleID.EatingUsing, true, SoundID.Item3, 12, 12)
        {
            RestoredKi = restoredKi;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 30;
        }

        public override bool CanUseItem(Player player) => base.CanUseItem(player) && !player.HasBuff(mod.BuffType(nameof(KiPotionSicknessDebuff)));

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().ModifyKi(RestoredKi);

            player.AddBuff(mod.BuffType<KiPotionSicknessDebuff>(), 60 * Constants.TICKS_PER_SECOND);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), RestoredKi, false, false);
            return true;
        }

        public int RestoredKi { get; }
    }
}