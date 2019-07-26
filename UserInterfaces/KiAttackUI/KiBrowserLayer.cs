using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.KiAttackUI
{
	public sealed class KiBrowserLayer : GameInterfaceLayer
	{
		public KiBrowserLayer(KiBrowserUIMenu browserMenu, UserInterface @interface) : base(browserMenu.GetType().FullName, InterfaceScaleType.UI)
		{
			BrowserMenu = browserMenu;
			Interface = @interface;
		}

		protected override bool DrawSelf()
		{
			if (BrowserMenu.Visible)
				Interface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);

			return true;
		}

		public KiBrowserUIMenu BrowserMenu { get; }

		public UserInterface Interface { get; }
	}
}
