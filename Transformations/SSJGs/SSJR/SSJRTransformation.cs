using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJGs.SSJR
{
    public sealed class SSJRTransformation : TransformationDefinition
    {
        public SSJRTransformation(params TransformationDefinition[] parents) : base(
            "SSJR", "Super Saiyan Rosé", typeof(SSJRTransformationBuff),
            4.6f, 2.8f, 34, 
            new TransformationDrain(300f / Constants.TICKS_PER_SECOND, 150f / Constants.TICKS_PER_SECOND), 
            new SSJRAppearance(), 
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
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJRTransformation), 8, 3, BlendState.AlphaBlend, true),
                new LightingAppearance(new float[] { 2.01f, 0.72f, 1.43f })),
            new HairAppearance(new Color(255, 160, 171)), new Color(199, 30, 99), new Color(170, 174, 183))
        {
        }
    }
}
