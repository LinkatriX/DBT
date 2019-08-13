using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using DBT.Extensions;
using DBT.Transformations;
using Microsoft.Xna.Framework.Graphics;
using DBT.UserInterfaces.Buttons;
using DBT.UserInterfaces.HairMenu.StylePreviews;
using DBT.Players;
using DBT.HairStyles;
using DBT.Helpers;

namespace DBT.UserInterfaces.HairMenu
{
    internal class HairMenu : DBTMenu
    {
        public static bool menuVisible;
        private bool selected = false;
        public UIImage backPanelImage;
        public string hairText = null;

        public override void OnInitialize()
        {
            base.OnInitialize();

            BackPanel = new UIPanel();

            BackPanel.Width.Set(384f, 0f);
            BackPanel.Height.Set(407f, 0f);
            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);
            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);

            backPanelImage = new UIImage(StylePreviewGFX.hairBackPanel);
            backPanelImage.Width.Set(StylePreviewGFX.hairBackPanel.Width, 0f);
            backPanelImage.Height.Set(StylePreviewGFX.hairBackPanel.Height, 0f);
            backPanelImage.Left.Set(-12, 0f);
            backPanelImage.Top.Set(-12, 0f);

            InitializeText("Hair Selection Menu", 8, -16, 0.88f, Color.White, backPanelImage);

            //InitializeText("Choose a style for your hair in each of the following columns.\nYou must select a style for each form presented below.", 8, 6, 0.62f, Color.White, backPanelImage);

            InitializeText("Style 1: Goku", 12, 58, 0.56f, Color.White, backPanelImage);

            InitializeButton(StylePreviewGFX.style1BasePreview, new MouseEvent((evt, element) => SelectHairStyle(HairStyleManager.Instance.Goku, evt, element)), 32, 80, backPanelImage);


            InitializeText("Style 2: Gogeta", 12, 120, 0.56f, Color.White, backPanelImage);

            /*InitializeButton(ref _style2BaseButton, GFX.style2BasePreview, new MouseEvent((evt, element) => SelectHairStyle(2, evt, element)), 36, 142, backPanelImage);


            InitializeText("Style 3: Vegeta", 0.56f, 12, 182, Color.White, backPanelImage);

            InitializeButton(ref _style3BaseButton, GFX.style3BasePreview, new MouseEvent((evt, element) => SelectHairStyle(3, evt, element)), 36, 204, backPanelImage);

            InitializeText("Style 4: Raditz", 0.56f, 12, 244, Color.White, backPanelImage);

            InitializeButton(ref _style4BaseButton, GFX.style4BasePreview, new MouseEvent((evt, element) => SelectHairStyle(4, evt, element)), 36, 266, backPanelImage);

            InitializeText("Style 5: Broly", 0.56f, 12, 306, Color.White, backPanelImage);

            InitializeButton(ref _style5BaseButton, GFX.style5BasePreview, new MouseEvent((evt, element) => SelectHairStyle(5, evt, element)), 36, 328, backPanelImage);


            InitializeButton(ref _confirmButton, GFX.hairConfirmButton, new MouseEvent(ConfirmHair), GFX.hairBackPanel.Width - GFX.hairConfirmButton.Width - 12, GFX.hairBackPanel.Height - GFX.hairConfirmButton.Height - 7, backPanelImage);

            InitializeHoverTextButton(ref _keepHairButton, GFX.keepHairButton, new MouseEvent((evt, element) => SelectHairStyle(0, evt, element)), 12, 372, backPanelImage, "Use the hair you chose at character creation for your base form hairstyle instead of the above ones.");*/


            BackPanel.Append(backPanelImage);

            Append(BackPanel);
        }

        private void ConfirmHair(UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer player = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            if (selected)
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
                menuVisible = false;
                player.HairChecked = true;
            }
            else
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose, Main.LocalPlayer.position);
            }
        }

        // TODO Change this to dynamicity.
        private void SelectHairStyle(HairStyle Choice, UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer player = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            player.ChosenHairStyle = Choice;
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            selected = true;
        }
    }
}