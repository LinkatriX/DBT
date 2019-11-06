using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void UpdateProgression(NPC npcKilled)
        {
            if (npcKilled.boss)
                UpdateBossKill(npcKilled);
            else
                UpdateKill(npcKilled);

            UpdateKiSkillUnlocks();
        }

        public void UpdateBossKill(NPC npcKilled)
        {
            int divide = 0;
            if (!Main.hardMode)
                divide = 2;
            else if (Main.hardMode)
                divide = 5;
            if (NPC.downedAncientCultist)
                divide = 8;
            if (!BossesKilled.Contains(npcKilled.whoAmI))
            {
                Gain = npcKilled.lifeMax / 2 * 1f * ((npcKilled.lifeMax - Powerlevel) / (npcKilled.lifeMax + Powerlevel) / 2) / divide;
                BossesKilled.Add(npcKilled.whoAmI);
            }
            if (BossesKilled.Contains(npcKilled.whoAmI))
            {
                Gain = npcKilled.lifeMax / 2 * 1f * ((npcKilled.lifeMax - Powerlevel) / (npcKilled.lifeMax + Powerlevel) / 2) / divide * 5;
            }

            Main.NewText("Ki gain is: " + Gain);

            if (Gain >= 1)
            {
                if (GainLimits(Gain))
                {
                    int text = CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), "+" + (int)Gain + "Max Ki", true, false);
                    Main.combatText[text].scale = 0.5f;
                    MaxKiModifierPerm += Gain;
                }
            }
        }

        public void UpdateKill(NPC npcKilled)
        {
            Gain = npcKilled.lifeMax / 2 * 1f * ((npcKilled.lifeMax - Powerlevel / 100) / (npcKilled.lifeMax + Powerlevel / 100) / 2) / 6;

            if (Gain >= 1)
            {
                if (GainLimits(Gain))
                {
                    if (!Main.dedServ)
                    {
                        int text = CombatText.NewText(new Rectangle((int) player.position.X, (int) player.position.Y, player.width, player.height), new Color(51, 204, 255), "+" + (int) Gain + "Max Ki", true, false);
                        Main.combatText[text].scale = 0.5f;
                    }

                    MaxKiModifierPerm += Gain;
                }
            }
        }

        public bool GainLimits(float gainAmount)
        {
            if (!NPC.downedPlantBoss)
                if (gainAmount > 1000)
                {
                    Gain = 1000;
                    return true;
                }
                    
            if (NPC.downedPlantBoss && !NPC.downedMoonlord)
                if (gainAmount > 2500)
                {
                    Gain = 2500;
                    return true;
                }
                    
            if (NPC.downedMoonlord)
                if (gainAmount > 5000)
                {
                    Gain = 5000;
                    return true;
                }    
            return true;
        }

        public float Powerlevel
        {
            get
            {
                return MaxKi * 1.5f * ((player.meleeDamage + player.rangedDamage + player.magicDamage + player.thrownDamage + player.minionDamage + KiDamageMultiplier) / 6) + (player.statDefense * 50);
            }
        }
        public IList<int> BossesKilled { get; set; }
        public float ProgressionKiRegenerationModifier { get; set; }
        public float ProgressionMaxKiModifier { get; set; }

        public float Gain { get; set; }
    }
}
