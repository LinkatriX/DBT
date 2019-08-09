using DBT.Players;

namespace DBT.Skills
{
    public class SkillCharacteristics
    {
        /// <summary></summary>
        /// /// <param name="skillChargeCharacteristics"></param>
        /// <param name="baseDamageMultiplierPerCharge">The number by which to multiply the damage.</param>
        /// <param name="baseDamage">The base damage done by the skill per damage tick.</param>
        /// <param name="baseShootSpeed"></param>
        /// <param name="baseKnockback"></param>
        /// <param name="baseKnockbackMultiplierPerCharge"></param>
        /// <param name="baseCriticalChance"></param>
        /// <param name="baseCriticalChanceMultiplierPerCharge"></param>
        /// <param name="baseCriticalMultiplier"></param>
        /// <param name="baseCriticalMultiplierMultiplierPerCharge"></param>
        public SkillCharacteristics(SkillChargeCharacteristics skillChargeCharacteristics, int baseDamage, float baseDamageMultiplierPerCharge, float baseShootSpeed, float baseKnockback, float baseKnockbackMultiplierPerCharge, 
            float baseCriticalChance, float baseCriticalChanceMultiplierPerCharge, float baseCriticalMultiplier, float baseCriticalMultiplierMultiplierPerCharge)
        {
            ChargeCharacteristics = skillChargeCharacteristics;

            BaseDamage = baseDamage;
            BaseDamageMultiplierPerCharge = baseDamageMultiplierPerCharge;

            BaseShootSpeed = baseShootSpeed;

            BaseKnockback = baseKnockback;
            BaseKnockbackMultiplierPerCharge = baseKnockbackMultiplierPerCharge;

            BaseCriticalChance = baseCriticalChance;
            BaseCriticalChanceMultiplierPerCharge = baseCriticalChanceMultiplierPerCharge;

            BaseCriticalMultiplier = baseCriticalMultiplier;
            BaseCriticalMultiplierMultiplierPerCharge = baseCriticalMultiplierMultiplierPerCharge;
        }


        public virtual float GetDamage(DBTPlayer dbtPlayer, int chargeLevel)
        {
            int damage = (int)BaseDamage;
            GetDamage(dbtPlayer, ref damage, chargeLevel);

            return damage;
        }

        public virtual void GetDamage(DBTPlayer dbtPlayer, ref int damage, int chargeLevel)
        {
            damage = (int)(damage * GetDamageMultiplierPerCharge(dbtPlayer) * chargeLevel);
        }

        public virtual float GetDamageMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseDamageMultiplierPerCharge;


        public virtual float GetShootSpeed(DBTPlayer dbtPlayer, int chargeLevel) => BaseShootSpeed;


        public virtual float GetKnockback(DBTPlayer dbtPlayer, int chargeLevel)
        {
            float knockback = BaseKnockback;
            GetKnockback(dbtPlayer, ref knockback, chargeLevel);

            return knockback;
        }

        public virtual void GetKnockback(DBTPlayer dbtPlayer, ref float knockback, int chargeLevel)
        {
            knockback *= GetKnockbackMultiplierPerCharge(dbtPlayer) * (chargeLevel + 1);
        }

        public virtual float GetKnockbackMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseKnockbackMultiplierPerCharge;


        public virtual float GetCriticalChance(DBTPlayer dbtPlayer, int chargeLevel) => BaseCriticalChance * GetCriticalChanceMultiplierPerCharge(dbtPlayer) * chargeLevel;

        public virtual float GetCriticalChanceMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseCriticalChanceMultiplierPerCharge;


        public virtual float GetCriticalMultiplier(DBTPlayer dbtPlayer, int chargeLevel) => BaseCriticalMultiplier * GetCriticalMultiplierMultiplierPerCharge(dbtPlayer) * chargeLevel;

        public virtual float GetCriticalMultiplierMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseCriticalMultiplierMultiplierPerCharge;


        public virtual float GetSkillCooldown(DBTPlayer dbtPlayer, int chargeLevel) => BaseSkillCooldown;


        public SkillChargeCharacteristics ChargeCharacteristics { get; }

        public int BaseDamage { get; protected set; }
        public float BaseDamageMultiplierPerCharge { get; protected set; }

        public float BaseShootSpeed { get; protected set; }

        public float BaseKnockback { get; protected set; }
        public float BaseKnockbackMultiplierPerCharge { get; protected set; }

        public float BaseCriticalChance { get; protected set; }
        public float BaseCriticalChanceMultiplierPerCharge { get; protected set; }

        public float BaseCriticalMultiplier { get; protected set; }
        public float BaseCriticalMultiplierMultiplierPerCharge { get; protected set; }

        public bool Channel { get; protected set; }

        public int BaseSkillCooldown { get; protected set; }
    }
}