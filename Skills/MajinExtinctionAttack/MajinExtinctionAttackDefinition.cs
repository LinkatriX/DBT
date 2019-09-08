namespace DBT.Skills.MajinExtinctionAttack
{//This move will need to be completely redone, so I'm treating it as a new attack and leaving it for later.
    public sealed class MajinExtinctionAttackDefinition : SkillDefinition
    {
        public MajinExtinctionAttackDefinition() : base("MajinExtinctionAttack", "Majin Extinction Attack", "'Human Extinction...'", typeof(MajinExtinctionAttackItem), new MajinExtinctionAttackCharacteristics())
        {
        }
    }

    public sealed class MajinExtinctionAttackCharacteristics : SkillCharacteristics
    {
        public MajinExtinctionAttackCharacteristics() : base(new MajinExtinctionAttackChargeCharacteristics(), 70, 1f, 16f, 5f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class MajinExtinctionAttackChargeCharacteristics : SkillChargeCharacteristics
    {
        public MajinExtinctionAttackChargeCharacteristics() : base(0, 0, 500, 0)
        {
        }
    }
}
