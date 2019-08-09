using DBT.Buffs;
using DBT.Commons.Items;
using DBT.Commons.Users;
using DBT.Extensions;
using DBT.Helpers;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Consumables
{
    public sealed class MRE : DBTConsumable, IIsPatreonLocked
    {
        public MRE() : base("MRE", "A full course meal, grants a variety of bonuses.", 32, 32, Item.buyPrice(silver: 30), ItemRarityID.Orange, 
            ItemUseStyleID.EatingUsing, true, SoundID.Item1, 17, 17)
        {
        }

        public override bool UseItem(Player player)
        {
            int durationMultiplier = this.IsDonator() ? 2 : 1;

            player.AddBuff(mod.BuffType<MREBuff>(), 360 * Constants.TICKS_PER_SECOND * durationMultiplier);
            player.AddBuff(BuffID.WellFed, 3600 * Constants.TICKS_PER_SECOND * durationMultiplier);

            return base.UseItem(player);
        }

        public Donator Donator => SteamHelper.CanadianMRE;
    }
}