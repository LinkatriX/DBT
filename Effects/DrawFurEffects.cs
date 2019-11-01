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
    public sealed class DrawFurEffects : PlayerLayer
    {
        public DrawFurEffects() : base(DBTMod.Instance.Name, "FurLayer", null, DrawLayer)
        {
        }

        
        public static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            DBTPlayer dbtPlayer = drawInfo.drawPlayer.GetModPlayer<DBTPlayer>();
            TransformationDefinition transformation = dbtPlayer.FirstTransformation?.Definition;

            Texture2D bodyTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_Body");
            Texture2D armsTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_Arms");
            Texture2D eyesTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_Eyes");

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
                Body.visible = false;
                Arms.visible = false;

                if (dbtPlayer.IsTransformed(TransformationDefinitionManager.Instance.SSJ4) || dbtPlayer.IsTransformed(TransformationDefinitionManager.Instance.SSJ4FP))
                {
                    if (!drawInfo.drawPlayer.Male)
                        bodyTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_FemaleBody");
                    else
                        bodyTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_Body");

                    armsTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_Arms");
                    eyesTexture = DBTMod.Instance.GetTexture("Transformations/SSJs/SSJ4s/SSJ4/SSJ4_Eyes");
                }
                if (dbtPlayer.IsTransformed(TransformationDefinitionManager.Instance.SSJ5))
                {
                    if (!drawInfo.drawPlayer.Male)
                        bodyTexture = DBTMod.Instance.GetTexture("Transformations/Patreon/SSJ5/SSJ5_FemaleBody");
                    else
                        bodyTexture = DBTMod.Instance.GetTexture("Transformations/Patreon/SSJ5/SSJ5_Body");

                    armsTexture = DBTMod.Instance.GetTexture("Transformations/Patreon/SSJ5/SSJ5_Arms");
                    eyesTexture = DBTMod.Instance.GetTexture("Transformations/Patreon/SSJ5/SSJ5_Eyes");
                }

                float drawX = (drawInfo.position.X + drawInfo.drawPlayer.width / XOffset - Main.screenPosition.X); // when looking right, add 3
                float drawY = (drawInfo.position.Y + drawInfo.drawPlayer.height / 0.08f - Main.screenPosition.Y);

                // TODO Verify and move Y Offset.
                Main.spriteBatch.Draw(bodyTexture, new Vector2(drawX, drawY + 25), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(bodyTexture.Width / 2f, bodyTexture.Height / 2f), 1f, spriteEffects, 0);
                Main.spriteBatch.Draw(armsTexture, new Vector2(drawX, drawY + 25), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(armsTexture.Width / 2f, armsTexture.Height / 2f), 1f, spriteEffects, 0);
                Main.spriteBatch.Draw(eyesTexture, new Vector2(drawX, drawY + 25), drawInfo.drawPlayer.bodyFrame, Color.White, 0f, new Vector2(eyesTexture.Width / 2f, eyesTexture.Height / 2f), 1f, spriteEffects, 0);
            }
        }
    }
}
