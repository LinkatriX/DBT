using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Effects
{
    public class CustomBodySkinLayer : PlayerLayer
    {
        private static Texture2D _customBodySkin;


        public CustomBodySkinLayer() : base(DBTMod.Instance.Name, "CustomBodySkin", DrawLayer)
        {
            if (_customBodySkin == null)
                _customBodySkin = this.GetType().GetModFromType().GetTexture(this.GetType().GetRootPath() + "/CustomBody");
        }


        public static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            Skin.visible = false;

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

            float drawX = (drawInfo.position.X + drawInfo.drawPlayer.width / XOffset - Main.screenPosition.X); // when looking right, add 3
            float drawY = (drawInfo.position.Y + drawInfo.drawPlayer.height / 0.08f - Main.screenPosition.Y) + 25;

            Main.spriteBatch.Draw(_customBodySkin, new Vector2(drawX, drawY), drawInfo.drawPlayer.bodyFrame, 
                drawInfo.drawPlayer.skinColor, 0, new Vector2(_customBodySkin.Width / 2f, _customBodySkin.Height / 2f), 1f, spriteEffects, 0);
        }
    }
}