using DBT.Helpers;
using DBT.NPCs.Misc;
using DBT.Players;
using DBT.UserInterfaces.WishMenu;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

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

        public override void RightClick(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            dbtPlayer.WishActive = true;
            WishMenu.menuVisible = true;
            NPC.NewNPC((int)player.position.X, (int)player.position.Y, ModContent.NPCType<ShenronNPC>());
            NetMessage.SendData(MessageID.SyncNPC);
            SoundHelper.PlayCustomSound("Sounds/DBSummon", player, 0.5f);
            Main.time = 72000;
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

        /// <summary>
        ///     Return an item type (int) using the name of an item.
        /// </summary>
        /// <param name="name">The internal name of the item.</param>
        public static int GetItemTypeFromName(string name)
        {
            if (DBTMod.Instance.GetItem(name) != null && DBTMod.Instance.GetItem(name).item != null)
                return DBTMod.Instance.GetItem(name).item.type;

            return -1;
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