using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Kaiokens.SSGKKs.SSJRKK
{
    public sealed class SSJRKKTransformation : TransformationDefinition
    {
        public SSJRKKTransformation(params TransformationDefinition[] parents) : base("SSJRKK", "Super Saiyan Rosé Kaioken", typeof(SSJRKKTransformationBuff),
            5.35f, 3.175f, 44, new TransformationDrain(210f / Constants.TICKS_PER_SECOND, 160f / Constants.TICKS_PER_SECOND),
            new SSJRKKTransformationAppearance(), parents: parents)
        {
        }
    }

    public sealed class SSJRKKTransformationBuff : TransformationBuff
    {
        public SSJRKKTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJRKK)
        {
        }
    }

    public sealed class SSJRKKTransformationAppearance : TransformationAppearance
    {
        public SSJRKKTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJRKKTransformation), 8, 3, BlendState.AlphaBlend, true),
                new LightingAppearance(new float[] { 2.55f, 0f, 0.79f })),
            new HairAppearance(new Color(255, 135, 163)), new Color(255, 135, 163), new Color(170, 174, 183))
        {
        }
    }
}
