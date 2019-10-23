using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Buffs
{
    public sealed class ZenkaiCharmBuff : DBTBuff
    {
        public ZenkaiCharmBuff() : base("Zenkai Charm", "Your inherent saiyan ability is active.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 2;

            if (player.buffTime[buffIndex] <= 0)
                player.AddBuff(ModContent.BuffType<ZenkaiCooldownBuff>(), 7200);
        }
    }
}