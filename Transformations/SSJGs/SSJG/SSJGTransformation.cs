using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJGs.SSJG
{
    public sealed class SSJGTransformation : TransformationDefinition
    {
        public SSJGTransformation(params TransformationDefinition[] parents) : base(
            "SSJG", "Super Saiyan God", typeof(SSJGTransformationBuff),
            3.5f, 2.25f, 24, 
            new TransformationDrain(200f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND), 
            new SSJGAppearance(),
            parents: parents)
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
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJGTransformation), 8, 3, BlendState.AlphaBlend, true),
                new LightingAppearance(new float[] { 1.25f, 0.45f, 0.28f })), 
            new HairAppearance(new Color(228, 41, 120)), Color.Red, new Color(228, 41, 120))//Old color: 255, 57, 74
        {
        }
    }
}
