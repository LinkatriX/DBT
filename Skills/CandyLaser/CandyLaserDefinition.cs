namespace DBT.Skills.CandyLaser
{
    public sealed class CandyLaserDefinition : SkillDefinition
    {
        public CandyLaserDefinition() : base("CandyLaser", "Candy Laser", "Fires a beam of energy that transforms your enemy into candy. Doesn't change bosses.", new CandyLaserCharacteristics())
        {
        }
    }

    public sealed class CandyLaserCharacteristics : SkillCharacteristics
    {
        public CandyLaserCharacteristics() : base(new CandyLaserChargeCharacteristics(), 142, 1f, 14f, 4f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class CandyLaserChargeCharacteristics : SkillChargeCharacteristics
    {
        public CandyLaserChargeCharacteristics() : base(0, 0, 300, 0)
        {
        }
    }
}
