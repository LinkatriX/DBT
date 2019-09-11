using DBT.Items.DragonBalls;
using System.Collections.Generic;
using Terraria;

namespace DBT.Players
{
    public partial class DBTPlayer
    {

        public void DestroyOneOfEachDragonBall(Player player)
        {
            List<int> dragonBallTypeAlreadyRemoved = new List<int>();
            foreach (var item in player.inventory)
            {
                if (item == null)
                    continue;
                if (item.modItem == null)
                    continue;
                if (item.modItem is DragonBall)
                {
                    // only remove one of each type of dragon ball. If the player has extras, leave them. Lucky them.
                    if (dragonBallTypeAlreadyRemoved.Contains(item.type))
                        continue;
                    dragonBallTypeAlreadyRemoved.Add(item.type);
                    item.TurnToAir();
                }
            }
        }

        public const int POWER_WISH_MAXIMUM = 5;
        public bool WishActive { get; set; }        
        public int PowerWishesLeft { get; set; } = 5;
        public int ImmortalityWishesLeft { get; set; } = 1;
        public int SkillWishesLeft { get; set; } = 3;
        public int AwakeningWishesLeft { get; set; } = 5;
        public int ImmortalityRevivesLeft { get; set; }
    }
}
