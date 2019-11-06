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
        public void UpdateSkillUnlocks()
        {
            
        }
        public void UpdateKiSkillUnlocks()
        {
            if (CheckKiRequirement(650))
                AcquireSkill(SkillDefinitionManager.Instance.KiBeam);
            if (CheckKiRequirement(800))
                AcquireSkill(SkillDefinitionManager.Instance.EnergyWave);
            if (CheckKiRequirement(1100))
                AcquireSkill(SkillDefinitionManager.Instance.EnergyBlastBarrage);
            if (CheckKiRequirement(1250))
                AcquireSkill(SkillDefinitionManager.Instance.Masenko);
        }
    }
}
