namespace DBT.Skills.HellzoneGrenade
{
    public sealed class HellzoneGrenadeDefinition : SkillDefinition
    {
        public HellzoneGrenadeDefinition() : base("HellzoneGrenade", "Hellzone Grenade", "Fires a barrage of homing ki blasts.", new HellzoneGrenadeCharactersitics())
        {
        }
    }

    public sealed class HellzoneGrenadeCharactersitics : SkillCharacteristics
    {
        public HellzoneGrenadeCharactersitics() : base(new HellzoneGrenadeChargeCharacteristics(), 52, 1f, 17f, 6f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class HellzoneGrenadeChargeCharacteristics : SkillChargeCharacteristics
    {
        public HellzoneGrenadeChargeCharacteristics() : base(0, 0, 75, 0)
        {
        }
    }
}
