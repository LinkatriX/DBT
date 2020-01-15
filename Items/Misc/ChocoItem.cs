using DBT.Helpers;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Misc
{
    public abstract class ChocoItem : DBTItem
    {
        protected ChocoItem(string displayName, string tooltip, int width, int height) : base(displayName, tooltip, width, height) 
        {
        }

        public override bool ItemSpace(Player player)
        {
            return true;
        }

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override bool OnPickup(Player player)
        {
            SoundHelper.PlayVanillaSound(SoundID.NPCDeath7, player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            dbtPlayer.ModifyKi(25);
            player.statLife += 15;
            player.HealEffect(10);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), 25, false, false);
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }

    public class Choco1 : ChocoItem 
    {
        public Choco1() : base("Chocolate", "A tasty treat created with magic.", 20, 8) 
        {
        }
    }

    public class Choco2 : ChocoItem
    {
        public Choco2() : base("Chocolate", "A tasty treat created with magic.", 26, 14)
        {
        }
    }

    public class Choco3 : ChocoItem
    {
        public Choco3() : base("Chocolate", "A tasty treat created with magic.", 14, 22)
        {
        }
    }

    public class CoffeeCandy : ChocoItem 
    {
        public CoffeeCandy() : base("Coffee Candy", "A coffee flavored treat created with magic.", 14, 14) 
        {
        }
    }
}
