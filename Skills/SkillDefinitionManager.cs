using DBT.Skills.EnergyWave;
using DBT.Skills.KiBlast;
using DBT.Skills.KiBeam;
using DBT.Skills.DestructoDisk;
using DBT.Skills.DoubleSunday;
using DBT.Skills.EnergyBlastBarrage;
using DBT.Skills.Masenko;
using DBT.Skills.SpiritBall;
using DBT.Skills.BloodThief;
using DBT.Skills.GalickGun;
using DBT.Skills.HellzoneGrenade;
using DBT.Skills.Kamehameha;
using DBT.Skills.BigBangAttack;
using DBT.Skills.EnergyShot;
using DBT.Skills.Scattershot;
using DBT.Skills.SpecialBeamCannon;
using DBT.Skills.SpiritBomb;
using DBT.Skills.SuperKamehameha;
using DBT.Skills.TrapShooter;
using DBT.Skills.DestructoDiskAssault;
using DBT.Skills.FinalFlash;
using DBT.Skills.Kamehamehax10;
using DBT.Skills.SuperEnergyBarrage;
using DBT.Skills.Supernova;
using DBT.Skills.BlackPowerBall;
using DBT.Skills.CandyLaser;
using DBT.Skills.CelestialRapture;
using DBT.Skills.FinalShineAttack;
using DBT.Skills.HolyWrath;
using DBT.Skills.MajinExtinctionAttack;
using DBT.Skills.SuperSpiritBomb;
using DBT.Dynamicity;
using WebmilioCommons.Managers;
using DBT.Skills.DemonBarrage;
using DBT.Skills.DeathSaucer;

namespace DBT.Skills
{
    public sealed class SkillDefinitionManager : SingletonManager<SkillDefinitionManager, SkillDefinition>
    {
        public override void DefaultInitialize()
        {
            #region Tier 1 Skills
            KiBlast = Add(new KiBlastDefinition()) as KiBlastDefinition;
            KiBeam = Add(new KiBeamDefinition(KiBlast)) as KiBeamDefinition;
            EnergyWave = Add(new EnergyWaveDefinition(KiBlast)) as EnergyWaveDefinition;
            #endregion

            #region Tier 2 Skills
            DestructoDisk = Add(new DestructoDiskDefinition(KiBeam)) as DestructoDiskDefinition;
            DoubleSunday = Add(new DoubleSundayDefinition(EnergyWave)) as DoubleSundayDefinition;
            EnergyBlastBarrage = Add(new EnergyBlastBarrageDefinition(KiBlast)) as EnergyBlastBarrageDefinition;
            Masenko = Add(new MasenkoDefinition(EnergyWave)) as MasenkoDefinition;
            SpiritBall = Add(new SpiritBallDefinition(KiBlast)) as SpiritBallDefinition;
            #endregion

            #region Tier 3 Skills
            BloodThief = Add(new BloodThiefDefinition(KiBlast)) as BloodThiefDefinition;
            DemonBarrage = Add(new DemonBarrageDefinition(BloodThief)) as DemonBarrageDefinition;
            GalickGun = Add(new GalickGunDefinition(EnergyWave)) as GalickGunDefinition;
            HellzoneGrenade = Add(new HellzoneGrenadeDefinition(EnergyBlastBarrage)) as HellzoneGrenadeDefinition;
            Kamehameha = Add(new KamehamehaDefinition(EnergyWave)) as KamehamehaDefinition;
            DeathSaucer = Add(new DeathSaucerDefinition()) as DeathSaucerDefinition;
            #endregion

            #region Tier 4 Skills
            BigBangAttack = Add(new BigBangAttackDefinition(SpiritBall)) as BigBangAttackDefinition;
            EnergyShot = Add(new EnergyShotDefinition(KiBlast)) as EnergyShotDefinition;
            Scattershot = Add(new ScattershotDefinition(HellzoneGrenade)) as ScattershotDefinition;
            SpecialBeamCannon = Add(new SpecialBeamCannonDefinition(KiBeam)) as SpecialBeamCannonDefinition;
            SpiritBomb = Add(new SpiritBombDefinition(SpiritBall)) as SpiritBombDefinition;
            SuperKamehameha = Add(new SuperKamehamehaDefinition(Kamehameha)) as SuperKamehamehaDefinition;
            TrapShooter = Add(new TrapShooterDefinition(EnergyShot)) as TrapShooterDefinition;
            #endregion

            # region Tier 5 Skills
            DestructoDiskAssault = Add(new DestructoDiskAssaultDefinition(DestructoDisk)) as DestructoDiskAssaultDefinition;
            FinalFlash = Add(new FinalFlashDefinition(GalickGun)) as FinalFlashDefinition;
            Kamehamehax10 = Add(new Kamehamehax10Definition(SuperKamehameha)) as Kamehamehax10Definition;
            SuperEnergyBarrage = Add(new SuperEnergyBarrageDefinition(EnergyBlastBarrage)) as SuperEnergyBarrageDefinition;
            Supernova = Add(new SupernovaDefinition(SpiritBall)) as SupernovaDefinition;
            #endregion

            #region Tier 6 Skills
            BlackPowerBall = Add(new BlackPowerBallDefinition(EnergyShot)) as BlackPowerBallDefinition;
            CandyLaser = Add(new CandyLaserDefinition(KiBeam)) as CandyLaserDefinition;
            CelestialRapture = Add(new CelestialRaptureDefinition(KiBlast)) as CelestialRaptureDefinition;
            FinalShineAttack = Add(new FinalShineAttackDefinition(FinalFlash)) as FinalShineAttackDefinition;
            HolyWrath = Add(new HolyWrathDefinition(Supernova)) as HolyWrathDefinition;
            MajinExtinctionAttack = Add(new MajinExtinctionAttackDefinition(SuperEnergyBarrage)) as MajinExtinctionAttackDefinition;
            SuperSpiritBomb = Add(new SuperSpiritBombDefinition(SpiritBomb)) as SuperSpiritBombDefinition;
            #endregion

            Tree = new Tree<SkillDefinition>(byIndex);

            base.DefaultInitialize();
        }

        #region Skill Properties
        public EnergyWaveDefinition EnergyWave { get; private set; }
        public KiBlastDefinition KiBlast { get; private set; }
        public KiBeamDefinition KiBeam { get; private set; }
        public DestructoDiskDefinition DestructoDisk { get; private set; }
        public DoubleSundayDefinition DoubleSunday { get; private set; }
        public EnergyBlastBarrageDefinition EnergyBlastBarrage { get; private set; }
        public MasenkoDefinition Masenko { get; private set; }
        public SpiritBallDefinition SpiritBall { get; private set; }
        public BloodThiefDefinition BloodThief { get; private set; }
        public DemonBarrageDefinition DemonBarrage { get; private set; }
        public GalickGunDefinition GalickGun { get; private set; }
        public HellzoneGrenadeDefinition HellzoneGrenade { get; private set; }
        public KamehamehaDefinition Kamehameha { get; private set; }
        public DeathSaucerDefinition DeathSaucer { get; private set; }
        public BigBangAttackDefinition BigBangAttack { get; private set; }
        public EnergyShotDefinition EnergyShot { get; private set; }
        public ScattershotDefinition Scattershot { get; private set; }
        public SpecialBeamCannonDefinition SpecialBeamCannon { get; private set; }
        public SpiritBombDefinition SpiritBomb { get; private set; }
        public SuperKamehamehaDefinition SuperKamehameha { get; private set; }
        public TrapShooterDefinition TrapShooter { get; private set; }
        public DestructoDiskAssaultDefinition DestructoDiskAssault { get; private set; }
        public FinalFlashDefinition FinalFlash { get; private set; }
        public Kamehamehax10Definition Kamehamehax10 { get; private set; }
        public SuperEnergyBarrageDefinition SuperEnergyBarrage { get; private set; }
        public SupernovaDefinition Supernova { get; private set; }
        public BlackPowerBallDefinition BlackPowerBall { get; private set; }
        public CandyLaserDefinition CandyLaser { get; private set; }
        public CelestialRaptureDefinition CelestialRapture { get; private set; }
        public FinalShineAttackDefinition FinalShineAttack { get; private set; }
        public HolyWrathDefinition HolyWrath { get; private set; }
        public MajinExtinctionAttackDefinition MajinExtinctionAttack { get; private set; }
        public SuperSpiritBombDefinition SuperSpiritBomb { get; private set; }

        public Tree<SkillDefinition> Tree { get; private set; }
        #endregion
    }
}