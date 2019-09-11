using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Consumables
{
    public sealed class HairStylingKit : DBTConsumable
    {
        public HairStylingKit() : base("Hair Styling Kit", "Allows anyone to restyle their hair, even a saiyan!", 16, 20, Item.buyPrice(gold: 2, silver: 50), ItemRarityID.Green,
            ItemUseStyleID.EatingUsing, true, SoundID.Item1, 17, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.maxStack = 1;
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.HairChecked = false;
            return true;
        }
    }
}