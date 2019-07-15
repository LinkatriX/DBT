using DBT.Managers;
using DBT.Skills.DebugKiBlast;
using DBT.Skills.KiBlast;
using DBT.Skills.KiBeam;
using DBT.Skills.DestructoDisk;
using DBT.Skills.Supernova;
using DBT.Skills.CelestialRapture;

namespace DBT.Skills
{
    public sealed class SkillDefinitionManager : SingletonManager<SkillDefinitionManager, SkillDefinition>
    {
        internal override void DefaultInitialize()
        {
            DebugKiBlast = Add(new DebugKiBlastDefinition()) as DebugKiBlastDefinition;
            KiBlast = Add(new KiBlastDefinition()) as KiBlastDefinition;
            KiBeam = Add(new KiBeamDefinition()) as KiBeamDefinition;

            DestructoDisk = Add(new DestructoDiskDefinition()) as DestructoDiskDefinition;

            Supernova = Add(new SupernovaDefinition()) as SupernovaDefinition;

            CelestialRapture = Add(new CelestialRaptureDefinition()) as CelestialRaptureDefinition;

            base.DefaultInitialize();
        }

        public DebugKiBlastDefinition DebugKiBlast { get; private set; }
        public KiBlastDefinition KiBlast { get; private set; }
        public KiBeamDefinition KiBeam { get; private set; }
        public DestructoDiskDefinition DestructoDisk { get; private set; }
        public SupernovaDefinition Supernova { get; private set; }
        public CelestialRaptureDefinition CelestialRapture { get; private set; }
    }
}