using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
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
