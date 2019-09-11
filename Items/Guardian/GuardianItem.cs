using DBT.Commons.Items;
using Terraria.ID;

namespace DBT.Items.Guardian
{
    public abstract class GuardianItem : DBTItem, IIsGuardianItem
    {
        protected GuardianItem(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White) : base(displayName, tooltip, width, height)
        {
        }

        public override bool CloneNewInstances => true;
    }
}
