using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace DBT.UserInterfaces.HairMenu
{
    public class HairGFX
    {
        private const string
            HAIR_BACK_PANEL = HAIR_MENU_DIRECTORY + "HairBackPanel",
            HAIR_BACK_PANEL_L = HAIR_MENU_DIRECTORY + "HairBackPanelL",
            HAIR_CONFIRM_BUTTON = BUTTON_DIRECTORY + "ConfirmButton",
            KEEP_HAIR_BUTTON = BUTTON_DIRECTORY + "KeepHairButton",
            HAIR_MENU_DIRECTORY = "UserInterfaces/HairMenu/",
            BUTTON_DIRECTORY = HAIR_MENU_DIRECTORY + "Buttons/",
            STYLE_PREVIEWS_DIRECTORY = HAIR_MENU_DIRECTORY + "StylePreviews/",
            STYLE_ONE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style1/",
            STYLE_TWO_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style2/",
            STYLE_THREE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style3/",
            STYLE_FOUR_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style4/",
            STYLE_FIVE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style5/",
            STYLE_SIX_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style6/",
            STYLE_SEVEN_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style7/",
            STYLE_EIGHT_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style8/",
            STYLE_NINE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style9/",
            STYLE_TEN_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style10/",
            STYLE_ONE = STYLE_ONE_DIRECTORY + "Style1",
            STYLE_ONE_L = STYLE_ONE_DIRECTORY + "Style1L",
            STYLE_ONE_M = STYLE_ONE_DIRECTORY + "Style1M",
            STYLE_TWO = STYLE_TWO_DIRECTORY + "Style2",
            STYLE_TWO_L = STYLE_TWO_DIRECTORY + "Style2L",
            STYLE_TWO_M = STYLE_TWO_DIRECTORY + "Style2M",
            STYLE_THREE = STYLE_THREE_DIRECTORY + "Style3",
            STYLE_THREE_L = STYLE_THREE_DIRECTORY + "Style3L",
            STYLE_THREE_M = STYLE_THREE_DIRECTORY + "Style3M",
            STYLE_FOUR = STYLE_FOUR_DIRECTORY + "Style4",
            STYLE_FOUR_L = STYLE_FOUR_DIRECTORY + "Style4L",
            STYLE_FOUR_M = STYLE_FOUR_DIRECTORY + "Style4M",
            STYLE_FIVE = STYLE_FIVE_DIRECTORY + "Style5",
            STYLE_FIVE_L = STYLE_FIVE_DIRECTORY + "Style5L",
            STYLE_FIVE_M = STYLE_FIVE_DIRECTORY + "Style5M",
            STYLE_SIX = STYLE_SIX_DIRECTORY + "Style6",
            STYLE_SIX_L = STYLE_SIX_DIRECTORY + "Style6L",
            STYLE_SIX_M = STYLE_SIX_DIRECTORY + "Style6M",
            STYLE_SEVEN = STYLE_SEVEN_DIRECTORY + "Style7",
            STYLE_SEVEN_L = STYLE_SEVEN_DIRECTORY + "Style7L",
            STYLE_SEVEN_M = STYLE_SEVEN_DIRECTORY + "Style7M",
            STYLE_EIGHT = STYLE_EIGHT_DIRECTORY + "Style8",
            STYLE_EIGHT_L = STYLE_EIGHT_DIRECTORY + "Style8L",
            STYLE_EIGHT_M = STYLE_EIGHT_DIRECTORY + "Style8M",
            STYLE_NINE = STYLE_NINE_DIRECTORY + "Style9",
            STYLE_NINE_L = STYLE_NINE_DIRECTORY + "Style9L",
            STYLE_NINE_M = STYLE_NINE_DIRECTORY + "Style9M",
            STYLE_TEN = STYLE_TEN_DIRECTORY + "Style10",
            STYLE_TEN_L = STYLE_TEN_DIRECTORY + "Style10L",
            STYLE_TEN_M = STYLE_TEN_DIRECTORY + "Style10M",
            ARROW_LEFT = BUTTON_DIRECTORY + "ArrowLeft",
            ARROW_RIGHT = BUTTON_DIRECTORY + "ArrowRight",
            BASE_SELECT = BUTTON_DIRECTORY + "BaseSelect",
            LEGENDARY_SELECT = BUTTON_DIRECTORY + "LegendarySelect";


        public static Texture2D
            hairBackPanel,
            hairBackPanelL,
            hairConfirmButton,
            keepHairButton,
            style1,
            style1L,
            style1M,
            style2,
            style2L,
            style2M,
            style3,
            style3L,
            style3M,
            style4,
            style4L,
            style4M,
            style5,
            style5L,
            style5M,
            style6,
            style6L,
            style6M,
            style7,
            style7L,
            style7M,
            style8,
            style8L,
            style8M,
            style9,
            style9L,
            style9M,
            style10,
            style10L,
            style10M,
            arrowLeft,
            arrowRight,
            baseSelect,
            legendarySelect;

        public static void LoadHairGFX(Mod mod)
        {
            hairBackPanel = mod.GetTexture(HAIR_BACK_PANEL);
            hairBackPanelL = mod.GetTexture(HAIR_BACK_PANEL_L);
            hairConfirmButton = mod.GetTexture(HAIR_CONFIRM_BUTTON);
            keepHairButton = mod.GetTexture(KEEP_HAIR_BUTTON);

            style1 = mod.GetTexture(STYLE_ONE);
            style1L = mod.GetTexture(STYLE_ONE_L);
            style1M = mod.GetTexture(STYLE_ONE_M);
            style2 = mod.GetTexture(STYLE_TWO);
            style2L = mod.GetTexture(STYLE_TWO_L);
            style2M = mod.GetTexture(STYLE_TWO_M);
            style3 = mod.GetTexture(STYLE_THREE);
            style3L = mod.GetTexture(STYLE_THREE_L);
            style3M = mod.GetTexture(STYLE_THREE_M);
            style4 = mod.GetTexture(STYLE_FOUR);
            style4L = mod.GetTexture(STYLE_FOUR_L);
            style4M = mod.GetTexture(STYLE_FOUR_M);
            style5 = mod.GetTexture(STYLE_FIVE);
            style5L = mod.GetTexture(STYLE_FIVE_L);
            style5M = mod.GetTexture(STYLE_FIVE_M);
            style6 = mod.GetTexture(STYLE_SIX);
            style6L = mod.GetTexture(STYLE_SIX_L);
            style6M = mod.GetTexture(STYLE_SIX_M);
            style7 = mod.GetTexture(STYLE_SEVEN);
            style7L = mod.GetTexture(STYLE_SEVEN_L);
            style7M = mod.GetTexture(STYLE_SEVEN_M);
            style8 = mod.GetTexture(STYLE_EIGHT);
            style8L = mod.GetTexture(STYLE_EIGHT_L);
            style8M = mod.GetTexture(STYLE_EIGHT_M);
            style9 = mod.GetTexture(STYLE_NINE);
            style9L = mod.GetTexture(STYLE_NINE_L);
            style9M = mod.GetTexture(STYLE_NINE_M);
            style10 = mod.GetTexture(STYLE_TEN);
            style10L = mod.GetTexture(STYLE_TEN_L);
            style10M = mod.GetTexture(STYLE_TEN_M);
            arrowLeft = mod.GetTexture(ARROW_LEFT);
            arrowRight = mod.GetTexture(ARROW_RIGHT);
            baseSelect = mod.GetTexture(BASE_SELECT);
            legendarySelect = mod.GetTexture(LEGENDARY_SELECT);
        }

        public static void UnloadHairGFX()
        {
            hairBackPanel = null;
            hairBackPanelL = null;
            hairConfirmButton = null;
            keepHairButton = null;
            style1 = null;
            style1L = null;
            style1M = null;
            style2 = null;
            style2L = null;
            style2M = null;
            style3 = null;
            style3L = null;
            style3M = null;
            style4 = null;
            style4L = null;
            style4M = null;
            style5 = null;
            style5L = null;
            style5M = null;
            style6 = null;
            style6L = null;
            style6M = null;
            style7 = null;
            style7L = null;
            style7M = null;
            style8 = null;
            style8L = null;
            style8M = null;
            style9 = null;
            style9L = null;
            style9M = null;
            style10 = null;
            style10L = null;
            style10M = null;
            arrowLeft = null;
            arrowRight = null;
            baseSelect = null;
            legendarySelect = null;
        }
    }
}
