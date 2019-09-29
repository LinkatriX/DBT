using DBT.Auras;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.LSSJs.Wrathful
{
    public partial class WrathfulTransformation : TransformationDefinition
    {
        public WrathfulTransformation(params TransformationDefinition[] parents)
            : base("Wrathful", "Wrathful", typeof(WrathfulTransformationBuff), 1.5f, 1.25f, 3,
            new TransformationDrain(60f / Constants.TICKS_PER_SECOND),
            new WrathfulTransformationAppearance(),
            new TransformationOverload(0, 0), mastereable: false,
            parents: parents)
        {
        }

        #region Wrathful Act 2 and Act 3 Multipliers

        public override float GetDamageMultiplier(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.BaseMaxKi >= 10000)
            {
                return BaseDamageMultiplier + 1.4f;
            }
            else
            {
                if (dbtPlayer.BaseMaxKi >= _wrathfulAct2KiRequirement && dbtPlayer.BaseMaxKi < 10000)
                {
                    return BaseDamageMultiplier + 0.375f;
                }
            }
            return BaseDamageMultiplier;
        }


        public override float GetSpeedMultiplier(DBTPlayer dbtPlayer) 
        {
            if (dbtPlayer.BaseMaxKi >= _wrathfulAct3KiRequirement)
            {
                return BaseSpeedMultiplier + 0.7f;
            }
            else
            {
                if (dbtPlayer.BaseMaxKi >= _wrathfulAct2KiRequirement && dbtPlayer.BaseMaxKi < _wrathfulAct3KiRequirement)
                {
                    return BaseSpeedMultiplier + 1.375f;
                }
            }
            return BaseSpeedMultiplier;
        }

        public override int GetDefenseAdditive(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.BaseMaxKi >= _wrathfulAct3KiRequirement)
            {
                return BaseDefenseAdditive + 17;
            }
            else
            {
                if (dbtPlayer.BaseMaxKi >= _wrathfulAct2KiRequirement && dbtPlayer.BaseMaxKi < _wrathfulAct3KiRequirement)
                {
                    return BaseDefenseAdditive + 7;
                }
            }
            return BaseDefenseAdditive;
        }

        public override float GetUnmasteredKiDrain(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.BaseMaxKi >= _wrathfulAct3KiRequirement)
            {
                return base.GetUnmasteredKiDrain(dbtPlayer) * (160f / Constants.TICKS_PER_SECOND); //Ki drain of 160 ki/s
            }
            else
            {
                if (dbtPlayer.BaseMaxKi >= _wrathfulAct2KiRequirement && dbtPlayer.BaseMaxKi < _wrathfulAct3KiRequirement)
                {
                    return base.GetUnmasteredKiDrain(dbtPlayer) * 2; //Ki drain of 120 ki/s
                }
            }
            return base.GetUnmasteredKiDrain(dbtPlayer);
        }
#endregion

        private readonly float _wrathfulAct2KiRequirement = 5000f, _wrathfulAct3KiRequirement = 10000f;
    }

    public sealed class WrathfulTransformationBuff : TransformationBuff
    {
        public WrathfulTransformationBuff() : base(TransformationDefinitionManager.Instance.Wrathful)
        {
        }
    }

    public sealed class WrathfulTransformationAppearance : TransformationAppearance
    {
        public WrathfulTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(WrathfulTransformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.Lime, new Color(211, 186, 44))
        {
        }
    }
}