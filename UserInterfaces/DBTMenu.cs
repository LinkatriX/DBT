using DBT.UserInterfaces.Buttons;
using DBT.UserInterfaces.KiAttackUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DBT.UserInterfaces
{
    // Credit to X3n0ph0b3 / MerceriusXeno.
    public class DBTMenu : UIState
    {
        protected UIImageButton InitializeButton(Texture2D texture, MouseEvent onClick, float offsetX, float offsetY, UIElement parent = null)
        {
            UIImageButton button = new UIImageButton(texture);
            button.OnClick += onClick;

            InitializeUIElement<UIImageButton>(ref button, texture, offsetX, offsetY, parent);
            return button;
        }

        protected UIHoverImageButton InitializeHoverTextButton(Texture2D texture, string hoverText, MouseEvent onClick, float offsetX, float offsetY, UIElement parent = null)
        {
            UIHoverImageButton button = new UIHoverImageButton(texture, hoverText);
            button.OnClick += onClick;

            InitializeUIElement<UIHoverImageButton>(ref button, texture, offsetX, offsetY, parent);
            return button;
        }

        protected UIImage InitializeImage(Texture2D texture, float offsetX, float offsetY, UIElement parent = null)
        {
            UIImage image = new UIImage(texture);

            InitializeUIElement<UIImage>(ref image, texture, offsetX, offsetY, parent);
            return image;
        }

		protected KiSlotButtonDefinition InitializeKiMenuButton(MouseEvent onClick, Texture2D texture, string hoverText, bool isAccessed, float offsetX, float offsetY, UIElement parent = null)
		{
			KiSlotButtonDefinition button = new KiSlotButtonDefinition(texture, hoverText, isAccessed);
			button.OnClick += onClick;

			InitializeUIElement<KiSlotButtonDefinition>(ref button, texture, offsetX, offsetY, parent);
			return button;
		}

        protected UIText InitializeText(string shownText, float offsetX, float offsetY, float scale = 1, Color color = default(Color), UIElement parent = null)
        {
            UIText text = new UIText(shownText, scale);

            text.Width.Set(16f, 0f);
            text.Height.Set(16f, 0f);
            text.Left.Set(offsetX, 0f);
            text.Top.Set(offsetY, 0f);

            text.TextColor = color;

            if (parent == null)
                BackPanel.Append(text);
            else
                parent.Append(text);

            return text;
        }


        private void InitializeUIElement<T>(ref T element, Texture2D texture, float offsetX, float offsetY, UIElement parent = null) where T : UIElement
        {
            element.Width.Set(texture.Width, 0.0f);
            element.Height.Set(texture.Height, 0.0f);
            element.Left.Set(offsetX, 0f);
            element.Top.Set(offsetY, 0f);

            if (parent == null)
                BackPanel.Append(element);
            else
                parent.Append(element);
        }


        #region Mouse Events

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);
            DragEnd(evt);
        }

        public void DragStart(UIMouseEvent evt)
        {
            Offset = new Vector2(evt.MousePosition.X - BackPanel.Left.Pixels, evt.MousePosition.Y - BackPanel.Top.Pixels);
            Dragging = true;
        }

        public void DragEnd(UIMouseEvent evt)
        {
            Dragging = false;

            BackPanel.Left.Set(evt.MousePosition.X - Offset.X, 0f);
            BackPanel.Top.Set(evt.MousePosition.Y - Offset.Y, 0f);

            Recalculate();
        }

        #endregion


        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            //Main.spriteBatch.Draw(Main.magicPixel, GetInnerDimensions().ToRectangle(), Color.Red * 0.6f);

            Vector2 mousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);

            if (BackPanel.ContainsPoint(mousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (Dragging)
            {
                BackPanel.Left.Set(mousePosition.X - Offset.X, 0f);
                BackPanel.Top.Set(mousePosition.Y - Offset.Y, 0f);

                Recalculate();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // don't remove.

            // Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
            if (BackPanel.ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;
        }

        protected UIText TitleText { get; set; }

        public UIPanel BackPanel { get; protected set; }
        public Texture2D BackPanelTexture { get; protected set; }
        public UIImage BackPanelImage { get; protected set; }

        public Vector2 Offset { get; protected set; }

        public bool Dragging { get; protected set; }
    }
}
