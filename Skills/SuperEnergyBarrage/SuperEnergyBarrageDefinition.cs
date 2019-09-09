namespace DBT.Skills.SuperEnergyBarrage
{
    public sealed class SuperEnergyBarrageDefinition : SkillDefinition
    {
        public SuperEnergyBarrageDefinition(params SkillDefinition[] parents) : base("SuperEnergyBarrage", "Super Energy Barrage", "Fires a powerful barrage of energy blasts.", typeof(SuperEnergyBarrageItem), new SuperEnergyBarrageCharacteristics(), parents: parents)
        {
        }
    }

    public sealed class SuperEnergyBarrageCharacteristics : SkillCharacteristics
    {
        public SuperEnergyBarrageCharacteristics() : base(new SuperEnergyBarrageChargeCharacteristics(), 88, 1f, 36f, 3f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class SuperEnergyBarrageChargeCharacteristics : SkillChargeCharacteristics
    {
        public SuperEnergyBarrageChargeCharacteristics() : base(0, 0, 125, 0)
        {
        }
    }
}
