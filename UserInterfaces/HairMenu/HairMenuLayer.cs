using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.HairMenu
{
    public sealed class HairMenuLayer : GameInterfaceLayer
    {
        public HairMenuLayer() : base(typeof(HairMenu).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (HairMenu.menuVisible)
            {
                DBTMod.Instance.hairMenuInterface.Update(Main._drawInterfaceGameTime);
                DBTMod.Instance.hairMenu.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}
