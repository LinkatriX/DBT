using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DBT.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using DBT.UserInterfaces.Buttons;
using DBT.UserInterfaces.Tabs;

namespace DBT.UserInterfaces.KiAttackUI
{
	public class KiBrowserUIMenu : DBTMenu
	{
		public KiBrowserUIMenu()
		{
			BackPanelTexture = ModContent.GetTexture("DBT/UserInterfaces/CharacterMenus/UnknownImage");
		}

		public override void OnInitialize()
		{
			BackPanel = new UIPanel();

			BackPanel.Width.Set(BackPanelTexture.Width, 0f);
			BackPanel.Height.Set(BackPanelTexture.Height, 0f);

			BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f, 0f);
			BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);

			BackPanel.BackgroundColor = new Color(0, 0, 0, 0);

			Append(BackPanel);

			BackPanelImage = new UIImage(BackPanelTexture);
			BackPanelImage.Width.Set(BackPanelTexture.Width, 0f);
			BackPanelImage.Height.Set(BackPanelTexture.Height, 0f);

			BackPanelImage.Left.Set(-12, 0f);
			BackPanelImage.Top.Set(-12, 0f);

			BackPanel.Append(BackPanelImage);

			base.OnInitialize();

			//KiSlotButtonDefinition button = InitializeKiMenuButton(OnClick, text, "HAHA", true, 0f, 6, BackPanel);

			UIHoverImageButton button = InitializeHoverTextButton(text, "Bruh",OnClick, 0, 6, BackPanelImage);

			UIPanel tabPanel = new UIPanel()
			{
				Width = new StyleDimension(BackPanelTexture.Width - 20, 0),
				Height = new StyleDimension(BackPanelTexture.Height - (0 + 10 + 6), 0),

				Left = new StyleDimension(0, 0),
				Top = new StyleDimension(0, 0),
			};

			Tab tab = new Tab(button, tabPanel);

			BackPanel.Append(tabPanel);
			tab.Panel.Deactivate();
		}

		private void OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.NewText("T is a clown");
		}

		Texture2D text = ModContent.GetTexture("DBT/UserInterfaces/CharacterMenus/UnknownImage");

		public bool Visible { get; set; }
	}
}
