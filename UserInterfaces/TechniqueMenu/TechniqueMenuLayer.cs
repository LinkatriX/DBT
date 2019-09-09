using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.TechniqueMenu
{
    public sealed class TechniqueMenuLayer : GameInterfaceLayer
    {
        public TechniqueMenuLayer(TechniqueMenu techniqueMenu, UserInterface @interface) : base(techniqueMenu.GetType().FullName, InterfaceScaleType.UI)
        {
            TechniqueMenu = techniqueMenu;
            Interface = @interface;
        }

        protected override bool DrawSelf()
        {
            if (TechniqueMenu.Visible)
                Interface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);

            return true;
        }

        public TechniqueMenu TechniqueMenu { get; }

        public UserInterface Interface { get; }
    }
}
