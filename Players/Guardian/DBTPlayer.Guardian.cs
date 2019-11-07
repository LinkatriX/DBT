using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;
using WebmilioCommons.Tinq;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetGuardianEffects()
        {
            BaseHealingBonus = 0;
        }

        public void BuffTeam<T>(int duration) where T : ModBuff => BuffTeam(ModContent.BuffType<T>(), duration);

        public void BuffTeam(int buffId, int duration) =>
            Main.player.WhereActive(p => !p.dead && p.team != 0 && Main.LocalPlayer.team == p.team).Do(p => p.AddBuff(buffId, duration));

        public int BaseHealingBonus { get; set; } = 0;
    }
}
