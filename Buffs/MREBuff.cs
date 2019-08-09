using DBT.Players;
using Terraria;

namespace DBT.Buffs
{
    public sealed class MREBuff : DBTBuff
    {
        public MREBuff() : base("MRE", "You feel extremely full and super refreshed. Your natural abilities are enhanced.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            dbtPlayer.NaturalKiRegenerationMultiplier += 0.3f;
            dbtPlayer.KiOrbGrabRange += 2;

            player.lifeRegen += 2;
            player.statDefense += 7;
            player.lifeMagnet = true;
        }
    }
}