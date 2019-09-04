using System;
using System.Collections.Generic;
using System.Linq;
using DBT.Dynamicity;
using DBT.Extensions;
using DBT.Items;
using DBT.Players;
using DBT.Skills;
using DBT.Transformations;
using DBT.UserInterfaces.Buttons;
using DBT.UserInterfaces.Tabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBT.UserInterfaces.TechniqueMenu
{
    public sealed class TechniqueMenu : DBTMenu
    {
        public const int
            PADDING_X = 10,
            PADDING_Y = PADDING_X,
            TITLE_Y_SPACE = 50,
            SMALL_SPACE = 4;

        private const string
            TECHNIQUE_MENU_PATH = "UserInterfaces/TechniqueMenu",
            UNKNOWN_TEXTURE = TECHNIQUE_MENU_PATH + "/UnknownImage", UNKNOWN_GRAY_TEXTURE = TECHNIQUE_MENU_PATH + "/UnknownImageGray", LOCKED_TEXTURE = TECHNIQUE_MENU_PATH + "/LockedImage";

        private int _panelsYOffset = 0;

        private readonly Dictionary<SkillDefinition, UIImagePair> _skillImagePairs = new Dictionary<SkillDefinition, UIImagePair>();

        public TechniqueMenu(Mod authorMod)
        {
            AuthorMod = authorMod;
            BackPanelTexture = authorMod.GetTexture(TECHNIQUE_MENU_PATH + "/BackPanel");
            InfoPanelTexture = authorMod.GetTexture(TECHNIQUE_MENU_PATH + "/InfoPanel");

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
            BackPanel.RemoveAllChildren();
            BackPanel.Append(BackPanelImage);

            List<Node<SkillDefinition>> rootNodes = SkillDefinitionManager.Instance.Tree.Nodes
                .Where(t => t.Value.CheckPrePlayerConditions())
                .Where(t => t.Parents.Count == 0).ToList();

            List<Node<SkillDefinition>> others = new List<Node<SkillDefinition>>();

            int lastXOffset = 0;

            foreach (Node<SkillDefinition> rootNode in rootNodes)
                if (rootNode.Value.SkillIcon.Height > _panelsYOffset)
                    _panelsYOffset = rootNode.Value.SkillIcon.Height;

            _panelsYOffset += PADDING_Y;

            foreach (Node<SkillDefinition> rootNode in rootNodes)
            {
                if (rootNode.Children.Count == 0)
                {
                    others.Add(rootNode);
                    continue;
                }

                lastXOffset += PADDING_X * 2;// Prior code: (_tabs.Count + 1); changed due to the spacing growing exponentially with each tab.

                int yOffset = 0;

                RecursiveInitializeTransformation(BackPanel, rootNode, ref yOffset);

                lastXOffset += rootNode.Value.SkillIcon.Width;
            }
        }

        public override void Update(GameTime gameTime)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            // Imported from old transformations menu
            foreach (KeyValuePair<SkillDefinition, UIImagePair> kvp in _skillImagePairs)
            {
                bool unlockable = kvp.Key.DoesDisplayInTechniqueMenu(dbtPlayer);
                bool visible = kvp.Key.DoesDisplayInTechniqueMenu(dbtPlayer);

                if (!visible)
                {
                    kvp.Value.button.Width = StyleDimension.Empty;
                    kvp.Value.button.Height = StyleDimension.Empty;
                    kvp.Value.button.SetVisibility(0f, 0f);
                }

                kvp.Value.unknownImage.ImageScale = visible && unlockable ? 0f : 1f;
                kvp.Value.unknownImageGray.ImageScale = visible && unlockable && dbtPlayer.HasAcquiredSkill(kvp.Key) ? 0f : 1f;
                kvp.Value.lockedImage.ImageScale = visible && unlockable ? 0f : 1f;

                //Main.NewText(kvp.Key.ToString() + "'s unknown image is at" + kvp.Value.unknownImage.ImageScale);
                //Main.NewText(kvp.Key.ToString() + "'s unknown image gray is at" + kvp.Value.unknownImageGray.ImageScale);
                //Main.NewText(kvp.Key.ToString() + "'s locked image is at" + kvp.Value.lockedImage.ImageScale);
            }
        }

        private void DrawTransformation(UIPanel panel, SkillDefinition skill, Texture2D icon, int left, int top)
        {
            UIImageButton skillButton = null;
            UIImage
                unknownImage = null,
                unknownGrayImage = null,
                lockedImage = null;

            skillButton = InitializeButton(icon, new MouseEvent((evt, element) => TrySelectingSkill(skill, evt, element)), left, top, panel);

            unknownImage = InitializeImage(UnknownImageTexture, 0, 0, skillButton);
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

            if (!_skillImagePairs.ContainsKey(skill))
                _skillImagePairs.Add(skill, new UIImagePair(new Point(left, top), skillButton, unknownImage, unknownGrayImage, lockedImage));
        }

        private void RecursiveInitializeTransformation(UIPanel panel, Node<SkillDefinition> node, ref int yOffset)
        {
            SkillDefinition skill = node.Value;
            Texture2D texture = skill.SkillIcon;

            if (!CheckIfDraw(skill)) return;
            int xOffset = PADDING_X;

            if (node.Parents.Count > 0 && _skillImagePairs.ContainsKey(node.Parents[0].Value))
            {
                Node<SkillDefinition> previousNode = node.Parents[0];
                UIImagePair previousPair = _skillImagePairs[previousNode.Value];

                xOffset = _skillImagePairs[previousNode.Value].position.X + (int)previousPair.button.Width.Pixels + SMALL_SPACE * 2;
            }

            DrawTransformation(panel, skill, texture, xOffset, yOffset);

            for (int i = 0; i < node.Children.Count; i++)
            {
                Node<SkillDefinition> child = node.Children[i];
                RecursiveInitializeTransformation(panel, child, ref yOffset);

                if (node.Children.Count > 1 && CheckIfDraw(child) && node.Children[node.Children.Count - 1] != child)
                {
                    yOffset += texture.Height + SMALL_SPACE * 2;
                }
            }
        }

        private static bool CheckIfDraw(Node<SkillDefinition> node) => node.Value.CheckPrePlayerConditions();
        private static bool CheckIfDraw(SkillDefinition skill) => skill.CheckPrePlayerConditions();

        private void TrySelectingSkill(SkillDefinition def, UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            DrawInfoPanel(def);

            if (def.CheckPrePlayerConditions() && dbtPlayer.HasAcquiredSkill(def))
            {
                // TODO Add sounds.
                //SoundHelper.PlayVanillaSound(SoundID.MenuTick);

                if (!dbtPlayer.ActiveSkills.Contains(def))
                {
                    //dbtPlayer.SelectTransformation(def);
                    //Main.NewText($"Selected {def.DisplayName}, Mastery: {Math.Round(def.GetMaxMastery(dbtPlayer) * def.GetCurrentMastery(dbtPlayer), 2)}%");
                }
                //else
                    //Main.NewText($"{def.DisplayName} Mastery: {Math.Round(100f * def.GetCurrentMastery(dbtPlayer), 2)}%");
            }
        }

        private void DrawInfoPanel(SkillDefinition def)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            

            bool hasOverload = false;

            if (!InfoPanelOpened)
            {
                InfoPanelOpened = true;
                InfoPanel = InitializeImage(InfoPanelTexture, -12, 294, BackPanel);
                InfoPanel.Width.Set(InfoPanelTexture.Width, 0f);
                InfoPanel.Height.Set(InfoPanelTexture.Height, 0f);

                SkillName = InitializeText(def.DisplayName, 12, 8, 0.8f, Color.White, InfoPanel);
                SkillStats = InitializeText("Stats: \nBase Ki Damage: " + def.Characteristics.BaseDamage + "x \nAttack Speed: " + def.Characteristics.BaseShootSpeed + " \nKi Drain:" + def.Characteristics.ChargeCharacteristics.BaseCastKiDrain * 60 + "/s", 12, 28, 0.6f, Color.White, InfoPanel);
                SkillUnlock = InitializeText(def.DisplayName, 30, 16, 0f, Color.White, InfoPanel);
            }
            else
            {
                InfoPanel = null;
                SkillName = null;
                SkillStats = null;
                SkillUnlock = null;

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
            SkillName = null,
            SkillStats = null,
            SkillUnlock = null;

        public static TransformationDefinition LastActiveTransformationTab { get; internal set; }
    }
}
