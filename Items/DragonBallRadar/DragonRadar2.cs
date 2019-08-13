using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.DragonBallRadar
{
    public sealed class DragonRadar2 : DragonRadarItem
    {
        public DragonRadar2() : base("Dragon Radar MK2", "A medium tech piece of equipment used to locate dragon balls." +
            "\nHolding this will point you in the direction of the nearest dragon ball with reasonable accuracy." +
            "\nGetting too close to a dragon ball will overload the radar." +
            "\nWon't point to Dragon Balls you're holding in your inventory.")
        {
        }

        public override void HoldItem(Player player)
        {
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            modPlayer.isHoldingDragonRadarMk2 = true;
            base.HoldItem(player);
        }
    }
}