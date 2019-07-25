using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DBT.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using DBT.Items.Accessories;

namespace DBT.UserInterfaces.KiAttackUI
{
	public class KiAttackUIMenu : UIState
	{
		public override void OnInitialize()
		{
			base.OnInitialize();

			BackPanel = new UIPanel();
			BackPanel.Width.Set(480, 0f);
			BackPanel.Height.Set(480, 0f);
			BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f, 0f);
			BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);
			BackPanel.BackgroundColor = new Color(67, 58, 58);
			Append(BackPanel);
		}

		//Texture2D text = ModContent.GetTexture("DBT/Items/Accessories/BatteKit");

		private UIPanel BackPanel { get; set; }

		public delegate void Call(UIMouseEvent evt, UIElement listeningElement);

		public Call NextPageDel;
	}
}
