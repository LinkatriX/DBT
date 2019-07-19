namespace DBT.Skills.GalickGun
{
    public sealed class GalickGunDefinition : SkillDefinition
    {
        public GalickGunDefinition() : base("GalickGun", "Galick Gun", "No it doesn't smell like garlic.\n" + DEFAULT_BEAM_INSTRUCTIONS, new GalickGunCharacteristics())
        {
        }
    }

    public sealed class GalickGunCharacteristics : SkillCharacteristics
    {
        public GalickGunCharacteristics() : base(new GalickGunChargeCharacteristics(), 94, 94f / Constants.TICKS_PER_SECOND, 0f, 2f, 1f, 0.05f, 1f, 2f, 1f)
        {
        }
    }

    public sealed class GalickGunChargeCharacteristics : SkillChargeCharacteristics
    {
        public GalickGunChargeCharacteristics() : base(60, 9, 80, 80 / Constants.TICKS_PER_SECOND)
        {
        }
    }
}
