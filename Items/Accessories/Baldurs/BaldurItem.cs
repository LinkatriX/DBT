using System.Collections.Generic;
using DBT.Extensions;
using DBT.Players;
using Terraria;

namespace DBT.Items.Accessories.Baldurs
{
    public abstract class BaldurItem : DBTItem, IIsBaldur
    {
        protected BaldurItem(string displayName, string tooltip, int value, int defense, int rarity, float defenseMultiplier, int width = 18, int height = 30) : base(displayName, tooltip, width, height, value, defense, rarity)
        {
            DefenseMultiplier = defenseMultiplier;
        }

        public virtual bool OnPlayerPostUpdateChargingTick(DBTPlayer dbtPlayer, ref float defenseMultiplier)
        {
            defenseMultiplier += DefenseMultiplier;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot > ItemCheckExtensions.SOCIAL_ARMOR_START_INDEX) return true; // The player can always equip the item if its in social.
            List<int> baldurIndexes = new List<int>();

            for (int i = 0; i < player.armor.Length; i++)
                if (player.armor[i] != null && player.armor[i].modItem != null && player.armor[i].modItem is BaldurItem)
                    baldurIndexes.Add(i);

            List<IIsBaldur> baldurItems = player.GetItemsByType<IIsBaldur>(accessories: true);
            if (baldurItems.Count == 1 && baldurIndexes.Contains(slot)) return true; // We check if the count is 1 to make sure the person hasn't hacked two or three Baldur items in their accessories.

            // If even after all these checks, the player still wants to equip one in a slot that isn't already occupied by a Baldur item, return false.
            if (baldurItems.Count > 0) return false;

            return true;
        }

        public float DefenseMultiplier { get; }
    }
}