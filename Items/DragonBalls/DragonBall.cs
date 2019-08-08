using System;
using System.Collections.Generic;
using DBT.Extensions;
using Terraria;
using Terraria.ID;

namespace DBT.Items.DragonBalls
{
    public abstract class DragonBall : DBTItem
    {
        protected DragonBall(DragonBallStarCount starCount) : base(starCount + " Star Dragon Ball", 
            "A mystical ball with " + starCount.ToString().ToLower() + ' ' + 
            (starCount == DragonBallStarCount.One ? "star" : "stars") + " inscribed on it.\nRight-click while holding all seven to make your wish."
            , 20, 20, value: 0, defense: 0, rarity: ItemRarityID.Expert)
        {
            StarCount = starCount;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = false;
        }

        public override bool ConsumeItem(Player player) => false;

        public override bool CanRightClick() => true;

        public bool CarryingAllDragonBalls(Player player)
        {
            List<DragonBall> dragonBalls = player.GetItemsByType<DragonBall>(inventory: true);
            List<DragonBallStarCount> dragonBallStars = new List<DragonBallStarCount>(7);

            for (int i = 0; i < dragonBalls.Count; i++)
                if (!dragonBallStars.Contains(dragonBalls[i].StarCount))
                    dragonBallStars.Add(dragonBalls[i].StarCount);

            return dragonBallStars.Count == Enum.GetNames(typeof(DragonBallStarCount)).Length;
        }

        public static int GetDragonBallNumber(Player player)
        {
            List<DragonBall> dragonBalls = player.GetItemsByType<DragonBall>();
            int starNumber = 0;

            for (int i = 0; i < 6; i++)
            {
                if ((int)dragonBalls[i].StarCount == starNumber)
                {
                    break;
                }
                else
                {
                    starNumber++;
                }
            }
            return starNumber;
        }

        public static string GetDragonBallItemTypeFromNumber(int whichDragonBall)
        {
            switch (whichDragonBall)
            {
                case 1:
                    return "OneStarDragonBall";
                case 2:
                    return "TwoStarDragonBall";
                case 3:
                    return "ThreeStarDragonBall";
                case 4:
                    return "FourStarDragonBall";
                case 5:
                    return "FiveStarDragonBall";
                case 6:
                    return "SixStarDragonBall";
                case 7:
                    return "SevenStarDragonBall";
                default:
                    return "";
            }
        }

        public DragonBallStarCount StarCount { get; }
    }

    public enum DragonBallStarCount
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven
    }

    public class OneStarDragonBall : DragonBall
    {
        public OneStarDragonBall() : base(DragonBallStarCount.One)
        {
        }
    }

    public class TwoStarDragonBall : DragonBall
    {
        public TwoStarDragonBall() : base(DragonBallStarCount.Two)
        {
        }
    }

    public class ThreeStarDragonBall : DragonBall
    {
        public ThreeStarDragonBall() : base(DragonBallStarCount.Three)
        {
        }
    }

    public class FourStarDragonBall : DragonBall
    {
        public FourStarDragonBall() : base(DragonBallStarCount.Four)
        {
        }
    }

    public class FiveStarDragonBall : DragonBall
    {
        public FiveStarDragonBall() : base(DragonBallStarCount.Five)
        {
        }
    }

    public class SixStarDragonBall : DragonBall
    {
        public SixStarDragonBall() : base(DragonBallStarCount.Six)
        {
        }
    }

    public class SevenStarDragonBall : DragonBall
    {
        public SevenStarDragonBall() : base(DragonBallStarCount.Seven)
        {
        }
    }
}