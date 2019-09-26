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

            BackPanelImage.Width.Set(263f, 0f);
            BackPanelImage.Height.Set(447f, 0f);
            BackPanelImage.Left.Set(Main.screenWidth / 2f - BackPanelImage.Width.Pixels / 2f + 12, 0f);
            BackPanelImage.Top.Set(Main.screenHeight / 2f - BackPanelImage.Height.Pixels / 2f, 0f);


            Append(BackPanelImage);
        }

        public bool MenuVisible { get; set; } = false;
    }
}