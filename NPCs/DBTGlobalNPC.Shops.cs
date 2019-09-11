using Terraria;
using Terraria.ID;
using DBT.Items.Consumables;

namespace DBT.NPCs
{
    public sealed partial class DBTGlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Stylist)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<HairStylingKit>());
                nextSlot++;
            }
        }
    }
}
