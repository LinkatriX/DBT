using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.LSSJs.SSJCType
{
    public sealed class SSJCTypeTransformation : TransformationDefinition
    {
        public SSJCTypeTransformation(params TransformationDefinition[] parents) : base(
            "SSJCType", "Super Saiyan C-Type", typeof(SSJCTypeTransformationBuff),
            4f, 2.5f, 35, 
            new TransformationDrain(250f / Constants.TICKS_PER_SECOND, 125f / Constants.TICKS_PER_SECOND),
            new SSJCTypeTransformationAppearance(),
            new TransformationOverload(0.06f, 0.03f), 
            parents: parents)
        {
        }

        public sealed class SSJCTypeTransformationBuff : TransformationBuff
        {
            public SSJCTypeTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJC)
            {
            }
        }

        public sealed class SSJCTypeTransformationAppearance : TransformationAppearance
        {
            public SSJCTypeTransformationAppearance() : base(
                new AuraAppearance(new AuraAnimationInformation(typeof(SSJCTypeTransformation), 4, 3, BlendState.Additive, true),
                    new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
                new HairAppearance(new Color(228, 255, 28)),
                /*new OnTransformationAnimation(5, 5, 0, TransformationDefinitionManager.Instance.SSJC, new PlayerDrawInfo(),
                    typeof(SSJCTypeTransformation)),*/ Color.Lime, Color.Turquoise)
            {
            }
        }


    }
}