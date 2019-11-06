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
    public sealed class DrawBodyEffects : PlayerLayer
    {
        public DrawBodyEffects() : base(DBTMod.Instance.Name, "BodyLayer", null, DrawLayer)
        {
        }

        
        public static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            DBTPlayer dbtPlayer = drawInfo.drawPlayer.GetModPlayer<DBTPlayer>();
            TransformationDefinition transformation = dbtPlayer.FirstTransformation?.Definition;

            float XOffset;

            SpriteEffects spriteEffects;

            if (drawInfo.drawPlayer.direction == 1)
            {
                spriteEffects = SpriteEffects.None;
                XOffset = 2.1f;
            }
            else
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
                XOffset = 2f;
            }

            if (transformation != null)
            {
                Texture2D
                    customBody = transformation.Appearance.CustomBody,
                    customFemaleBody = transformation.Appearance.CustomFemaleBody,
                    customArms = transformation.Appearance.CustomArms,
                    customEyes = transformation.Appearance.CustomEyes;

                float drawX = (drawInfo.position.X + drawInfo.drawPlayer.width / XOffset - Main.screenPosition.X); // when looking right, add 3
                float drawY = (drawInfo.position.Y + drawInfo.drawPlayer.height / 0.08f - Main.screenPosition.Y) + 25;

                if (customBody != null || customFemaleBody != null)
                {
                    Body.visible = false;
                    Skin.visible = false;

                    if (drawInfo.drawPlayer.Male && customBody != null)
                        Main.spriteBatch.Draw(customBody, new Vector2(drawX, drawY), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(customBody.Width / 2f, customBody.Height / 2f), 1f, spriteEffects, 0);
                    else if (!drawInfo.drawPlayer.Male)
                    {
                        Texture2D toDraw = customFemaleBody ?? customBody;

                        Main.spriteBatch.Draw(toDraw, new Vector2(drawX, drawY + 25), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(toDraw.Width / 2f, toDraw .Height / 2f), 1f, spriteEffects, 0);
                    }
                }

                if (customArms != null)
                {
                    Arms.visible = false;
                    Main.spriteBatch.Draw(customArms, new Vector2(drawX, drawY), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(customArms.Width / 2f, customArms.Height / 2f), 1f, spriteEffects, 0);
                }

                if (customEyes != null)
                    Main.spriteBatch.Draw(customEyes, new Vector2(drawX, drawY), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(customEyes.Width / 2f, customEyes.Height / 2f), 1f, spriteEffects, 0);

                // TODO Verify and move Y Offset.

            }
        }
    }
}
