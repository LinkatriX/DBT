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

    public class DragonBallOneStar : DragonBall
    {
        public DragonBallOneStar() : base(DragonBallStarCount.One)
        {
        }
    }

    public class DragonBallTwoStar : DragonBall
    {
        public DragonBallTwoStar() : base(DragonBallStarCount.Two)
        {
        }
    }

    public class DragonBallThreeStar : DragonBall
    {
        public DragonBallThreeStar() : base(DragonBallStarCount.Three)
        {
        }
    }

    public class DragonBallFourStar : DragonBall
    {
        public DragonBallFourStar() : base(DragonBallStarCount.Four)
        {
        }
    }

    public class DragonBallFiveStar : DragonBall
    {
        public DragonBallFiveStar() : base(DragonBallStarCount.Five)
        {
        }
    }

    public class DragonBallSixStar : DragonBall
    {
        public DragonBallSixStar() : base(DragonBallStarCount.Six)
        {
        }
    }

    public class DragonBallSevenStar : DragonBall
    {
        public DragonBallSevenStar() : base(DragonBallStarCount.Seven)
        {
        }
    }
}