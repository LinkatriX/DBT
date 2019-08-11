using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace DBT.UserInterfaces.WishMenu
{
    public class WishMenuGFX
    {
        private const string
            WISH_MENU_DIRECTORY = "UserInterfaces/WishMenu/",
            WISH_BACK_PANEL = WISH_MENU_DIRECTORY + "WishBackPanel",
            GRANT_BUTTON = WISH_MENU_DIRECTORY + "GrantButton",
            WISH_POWER_BUTTON = WISH_MENU_DIRECTORY + "WishforPower",
            WISH_AWAKEN_BUTTON = WISH_MENU_DIRECTORY + "WishforAwakening",
            WISH_IMMORTAL_BUTTON = WISH_MENU_DIRECTORY + "WishforImmortality",
            WISH_GENETIC_BUTTON = WISH_MENU_DIRECTORY + "WishforGenetics",
            WISH_SKILL_BUTTON = WISH_MENU_DIRECTORY + "WishforSkill",
            WISH_WEALTH_BUTTON = WISH_MENU_DIRECTORY + "WishforWealth";

        public static Texture2D
            wishBackPanel,
            wishforPower,
            wishforWealth,
            wishforImmortality,
            wishforGenetics,
            wishforSkill,
            wishforAwakening,
            grantButton;

        public static void LoadWishGFX(Mod mod)
        {
            wishBackPanel = mod.GetTexture(WISH_BACK_PANEL);
            wishforPower = mod.GetTexture(WISH_POWER_BUTTON);
            wishforWealth = mod.GetTexture(WISH_WEALTH_BUTTON);
            wishforImmortality = mod.GetTexture(WISH_IMMORTAL_BUTTON);
            wishforGenetics = mod.GetTexture(WISH_GENETIC_BUTTON);
            wishforSkill = mod.GetTexture(WISH_SKILL_BUTTON);
            wishforAwakening = mod.GetTexture(WISH_AWAKEN_BUTTON);
            grantButton = mod.GetTexture(GRANT_BUTTON);
        }

        public static void UnloadWishGFX()
        {
            wishBackPanel = null;
            wishforPower = null;
            wishforWealth = null;
            wishforImmortality = null;
            wishforGenetics = null;
            wishforSkill = null;
            wishforAwakening = null;
            grantButton = null;
        }
    }
}
