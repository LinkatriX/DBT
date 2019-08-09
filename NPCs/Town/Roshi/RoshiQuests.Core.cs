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
        public override void Load(TagCompound tag)
        {
            CurrentQuest = tag.GetInt("CurrentQuest");
            QuestsCompletedToday = tag.GetInt("QuestsCompletedToday");
            QuestsCompleted = tag.GetInt("QuestsCompleted");
            QuestKills = tag.GetInt("QuestKills");
        }

        public override TagCompound Save()
        {
            var tag = new TagCompound();
            tag.Set("CurrentQuest", CurrentQuest);
            tag.Set("QuestsCompletedToday", QuestsCompletedToday);
            tag.Set("QuestsCompleted", QuestsCompleted);
            tag.Set("QuestKills", QuestKills);
            return tag;
        }

        public Quest GetCurrentQuest() => Quests[CurrentQuest];

        public void CompleteQuest()
        {
            QuestsCompletedToday++;
            QuestsCompleted++;
            CurrentQuest = -1;
            QuestKills = 0;
        }
        public void SpawnReward(NPC npc)
        {
            Main.PlaySound(24, -1, -1, 1);
            
            if(GetCurrentQuest().ModdedQuestReward != "")
            {
                int number2 = 0;
                number2 = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,
                    mod.ItemType(GetCurrentQuest().ModdedQuestReward), GetCurrentQuest().QuestRewardAmount, false, 0, false, false);
                if (Main.netMode == 1 && number2 >= 0)
                    NetMessage.SendData(21, -1, -1, null, number2, 1f, 0.0f, 0.0f, 0, 0, 0);
            }
            if (GetCurrentQuest().ModdedQuestReward == "" && GetCurrentQuest().QuestReward != 0)
            {
                int number2 = 0;
                number2 = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height,
                    (GetCurrentQuest().QuestReward), GetCurrentQuest().QuestRewardAmount, false, 0, false, false);
                if (Main.netMode == 1 && number2 >= 0)
                    NetMessage.SendData(21, -1, -1, null, number2, 1f, 0.0f, 0.0f, 0, 0, 0);
            }

        }

        public override void PostUpdate()
        {
            if (Main.dayTime && QuestsCompletedToday > 0)
            {
                if (Main.time < 1 || (Main.fastForwardTime && Main.time < 61)) //Time stuff that makes no sense
                {
                    CurrentQuest = -1;
                    QuestsCompletedToday = 0;
                    QuestKills = 0;
                }
            }
            //Main.NewText("Current quest is:" + CurrentQuest);
            //Main.NewText("Quests completed:" + QuestsCompleted);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (CurrentQuest >= 0 && GetCurrentQuest() is KillQuest)
            {
                foreach (var i in (GetCurrentQuest() as KillQuest).TargetType)
                    if (target.life <= 0 && target.type == i)
                        QuestKills++;
            }
        }
    

        public static int ChooseNewQuest()
        {
            Player player = Main.LocalPlayer;
            var roshiQuests = player.GetModPlayer<RoshiQuests>();

            return roshiQuests.QuestsCompleted;
        }
        public bool CheckQuest()
        {
            if (CurrentQuest < 0)
                return false;

            if (GetCurrentQuest() is ItemQuest)
                return (GetCurrentQuest() as ItemQuest).CheckCompletion(Main.player[Main.myPlayer]);

            else if (GetCurrentQuest() is KillQuest)
                return (GetCurrentQuest() as KillQuest).CheckCompletion(Main.player[Main.myPlayer]);

            return false;
        }

        public static List<Quest> Quests = new List<Quest>();
        public int QuestsCompleted { get; set; }
        public int QuestsCompletedToday { get; set; }
        public int QuestLimitPerDay { get; set; } = 3;
        public int CurrentQuest { get; set; } = -1;
        public int QuestKills { get; set; }
    }

    public abstract class Quest
    {
        protected Quest(string name, double weight = 1d, string specialThanks = "Thanks for your assistance!")
        {
            QuestName = name;
            Weight = weight;
            ThanksMessage = specialThanks;
        }

        public abstract bool CheckCompletion(Player player);

        public override string ToString()
        {
            return Language.GetTextValue(QuestName, Main.LocalPlayer.name);
        }

        public string SayThanks()
        {
            return Language.GetTextValue(ThanksMessage, Main.LocalPlayer.name);
        }

        public string QuestName { get; set; }
        public string ModdedQuestReward { get; set; } = "";
        public int QuestReward { get; set; }
        public int QuestRewardAmount { get; set; } = 1;
        public string ThanksMessage { get; set; }
        public double Weight { get; set; }
    }

    public class ItemQuest : Quest
    {
        public ItemQuest(string name, int itemType, int itemAmount = 1, double weight = 1d, string specialThanks = "Thanks for your assistance! Here's the promised reward.") : base(name, weight, specialThanks)
        {
            ItemType = itemType;
            ItemAmount = itemAmount;
        }

        public override bool CheckCompletion(Player player)
        {
            if (player.CountItem(ItemType, ItemAmount) >= ItemAmount)
            {
                int leftToRemove = ItemAmount;
                foreach (var item in player.inventory)
                {
                    if (item.type == ItemType)
                    {
                        int removed = Math.Min(item.stack, leftToRemove);
                        item.stack -= removed;
                        leftToRemove -= removed;
                        if (item.stack <= 0)
                            item.SetDefaults();
                        if (leftToRemove <= 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public int ItemAmount { get; set; }
        public int ItemType { get; set; }
    }
    public class KillQuest : Quest
    {
        public KillQuest(string name, int[] targetType, string targetName, int targetCount = 1, double weight = 1d, string specialThanks = "Thanks for your assistance! Here's the promised reward.") : base(name, weight, specialThanks)
        {
            TargetType = targetType;
            TargetCount = targetCount;
            TargetName = targetName;
        }

        public override bool CheckCompletion(Player player)
        {
            if (player.GetModPlayer<RoshiQuests>().QuestKills >= TargetCount)
            {
                player.GetModPlayer<RoshiQuests>().QuestKills = 0;
                return true;
            }
            return false;
        }

        public int TargetCount { get; set; }
        public int[] TargetType { get; set; }
        public string TargetName { get; set; }
    }
}
