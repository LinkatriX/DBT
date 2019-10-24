using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces
{
    public sealed class NamekianBookUILayer : GameInterfaceLayer
    {
        public NamekianBookUILayer() : base(typeof(NamekianBookUI).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (DBTMod.Instance.namekBookUI.MenuVisible)
            {
                DBTMod.Instance.namekBookInterface.Update(Main._drawInterfaceGameTime);
                DBTMod.Instance.namekBookUI.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}
