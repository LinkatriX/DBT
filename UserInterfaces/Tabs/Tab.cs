using Terraria.UI;

namespace DBT.UserInterfaces.Tabs
{
    public class Tab
    {
        public Tab(UIElement element)
        {
            Element = element;
        }

        public UIElement Element { get; }
    }
}