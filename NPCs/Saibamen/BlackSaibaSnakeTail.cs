using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Saibamen
{
    public class BlackSaibaSnakeTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Snakeaman");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.lifeMax = 300;
            npc.defense = 15;
        }
    }
}
