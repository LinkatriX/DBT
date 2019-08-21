using DBT.Auras;
using DBT.Items.DragonBalls;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
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

        /// <summary>
        ///     Whether a single dragon ball of a specific type is present in an inventory.
        /// </summary>
        /// <param name="inventory">The inventory being checked.</param>
        /// <param name="whichDragonBall">Which (int) dragon ball we're looking for.</param>
        /// <returns></returns>
        public static bool IsDragonBallPresent(this Item[] inventory, int whichDragonBall)
        {
            return
            (
                from item in inventory
                where item != null && item.modItem != null
                where item.modItem is DragonBall
                select (DragonBall)item.modItem
            ).Any(
                dbItem => dbItem.item.type == DragonBall.GetItemTypeFromName(DragonBall.GetDragonBallItemTypeFromNumber(whichDragonBall))
            );
        }
    }
}