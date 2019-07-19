using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace DBT.UserInterfaces.Tabs
{
    public class Tab
    {
        protected readonly List<UIElement> elements = new List<UIElement>();

        public Tab(UIImageButton tabButton, UIPanel panel)
        {
            TabButton = tabButton;

            Panel = panel;
        }

        public void Add(UIElement element)
        {
            elements.Add(element);
            Panel.AddOrRemoveChild(element, true);
        }

        public UIImageButton TabButton { get; }

        public UIPanel Panel { get; }
    }
}