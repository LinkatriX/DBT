using DBT.Players;
using Terraria;

namespace DBT.Items.Developer
{
    public sealed class KiAdder : DeveloperItem
    {
        public KiAdder() : base(24, 24)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.BaseMaxKi += 1000;
            return true;
        }
    }

    public sealed class KiRemover : DeveloperItem
    {
        public KiRemover() : base(24, 24)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.BaseMaxKi -= 1000;
            return true;
        }
    }

    public sealed class KiFiller : DeveloperItem
    {
        public KiFiller() : base(24, 24)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.ModifyKi(dbtPlayer.MaxKi);
            return true;
        }
    }

    public sealed class KiEmptier : DeveloperItem
    {
        public KiEmptier() : base(24, 24)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.ModifyKi(-dbtPlayer.Ki);
            return true;
        }
    }
}