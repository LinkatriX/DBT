using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace DBT.NPCs.Town.Roshi
{
    //lots of this code was restructured from antiaris with love <3
    public partial class RoshiQuests : ModPlayer
    {
        public override void Initialize()
        {
            Quests.Clear();

            Quest quest = new ItemQuest(
                "Oh, its you. I can sense some latent power from you so let me assist. Head to your controls in the options menu and bind hotkeys to every entry there. The most important ones are Energy Charge, Transform, Power down and Transformation Menu.",
                ItemID.Wood, 1, 1,
                "Energy charge lets you charge your energy bar at the top of the screen. Transform lets you ascend into unlocked forms with power down being the opposite, there is also some more unique interactions with these keys, such as pressing charge and transform together to ascend to an alternative form, like ASSJ. Transformation menu lets you open a menu which lets you select which form to transform into alongside the masteries of those forms. The other hotkeys require unlocks to be used however, so look out for those.")
            {
                ModdedQuestReward = "KiBlastItem",
                QuestRewardAmount = 1
            };
            Quests.Add(quest);

            quest = new ItemQuest(
                "Ah, welcome back. If you don't mind, could you bring me 30 pieces of that gel-like substance those slimes hold, it'll help with my 'research'.",
                ItemID.Gel, 30, 1,
                "Thanks for your assistance, let me assist in your training for a bit, also take this extra money I had laying around.")
            {
                QuestReward = ItemID.SilverCoin,
                QuestRewardAmount = 35
            };
            Quests.Add(quest);

            int[] antlions = {NPCID.Antlion};
            quest = new KillQuest(
                "Ah, " + player.name + ", are you ready for a bit of training? Head over to the desert and kill 5 of those weird spitting bugs.",
                antlions, "Antlion", 5, 1,
                "Good job, look how much stronger you are already! Here's some more money for your efforts.")
            {
                QuestReward = ItemID.SilverCoin,
                QuestRewardAmount = 50
            };
            Quests.Add(quest);

            //Kill quest example
            /*int[] kingSlime = {NPCID.KingSlime};
            quest = new KillQuest(
                "Ah," + player.name +
                "I could use some more of your help, I haven't been able to get to the other side of the ocean as this big slime keeps getting in my way once I get to the other beach, if you could take care of it for me then I'll certainly reward you.",
                kingSlime, "King Slime", 1, 1d,
                "I really appreciate you killing that thing for me, here's some extra boots that I found while exploring earlier.")
            {
                QuestReward = ItemID.HermesBoots,
                QuestRewardAmount = 1
            };
            Quests.Add(quest);*/
        }
    }
}