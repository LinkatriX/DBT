using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.WishMenu
{
    public sealed class WishMenuLayer : GameInterfaceLayer
    {
        public WishMenuLayer() : base(typeof(WishMenu).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (WishMenu.menuVisible)
            {
                DBTMod.Instance.wishMenuInterface.Update(Main._drawInterfaceGameTime);
                DBTMod.Instance.wishMenu.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}
