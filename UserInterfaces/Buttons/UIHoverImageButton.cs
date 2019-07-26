using DBT.Players;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace DBT.UserInterfaces.Buttons
{
    // Credits to ExampleMod (BlushieMagic).
    public class UIHoverImageButton : UIImageButton
    {
        public UIHoverImageButton(Texture2D texture, string hoverText) : base(texture)
        {
            HoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

                if (dbtPlayer == null)
                    Main.hoverItemName = HoverText;
                else
                    Main.hoverItemName = GetHoverText(dbtPlayer);
            }
        }


        public virtual string GetHoverText(DBTPlayer dbtPlayer) => HoverText;


        public string HoverText { get; }
    }
}
