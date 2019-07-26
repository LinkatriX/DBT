using DBT.Auras;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace DBT.Extensions
{
    public static class PlayerExtensions
    {
        public static void DrawAura(this DBTPlayer modPlayer, AuraAnimationInformation aura)
        {
            Player player = modPlayer.player;
            Texture2D texture = aura.GetTexture(modPlayer);
            Rectangle textureRectangle = new Rectangle(0, aura.GetHeight(modPlayer) * aura.FramesCount, texture.Width, aura.GetHeight(modPlayer));
            float scale = aura.GetAuraScale(modPlayer);
            Tuple<float, Vector2> rotationAndPosition = aura.GetRotationAndPosition(modPlayer);
            float rotation = rotationAndPosition.Item1;
            Vector2 position = rotationAndPosition.Item2;

            SpriteBatchExtensions.SetSpriteBatchForPlayerLayerCustomDraw(aura.BlendState, modPlayer.GetPlayerSamplerState());

            // custom draw routine
            Main.spriteBatch.Draw(texture, position - Main.screenPosition, textureRectangle, Color.White, rotation, new Vector2(aura.GetWidth(modPlayer), aura.GetHeight(modPlayer)) * 0.5f, scale, SpriteEffects.None, 0f);

            SpriteBatchExtensions.ResetSpriteBatchForPlayerDrawLayers(modPlayer.GetPlayerSamplerState());
        }
    }
}