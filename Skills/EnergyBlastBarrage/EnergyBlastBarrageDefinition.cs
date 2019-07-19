namespace DBT.Skills.EnergyBlastBarrage
{
    public sealed class EnergyBlastBarrageDefinition : SkillDefinition
    {
        public EnergyBlastBarrageDefinition() : base("EnergyBlastBarrage", "Energy Blast Barrage", "Fires off continuous ki blasts. Charge to increase the length of the barrage.", new EnergyBlastBarrageCharacteristics())
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
