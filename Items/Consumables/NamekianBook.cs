using DBT.Buffs;
using DBT.Commons.Items;
using DBT.Commons.Users;
using DBT.Helpers;
using DBT.UserInterfaces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables
{
    public sealed class NamekianBook : DBTConsumable
    {
        public NamekianBook() : base("Ancient Namekian Book of Legends", "A weathered and torn book, containing the oldest knowledge of the namekian race.", 36, 42, Item.buyPrice(0), ItemRarityID.Cyan, 
            ItemUseStyleID.HoldingOut, true, DBTMod.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Bookread"), 1, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.consumable = false;
            item.autoReuse = false;
            item.reuseDelay = 17;
        }

        public override bool UseItem(Player player)
        {
            DBTMod.Instance.namekBookUI.MenuVisible = !DBTMod.Instance.namekBookUI.MenuVisible;

            return base.UseItem(player);
        }
    }
}