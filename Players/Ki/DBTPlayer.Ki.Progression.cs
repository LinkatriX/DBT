using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using DBT.Commons;
using DBT.Extensions;
using DBT.Network;
using Terraria;
using Terraria.ID;

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
        }

        public void UpdateBossKill(NPC npcKilled)
        {
            int divide = 0;
            float gain = 0f;
            if (!Main.hardMode)
                divide = 2;
            else if (Main.hardMode)
                divide = 5;
            if (NPC.downedAncientCultist)
                divide = 8;
            if (!BossesKilled.Contains(npcKilled.whoAmI))
            {
                // If you killed EoC for the first time with 700 max ki, a damage multi of x1.1 and 8 defence then you would gain 280 max ki.
                gain = npcKilled.lifeMax / 2 * 1f * ((npcKilled.lifeMax - Powerlevel) / (npcKilled.lifeMax + Powerlevel) / 2) / divide;
                BossesKilled.Add(npcKilled.whoAmI);
            }
            if (BossesKilled.Contains(npcKilled.whoAmI))
            {
                gain = npcKilled.lifeMax / 2 * 1f * ((npcKilled.lifeMax - Powerlevel) / (npcKilled.lifeMax + Powerlevel) / 2) / divide * 5;
            }

            if (gain >= 1)
            {
                if (GainLimits(gain))
                {
                    int text = CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), "+" + (int)gain + "Max Ki", true, false);
                    Main.combatText[text].scale = 0.5f;
                    MaxKiModifierPerm += gain;
                }
            }
        }

        public void UpdateKill(NPC npcKilled)
        {
            float gain = 0f;
            gain = npcKilled.lifeMax / 2 * 1f * ((npcKilled.lifeMax - Powerlevel / 100) / (npcKilled.lifeMax + Powerlevel / 100) / 2) / 6;

            if (gain >= 1)
            {
                if (GainLimits(gain))
                {
                    int text = CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), "+" + (int)gain + "Max Ki", true, false);
                    Main.combatText[text].scale = 0.5f;
                    MaxKiModifierPerm += gain;
                }
            }
        }

        public bool GainLimits(float gain)
        {
            if (!NPC.downedPlantBoss)
                if (gain > 1000)
                {
                    gain = 1000;
                    return true;
                }
                    
            if (NPC.downedPlantBoss && !NPC.downedMoonlord)
                if (gain > 2500)
                {
                    gain = 2500;
                    return true;
                }
                    
            if (NPC.downedMoonlord)
                if (gain > 5000)
                {
                    gain = 5000;
                    return true;
                }    
            return true;
        }

        public float Powerlevel
        {
            get
            {
                return MaxKi * 1.5f * (player.meleeDamage + player.rangedDamage + player.magicDamage + player.thrownDamage + player.minionDamage + KiDamageMultiplier / 6) + (player.statDefense * 50);
            }
        }
        public IList<int> BossesKilled { get; set; }
        public float ProgressionKiRegenerationModifier { get; set; }
        public float ProgressionMaxKiModifier { get; set; }
    }
}
