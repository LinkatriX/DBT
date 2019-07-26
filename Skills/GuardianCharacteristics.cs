using DBT.Players;

namespace DBT.Skills
{
    public class GuardianCharacteristics
    {
        /// <summary></summary>
        /// /// <param name="skillChargeCharacteristics"></param>
        /// <param name="baseHealingMultiplierPerCharge">The number by which to multiply the healing per charge.</param>
        /// <param name="baseHealing">The base healing done per hit.</param>
        /// <param name="baseShootSpeed"></param>
        public GuardianCharacteristics(SkillChargeCharacteristics skillChargeCharacteristics, int baseHealing, float baseHealingMultiplierPerCharge, float baseShootSpeed, int baseKiRestore, float baseKiRestoreMultiplerPerCharge)
        {
            ChargeCharacteristics = skillChargeCharacteristics;

            BaseHealing = baseHealing;
            BaseHealingMultiplierPerCharge = baseHealingMultiplierPerCharge;

            BaseShootSpeed = baseShootSpeed;

            BaseKiRestore = baseKiRestore;
            BaseKiRestoreMultiplierPerCharge = baseKiRestoreMultiplerPerCharge;
        }


        public virtual float GetHealing(DBTPlayer dbtPlayer, int chargeLevel)
        {
            int healing = BaseHealing;
            GetHealing(dbtPlayer, ref healing, chargeLevel);

            return healing;
        }

        public virtual void GetHealing(DBTPlayer dbtPlayer, ref int healing, int chargeLevel)
        {
            healing = (int)(healing * GetHealingMultiplierPerCharge(dbtPlayer) * chargeLevel);
        }

        public virtual float GetKiRestore(DBTPlayer dbtPlayer, int chargeLevel)
        {
            int kiRestore = BaseKiRestore;
            GetKiRestore(dbtPlayer, ref kiRestore, chargeLevel);

            return kiRestore;
        }

        public virtual void GetKiRestore(DBTPlayer dbtPlayer, ref int kiRestore, int chargeLevel)
        {
            kiRestore = (int)(kiRestore * GetKiRestoreMultiplierPerCharge(dbtPlayer) * chargeLevel);
        }

        public virtual float GetHealingMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseHealingMultiplierPerCharge;
        public virtual float GetKiRestoreMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseKiRestoreMultiplierPerCharge;

        public virtual float GetShootSpeed(DBTPlayer dbtPlayer, int chargeLevel) => BaseShootSpeed;

        public virtual float GetSkillCooldown(DBTPlayer dbtPlayer, int chargeLevel) => BaseSkillCooldown;


        public SkillChargeCharacteristics ChargeCharacteristics { get; }

        public int BaseHealing { get; protected set; }
        public int BaseKiRestore { get; protected set; }
        public float BaseHealingMultiplierPerCharge { get; protected set; }
        public float BaseKiRestoreMultiplierPerCharge { get; protected set; }
        public float BaseShootSpeed { get; protected set; }
        public bool Channel { get; protected set; }
        public int BaseSkillCooldown { get; protected set; }
    }
}