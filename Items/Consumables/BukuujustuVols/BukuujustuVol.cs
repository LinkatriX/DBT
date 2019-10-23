using DBT.Helpers;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.BukuujustuVols
{
    public abstract class BukuujustuVol : DBTConsumable
    {
        protected BukuujustuVol(string displayName, string tooltip, int rarity) :
            base(displayName, tooltip, 40, 40, 0, rarity, ItemUseStyleID.EatingUsing, true, DBTMod.Instance.GetLegacySoundSlot(SoundType.Custom, "Sounds/Bookread"), 17, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 1;
        }
    }
}