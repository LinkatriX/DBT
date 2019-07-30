using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.NPCs
{
    public sealed class DBTGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (dbtPlayer == null)
                return;

            if (npc.lastInteraction == Main.LocalPlayer.whoAmI)
                dbtPlayer.OnKilledNPC(npc);
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (dbtPlayer.AliveTownNPCs.ContainsKey(npc))
                dbtPlayer.AliveTownNPCs[npc] += 1;

            base.GetChat(npc, ref chat);
        }
    }
}
