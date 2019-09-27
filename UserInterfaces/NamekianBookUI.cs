using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using DBT.Players;
using DBT.Helpers;
using ReLogic.Graphics;

namespace DBT.UserInterfaces
{
    internal class NamekianBookUI : DBTMenu
    {
        public override void OnInitialize()
        {
            base.OnInitialize();

            BackPanelImage = new UIImage(DBTMod.Instance.GetTexture("UserInterfaces/NamekianBookUI"));

            BackPanelImage.Width.Set(598f, 0f);
            BackPanelImage.Height.Set(400f, 0f);
            BackPanelImage.Left.Set(-12, 0f);
            BackPanelImage.Top.Set(-12, 0f);

            BackPanel = new UIPanel();

            BackPanel.Width.Set(BackPanelImage.Width.Pixels, 0f);
            BackPanel.Height.Set(BackPanelImage.Height.Pixels, 0f);

            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);

            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);
            BackPanel.BorderColor = Color.Transparent;

            Append(BackPanel);

            InitializeText("An old legend says that a godly power can\nbe awakened by converging the power of\n5 saiyan souls into one vessel,\nbut only the dragon may grant this strength\nand it's powers must be augmented by some\notherwordly power.", 44, 184, 0.7f, Color.White, BackPanelImage);

            BackPanel.Append(BackPanelImage);
        }

        public bool MenuVisible { get; set; } = false;
    }
}