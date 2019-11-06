using DBT.Items.DragonBallRadar;
using DBT.Items.DragonBalls;
using DBT.Players;
using DBT.Transformations;
using DBT.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Effects
{
    public sealed class DrawTailEffects : PlayerLayer
    {
        public DrawTailEffects(int index) : base(DBTMod.Instance.Name, "TailLayer" + index, null, DrawLayer)
        {
        }

        
        public static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            DBTPlayer modPlayer = drawInfo.drawPlayer.GetModPlayer<DBTPlayer>();
            TransformationDefinition transformation = modPlayer.ActiveTransformations.FirstOrDefault();
            
            int frame = modPlayer.TailFrameTimer / 8;
            Color tailColor = Color.White;
            float XOffset;
            SpriteEffects spriteEffects;

            if (drawInfo.drawPlayer.direction == 1)
            {
                spriteEffects = SpriteEffects.None;
                XOffset = 3f;
            }
            else
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
                XOffset = 1.8f;
            }
            if (transformation != null)
            {
                if (modPlayer.IsTransformed(TransformationDefinitionManager.Instance.SSJ4) || modPlayer.IsTransformed(TransformationDefinitionManager.Instance.SSJ4FP))
                {
                    tailColor = new Color(224, 51, 66);
                }
                else
                {
                    tailColor = transformation.Appearance.Hair.Color.Value;
                }
            }
            else
            {
                tailColor = drawInfo.drawPlayer.hairColor;
            }

            Color color = Lighting.GetColor((int)drawInfo.drawPlayer.position.X / 16, (int)drawInfo.drawPlayer.position.Y / 16, tailColor);
            //Color trueTailColor = new Color(tailColor.R, tailColor.G, tailColor.B, alpha.A);
            Texture2D texture = DBTMod.Instance.GetTexture("Effects/PrimalTail");
            int frameSize = texture.Height / 14;
            float drawX = (drawInfo.position.X + drawInfo.drawPlayer.width / XOffset - Main.screenPosition.X);
            float drawY = (drawInfo.position.Y + drawInfo.drawPlayer.height / 0.11f - Main.screenPosition.Y);
            Main.spriteBatch.Draw(texture, new Vector2(drawX, drawY), new Rectangle(0, frameSize * frame, texture.Width, frameSize), color, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, spriteEffects, 0);
        }
    }
}
