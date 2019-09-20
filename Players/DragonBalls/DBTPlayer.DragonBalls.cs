using DBT.Helpers;
using DBT.Items.DragonBalls;
using System;
using System.Collections.Generic;
using Terraria;
using WebmilioCommons.Extensions;

namespace DBT.Players
{
    public partial class DBTPlayer
    {
        int soundTimer = 0;
        public void PostUpdateDragonBalls()
        {
            soundTimer++;
            if (CarryingAllDragonBalls(player) && !WishActive)
                if (soundTimer > 300)
                {
                    SoundHelper.PlayCustomSound("Sounds/DBReady", player, 0.5f);
                    soundTimer = 0;
                }
                    
        }

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

        public void DoRitual()
        {

        }

        public bool CarryingAllDragonBalls(Player player)
        {
            List<DragonBall> dragonBalls = player.GetItemsByType<DragonBall>(inventory: true);
            List<DragonBallStarCount> dragonBallStars = new List<DragonBallStarCount>(7);

            for (int i = 0; i < dragonBalls.Count; i++)
                if (!dragonBallStars.Contains(dragonBalls[i].StarCount))
                    dragonBallStars.Add(dragonBalls[i].StarCount);

            return dragonBallStars.Count == Enum.GetNames(typeof(DragonBallStarCount)).Length;
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
