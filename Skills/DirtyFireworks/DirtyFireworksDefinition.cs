namespace DBT.Skills.DirtyFireworks
{
    public sealed class DirtyFireworksDefinition : SkillDefinition
    {
        public DirtyFireworksDefinition(params SkillDefinition[] parents) : base("DirtyFireworks", "Dirty Fireworks", "Immobilizes your opponent before blowing them to pieces.", typeof(DirtyFireworksItem) , new DirtyFireworksCharacteristics(), parents: parents)
        {
        }
    }

    public sealed class DirtyFireworksCharacteristics : SkillCharacteristics
    {
        public DirtyFireworksCharacteristics() : base(new DirtyFireworksChargeCharacteristics(), 1, 1f, 35f, 1f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class DirtyFireworksChargeCharacteristics : SkillChargeCharacteristics
    {
        public DirtyFireworksChargeCharacteristics() : base(0, 0, 110, 0)
        {
        }
    }
}
