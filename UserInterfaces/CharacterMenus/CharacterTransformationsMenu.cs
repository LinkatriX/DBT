using System;
using System.Collections.Generic;
using System.Linq;
using DBT.Dynamicity;
using DBT.Players;
using DBT.Transformations;
using DBT.UserInterfaces.Buttons;
using DBT.UserInterfaces.Tabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using WebmilioCommons.Extensions;

namespace DBT.UserInterfaces.CharacterMenus
{
    public sealed class CharacterTransformationsMenu : DBTMenu
    {
        public const int
            PADDING_X = 10,
            PADDING_Y = PADDING_X,
            TITLE_Y_SPACE = 50,
            SMALL_SPACE = 4;

        private const string
            CHARACTER_MENU_PATH = "UserInterfaces/CharacterMenus",
            UNKNOWN_TEXTURE = CHARACTER_MENU_PATH + "/UnknownImage", UNKNOWN_GRAY_TEXTURE = CHARACTER_MENU_PATH + "/UnknownImageGray", LOCKED_TEXTURE = CHARACTER_MENU_PATH + "/LockedImage";

        private int _panelsYOffset = 0;

        private readonly List<Tab> _tabs = new List<Tab>();
        private readonly Dictionary<UIHoverImageButton, Tab> _tabButtons = new Dictionary<UIHoverImageButton, Tab>();
        private readonly Dictionary<Tab, TransformationDefinition> _tabsForTransformations = new Dictionary<Tab, TransformationDefinition>();

        private readonly Dictionary<TransformationDefinition, UIImagePair> _transformationImagePairs = new Dictionary<TransformationDefinition, UIImagePair>();

        public CharacterTransformationsMenu(Mod authorMod)
        {
            AuthorMod = authorMod;
            BackPanelTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/BackPanel");
            InfoPanelTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/InfoPanel");

            UnknownImageTexture = authorMod.GetTexture(UNKNOWN_TEXTURE);
            UnknownGrayImageTexture = authorMod.GetTexture(UNKNOWN_GRAY_TEXTURE);
            LockedImageTexture = authorMod.GetTexture(LOCKED_TEXTURE);
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
        }

        // The reason all of this is called in OnPlayerEnterWorld is because the icons themselves don't make it to the UI if the player cannot possibly access them. We need to reset the back panel every time.
        internal void OnPlayerEnterWorld(DBTPlayer dbtPlayer)
        {
            _tabs.Clear();
            _tabButtons.Clear();
            _tabsForTransformations.Clear();

            BackPanel.RemoveAllChildren();
            BackPanel.Append(BackPanelImage);

            List<Node<TransformationDefinition>> rootNodes = TransformationDefinitionManager.Instance.Tree.Nodes
                .Where(t => t.Value.CheckPrePlayerConditions() && t.Value.BaseConditions(dbtPlayer) && t.Value.DoesDisplayInCharacterMenu(dbtPlayer))
                .Where(t => t.Parents.Count == 0).ToList();

            List<Node<TransformationDefinition>> others = new List<Node<TransformationDefinition>>();

            int lastXOffset = 0;

            foreach (Node<TransformationDefinition> rootNode in rootNodes)
                if (rootNode.Value.TransformationIcon.Height > _panelsYOffset)
                    _panelsYOffset = rootNode.Value.TransformationIcon.Height;

            _panelsYOffset += PADDING_Y;

            foreach (Node<TransformationDefinition> rootNode in rootNodes)
            {
                if (rootNode.Children.Count == 0)
                {
                    others.Add(rootNode);
                    continue;
                }

                lastXOffset += PADDING_X * 2;// Prior code: (_tabs.Count + 1); changed due to the spacing growing exponentially with each tab.

                UIHoverImageButton tabButton = InitializeHoverTextButton(rootNode.Value.TransformationIcon, rootNode.Value.TabHoverText, OnUITabClick, lastXOffset, PADDING_Y, BackPanelImage);

                UIPanel tabPanel = new UIPanel()
                {
                    Width = new StyleDimension(BackPanelTexture.Width - 20, 0),
                    Height = new StyleDimension(BackPanelTexture.Height - (_panelsYOffset + 10 + PADDING_Y), 0),

                    Left = new StyleDimension(0, 0),
                    Top = new StyleDimension(0, 0),

                    BackgroundColor = Color.Transparent,
                    BorderColor = rootNode.Value.Appearance.GeneralColor.HasValue ? rootNode.Value.Appearance.GeneralColor.Value : Color.White
                };

                BackPanelImage.Append(tabPanel);

                Tab tab = new Tab(tabButton, tabPanel);
                tab.Panel.Deactivate();

                _tabs.Add(tab);
                _tabButtons.Add(tabButton, tab);
                _tabsForTransformations.Add(tab, rootNode.Value);

                int yOffset = 0;

                RecursiveInitializeTransformation(tab.Panel, rootNode, ref yOffset);

                lastXOffset += rootNode.Value.TransformationIcon.Width;
            }
        }

        public override void Update(GameTime gameTime)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            foreach (Tab tab in _tabButtons.Values)
            {
                if (_tabsForTransformations[tab] != LastActiveTransformationTab)
                {
                    tab.Panel.Left.Set(-Main.screenWidth, 0);
                    tab.Panel.Top.Set(-Main.screenHeight, 0);
                }
                else
                {
                    tab.Panel.Left.Set(12, 0);
                    tab.Panel.Top.Set(_panelsYOffset + 6, 0);
                }
            }

            foreach (KeyValuePair<UIHoverImageButton, Tab> kvp in _tabButtons)
            {
                /*if (dbtPlayer.HasAcquiredTransformation(_tabsForTransformations[kvp.Value]))
                {
                    kvp.Value.TabButton.SetImage(Main.magicPixel);
                }
                else*/
                kvp.Value.TabButton.SetImage(_tabsForTransformations[kvp.Value].TransformationIcon);
            }

            // Imported from old transformations menu
            foreach (KeyValuePair<TransformationDefinition, UIImagePair> kvp in _transformationImagePairs)
            {
                bool unlockable = kvp.Key.CanUnlock(dbtPlayer);
                bool visible = kvp.Key.DoesDisplayInCharacterMenu(dbtPlayer);

                if (!visible)
                {
                    kvp.Value.button.Width = StyleDimension.Empty;
                    kvp.Value.button.Height = StyleDimension.Empty;
                    kvp.Value.button.SetVisibility(0f, 0f);
                }

                kvp.Value.unknownImage.ImageScale = visible && unlockable ? 0f : 1f;
                kvp.Value.unknownImageGray.ImageScale = visible && unlockable && dbtPlayer.HasAcquiredTransformation(kvp.Key) ? 0f : 1f;
                kvp.Value.lockedImage.ImageScale = visible && unlockable ? 0f : 1f;

                //Main.NewText(kvp.Key.ToString() + "'s unknown image is at" + kvp.Value.unknownImage.ImageScale);
                //Main.NewText(kvp.Key.ToString() + "'s unknown image gray is at" + kvp.Value.unknownImageGray.ImageScale);
                //Main.NewText(kvp.Key.ToString() + "'s locked image is at" + kvp.Value.lockedImage.ImageScale);
            }
        }

        private void DrawTransformation(UIPanel panel, TransformationDefinition transformation, Texture2D icon, int left, int top)
        {
            UIImageButton transformationButton = null;
            UIImage
                unknownImage = null,
                unknownGrayImage = null,
                lockedImage = null;

            transformationButton = InitializeButton(icon, new MouseEvent((evt, element) => TrySelectingTransformation(transformation, evt, element)), left, top, panel);

            unknownImage = InitializeImage(UnknownImageTexture, 0, 0, transformationButton);
            unknownImage.ImageScale = 0f;
            unknownImage.Width.Set(1, 0f);
            unknownImage.Height.Set(1, 0f);

            unknownGrayImage = InitializeImage(UnknownGrayImageTexture, 0, 0, unknownImage);
            unknownGrayImage.ImageScale = 0f;
            unknownGrayImage.Width.Set(1, 0f);
            unknownGrayImage.Height.Set(1, 0f);

            lockedImage = InitializeImage(LockedImageTexture, 0, 0, unknownGrayImage);
            lockedImage.ImageScale = 0f;
            lockedImage.Width.Set(1, 0f);
            lockedImage.Height.Set(1, 0f);

            if (!_transformationImagePairs.ContainsKey(transformation))
                _transformationImagePairs.Add(transformation, new UIImagePair(new Point(left, top), transformationButton, unknownImage, unknownGrayImage, lockedImage));
        }

        private void RecursiveInitializeTransformation(UIPanel panel, Node<TransformationDefinition> node, ref int yOffset)
        {
            TransformationDefinition transformation = node.Value;
            Texture2D texture = transformation.BuffType.GetTexture();

            if (!CheckIfDraw(transformation)) return;
            int xOffset = PADDING_X;

            if (node.Parents.Count > 0 && _transformationImagePairs.ContainsKey(node.Parents[0].Value))
            {
                Node<TransformationDefinition> previousNode = node.Parents[0];
                UIImagePair previousPair = _transformationImagePairs[previousNode.Value];

                xOffset = _transformationImagePairs[previousNode.Value].position.X + (int)previousPair.button.Width.Pixels + SMALL_SPACE * 2;
            }

            DrawTransformation(panel, transformation, texture, xOffset, yOffset);

            for (int i = 0; i < node.Children.Count; i++)
            {
                Node<TransformationDefinition> child = node.Children[i];
                RecursiveInitializeTransformation(panel, child, ref yOffset);

                if (node.Children.Count > 1 && CheckIfDraw(child) && node.Children[node.Children.Count - 1] != child)
                {
                    yOffset += texture.Height + SMALL_SPACE * 2;
                }
            }
        }

        private static bool CheckIfDraw(Node<TransformationDefinition> node) => node.Value.DisplayInMenu && node.Value.CheckPrePlayerConditions();
        private static bool CheckIfDraw(TransformationDefinition transformation) => transformation.DisplayInMenu && transformation.CheckPrePlayerConditions();

        private void OnUITabClick(UIMouseEvent evt, UIElement listeningelement)
        {
            UIHoverImageButton button = listeningelement as UIHoverImageButton;

            LastActiveTransformationTab = _tabsForTransformations[_tabButtons[button]];

            InfoPanelOpened = false;
        }


        private void TrySelectingTransformation(TransformationDefinition def, UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            DrawInfoPanel(def);

            if (def.CheckPrePlayerConditions() && dbtPlayer.HasAcquiredTransformation(def) && def.BaseConditions(dbtPlayer))
            {
                // TODO Add sounds.
                //SoundHelper.PlayVanillaSound(SoundID.MenuTick);

                if (!dbtPlayer.SelectedTransformations.Contains(def))
                {
                    dbtPlayer.SelectTransformation(def);
                    Main.NewText($"Selected {def.DisplayName}, Mastery: {Math.Round(def.GetMaxMastery(dbtPlayer) * def.GetCurrentMastery(dbtPlayer), 2)}%");
                }
                else
                    Main.NewText($"{def.DisplayName} Mastery: {Math.Round(100f * def.GetCurrentMastery(dbtPlayer), 2)}%");
            }
        }

        private void DrawInfoPanel(TransformationDefinition def)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            

            bool hasOverload = false;

            if (!InfoPanelOpened)
            {
                InfoPanelOpened = true;
                InfoPanel = InitializeImage(InfoPanelTexture, -12, 294, BackPanel);
                InfoPanel.Width.Set(InfoPanelTexture.Width, 0f);
                InfoPanel.Height.Set(InfoPanelTexture.Height, 0f);

                if (def.GetUnmasteredOverloadGrowthRate(dbtPlayer) > 0)
                    hasOverload = true;

                formName = InitializeText(def.DisplayName, 12, 8, 0.8f, def.Appearance.GeneralColor.Value, InfoPanel);

                if (def.GetMasteredKiDrain(dbtPlayer) <= 0 && def.GetUnmasteredKiDrain(dbtPlayer) != 0)
                {
                    formStats = InitializeText("Stats: \nDamage: " + (def.GetDamageMultiplier(dbtPlayer) - 1f) * 100 + "% \nSpeed: "
                    + (def.GetSpeedMultiplier(dbtPlayer) - 1f) * 100 + "% \nKi Drain: " + def.GetUnmasteredKiDrain(dbtPlayer) * 60 + "/s"
                    + (hasOverload ? "\nOverload: While Unmastered = " + def.GetUnmasteredOverloadGrowthRate(dbtPlayer) * 60
                    + "/s + While Mastered = " + def.GetMasteredOverloadGrowthRate(dbtPlayer) * 60 + "/s" : null), 12, 28, 0.6f, Color.White, InfoPanel);
                }
                else
                {
                    if (def.GetUnmasteredKiDrain(dbtPlayer) <= 0)
                    {
                        formStats = InitializeText("Stats: \nDamage: " + (def.GetDamageMultiplier(dbtPlayer) - 1f) * 100 + "% \nSpeed: " +
                        (def.GetSpeedMultiplier(dbtPlayer) - 1f) * 100 + "%" + (hasOverload ? "\nOverload: While Unmastered = " +
                        def.GetUnmasteredOverloadGrowthRate(dbtPlayer) * 60 + "/s + While Mastered = " +
                        def.GetMasteredOverloadGrowthRate(dbtPlayer) * 60 + "/s" : null), 12, 28, 0.6f, Color.White, InfoPanel);
                    }
                    else
                    {
                        formStats = InitializeText("Stats: \nDamage: " + (def.GetDamageMultiplier(dbtPlayer) - 1f) * 100 + "% \nSpeed: "
                        + (def.GetSpeedMultiplier(dbtPlayer) - 1f) * 100 + "% \nKi Drain: While Unmastered = " + def.GetUnmasteredKiDrain(dbtPlayer) * 60
                        + "/s + While Mastered = " + def.GetMasteredKiDrain(dbtPlayer) * 60 + "/s" + (hasOverload ? "\nOverload: While Unmastered = "
                        + def.GetUnmasteredOverloadGrowthRate(dbtPlayer) * 60 + "/s + While Mastered = " + def.GetMasteredOverloadGrowthRate(dbtPlayer) * 60
                        + "/s" : null), 12, 28, 0.6f, Color.White, InfoPanel);
                    }
                }
                formUnlock = InitializeText(def.DisplayName, 30, 16, 0f, Color.White, InfoPanel);
            }
            else
            {
                InfoPanel = null;
                formName = null;
                formStats = null;
                formUnlock = null;

                InfoPanelOpened = false;
                DrawInfoPanel(def);
            }
        }

        public Mod AuthorMod { get; }
        public bool Visible { get; set; }

        public Texture2D UnknownImageTexture { get; }
        public Texture2D UnknownGrayImageTexture { get; }
        public Texture2D LockedImageTexture { get; }

        public Texture2D InfoPanelTexture { get; }
        public bool InfoPanelOpened { get; internal set; }

        public UIImage InfoPanel { get; set; } = null;

        public UIText
            formName = null,
            formStats = null,
            formUnlock = null;

        public static TransformationDefinition LastActiveTransformationTab { get; internal set; }
    }
}
