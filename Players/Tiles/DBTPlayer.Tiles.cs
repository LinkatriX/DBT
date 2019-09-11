using DBT.Buffs;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void PostUpdateTiles()
        {
            if (KiDiffuser)
                player.AddBuff(mod.BuffType<KiDiffuserBuff>(), 1);
        }

        public bool KiDiffuser { get; set; }
    }
}
