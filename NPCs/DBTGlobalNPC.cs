using DBT.Players;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DBT.NPCs
{
	public sealed class DBTGlobalNPC : GlobalNPC
	{
		public override void AI(NPC npc) //Need a method that is called every tick.
		{
			base.AI(npc);
		}

		public override void NPCLoot(NPC npc)
		{
			DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

			if (dbtPlayer == null)
				return;

			if (npc.lastInteraction == Main.LocalPlayer.whoAmI)
				dbtPlayer.OnKilledNPC(npc);
		}

		public void DetectPlayerKills(NPC npc, DBTPlayer dbtPlayer)
		{
			if (npc.lastInteraction == Main.LocalPlayer.whoAmI) //detecs wheter or not the recent NPC has communicated with the player.
				
		}
	}
}
