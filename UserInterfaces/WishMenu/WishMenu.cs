using DBT.Helpers;
using DBT.Players;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace DBT.UserInterfaces.WishMenu
{
    internal class WishMenu : DBTMenu
    {
        public static bool menuVisible;
        private static string _descTextValue = "Select one of the wishes above to grant your deepest desire.\nCertain wishes have limits.";
        private static string _wishTextValue = "";

        public enum WishSelectionID : int
        {
            None = 0,
            Power = 1,
            Wealth = 2,
            Immortality = 3,
            Genetic = 4,
            Skill = 5,
            Awakening = 6,
            Ritual = 7
        }
        public static WishSelectionID wishSelection;

        public override void OnInitialize()
        {
            base.OnInitialize();

            BackPanel = new UIPanel();
            BackPanel.Width.Set(364f, 0f);
            BackPanel.Height.Set(192f, 0f);
            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f, 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);
            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);

            BackPanelImage = new UIImage(WishMenuGFX.wishBackPanel);
            BackPanelImage.Width.Set(WishMenuGFX.wishBackPanel.Width, 0f);
            BackPanelImage.Height.Set(WishMenuGFX.wishBackPanel.Height, 0f);
            BackPanelImage.Left.Set(-12, 0f);
            BackPanelImage.Top.Set(-12, 0f);

            InitializeText("I Wish for...", 8, 6, 0.66f, Color.Yellow, BackPanelImage);

            InitializeText(_wishTextValue, 10, 82, 0.8f, new Color(244, 203, 39), BackPanelImage);

            InitializeText(_descTextValue, 10, 100, 0.66f, Color.Yellow, BackPanelImage);

            InitializeButton(WishMenuGFX.wishforPower, new MouseEvent(SelectButtonPower), 10, 22, BackPanelImage);

            InitializeButton(WishMenuGFX.wishforWealth, new MouseEvent(SelectButtonWealth), 55, 22, BackPanelImage);

            InitializeButton(WishMenuGFX.wishforImmortality, new MouseEvent(SelectButtonImmortality), 100, 22, BackPanelImage);

            InitializeButton(WishMenuGFX.wishforGenetics, new MouseEvent(SelectButtonGenetics), 145, 22, BackPanelImage);

            InitializeButton(WishMenuGFX.wishforSkill, new MouseEvent(SelectButtonSkill), 190, 22, BackPanelImage);

            InitializeButton(WishMenuGFX.wishforAwakening, new MouseEvent(SelectButtonAwakening), 235, 22, BackPanelImage);

            RitualButton = InitializeButton(WishMenuGFX.wishforRitual, new MouseEvent(SelectButtonRitual), 280, 22, BackPanelImage);

            InitializeButton(WishMenuGFX.grantButton, new MouseEvent(GrantWish), WishMenuGFX.wishBackPanel.Width - WishMenuGFX.grantButton.Width - 12, WishMenuGFX.wishBackPanel.Height - WishMenuGFX.grantButton.Height - 12, BackPanelImage);

            InitializeText("Grant Wish", 14, -12, 0.66f, Color.Yellow);

            BackPanel.Append(BackPanelImage);

            Append(BackPanel);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (!NPC.LunarApocalypseIsUp)
            {
                RitualButton.Width = StyleDimension.Empty;
                RitualButton.Height = StyleDimension.Empty;
                RitualButton.SetVisibility(0f, 0f);
            } 

        }

        private void SelectButtonPower(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            _wishTextValue = "Power";
            _descTextValue = "Wish for a permanent increase in\nMaximum Health, Maximum Ki and Damage.\nWish limit = " + DBTPlayer.POWER_WISH_MAXIMUM + ", Wishes left = " + modPlayer.PowerWishesLeft;                
            wishSelection = WishSelectionID.Power;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void SelectButtonWealth(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            _wishTextValue = "Wealth";
            _descTextValue = "Wish for money beyond your wildest dreams,\n5 Platinum coins in pre-hardmode,\n50 Platinum coins + lucky coin in hardmode.\nWish limit = ∞, Wishes left = ∞";
            wishSelection = WishSelectionID.Wealth;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void SelectButtonImmortality(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            _wishTextValue = "Immortality";
            _descTextValue = "Wish for infinite life, reviving at full life\nfor the next 3 deaths.\nWish limit = 1, Wishes left = " + modPlayer.ImmortalityWishesLeft;
            wishSelection = WishSelectionID.Immortality;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void SelectButtonGenetics(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            _wishTextValue = "Genetic Reshuffle";
            _descTextValue = "Wish for a new [guaranteed] genetic trait.\nAll traits have an equal chance of being rolled.\nWish limit = ∞, Wishes left = ∞";
            wishSelection = WishSelectionID.Genetic;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void SelectButtonSkill(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            _wishTextValue = "Technique";
            _descTextValue = "Wish for a powerful attack. What you get depends on\nhow many times you have wished this.\nWish limit = 3, Wishes left = " + modPlayer.SkillWishesLeft;
            wishSelection = WishSelectionID.Skill;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void SelectButtonAwakening(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            _wishTextValue = "Awakening";
            _descTextValue = "Wish to awaken your latent power.\nInstantly masters your strongest form.\nWish limit = 5, Wishes left = " + modPlayer.AwakeningWishesLeft;
            wishSelection = WishSelectionID.Awakening;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void SelectButtonRitual(UIMouseEvent evt, UIElement listeningElement)
        {
            Deactivate();
            Initialize();
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            _wishTextValue = "Summon Recollection";
            _descTextValue = "Wish to recall ancient spirits of the past.\nBegins the ritual.";
            wishSelection = WishSelectionID.Ritual;
            Main.PlaySound(SoundID.MenuTick);
        }

        private void GrantWish(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            bool usedWish = false;
            switch (wishSelection)
            {
                case WishSelectionID.Power:
                    if (modPlayer.PowerWishesLeft > 0)
                    {
                        usedWish = true;
                        DoPowerWish();
                    }
                    else
                    {
                        Main.PlaySound(SoundID.MenuClose);
                    }
                    break;
                case WishSelectionID.Wealth:
                    usedWish = true;
                    DoWealthWish();
                    break;
                case WishSelectionID.Immortality:
                    if (modPlayer.ImmortalityWishesLeft > 0)
                    {
                        usedWish = true;
                        DoImmortalityWish();
                    }
                    else
                    {
                        Main.PlaySound(SoundID.MenuClose);
                    }
                    break;
                case WishSelectionID.Genetic:
                    usedWish = true;
                    DoGeneticWish();
                    break;
                case WishSelectionID.Awakening:
                    if (modPlayer.AwakeningWishesLeft > 0)
                    {
                        usedWish = true;
                        DoAwakeningWish();
                    }
                    else
                    {
                        Main.PlaySound(SoundID.MenuClose);
                    }
                    break;
                case WishSelectionID.Ritual:
                    usedWish = true;
                    DoRitualWish();
                    break;
                default:
                    break;
            }

            if (usedWish)
            {
                SoundHelper.PlayCustomSound("Sounds/WishGranted", player, 0.5f);
                wishSelection = WishSelectionID.None;
                modPlayer.DestroyOneOfEachDragonBall(modPlayer.player);
                modPlayer.WishActive = false;
                Main.PlaySound(SoundID.MenuClose);
            }

            Initialize();
        }  

        private static void DoPowerWish()
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            modPlayer.MaxKiModifierPerm += 500;
            modPlayer.PowerWishesLeft -= 1;
            menuVisible = false;
            modPlayer.WishActive = false;
        }

        private static void DoWealthWish()
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            Player player = Main.LocalPlayer;
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.PlatinumCoin, 50);
                player.QuickSpawnItem(ItemID.LuckyCoin, 1);
            }
            else
            {
                player.QuickSpawnItem(ItemID.PlatinumCoin, 5);
            }
            
            menuVisible = false;
            modPlayer.WishActive = false;
        }

        private static void DoImmortalityWish()
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            modPlayer.ImmortalityRevivesLeft += 3;
            modPlayer.ImmortalityWishesLeft -= 1;
            menuVisible = false;
            modPlayer.WishActive = false;
        }

        private static void DoGeneticWish()
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            //modPlayer.Trait = TraitManager.Instance.GetRandomTrait(modPlayer.PlayerTrait);

            menuVisible = false;
            modPlayer.WishActive = false;
        }

        private static void DoAwakeningWish()
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            modPlayer.AwakeningWishesLeft -= 1;
            modPlayer.ChangeMastery(modPlayer.AcquiredTransformations.Keys.Last(), 1f);
            menuVisible = false;
            modPlayer.WishActive = false;
        }

        private static void DoRitualWish()
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            modPlayer.DoRitual();
            menuVisible = false;
            modPlayer.WishActive = false;
        }

        public UIImageButton RitualButton { get; set; }
    }
}