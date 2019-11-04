using DBT.Auras;
using DBT.Transformations.Appearance;
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
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJKKTransformation), 4, 3, BlendState.Additive, true),
            new LightingAppearance(new float[] { 0.2f, 0f, 0f })),
            new HairAppearance(new Color(255, 245, 197)), Color.Red, new Color(254, 167, 202))
        {
        }
    }
}