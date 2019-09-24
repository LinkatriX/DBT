using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Kaiokens.Kaioken
{
    public abstract class KaiokenTransformation : TransformationDefinition
    {
        protected KaiokenTransformation(float damageMultiplier, float speedMultiplier, int defenseAdditive, float kaiokenLevel, System.Type kaiokenBuffType,
            float baseScale = 1f, params TransformationDefinition[] parents) : base(
            "Kaioken" + GetKaiokenLevelString(kaiokenLevel), "Kaioken " + GetKaiokenLevelString(kaiokenLevel),
            kaiokenBuffType, damageMultiplier, speedMultiplier, defenseAdditive, new TransformationDrain(0f, 0f),
            new KaiokenTransformationAppearance(baseScale *= (0.9f + 0.1f * kaiokenLevel)), TransformationOverload.Zero, parents: parents)
        {
            DamageMultiplier = damageMultiplier;

            SpeedMultiplier = speedMultiplier;

            DefenseAdditive = defenseAdditive;

            KaiokenLevel = kaiokenLevel;
        }

        public static string GetKaiokenLevelString(float displayKaiokenLevel)
        {
            switch (displayKaiokenLevel)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return "2X";
                case 2:
                    return "3X";
                case 3:
                    return "4X";
                case 4:
                    return "10X";
                case 5:
                    return "20X";
                default:
                    return string.Empty;
            }
        }

        public float DamageMultiplier { get; }

        public float SpeedMultiplier { get; }

        public int DefenseAdditive { get; }

        public float KaiokenLevel { get; }
    }


    #region Kaioken Buff Classes

    public sealed class Kaioken2XTransformationBuff : TransformationBuff
    {
        public Kaioken2XTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken2x)
        {
        }
    }

    public sealed class Kaioken3XTransformationBuff : TransformationBuff
    {
        public Kaioken3XTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken3x)
        {
        }
    }

    public sealed class Kaioken4XTransformationBuff : TransformationBuff
    {
        public Kaioken4XTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken4x)
        {
        }
    }

    public sealed class Kaioken10XTransformationBuff : TransformationBuff
    {
        public Kaioken10XTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken10x)
        {
        }
    }

    public sealed class Kaioken20XTransformationBuff : TransformationBuff
    {
        public Kaioken20XTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken20x)
        {
        }
    }

    #endregion

    #region Transformation Definitions

    public sealed class KaiokenTransformationAppearance : TransformationAppearance
    {
        public KaiokenTransformationAppearance(float auraScale) : base(
            new AuraAppearance(new AuraAnimationInformation("Auras/KaiokenAura", 4, 3, BlendState.Additive, true, baseScale: auraScale),
                new LightingAppearance(new float[] { 0.35f, 0, 0 })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.Red, new Color(32, 17, 11))
        {
        }
    }

    public sealed class Kaioken2XTransformation : KaiokenTransformation
    {
        public Kaioken2XTransformation(params TransformationDefinition[] parents) : base(1.05f, 1.025f, 1, 1, typeof(Kaioken2XTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken3XTransformation : KaiokenTransformation
    {
        public Kaioken3XTransformation(params TransformationDefinition[] parents) : base(1.10f, 1.05f, 2, 2, typeof(Kaioken3XTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken4XTransformation : KaiokenTransformation
    {
        public Kaioken4XTransformation(params TransformationDefinition[] parents) : base(1.15f, 1.075f, 3, 3, typeof(Kaioken4XTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken10XTransformation : KaiokenTransformation
    {
        public Kaioken10XTransformation(params TransformationDefinition[] parents) : base(1.20f, 1.10f, 4, 4, typeof(Kaioken10XTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken20XTransformation : KaiokenTransformation
    {
        public Kaioken20XTransformation(params TransformationDefinition[] parents) : base(1.30f, 1.15f, 5, 5, typeof(Kaioken20XTransformationBuff), parents: parents)
        {
        }
    }

    #endregion
}
