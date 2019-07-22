using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.OverloadBar
{
    public sealed class OverloadBarLayer : GameInterfaceLayer
    {
        public OverloadBarLayer() : base(typeof(OverloadBar).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (DBTMod.Instance.overloadBar.Visible)
            {
                DBTMod.Instance.overloadBarInterface.Update(Main._drawInterfaceGameTime);
                DBTMod.Instance.overloadBar.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}
