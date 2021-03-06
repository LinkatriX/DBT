﻿using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.LSSJs.LSSJ
{
    public sealed class LSSJTransformation : TransformationDefinition
    {
        public LSSJTransformation(params TransformationDefinition[] parents) : base(
            "LSSJ", "Legendary Super Saiyan", typeof(LSSJTransformationBuff),
            4.75f, 2.875f, 45, 
            new TransformationDrain(320f / Constants.TICKS_PER_SECOND, 160f / Constants.TICKS_PER_SECOND),
            new LSSJTransformationAppearance(),
            new TransformationOverload(0.1f, 0.05f),
            parents: parents)
        {
        }
    }

    public sealed class LSSJTransformationBuff : TransformationBuff
    {
        public LSSJTransformationBuff() : base(TransformationDefinitionManager.Instance.LSSJ)
        {
        }
    }

    public sealed class LSSJTransformationAppearance : TransformationAppearance
    {
        public LSSJTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(LSSJTransformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
            new HairAppearance(new Color(161, 253, 70)), Color.Lime, new Color(103, 219, 50))
        {
        }
    }
}