using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.KiBar
{
    public sealed class KiBarLayer : GameInterfaceLayer
    {
        public KiBarLayer() : base(typeof(KiBar).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (DBTMod.Instance.kiBar.Visible)
            {
                DBTMod.Instance.kiBarInterface.Update(Main._drawInterfaceGameTime);
                DBTMod.Instance.kiBar.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}
