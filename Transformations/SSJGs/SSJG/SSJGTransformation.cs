﻿using DBTR.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBTR.Transformations.SSJGs.SSJG
{
    public sealed class SSJGTransformation : TransformationDefinition
    {
        public SSJGTransformation(params TransformationDefinition[] parents) : base(
            "SSJG", "Super Saiyan God", typeof(SSJGTransformationBuff),
            3.5f, 3.5f, 24, 200f, 100f,
            new SSJGAppearance())
        {
        }
    }

    public sealed class SSJGTransformationBuff : TransformationBuff
    {
        public SSJGTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJG)
        {
        }
    }

    public sealed class SSJGAppearance : TransformationAppearance
    {
        public SSJGAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJGTransformation), 8, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1.25f, 0.45f, 0.28f })), 
            new HairAppearance(new Color(255, 57, 74)))
        {
        }
    }
}
