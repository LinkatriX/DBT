using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Kaiokens.SSGKKs.SSJBKK
{
    public sealed class SSJBKKTransformation :TransformationDefinition
    {
        public SSJBKKTransformation(params TransformationDefinition[] parents) : base("SSJBKK", "Super Saiyan Blue Kaioken", typeof(SSJBKKTransformationBuff),
            5.25f, 3.125f, 42, new TransformationDrain(200f / Constants.TICKS_PER_SECOND, 150f / Constants.TICKS_PER_SECOND), 
            new SSJBKKTransformationAppearance(), parents: parents) 
        {
        }
    }

    public sealed class SSJBKKTransformationBuff : TransformationBuff 
    {
        public SSJBKKTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJBKK) 
        {
        }
    }

    public sealed class SSJBKKTransformationAppearance : TransformationAppearance 
    {
        public SSJBKKTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJBKKTransformation), 8, 3, BlendState.AlphaBlend, true),
                new LightingAppearance(new float[] { 2.22f, .49f, 0.99f })),
            new HairAppearance(new Color(63, 226, 213)), new Color(63, 226, 213), new Color(65, 113, 153))
        {
        }
    }
}
