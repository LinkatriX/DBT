using DBT.Buffs;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void PostUpdateTiles()
        {
            if (KiDiffuser)
                player.AddBuff(ModContent.BuffType<KiDiffuserBuff>(), 1);
        }

        public bool KiDiffuser { get; set; }
    }
}
