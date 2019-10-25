using DBT.Helpers;
using DBT.Skills;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private readonly HitStunHelper HitStunHelper = new HitStunHelper();

        private void InitializeSkills()
        {
            AcquiredSkills = new List<SkillDefinition>();
            ActiveSkills = new List<SkillDefinition>();
        }
        internal void ResetSkillEffects()
        {
            SkillChargeLevelLimitModifier = 0;
            SkillChargeLevelLimitMultiplier = 1;

            SkillChargeMoveSpeedModifier = 1;
        }

        public bool HasAcquiredSkill(SkillDefinition definition)
        {
            for (int i = 0; i < AcquiredSkills.Count; i++)
                if (AcquiredSkills.Contains(definition))
                    return true;

            return false;
        }
        public bool HasSkillActive(SkillDefinition definition)
        {
            for (int i = 0; i < ActiveSkills.Count; i++)
                if (ActiveSkills.Contains(definition))
                    return true;

            return false;
        }

        public void AcquireSkill(SkillDefinition definition)
        {
            if (!HasAcquiredSkill(definition))
            {
                AcquiredSkills.Add(definition);
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(145, 228, 255), definition.DisplayName + " Unlocked!", true, false);
            }
                
        }
        public void EquipSkill(SkillDefinition definition)
        {
            if (!HasSkillActive(definition) && AcquiredSkills.Count < 4 && HasAcquiredSkill(definition))
            {
                ActiveSkills.Add(definition);
                player.PutItemInInventory(mod.ItemType(definition.Item.Name));
            }
            if (HasSkillActive(definition))
            {
                ActiveSkills.Remove(definition);
                foreach (var item in player.inventory)
                {
                    if (item == null)
                        continue;
                    if (item.type == mod.ItemType(definition.Item.Name))
                        item.TurnToAir();
                }
            }
        }

        private void HandleSkillsOnEnterWorld(Player player)
        {
            DBTMod.Instance.techniqueMenu.OnPlayerEnterWorld(player.GetModPlayer<DBTPlayer>());
        }

        public void ApplyHitStun(NPC target, int duration, float slowedTo, float recoversDuringFramePercent)
        {
            HitStunHelper.DoHitStun(target, duration, slowedTo, recoversDuringFramePercent);
        }

        public int SkillChargeLevelLimitModifier { get; set; }
        public int SkillChargeLevelLimitMultiplier { get; set; }
        public List<SkillDefinition> AcquiredSkills { get; internal set; }
        public List<SkillDefinition> ActiveSkills { get; internal set; }
        public float SkillChargeMoveSpeedModifier { get; set; }
    }
}
