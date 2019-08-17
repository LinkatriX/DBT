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
        public int hairSelected = 0;
        public string hairText = "Goku";
        public string totalText = "1/7";
        public Texture2D hairTexture = HairGFX.style1;
        public Texture2D prevHairTexture = HairGFX.style7M;
        public Texture2D nextHairTexture = HairGFX.style2M;
        public bool legendaryTab = false;
        public Texture2D panelTexture = HairGFX.hairBackPanel;
        public Texture2D bSelect = HairGFX.baseSelected;
        public Texture2D lSelect = HairGFX.legendarySelect;

        private UIHoverImageButton keepHairButtonThingy;
        public enum HairSelection : int
        {
            Goku = 1,
            Vegeta = 2,
            Broly = 3,
            Kale = 4,
            Caulifla = 5,
            Gine = 6,
            Nappa = 7
        }
        public static HairSelection hairSelection = HairSelection.Goku;

        public override void OnInitialize()
        {
            base.OnInitialize();

            BackPanel = new UIPanel();

            BackPanel.Width.Set(263f, 0f);
            BackPanel.Height.Set(447f, 0f);
            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);
            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);
            BackPanel.BorderColor = new Color(0, 0, 0, 0);

            BackPanelImage = new UIImage(panelTexture);
            BackPanelImage.Width.Set(HairGFX.hairBackPanel.Width, 0f);
            BackPanelImage.Height.Set(HairGFX.hairBackPanel.Height, 0f);
            BackPanelImage.Left.Set(-12, 0f);
            BackPanelImage.Top.Set(-12, 0f);

            InitializeText("Hair Selection Menu", 40, -16, 0.88f, Color.White, BackPanelImage);
           
            InitializeText(hairText, 96, 16, 0.6f, Color.White, BackPanelImage);

            InitializeText(totalText, 96, 28, 0.5f, Color.White, BackPanelImage);

            InitializeImage(hairTexture, 80, 54, BackPanelImage);

            InitializeImage(prevHairTexture, 8, 54, BackPanelImage);

            InitializeImage(nextHairTexture, 155, 54, BackPanelImage);

            InitializeButton(HairGFX.arrowLeft, new MouseEvent(LastHairStyle), 80, 26, BackPanelImage);

            InitializeButton(HairGFX.arrowRight, new MouseEvent(NextHairStyle), 136, 26, BackPanelImage);

            InitializeButton(HairGFX.hairConfirmButton, new MouseEvent(ConfirmHair), 148, 135, BackPanelImage);

            InitializeButton(bSelect, new MouseEvent(SelectBaseStyles), 82, 42, BackPanelImage);

            InitializeButton(lSelect, new MouseEvent(SelectLegendaryStyles), 136, 42, BackPanelImage);

            //InitializeHoverTextButton(HairGFX.keepHairButton, "Press this to instead use the hair you selected at character creation \nfor your base form, then goku style for the other forms.", new MouseEvent(ConfirmVanillaHair), -22, 135, BackPanelImage);

            keepHairButtonThingy = new UIHoverImageButton(HairGFX.keepHairButton, "Press this to instead use the hair you selected at character creation \nfor your base form, then goku style for the other forms.");
            keepHairButtonThingy.Left.Set(-22, 0f);
            keepHairButtonThingy.Top.Set(135, 0f);
            keepHairButtonThingy.OnClick += new MouseEvent(ConfirmVanillaHair);

            BackPanelImage.Append(keepHairButtonThingy);

            BackPanel.Append(BackPanelImage);

            Append(BackPanel);
        }

        public override void Update(GameTime gameTime)
        {
            if (legendaryTab)
                panelTexture = HairGFX.hairBackPanelL;
            else
                panelTexture = HairGFX.hairBackPanel;
            int currentSelection = (int)hairSelection;
            totalText = currentSelection + "/" + 7;
            CanDrag = false;
            #region Cancer
            if (hairSelection == HairSelection.Goku)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style1L;
                else
                    hairTexture = HairGFX.style1;
                hairText = "Goku";
                prevHairTexture = HairGFX.style7M;
                nextHairTexture = HairGFX.style2M;
            }
            if (hairSelection == HairSelection.Vegeta)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style2L;
                else
                    hairTexture = HairGFX.style2;
                hairText = "Vegeta";
                prevHairTexture = HairGFX.style1M;
                nextHairTexture = HairGFX.style3M;
            }
            if (hairSelection == HairSelection.Broly)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style3L;
                else
                    hairTexture = HairGFX.style3;
                hairText = "Broly";
                prevHairTexture = HairGFX.style2M;
                nextHairTexture = HairGFX.style4M;
            }
            if (hairSelection == HairSelection.Kale)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style4L;
                else
                    hairTexture = HairGFX.style4;
                hairText = "Kale";
                prevHairTexture = HairGFX.style3M;
                nextHairTexture = HairGFX.style5M;
            }
            if (hairSelection == HairSelection.Caulifla)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style5L;
                else
                    hairTexture = HairGFX.style5;
                hairText = "Caulifla";
                prevHairTexture = HairGFX.style4M;
                nextHairTexture = HairGFX.style6M;
            }
            if (hairSelection == HairSelection.Gine)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style6L;
                else
                    hairTexture = HairGFX.style6;
                hairText = "Gine";
                prevHairTexture = HairGFX.style5M;
                nextHairTexture = HairGFX.style7M;
            }
            if (hairSelection == HairSelection.Nappa)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style7L;
                else
                    hairTexture = HairGFX.style7;
                hairText = "Nappa";
                prevHairTexture = HairGFX.style6M;
                nextHairTexture = HairGFX.style1M;
            }
            #endregion
            base.Update(gameTime);
        }

        private void ConfirmHair(UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer player = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            menuVisible = false;
            
            player.HairChecked = true;
            switch (hairSelection)
            {
                case HairSelection.Goku:
                    player.ChosenHairStyle = HairStyleManager.Instance.Goku;
                    break;
                case HairSelection.Vegeta:
                    player.ChosenHairStyle = HairStyleManager.Instance.Vegeta;
                    break;
                case HairSelection.Broly:
                    player.ChosenHairStyle = HairStyleManager.Instance.Broly;
                    break;
                case HairSelection.Kale:
                    player.ChosenHairStyle = HairStyleManager.Instance.Kale;
                    break;
                case HairSelection.Caulifla:
                    player.ChosenHairStyle = HairStyleManager.Instance.Caulifla;
                    break;
                case HairSelection.Gine:
                    player.ChosenHairStyle = HairStyleManager.Instance.Gine;
                    break;
                case HairSelection.Nappa:
                    player.ChosenHairStyle = HairStyleManager.Instance.Nappa;
                    break;
            }
            Initialize();
        }

        private void ConfirmVanillaHair(UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer player = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            menuVisible = false;
            player.ChosenHairStyle = HairStyleManager.Instance.NoChoice;
            player.HairChecked = true;
            Initialize();
        }
        private void LastHairStyle(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            if (hairSelection == HairSelection.Goku)
                hairSelection = HairSelection.Nappa;
            else
                hairSelection -= 1;
            Initialize();
        }
        private void NextHairStyle(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            if (hairSelection == HairSelection.Nappa)
                hairSelection = HairSelection.Goku;
            else
                hairSelection += 1;
            Initialize();
        }

        private void SelectBaseStyles(UIMouseEvent evt, UIElement listeningElement)
        {
            legendaryTab = false;
            bSelect = HairGFX.baseSelected;
            lSelect = HairGFX.legendarySelect;
            Initialize();
        }
        private void SelectLegendaryStyles(UIMouseEvent evt, UIElement listeningElement)
        {
            legendaryTab = true;
            bSelect = HairGFX.baseSelect;
            lSelect = HairGFX.legendarySelected;
            Initialize();
        }
    }
}