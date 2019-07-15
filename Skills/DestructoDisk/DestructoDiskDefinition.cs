namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskDefinition : SkillDefinition
    {//Add armor piercing.
        public DestructoDiskDefinition() : base("DestructoDisk", "Destructo Disk", "Fires a disk that is capable of cutting through enemies. Charge to unleash a more powerful version.", new DestructoDiskCharacteristics())
        {
        }
    }

    public sealed class DestructoDiskCharacteristics : SkillCharacteristics
    {
        public DestructoDiskCharacteristics() : base(new DestructoDiskChargeCharacteristics(), 42, 1.5f, 20f, 3f, 1f, 0.05f, 1.5f, 2f, 1f)
        {
        }
    }

    public sealed class DestructoDiskChargeCharacteristics : SkillChargeCharacteristics
    {
        public DestructoDiskChargeCharacteristics() : base(180, 1, 40, 40)
        {
        }
    }
}