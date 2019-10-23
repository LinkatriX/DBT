using DBT.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void PostUpdateHandleTransformationsVisuals()
        {
        }

        /*public static DrawData TransformationAnimationDrawData(PlayerDrawInfo drawInfo, string transformationSpriteSheet, int frameCounterLimit, int numberOfFrames, int yOffset)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            Mod mod = DBTMod.Instance;
            int frame = modPlayer.TransformationFrameTimer / frameCounterLimit;
            Texture2D texture = mod.GetTexture(transformationSpriteSheet);
            int frameSize = texture.Height / numberOfFrames;
            int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(drawInfo.position.Y + frameSize + yOffset + drawPlayer.height / 0.6f - Main.screenPosition.Y);
            // we've hit the frame limit, so kill the animation
            if (frame == numberOfFrames)
            {
                modPlayer.IsTransformationAnimationPlaying = false;
            }
            return new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, frameSize * frame, texture.Width, frameSize), Color.White, 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
        }

        public void DrawTransformationAnimation(int frameCounterLimit, int numberOfFrames, TransformationDefinition transformation, PlayerDrawInfo drawInfo, Type transformationType, int yOffset = 0) 
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (modPlayer.IsTransformed(transformation))
            {
                Main.playerDrawData.Add(TransformationAnimationDrawData(drawInfo, GetTransformationAnimationFromType(transformationType), frameCounterLimit, numberOfFrames, yOffset));
                //isAnyAnimationPlaying = modPlayer.isTransformationAnimationPlaying;
            }
        }

        public static readonly PlayerLayer transformationEffects = new PlayerLayer("DBTMod", "TransformationAnimations", null, delegate (PlayerDrawInfo drawInfo)
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            

            if (modPlayer.IsTransformationAnimationPlaying)
                return;

            bool isAnyAnimationPlaying = false;

            //DrawTransformationAnimation();

            // if we made it this far, we don't want to get stuck in a transformation animation state just because one doesn't exist
            // cancel it so we can move on and show auras.
            if (!isAnyAnimationPlaying)
            {
                modPlayer.IsTransformationAnimationPlaying = false;
            }
        });

        private static string GetTransformationAnimationFromType(Type type) => type.GetTexturePath() + "Animation";

        public bool IsTransformationAnimationPlaying { get; set; } = false;

        public int TransformationFrameTimer { get; set; }*/
    }
}
