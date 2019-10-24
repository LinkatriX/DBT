using DBT.Items.Accessories.ArmCannons;
using DBT.Items.Materials;
using DBT.Items.Weapons;
using DBT.NPCs.Bosses.FriezaShip;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Items.Bags
{
    public class FFShipBag : DBTItem
    {
        public FFShipBag() : base("Treasure Bag", "{$CommonItemTooltip.RightClickToOpen}", 32, 32, 0, 0, 4)
        {
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            int choice = Main.rand.Next(1);

            if (choice == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<BeamRifle>());
            }
            if (choice == 1)
            {
                //player.QuickSpawnItem(ModContent.ItemType<HenchBlast>());
            }
            player.QuickSpawnItem(ModContent.ItemType<CyberneticParts>(), Main.rand.Next(7, 18));
            player.QuickSpawnItem(ModContent.ItemType<ArmCannonMK2>());
            //if (Main.rand.Next(10) == 0)
                //player.QuickSpawnItem(ModContent.ItemType<FFTrophy>());
        }

        public override int BossBagNPC => ModContent.NPCType<FriezaShip>();
    }
}