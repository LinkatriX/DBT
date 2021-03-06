﻿using Microsoft.Xna.Framework;

namespace DBT.Skills.EnergyBlastBarrage
{
    public sealed class EnergyBlastBarrageDefinition : SkillDefinition
    {
        public EnergyBlastBarrageDefinition(params SkillDefinition[] parents) : base("EnergyBlastBarrage", "Energy Blast Barrage", "Fires off continuous ki blasts.\nCharge to increase the length\nof the barrage.", typeof(EnergyBlastBarrageItem), new EnergyBlastBarrageCharacteristics(), new Vector2(366, 142), "Unlocked after hitting 1100 max ki.", parents: parents)
        {
        }
    }

    public sealed class EnergyBlastBarrageCharacteristics : SkillCharacteristics
    {
        public EnergyBlastBarrageCharacteristics() : base(new EnergyBlastBarrageChargeCharacteristics(), 34, 1f, 29f, 5f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class EnergyBlastBarrageChargeCharacteristics : SkillChargeCharacteristics
    {
        public EnergyBlastBarrageChargeCharacteristics() : base(60, 4, 50, 50)
        {
        }
    }
}
