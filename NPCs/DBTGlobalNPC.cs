using DBT.Players;
using Microsoft.Xna.Framework;
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

        public override bool CheckDead(NPC npc)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            if (dbtPlayer.AliveTownNPCs.Keys.Equals(npc))
            {
                dbtPlayer.DeathTriggers();
                dbtPlayer.AliveTownNPCs.Remove(npc);
            }
            return true;
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (dbtPlayer == null)
                return;

            if (dbtPlayer.AliveTownNPCs.ContainsKey(npc))
            {
                dbtPlayer.AliveTownNPCs[npc] += 5;
                if (dbtPlayer.AliveTownNPCs.Values.Equals(25))
                    Main.NewText("You are now friends with " + npc.GivenName, new Color(235, 189, 52));
                if (dbtPlayer.AliveTownNPCs.Values.Equals(50))
                    Main.NewText("You are now best friends with " + npc.GivenName, new Color(235, 189, 52));
                if (dbtPlayer.AliveTownNPCs.Values.Equals(100))
                    Main.NewText("You are now practically family with " + npc.GivenName, new Color(235, 189, 52));
            }
                
            else if (!dbtPlayer.AliveTownNPCs.ContainsKey(npc))
                dbtPlayer.AliveTownNPCs.Add(npc, 1);

            base.GetChat(npc, ref chat);
        }
    }
}
