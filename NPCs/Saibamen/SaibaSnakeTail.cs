﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Saibamen
{
    public class SaibaSnakeTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snakeaman");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
        }
    }
}
