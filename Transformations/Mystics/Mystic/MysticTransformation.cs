using DBT.Auras;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Mystics.Mystic
{
    public sealed class MysticTransformation : TransformationDefinition
    {
        public MysticTransformation(params TransformationDefinition[] parents) : base("Mystic", "Mystic", typeof(MysticTransformationBuff),
            1.6f, 1.30f, 3, new TransformationDrain(25f / Constants.TICKS_PER_SECOND, 0f), new MysticTransformationAppearance(), 
            new TransformationOverload(0, 0), mastereable: false,
            parents: parents)
        {
        }

        #region Mystic Act 2 and Act 3 Multipliers

        public override float GetDamageMultiplier(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
            {
                return BaseDamageMultiplier + 1.4f;
            }
            else
            {
                if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ2) && 
                    !dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
                {
                    return BaseDamageMultiplier + 0.8f;
                }
            }
            return BaseDamageMultiplier;
        }


        public override float GetSpeedMultiplier(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
            {
                return BaseSpeedMultiplier + 0.7f;
            }
            else
            {
                if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ2) &&
                    !dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
                {
                    return BaseSpeedMultiplier + 0.4f;
                }
            }
            return BaseSpeedMultiplier;
        }

        public override int GetDefenseAdditive(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
            {
                return BaseDefenseAdditive + 17;
            }
            else
            {
                if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ2) &&
                    !dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
                {
                    return BaseDefenseAdditive + 7;
                }
            }
            return BaseDefenseAdditive;
        }

        public override float GetUnmasteredKiDrain(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
            {
                return base.GetUnmasteredKiDrain(dbtPlayer) + (35f / Constants.TICKS_PER_SECOND); ;
            }
            else
            {
                if (dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ2) &&
                    !dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3))
                {
                    return base.GetUnmasteredKiDrain(dbtPlayer) + (15f / Constants.TICKS_PER_SECOND);
                }
            }
            return base.GetUnmasteredKiDrain(dbtPlayer);
        }
        #endregion
    }

    public sealed class MysticTransformationBuff : TransformationBuff
    {
        public MysticTransformationBuff() : base(TransformationDefinitionManager.Instance.Mystic)
        {
        }
    }

    public sealed class MysticTransformationAppearance : TransformationAppearance
    {
        public MysticTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation("Auras/BaseAura", 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1f, 1f, 1f })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.LightBlue, Main.LocalPlayer.eyeColor)
        {
        }
    }
}