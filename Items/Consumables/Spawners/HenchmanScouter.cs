using DBT.Items.KiStones;
using DBT.Items.Materials;
using DBT.Items.Materials.Metals;
using DBT.NPCs.Bosses.FriezaShip;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.Spawners
{
    public sealed class HenchmanScouter : DBTConsumable
    {
        public HenchmanScouter() : base("Henchman's Scouter", "A common scouter used by the lower echelons of the frieza force, it appears to have a communicator built into it.", 48, 26, 0, ItemRarityID.Orange, 4, true, null, 20, 20) 
        {
        }
        public override bool CanUseItem(Player player) => NPC.downedBoss2 && !NPC.AnyNPCs(ModContent.NPCType<FriezaShip>()) && !Worlds.DBTWorld.friezaShipTriggered;

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<FriezaShip>());
            Main.PlaySound(SoundID.Roar, player.position, 0);

            return true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ModContent.ItemType<CyberneticParts>(), 8);
            recipe.AddIngredient(ModContent.ItemType<ScrapMetal>(), 5);
            recipe.AddIngredient(ModContent.ItemType<KiStoneT1>(), 3);

            recipe.SetResult(this);
            recipe.AddRecipe();
		}
    }
}