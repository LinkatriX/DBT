using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace DBT.UserInterfaces.HairMenu.StylePreviews
{
    public class StylePreviewGFX
    {
        private const string
            HAIR_BACK_PANEL = HAIR_MENU_DIRECTORY + "HairBackPanel",
            HAIR_CONFIRM_BUTTON = HAIR_MENU_DIRECTORY + "ConfirmButton",
            KEEP_HAIR_BUTTON = HAIR_MENU_DIRECTORY + "KeepHairButton",
            HAIR_MENU_DIRECTORY = "UserInterfaces/HairMenu/",
            STYLE_PREVIEWS_DIRECTORY = HAIR_MENU_DIRECTORY + "StylePreviews/",
            STYLE_ONE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style1/",
            STYLE_TWO_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style2/",
            STYLE_THREE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style3/",
            STYLE_FOUR_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style4/",
            STYLE_FIVE_DIRECTORY = STYLE_PREVIEWS_DIRECTORY + "Style5/",
            STYLE_ONE_BASE = STYLE_ONE_DIRECTORY + "Base",
            STYLE_ONE_SSJ = STYLE_ONE_DIRECTORY + "SuperSaiyan1",
            STYLE_ONE_SSJ2 = STYLE_ONE_DIRECTORY + "SuperSaiyan2",
            STYLE_ONE_SSJ3 = STYLE_ONE_DIRECTORY + "SuperSaiyan3",
            STYLE_ONE_SSJ4 = STYLE_ONE_DIRECTORY + "SuperSaiyan4",
            STYLE_TWO_BASE = STYLE_TWO_DIRECTORY + "Base",
            STYLE_TWO_SSJ = STYLE_TWO_DIRECTORY + "SuperSaiyan1",
            STYLE_TWO_SSJ2 = STYLE_TWO_DIRECTORY + "SuperSaiyan2",
            STYLE_TWO_SSJ3 = STYLE_TWO_DIRECTORY + "SuperSaiyan3",
            STYLE_TWO_SSJ4 = STYLE_TWO_DIRECTORY + "SuperSaiyan4",
            STYLE_THREE_BASE = STYLE_THREE_DIRECTORY + "Base",
            STYLE_THREE_SSJ = STYLE_THREE_DIRECTORY + "SuperSaiyan1",
            STYLE_THREE_SSJ2 = STYLE_THREE_DIRECTORY + "SuperSaiyan2",
            STYLE_THREE_SSJ3 = STYLE_THREE_DIRECTORY + "SuperSaiyan3",
            STYLE_THREE_SSJ4 = STYLE_THREE_DIRECTORY + "SuperSaiyan4",
            STYLE_FOUR_BASE = STYLE_FOUR_DIRECTORY + "Base",
            STYLE_FOUR_SSJ = STYLE_FOUR_DIRECTORY + "SuperSaiyan1",
            STYLE_FOUR_SSJ2 = STYLE_FOUR_DIRECTORY + "SuperSaiyan2",
            STYLE_FOUR_SSJ3 = STYLE_FOUR_DIRECTORY + "SuperSaiyan3",
            STYLE_FOUR_SSJ4 = STYLE_FOUR_DIRECTORY + "SuperSaiyan4",
            STYLE_FIVE_BASE = STYLE_FIVE_DIRECTORY + "Base",
            STYLE_FIVE_SSJ = STYLE_FIVE_DIRECTORY + "SuperSaiyan1",
            STYLE_FIVE_SSJ2 = STYLE_FIVE_DIRECTORY + "SuperSaiyan2",
            STYLE_FIVE_SSJ3 = STYLE_FIVE_DIRECTORY + "SuperSaiyan3",
            STYLE_FIVE_SSJ4 = STYLE_FIVE_DIRECTORY + "SuperSaiyan4";


        public static Texture2D
            hairBackPanel,
            hairConfirmButton,
            keepHairButton,
            style1BasePreview,
            style1SSJPreview,
            style1SSJ2Preview,
            style1SSJ3Preview,
            style1SSJ4Preview,
            style2BasePreview,
            style2SSJPreview,
            style2SSJ2Preview,
            style2SSJ3Preview,
            style2SSJ4Preview,
            style3BasePreview,
            style3SSJPreview,
            style3SSJ2Preview,
            style3SSJ3Preview,
            style3SSJ4Preview,
            style4BasePreview,
            style4SSJPreview,
            style4SSJ2Preview,
            style4SSJ3Preview,
            style4SSJ4Preview,
            style5BasePreview,
            style5SSJPreview,
            style5SSJ2Preview,
            style5SSJ3Preview,
            style5SSJ4Preview;

        public static void LoadPreviewGFX(Mod mod)
        {
            /*hairBackPanel = mod.GetTexture(HAIR_BACK_PANEL);
            hairConfirmButton = mod.GetTexture(HAIR_CONFIRM_BUTTON);
            keepHairButton = mod.GetTexture(KEEP_HAIR_BUTTON);

            style1BasePreview = mod.GetTexture(STYLE_ONE_BASE);
            style1SSJPreview = mod.GetTexture(STYLE_ONE_SSJ);
            style1SSJ2Preview = mod.GetTexture(STYLE_ONE_SSJ2);
            style1SSJ3Preview = mod.GetTexture(STYLE_ONE_SSJ3);
            style1SSJ4Preview = mod.GetTexture(STYLE_ONE_SSJ4);
            style2BasePreview = mod.GetTexture(STYLE_TWO_BASE);
            style2SSJPreview = mod.GetTexture(STYLE_TWO_SSJ);
            style2SSJ2Preview = mod.GetTexture(STYLE_TWO_SSJ2);
            style2SSJ3Preview = mod.GetTexture(STYLE_TWO_SSJ3);
            style2SSJ4Preview = mod.GetTexture(STYLE_TWO_SSJ4);
            style3BasePreview = mod.GetTexture(STYLE_THREE_BASE);
            style3SSJPreview = mod.GetTexture(STYLE_THREE_SSJ);
            style3SSJ2Preview = mod.GetTexture(STYLE_THREE_SSJ2);
            style3SSJ3Preview = mod.GetTexture(STYLE_THREE_SSJ3);
            style3SSJ4Preview = mod.GetTexture(STYLE_THREE_SSJ4);
            style4BasePreview = mod.GetTexture(STYLE_FOUR_BASE);
            style4SSJPreview = mod.GetTexture(STYLE_FOUR_SSJ);
            style4SSJ2Preview = mod.GetTexture(STYLE_FOUR_SSJ2);
            style4SSJ3Preview = mod.GetTexture(STYLE_FOUR_SSJ3);
            style4SSJ4Preview = mod.GetTexture(STYLE_FOUR_SSJ4);
            style5BasePreview = mod.GetTexture(STYLE_FIVE_BASE);
            style5SSJPreview = mod.GetTexture(STYLE_FIVE_SSJ);
            style5SSJ2Preview = mod.GetTexture(STYLE_FIVE_SSJ2);
            style5SSJ3Preview = mod.GetTexture(STYLE_FIVE_SSJ3);
            style5SSJ4Preview = mod.GetTexture(STYLE_FIVE_SSJ4);*/
        }

        public static void UnloadPreviewGFX()
        {
            /*hairBackPanel = null;
            hairConfirmButton = null;
            keepHairButton = null;
            style1BasePreview = null;
            style1SSJPreview = null;
            style1SSJ2Preview = null;
            style1SSJ3Preview = null;
            style1SSJ4Preview = null;
            style2BasePreview = null;
            style2SSJPreview = null;
            style2SSJ2Preview = null;
            style2SSJ3Preview = null;
            style2SSJ4Preview = null;
            style3BasePreview = null;
            style3SSJPreview = null;
            style3SSJ2Preview = null;
            style3SSJ3Preview = null;
            style3SSJ4Preview = null;
            style4BasePreview = null;
            style4SSJPreview = null;
            style4SSJ2Preview = null;
            style4SSJ3Preview = null;
            style4SSJ4Preview = null;
            style5BasePreview = null;
            style5SSJPreview = null;
            style5SSJ2Preview = null;
            style5SSJ3Preview = null;
            style5SSJ4Preview = null;*/
        }
    }

}
