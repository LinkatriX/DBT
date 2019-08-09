using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJGs.SSJR
{
    public sealed class SSJRTransformation : TransformationDefinition
    {
        public SSJRTransformation(params TransformationDefinition[] parents) : base(
            "SSJR", "Super Saiyan Rosé", typeof(SSJRTransformationBuff),
            4.75f, 4.75f, 34, 
            new TransformationDrain(5, 2.5f), 
            new SSJRAppearance(),
            new TransformationOverload(0, 0), 
            parents: parents)
        {
        }
    }

    public sealed class SSJRTransformationBuff : TransformationBuff
    {
        public SSJRTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJR)
        {
        }
    }

    public sealed class SSJRAppearance : TransformationAppearance
    {
        public SSJRAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJRTransformation), 8, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1.3f, 0.36f, 0.78f })),
            new HairAppearance(Color.White), Color.MistyRose)
        {
        }
    }
}
