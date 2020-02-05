namespace DBT.Skills.CelestialRapture
{
    //Needs balancing and code is currently broken.
    public sealed class CelestialRaptureDefinition : SkillDefinition
    {
        public CelestialRaptureDefinition(params SkillDefinition[] parents) : base("CelestialRapture", "Celestial Rapture", "Fires seeking ki blasts from all angles.", typeof(CelestialRaptureItem), new CelestialRaptureCharacteristics(), parents: parents)
        {
        }
        public override bool CheckPrePlayerConditions() => false;

        public sealed class CelestialRaptureCharacteristics : SkillCharacteristics
        {
            public CelestialRaptureCharacteristics() : base(new CelestialRaptureChargeCharacteristics(), 70, 1f, 16f, 5f, 1f)
            {
            }
        }

        public sealed class CelestialRaptureChargeCharacteristics : SkillChargeCharacteristics
        {
            public CelestialRaptureChargeCharacteristics() : base(0, 0, 499, 0)
            {
            }
        }
    }
}
