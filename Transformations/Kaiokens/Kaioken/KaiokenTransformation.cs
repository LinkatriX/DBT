using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Kaiokens.Kaioken
{
    public abstract class KaiokenTransformation : TransformationDefinition
    {
        public KaiokenTransformation(float damageMultiplier, float speedMultiplier, int defenseAdditive, float kaiokenLevel, System.Type kaiokenBuffType,
            float baseScale = 1f, params TransformationDefinition[] parents) : base(
            "Kaioken" + GetKaiokenLevelString(kaiokenLevel), "Kaioken " + GetKaiokenLevelString(kaiokenLevel),
            kaiokenBuffType, damageMultiplier, speedMultiplier, defenseAdditive, new TransformationDrain(0f, 0f), 
            new KaiokenTransformationAppearance(baseScale *= (0.9f + 0.1f * kaiokenLevel)), new TransformationOverload(0, 0), parents:parents)
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
                    return "2x";
                case 2:
                    return "3x";
                case 3:
                    return "4x";
                case 4:
                    return "10x";
                case 5:
                    return "20x";
                default:
                    return string.Empty;
            }
        }

        public float DamageMultiplier { get; private set; }

        public float SpeedMultiplier { get; private set; }

        public int DefenseAdditive { get; private set; }

        public float KaiokenLevel { get; private set; }
    }

    #region Kaioken Buff Classes

    public sealed class Kaioken2xTransformationBuff : TransformationBuff
    {
        public Kaioken2xTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken2x)
        {
        }
    }

    public sealed class Kaioken3xTransformationBuff : TransformationBuff
    {
        public Kaioken3xTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken3x)
        {
        }
    }

    public sealed class Kaioken4xTransformationBuff : TransformationBuff
    {
        public Kaioken4xTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken4x)
        {
        }
    }

    public sealed class Kaioken10xTransformationBuff : TransformationBuff
    {
        public Kaioken10xTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken10x)
        {
        }
    }

    public sealed class Kaioken20xTransformationBuff : TransformationBuff
    {
        public Kaioken20xTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken20x)
        {
        }
    }

#endregion

    public sealed class KaiokenTransformationAppearance : TransformationAppearance
    {
        public KaiokenTransformationAppearance(float auraScale) : base(
            new AuraAppearance(new AuraAnimationInformation("Auras/KaiokenAura", 4, 3, BlendState.Additive, true, baseScale: auraScale),
                new LightingAppearance(new float[] { 0.35f, 0, 0 })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.Red, new Color(32, 17, 11))
        {
        }
    }

    public sealed class Kaioken2xTransformation : KaiokenTransformation
    {
        public Kaioken2xTransformation(params TransformationDefinition[] parents) : base(1.05f, 1.025f, 1, 1,typeof(Kaioken2xTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken3xTransformation : KaiokenTransformation
    {
        public Kaioken3xTransformation(params TransformationDefinition[] parents) : base(1.10f, 1.05f, 2, 2, typeof(Kaioken3xTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken4xTransformation : KaiokenTransformation
    {
        public Kaioken4xTransformation(params TransformationDefinition[] parents) : base(1.15f, 1.075f, 3, 3, typeof(Kaioken4xTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken10xTransformation : KaiokenTransformation
    {
        public Kaioken10xTransformation(params TransformationDefinition[] parents) : base(1.20f, 1.10f, 4, 4, typeof(Kaioken10xTransformationBuff), parents: parents)
        {
        }
    }

    public sealed class Kaioken20xTransformation : KaiokenTransformation
    {
        public Kaioken20xTransformation(params TransformationDefinition[] parents) : base(1.30f, 1.15f, 5, 5, typeof(Kaioken20xTransformationBuff), parents: parents)
        {
        }
    }
}
