using DBT.Players;
using Terraria;

namespace DBT.Buffs
{
    public class KiDiffuserBuff : DBTBuff
    {
        public KiDiffuserBuff() : base("Ki Diffuser", "Gives some slight ki regen.")
        {
        }
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) =>
            player.GetModPlayer<DBTPlayer>().ModifyKi(0.15f);
    }
}
