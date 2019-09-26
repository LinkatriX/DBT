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
        public NamekianBook() : base("Ancient Namekian Book of Rites", "A weathered and torn book, containing the oldest knowledge of the namekian race.", 36, 42, Item.buyPrice(0), ItemRarityID.Cyan, 
            ItemUseStyleID.EatingUsing, true, DBTMod.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Bookread"), 17, 17)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTMod.Instance.namekBookUI.MenuVisible = !DBTMod.Instance.namekBookUI.MenuVisible;

            return base.UseItem(player);
        }

        public Donator Donator => SteamHelper.CanadianMRE;
    }
}