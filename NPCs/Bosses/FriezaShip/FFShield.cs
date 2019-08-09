using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace DBT.NPCs.Bosses.FriezaShip
{
    public class FFShield : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frieza Force Deflection Shield");
        }
        public override void SetDefaults()
        {
            npc.width = 220;
            npc.height = 120;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 800;
            npc.HitSound = SoundID.NPCHit1;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
        }

        public override void AI()
        {
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (i == mod.NPCType<FriezaShip>())
				{
					NPC friezaShip = Main.npc[i];
					break;
				}
			}

			if (friezaNPC != null)
                npc.position = friezaNPC.position;


            if (!NPC.AnyNPCs(mod.NPCType<FriezaShip>()))
                npc.life = 0; 
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            DrawData drawData = new DrawData(TextureManager.Load("Images/Misc/Perlin"), npc.Center - new Vector2(130, 90), npc.getRect(), Color.White, npc.rotation, new Vector2(300f, 300f), npc.scale, SpriteEffects.None, 0);
            GameShaders.Misc["ForceField"].UseColor(Color.White);
            GameShaders.Misc["ForceField"].Apply(drawData);
            drawData.Draw(Main.spriteBatch);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            return;

        }
        public NPC friezaNPC { get; set; }
    }
}
