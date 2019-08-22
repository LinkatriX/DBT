using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.LSSJs.SSJCType
{
    public sealed class SSJCTypeTransformation : TransformationDefinition
    {
        public SSJCTypeTransformation(params TransformationDefinition[] parents) : base(
            "SSJCType", "Super Saiyan C-Type", typeof(SSJCTypeTransformationBuff),
            3.9f, 3.9f, 26, 
            new TransformationDrain(4, 2),
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
                new AuraAppearance(new AuraAnimationInformation(typeof(SSJCTypeTransformation), 4, 3, BlendState.Additive, 1f, true),
                    new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
                new HairAppearance(Color.White), Color.Lime, Color.Turquoise)
            {
            }
        }
    }
}