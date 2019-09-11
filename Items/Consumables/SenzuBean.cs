using System.Collections.Generic;
using DBT.Buffs;
using DBT.Commons.Items.SenzuBeans;
using DBT.Players;
using Terraria;
using Terraria.ID;
using WebmilioCommons.Extensions;

namespace DBT.Items.Consumables
{
    public sealed class SenzuBean : DBTConsumable
    {
        public SenzuBean() : base("Senzu Bean", "Restores your body and Ki", 16, 20, Item.buyPrice(silver: 20), ItemRarityID.Pink,
            ItemUseStyleID.EatingUsing, true, SoundID.Item3, 17, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.potion = false;
            item.healLife = 9001;
            item.healMana = 9001;
            item.maxStack = 10;
        }

        public override bool UseItem(Player player)
        {
            bool baseResult = base.UseItem(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            List<IAffectSenzuBeanCooldown> items = player.GetItemsByType<IAffectSenzuBeanCooldown>(armor: true, accessories: true);

            float cooldown = 18000;

            for (int i = 0; i < items.Count; i++)
                items[i].AffectSenzuBeanCooldown(dbtPlayer, this, ref cooldown);

            dbtPlayer.ModifyKi(dbtPlayer.MaxKi - dbtPlayer.Ki);
            return baseResult;
        }

        public override bool CanUseItem(Player player) => !player.HasBuff<SenzuCooldownBuff>() && base.CanUseItem(player);
    }
}