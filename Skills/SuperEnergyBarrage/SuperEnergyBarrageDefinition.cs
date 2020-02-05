using Microsoft.Xna.Framework;

namespace DBT.Skills.SuperEnergyBarrage
{
    public sealed class SuperEnergyBarrageDefinition : SkillDefinition
    {
        public SuperEnergyBarrageDefinition(params SkillDefinition[] parents) : base("SuperEnergyBarrage", "Super Energy Barrage", "Fires a powerful barrage of energy blasts.", typeof(SuperEnergyBarrageItem), new SuperEnergyBarrageCharacteristics(), new Vector2(366, 82), parents: parents)
        {
        }
    }

    public sealed class SuperEnergyBarrageCharacteristics : SkillCharacteristics
    {
        public SuperEnergyBarrageCharacteristics() : base(new SuperEnergyBarrageChargeCharacteristics(), 88, 1f, 36f, 3f, 1f)
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
