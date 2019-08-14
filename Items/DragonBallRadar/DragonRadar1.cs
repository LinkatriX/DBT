using DBT.Players;
using Terraria;

namespace DBT.Items.DragonBallRadar
{
    public sealed class DragonRadar1 : DragonRadarItem
    {
        public DragonRadar1() : base("Dragon Radar MK1", "A low tech piece of equipment used to locate dragon balls." +
            "\nHolding this will point you in the direction of the nearest dragon ball with terrible accuracy." +
            "\nGetting too close to a dragon ball will overload the radar." +
            "\nWon't point to Dragon Balls you're holding in your inventory.")
        {
            
        }

        public override void HoldItem(Player player)
        {
            DBTPlayer modPlayer = player.GetModPlayer<DBTPlayer>();
            modPlayer.isHoldingDragonRadarMk1 = true;
            base.HoldItem(player);
        }
    }
}
