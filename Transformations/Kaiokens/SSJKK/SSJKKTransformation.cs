using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Kaiokens.SSJKK
{
    public sealed class SSJKKTransformation : TransformationDefinition
    {
        public SSJKKTransformation(params TransformationDefinition[] parents) : base("SSJKK", "Super Kaioken",
            typeof(SSJKKTransformationBuff), 2.25f, 1.625f, 8, new TransformationDrain(120 / Constants.TICKS_PER_SECOND),
            new SSJKKAppearance(), new TransformationOverload(0, 0), mastereable: false, parents: parents)
        {
        }
    }

    public sealed class SSJKKTransformationBuff : TransformationBuff
    {
        public SSJKKTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJKK)
        {
        }
    }

    public sealed class SSJKKAppearance : TransformationAppearance
    {
        public SSJKKAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJKKTransformation), 4, 3, BlendState.AlphaBlend, true),
            new LightingAppearance(new float[] { 2f, 0.08f, 0.21f })),
            new HairAppearance(new Color(224, 73, 90)), new Color(232, 42, 49), new Color(254, 167, 202))
        {
        }
    }
}