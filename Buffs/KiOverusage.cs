using DBT.Players;
using Terraria;

namespace DBT.Buffs
{
    public sealed class KiOverusage : DBTBuff
    {
        public KiOverusage() : base("Ki Overusage", "You've overused your ki and can't control it anymore.\n10% reduced ki damage and movement speed and -10 defense.")
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

            player.GetModPlayer<DBTPlayer>().KiDamageMultiplier -= 0.10f;
            player.moveSpeed *= 0.90f;
            player.statDefense -= 10;
        }
    }
}