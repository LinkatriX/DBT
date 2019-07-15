namespace DBT.Skills.DestructoDiskAssault
{/*This move's previous code is pretty broken and will need to be mostly rewritten. Epic.
   Uncharged the move should fire off (2-3?) Destructo disks, while charged it should fire (5-6?). -Skipping*/
   //Add armor piercing.
    public sealed class DestructoDiskAssaultDefinition : SkillDefinition
    {
        public DestructoDiskAssaultDefinition() : base("DestructoDiskAssault", "Destructo Disk Assault", "Fires a barrage of Destructo Disks. Charge to unleash a larger assault.", new DestructoDiskAssaultCharacteristics())
        {
        }
    }

    public sealed class DestructoDiskAssaultCharacteristics : SkillCharacteristics
    {
        public DestructoDiskAssaultCharacteristics() : base(new DestructoDiskAssaultChargeCharacteristics(), 90, 1f, 20f, 5f, 1f, 0.075f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class DestructoDiskAssaultChargeCharacteristics : SkillChargeCharacteristics
    {
        public DestructoDiskAssaultChargeCharacteristics() : base(100, 4, 140, 70)
        {
        }
    }
}
