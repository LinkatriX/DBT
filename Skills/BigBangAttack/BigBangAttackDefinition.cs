namespace DBT.Skills.BigBangAttack
{
    public sealed class BigBangAttackDefinition : SkillDefinition
    {
        public BigBangAttackDefinition() : base("BigBangAttack", "Big Bang Attack", "A blast attack capable of being charged for greater damage.", new BigBangAttackCharacteristics())
        {
        }
    }
    public sealed class BigBangAttackCharacteristics : SkillCharacteristics
    {
        public BigBangAttackCharacteristics() : base(new BigBangAttackChargeCharacteristics(), 66, 1.5f, 25f, 6f, 1.2f, 0.05f, 1.02f, 2f, 1f)
        {
        }
    }

    public sealed class BigBangAttackChargeCharacteristics : SkillChargeCharacteristics
    {
        public BigBangAttackChargeCharacteristics() : base(120, 2, 100, 50)
        {
        }
    }
}
