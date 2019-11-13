using DBT.Players;
using DBT.Projectiles.FriezaForce;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Misc
{
	public class ShenronNPC : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shenron");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
			npc.width = 630;
			npc.height = 508;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 999999;
            npc.knockBackResist = 0f;
            npc.dontTakeDamage = true;
            npc.aiStyle = -1;
            npc.alpha = 255; //Start invisible
            npc.immortal = true;
            npc.noGravity = true;
		}

		public override void AI()
		{
            Main.NewText("Alpha is: " + npc.alpha);
			Player player = Main.player[npc.target];
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            npc.ai[0]++;
            npc.ai[1]++;
            if (!dbtPlayer.WishActive)
            {
                if (npc.alpha < 255)
                {
                    npc.alpha++;
                    if (npc.alpha == 254)
                        npc.active = false;
                }
                    
            }
            if (dbtPlayer.WishActive && npc.alpha > 0)
            {
                if (npc.ai[1] > 2)
                {
                    npc.alpha -= 2;
                    npc.ai[1] = 0;
                }
            }
            npc.TargetClosest(true);
            if (npc.velocity.Y == 0 || (npc.velocity.Y == 0.2f && npc.ai[0] == 100))
            {
                npc.velocity.Y -= 0.2f;
                npc.ai[0] = 0;
            }
            if (npc.velocity.Y == -0.2f && npc.ai[0] == 100)
            {
                npc.velocity.Y = 0.2f;
                npc.ai[0] = 0;
            }
            Lighting.AddLight(npc.Center - new Vector2(0, 100), new Vector3(10f - npc.alpha / 60, 10f - npc.alpha / 60, 2f - npc.alpha / 60));
		}
	}
}

