using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.CharacterMenus
{
    public sealed class CharacterTransformationsMenuLayer : GameInterfaceLayer
    {
        public CharacterTransformationsMenuLayer(CharacterTransformationsMenu transformationsMenu, UserInterface @interface) : base(transformationsMenu.GetType().FullName, InterfaceScaleType.UI)
        {
            TransformationsMenu = transformationsMenu;
            Interface = @interface;
        }

        protected override bool DrawSelf()
        {
            if (TransformationsMenu.Visible)
                Interface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);

            return true;
        }

        public CharacterTransformationsMenu TransformationsMenu { get; }

        public UserInterface Interface { get; }
    }
}
