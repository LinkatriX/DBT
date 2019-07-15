using System;
using System.Collections.Generic;
using System.Linq;
using DBT.Dynamicity;
using DBT.Extensions;
using DBT.Players;
using DBT.Transformations;
using DBT.UserInterfaces.Tabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBT.UserInterfaces.CharacterMenus
{
    public sealed class CharacterTransformationsMenu : DBTMenu
    {
        public const int
            PADDING_X = 20,
            PADDING_Y = PADDING_X,
            TITLE_Y_SPACE = 50,
            SMALL_SPACE = 4;

        private const string 
            CHARACTER_MENU_PATH = "UserInterfaces/CharacterMenus",
            UNKNOWN_TEXTURE = CHARACTER_MENU_PATH + "/UnknownImage", UNKNOWN_GRAY_TEXTURE = CHARACTER_MENU_PATH + "/UnknownImageGray", LOCKED_TEXTURE = CHARACTER_MENU_PATH + "/LockedImage";

        private readonly List<Tab> _tabs = new List<Tab>();
        private readonly List<UIElement> _elements = new List<UIElement>();

        public CharacterTransformationsMenu(Mod authorMod)
        {
            AuthorMod = authorMod;
            BackPanelTexture = authorMod.GetTexture(CHARACTER_MENU_PATH + "/BackPanel");

            UnknownImageTexture = authorMod.GetTexture(UNKNOWN_TEXTURE);
            UnknownGrayImageTexture = authorMod.GetTexture(UNKNOWN_GRAY_TEXTURE);
            LockedImageTexture = authorMod.GetTexture(LOCKED_TEXTURE);
        }

        public override void OnInitialize()
        {
            BackPanel = new UIPanel();

            BackPanel.Width.Set(BackPanelTexture.Width, 0f);
            BackPanel.Height.Set(BackPanelTexture.Height, 0f);

            BackPanel.Left.Set(Main.screenWidth / 2f - BackPanel.Width.Pixels / 2f , 0f);
            BackPanel.Top.Set(Main.screenHeight / 2f - BackPanel.Height.Pixels / 2f, 0f);

            BackPanel.BackgroundColor = new Color(0, 0, 0, 0);

            Append(BackPanel);


            BackPanelImage = new UIImage(BackPanelTexture);
            BackPanelImage.Width.Set(BackPanelTexture.Width, 0f);
            BackPanelImage.Height.Set(BackPanelTexture.Height, 0f);

            BackPanelImage.Left.Set(-12, 0f);
            BackPanelImage.Top.Set(-12, 0f);

            BackPanel.Append(BackPanelImage);

            //TitleText = InitializeText("Character Menu", BackPanelTexture.Bounds.X, -32 + TITLE_Y_SPACE, 1, Color.White);

            base.OnInitialize();
        }

        internal void OnPlayerEnterWorld(DBTPlayer dbtPlayer)
        {
            List<Node<TransformationDefinition>> rootNodes = TransformationDefinitionManager.Instance.Tree.Nodes
                .Where(t => t.Value.CheckPrePlayerConditions() && t.Value.BaseConditions(dbtPlayer) && t.Value.DoesDisplayInCharacterMenu(dbtPlayer))
                .Where(t => t.Parents.Count == 0).ToList();

            List<Node<TransformationDefinition>> others = new List<Node<TransformationDefinition>>();

            foreach (Node<TransformationDefinition> rootNode in rootNodes)
            {
                if (rootNode.Children.Count == 0)
                {
                    others.Add(rootNode);
                    continue;
                }

                UIImageButton tabButton = new UIImageButton(rootNode.Value.TransformationIcon);

                _tabs.Add(new Tab());
            }

            //_tabButtons.Add();
        }

        private static void TrySelectingTransformation(TransformationDefinition def, UIMouseEvent evt, UIElement listeningElement)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

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

        public Mod AuthorMod { get; }
        public bool Visible { get; set; }

        public Texture2D UnknownImageTexture { get; }
        public Texture2D UnknownGrayImageTexture { get; }
        public Texture2D LockedImageTexture { get; }
    }
}
