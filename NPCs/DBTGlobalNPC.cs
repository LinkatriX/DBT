using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.NPCs
{
    public sealed partial class DBTGlobalNPC : GlobalNPC
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
            if (dbtPlayer.AliveTownNPCs.ContainsKey(npc.type))
            {
                dbtPlayer.DeathTriggers();
                dbtPlayer.AliveTownNPCs.Remove(npc.type);
            }
            return true;
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (dbtPlayer == null)
                return;

            if (dbtPlayer.AliveTownNPCs.ContainsKey(npc.type))
            {
                if (dbtPlayer.FriendshipCooldown <= 0)
                {
                    if (dbtPlayer.AliveTownNPCs[npc.type] == 25)
                        Main.NewText("You are now friends with " + npc.GivenName + ".", new Color(235, 189, 52));
                    else if (dbtPlayer.AliveTownNPCs[npc.type] == 50)
                        Main.NewText("You are now best friends with " + npc.GivenName + ".", new Color(235, 189, 52));
                    else if (dbtPlayer.AliveTownNPCs[npc.type] == 100)
                        Main.NewText("You are now practically family with " + npc.GivenName + ".", new Color(235, 189, 52));

                    dbtPlayer.AliveTownNPCs[npc.type] += 1;
                    dbtPlayer.FriendshipCooldown = ModContent.GetInstance<DBTConfigServer>().FriendshipCooldownConfig * 60;
                }
            }
                
            else if (!dbtPlayer.AliveTownNPCs.ContainsKey(npc.type))
                dbtPlayer.AliveTownNPCs.Add(npc.type, 1);

            base.GetChat(npc, ref chat);
        }
    }
}
