using DBT.Skills;
using System.Collections.Generic;
using Terraria;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
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

        private void HandleSkillsOnEnterWorld(Player player)
        {
            DBTMod.Instance.techniqueMenu.OnPlayerEnterWorld(player.GetModPlayer<DBTPlayer>());
        }

        public int SkillChargeLevelLimitModifier { get; set; }
        public int SkillChargeLevelLimitMultiplier { get; set; }
        public List<SkillDefinition> AcquiredSkills { get; internal set; }
        public List<SkillDefinition> ActiveSkills { get; internal set; }
        public float SkillChargeMoveSpeedModifier { get; set; }
    }
}
