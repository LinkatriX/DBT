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
using ReLogic.Graphics;

namespace DBT.UserInterfaces.HairMenu
{
    internal class HairMenu : DBTMenu
    {
        public static bool menuVisible;
        private bool _selected = false;
        public int hairSelected = 0;
        public string hairText = "Goku";
        public string totalText = "1/7";
        public Texture2D hairTexture = HairGFX.style1;
        public Texture2D prevHairTexture = HairGFX.style7M;
        public Texture2D nextHairTexture = HairGFX.style2M;
        public bool legendaryTab = false;
        public Texture2D panelTexture = HairGFX.hairBackPanel;
        public enum HairSelection : int
        {
            Goku = 1,
            Vegeta = 2,
            Broly = 3,
            Kale = 4,
            Caulifla = 5,
            Gine = 6,
            Nappa = 7,
            FutureGohan = 8
        }
        public static HairSelection hairSelection = HairSelection.Goku;

        public override void OnInitialize()
        {
            base.OnInitialize();

            BackPanel = new UIPanel();

            BackPanel.Width.Set(263f, 0f);
            BackPanel.Height.Set(447f, 0f);
            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f + 12, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);
            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);
            BackPanel.BorderColor = new Color(0, 0, 0, 0);

            InitializeButton(HairGFX.arrowLeft, new MouseEvent(LastHairStyle), 55, -2, BackPanel);

            InitializeButton(HairGFX.arrowRight, new MouseEvent(NextHairStyle), 108, -2, BackPanel);

            InitializeButton(HairGFX.hairConfirmButton, new MouseEvent(ConfirmHair), 126, 113, BackPanel);

            InitializeButton(HairGFX.baseSelect, new MouseEvent(SelectBaseStyles), 54, 18, BackPanel);

            InitializeButton(HairGFX.legendarySelect, new MouseEvent(SelectLegendaryStyles), 111, 18, BackPanel);

            InitializeButton(HairGFX.keepHairButton, new MouseEvent(ConfirmVanillaHair), -47, 113, BackPanel);

            Append(BackPanel);
        }

        public override void Update(GameTime gameTime)
        {
            if (legendaryTab)
                panelTexture = HairGFX.hairBackPanelL;
            else
                panelTexture = HairGFX.hairBackPanel;
            int currentSelection = (int)hairSelection;
            totalText = currentSelection + "/" + 8;
            CanDrag = false;
            #region Cancer
            if (hairSelection == HairSelection.Goku)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style1L;
                else
                    hairTexture = HairGFX.style1;
                hairText = "Goku";
                prevHairTexture = HairGFX.style8M;
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
                nextHairTexture = HairGFX.style8M;
            }
            if (hairSelection == HairSelection.FutureGohan)
            {
                if (legendaryTab)
                    hairTexture = HairGFX.style8L;
                else
                    hairTexture = HairGFX.style8;
                hairText = "F.Gohan";
                prevHairTexture = HairGFX.style7M;
                nextHairTexture = HairGFX.style1M;
            }
            #endregion
            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            DrawBackPanel(spriteBatch);
            DrawMiddleHair(spriteBatch);
            DrawNextHair(spriteBatch);
            DrawPrevHair(spriteBatch);
            DrawTexts(spriteBatch);
        }

        public void DrawTexts(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Main.fontMouseText, "Hair Selection Menu", new Vector2(BackPanel.Left.Pixels + 28, BackPanel.Top.Pixels - 30), Color.WhiteSmoke, 0, Vector2.Zero, 0.88f, SpriteEffects.None, 0);
            spriteBatch.DrawString(Main.fontMouseText, totalText, new Vector2(BackPanel.Left.Pixels + 90, BackPanel.Top.Pixels + 16), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(Main.fontMouseText, hairText, new Vector2(BackPanel.Left.Pixels + 84, BackPanel.Top.Pixels - 4), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0);
        }

        public void DrawMiddleHair(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hairTexture, new Vector2(BackPanel.Left.Pixels + 68, BackPanel.Top.Pixels + 42), Color.White);
        }
        public void DrawPrevHair(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(prevHairTexture, new Vector2(BackPanel.Left.Pixels - 4, BackPanel.Top.Pixels + 42), Color.White);
        }
        public void DrawNextHair(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(nextHairTexture, new Vector2(BackPanel.Left.Pixels + 143, BackPanel.Top.Pixels + 42), Color.White);
        }

        public void DrawBackPanel(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelTexture, new Vector2(BackPanel.Left.Pixels - 12, BackPanel.Top.Pixels - 12), Color.White);
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
                case HairSelection.FutureGohan:
                    player.ChosenHairStyle = HairStyleManager.Instance.FutureGohan;
                    break;
            }
        }

        private void ConfirmVanillaHair(UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer player = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            menuVisible = false;
            player.ChosenHairStyle = HairStyleManager.Instance.NoChoice;
            player.HairChecked = true;
        }
        private void LastHairStyle(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            if (hairSelection == HairSelection.Goku)
                hairSelection = HairSelection.FutureGohan;
            else
                hairSelection -= 1;
        }
        private void NextHairStyle(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundHelper.PlayVanillaSound(SoundID.MenuTick, Main.LocalPlayer.position);
            if (hairSelection == HairSelection.FutureGohan)
                hairSelection = HairSelection.Goku;
            else
                hairSelection += 1;
        }

        private void SelectBaseStyles(UIMouseEvent evt, UIElement listeningElement)
        {
            legendaryTab = false;
        }
        private void SelectLegendaryStyles(UIMouseEvent evt, UIElement listeningElement)
        {
            legendaryTab = true;
        }
    }
}