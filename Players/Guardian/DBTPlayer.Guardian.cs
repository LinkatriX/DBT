using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetGuardianEffects()
        {
            BaseHealingBonus = 0;
        }

        public void BuffTeam<T>(int duration) where T : ModBuff => BuffTeam(ModContent.BuffType<T>(), duration);

        public void BuffTeam(int buffId, int duration)
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player ply = Main.player[i];

                if (ply != null && ply.team != 0 && !ply.dead && ply.active && Main.player[Main.myPlayer].team == ply.team)
                    ply.AddBuff(buffId, duration);
            }
        }

        public int BaseHealingBonus { get; set; } = 0;
    }
}
