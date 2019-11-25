using DBT.Players;
using Microsoft.Xna.Framework;
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
    public class BlackSaibaSnakeHead : ModNPC
    {
        internal int[] worms = new int[] { ModContent.NPCType<BlackSaibaSnakeHead>(), ModContent.NPCType<BlackSaibaSnakeBody>(), ModContent.NPCType<BlackSaibaSnakeTail>() };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Snakeaman");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.damage = 60;
            npc.lifeMax = 300;
            npc.defense = 30;
        }
        public override void AI()
        {
            BaseAI.AIWorm(npc, worms, Main.rand.Next(3, 5), 0, 14);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
                return spawnInfo.player.GetModPlayer<DBTPlayer>().zoneUGWasteland ? 0.8f : 0f;
            else
                return 0f;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 3f;
                if (num619 == 1)
                {
                    scaleFactor9 = 3f;
                }
                npc.width = 60;
                npc.height = 60;
                npc.damage = 140;
                int num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;
                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;
                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;
                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;
                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;
                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;
                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;
                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
                npc.active = false;
            }
        }
    }
}
