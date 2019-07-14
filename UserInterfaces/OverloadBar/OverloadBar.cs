using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.OverloadBar
{
    // TODO Make this save with player.
    internal class OverloadBar : UIState
    {
        private bool _dragging = false;
        private Vector2 _mouseOffset;

        public override void OnInitialize()
        {
            ResourceBar = new OverloadResourceBar(24, 128, 8);

            ResourceBar.Left.Set(615f, 0f);
            ResourceBar.Top.Set(20f, 0f);

            ResourceBar.OnMouseDown += new UIElement.MouseEvent(DragStart);
            ResourceBar.OnMouseUp += new UIElement.MouseEvent(DragEnd);

            Append(ResourceBar);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 mousePosition = new Vector2(Main.mouseX, Main.mouseY);

            if (ResourceBar.ContainsPoint(mousePosition))
                Main.LocalPlayer.mouseInterface = true;

            if (_dragging)
            {
                ResourceBar.Left.Set(mousePosition.X - _mouseOffset.X, 0f);
                ResourceBar.Top.Set(mousePosition.Y - _mouseOffset.Y, 0f);

                Recalculate();
            }
        }


        public void DragStart(UIMouseEvent evt, UIElement listeningElement)
        {
            _mouseOffset = new Vector2(evt.MousePosition.X - ResourceBar.Left.Pixels, evt.MousePosition.Y - ResourceBar.Top.Pixels);
            _dragging = true;
        }

        public void DragEnd(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            _dragging = false;

            ResourceBar.Left.Set(end.X - _mouseOffset.X, 0f);
            ResourceBar.Top.Set(end.Y - _mouseOffset.Y, 0f);

            Recalculate();
        }


        public OverloadResourceBar ResourceBar { get; private set; }

        public bool Visible { get; set; }
    }
}
