using System;
using System.Text;
using DBT.Buffs;
using DBT.Commons;
using DBT.Players;
using Terraria;

namespace DBT.Transformations
{
    public abstract class TransformationBuff : DBTBuff, IHasUnlocalizedName
    {
        protected TransformationBuff(TransformationDefinition definition) : base(definition.DisplayName, "")
        {
            Definition = definition;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            if (Definition == null) return;

            DisplayName.SetDefault(Definition.DisplayName);
            Description.SetDefault(BuildDefaultTooltip());

            Main.buffNoTimeDisplay[Type] = Definition.Duration == TransformationDefinition.TRANSFORMATION_LONG_DURATION;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            if (dbtPlayer == null) return;

            if (!dbtPlayer.IsTransformed(this) && dbtPlayer.player.HasBuff(Type))
            {
                player.ClearBuff(Type);
                return;
            }

            TransformationTimer++;
            bool isFormMastered = dbtPlayer.HasMastered(Definition);

            float kiDrain = -(isFormMastered ? Definition.GetMasteredKiDrain(dbtPlayer) : Definition.GetUnmasteredKiDrain(dbtPlayer));
            float healthDrain = -(isFormMastered ? Definition.GetMasteredHealthDrain(dbtPlayer) : Definition.GetUnmasteredHealthDrain(dbtPlayer));
            float overloadIncrease = (isFormMastered ? Definition.GetMasteredOverloadGrowthRate(dbtPlayer) : Definition.GetUnmasteredOverloadGrowthRate(dbtPlayer));

            if (kiDrain != 0f)
            {
                if (!isFormMastered)
                    kiDrain *= KiDrainMultiplier;

                kiDrain *= dbtPlayer.KiDrainMultiplier;
                dbtPlayer.ModifyKi(kiDrain);

                if (TransformationTimer % Definition.Drain.transformationStepDelay == 0 && KiDrainMultiplier < Definition.Drain.maxTransformationKiDrainMultiplier)
                    KiDrainMultiplier += Definition.Drain.kiMultiplierPerStep;
            }

            if (healthDrain != 0f)
            {
                if (!isFormMastered)
                    healthDrain *= HealthDrainMultiplier;

                healthDrain *= dbtPlayer.HealthDrainMultiplier;

                dbtPlayer.player.statLife -= (int) healthDrain;
                dbtPlayer.player.lifeRegenTime = 0;

                if (TransformationTimer % Definition.Drain.transformationStepDelay == 0 && HealthDrainMultiplier < Definition.Drain.maxTransformationHealthDrainMultiplier)
                    HealthDrainMultiplier += Definition.Drain.healthMultiplierPerStep;
            }

            if (overloadIncrease != 0f)
            {
                overloadIncrease *= dbtPlayer.OverloadIncreaseMultiplier;
                dbtPlayer.Overload += overloadIncrease;
            }

            float 
                damageMultiplier = Definition.GetDamageMultiplier(dbtPlayer),
                halvedDamageMultiplier = damageMultiplier / 2;

            player.allDamage *= halvedDamageMultiplier;
            dbtPlayer.KiDamageMultiplier *= damageMultiplier;

            player.statDefense += Definition.GetDefenseAdditive(dbtPlayer);

            float speedMultiplier = Definition.GetSpeedMultiplier(dbtPlayer);

            player.moveSpeed *= speedMultiplier;
            player.maxRunSpeed *= speedMultiplier;
            player.runAcceleration *= speedMultiplier;

            if (player.jumpSpeedBoost < 1f)
                player.jumpSpeedBoost = 1f;

            player.jumpSpeedBoost *= speedMultiplier;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = BuildDefaultTooltip(Main.LocalPlayer.GetModPlayer<DBTPlayer>());
        }


        #region Tooltips

        public virtual string BuildDefaultTooltip() =>
            BuildTooltip(Definition.BaseDamageMultiplier, Definition.BaseSpeedMultiplier, Definition.Drain.baseUnmasteredKiDrain, Definition.Drain.baseMasteredKiDrain, Definition.BaseDefenseAdditive, Definition.Drain.baseUnmasteredHealthDrain, Definition.Drain.baseMasteredHealthDrain, Definition.Overload.baseOverloadGrowthRate, Definition.Overload.masteredOverloadGrowthRate);

        public virtual string BuildDefaultTooltip(DBTPlayer player) =>
            BuildTooltip(Definition.GetDamageMultiplier(player), Definition.GetSpeedMultiplier(player), Definition.GetUnmasteredKiDrain(player), Definition.GetMasteredKiDrain(player), Definition.GetDefenseAdditive(player), Definition.GetUnmasteredHealthDrain(player), Definition.GetMasteredHealthDrain(player), Definition.GetUnmasteredOverloadGrowthRate(player), Definition.GetMasteredOverloadGrowthRate(player));

        private string BuildTooltip(float damageMultiplier, float speedMultiplier, float unmasteredKiDrain, float masteredKiDrain, int baseDefenseAdditive, float unmasteredHealthDrain, float masteredHealthDrain, float unmasteredOverloadGain, float masteredOverloadGain)
        {
            StringBuilder sb = new StringBuilder();

            float roundedDamageMultiplier = (float)Math.Round(damageMultiplier, 2);
            float roundedSpeedMultiplier = (float)Math.Round(speedMultiplier, 2);

            BuildDamageDefenseSpeedInline(sb, roundedDamageMultiplier, baseDefenseAdditive, roundedSpeedMultiplier);

            int roundedUnmasteredKiDrain = (int)Math.Round(unmasteredKiDrain * 60);
            int roundedMasteredKiDrain = (int)Math.Round(masteredKiDrain * 60);
            int roundedUnmasteredHealthDrain = (int) Math.Round(unmasteredHealthDrain * 60);
            int roundedMasteredHealthDrain = (int) Math.Round(masteredHealthDrain * 60);
            int roundedUnmasteredOverloadGain = (int)Math.Round(unmasteredOverloadGain * 60);
            int roundedMasteredOverloadGain = (int)Math.Round(masteredOverloadGain * 60);

            BuildKiHealthDrainInline(sb, roundedUnmasteredKiDrain, roundedMasteredKiDrain, roundedUnmasteredHealthDrain, roundedMasteredHealthDrain);

            BuildOverloadGainInline(sb, roundedUnmasteredOverloadGain, roundedMasteredOverloadGain);

            return sb.ToString();
        }

        private void BuildDamageDefenseSpeedInline(StringBuilder builder, float damage, int defense, float speed)
        {
            if (damage != 0)
            {
                builder.AppendFormat("{0}{1:F2}x {2}", damage > 0 ? '+' : '-', damage - 1, "Damage");

                if (speed != 0)
                    builder.Append(", ");
            }

            if (speed != 0)
            {
                builder.AppendFormat("{0}{1:F2}x {2}", speed > 0 ? '+' : '-', speed - 1, "Speed");

                if (defense != 0)
                    builder.Append(", ");
            }

            if (defense != 0)
                builder.AppendFormat("{0}{1} {2}", defense > 0 ? '+' : '-', defense, "Defense");

            if (damage != 0 || defense != 0 || speed != 0)
                builder.AppendLine();
        }

        private void BuildOverloadGainInline(StringBuilder builder, int unmasteredOverloadGain, int masteredOverloadGain)
        {
            if(unmasteredOverloadGain != 0 || masteredOverloadGain != 0)
            {
                if (unmasteredOverloadGain == masteredOverloadGain)
                    builder.AppendFormat("{0} Overload/Second\n", unmasteredOverloadGain);
                else
                {
                    if (unmasteredOverloadGain != 0)
                    {
                        builder.AppendFormat("{0}{1} Overload/Second {2}", unmasteredOverloadGain > 0 ? '+' : '+', unmasteredOverloadGain, "While Unmastered");

                        if (masteredOverloadGain != 0)
                            builder.Append(", ");
                    }

                    if (masteredOverloadGain > 0)
                        builder.AppendFormat("{0}{1} Overload/Second {2}", masteredOverloadGain > 0 ? '+' : '+', masteredOverloadGain, "While Mastered");

                    if (unmasteredOverloadGain != 0 && masteredOverloadGain != 0)
                        builder.AppendLine();
                }
            }
        }

        private void BuildKiHealthDrainInline(StringBuilder builder, int unmasteredKiDrain, int masteredKiDrain, int unmasteredHealthDrain, int masteredHealthDrain)
        {
            // "While Unmastered"
            // "While Mastered"

            if (unmasteredKiDrain != 0 || masteredKiDrain != 0)
            {
                if (unmasteredKiDrain == masteredKiDrain)
                    builder.AppendFormat("{0} Ki/Second\n", unmasteredKiDrain);
                else
                {
                    if (unmasteredKiDrain != 0 && masteredKiDrain != 0)
                    {
                        builder.AppendFormat("{0}{1} Ki/Second {2}", unmasteredKiDrain > 0 ? '-' : '+', unmasteredKiDrain, "While Unmastered");

                        if (masteredKiDrain != 0)
                            builder.Append(", ");
                    }
                    else
                    {
                        if (unmasteredKiDrain != 0 && masteredKiDrain <= 0)
                        {
                            builder.AppendFormat("{0}{1} Ki/Second {2}", unmasteredKiDrain > 0 ? '-' : '+', unmasteredKiDrain, "");
                        }
                    }

                    if (masteredKiDrain > 0)
                        builder.AppendFormat("{0}{1} Ki/Second {2}", masteredKiDrain > 0 ? '-' : '+', masteredKiDrain, "While Mastered");

                    if (unmasteredKiDrain != 0 && masteredKiDrain != 0)
                        builder.AppendLine();
                }
            }

            if (unmasteredHealthDrain != 0 || masteredHealthDrain != 0)
            {
                if (unmasteredHealthDrain == masteredHealthDrain)
                    builder.AppendFormat("{0} Health/Second\n", unmasteredKiDrain);
                else
                {
                    if (unmasteredHealthDrain != 0)
                    {
                        builder.AppendFormat("{0}{1} Health/Second {2}", unmasteredHealthDrain > 0 ? '-' : '+', unmasteredHealthDrain, "While Unmastered");

                        if (masteredHealthDrain != 0)
                            builder.Append(", ");
                    }

                    if (masteredHealthDrain != 0)
                        builder.AppendFormat("{0}{1} Health/Second {2}", masteredHealthDrain > 0 ? '-' : '+', masteredHealthDrain, "While Mastered");

                    if (unmasteredHealthDrain != 0 && masteredHealthDrain != 0)
                        builder.AppendLine();
                }
            }
        }

        #endregion


        public string UnlocalizedName => Definition.UnlocalizedName;

        public TransformationDefinition Definition { get; }

        
        public int TransformationTimer { get; private set; }

        public float KiDrainMultiplier { get; private set; } = 1;
        public float HealthDrainMultiplier { get; private set; } = 1;
    }
}
