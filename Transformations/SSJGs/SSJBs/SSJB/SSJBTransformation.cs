using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJGs.SSJBs.SSJB
{
    public sealed class SSJBTransformation : TransformationDefinition
    {
        public SSJBTransformation(params TransformationDefinition[] parents) : base(
            "SSJB", "Super Saiyan Blue", typeof(SSJBTransformationBuff),
            4.5f, 2.75f, 32,
            new TransformationDrain(300f / Constants.TICKS_PER_SECOND, 150f / Constants.TICKS_PER_SECOND), 
            new SSJBAppearance(),
            parents: parents)
        {
        }
    }

    public sealed class SSJBTransformationBuff : TransformationBuff
    {
        public SSJBTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJB)
        {
        }
    }

    public sealed class SSJBAppearance : TransformationAppearance
    {
        public SSJBAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJBTransformation), 8, 3, BlendState.AlphaBlend, true),
                new LightingAppearance(new float[] { 0.38f, 0.24f, 1.25f })),
            new HairAppearance(new Color(86, 238, 242)), Color.Blue, new Color(65, 113, 153))
        {
        }
    }
}
