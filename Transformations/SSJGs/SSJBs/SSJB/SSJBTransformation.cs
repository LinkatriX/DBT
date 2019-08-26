using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJGs.SSJBs.SSJB
{
    public sealed class SSJBTransformation : TransformationDefinition
    {
        public SSJBTransformation(params TransformationDefinition[] parents) : base(
            "SSJB", "Super Saiyan Blue", typeof(SSJBTransformationBuff),
            4.5f, 4.5f, 32,
            new TransformationDrain(5f, 2.5f), 
            new SSJBAppearance(),
            new TransformationOverload(0, 0), 
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
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJBTransformation), 8, 3, BlendState.Additive, 1f, true),//.AlphaBlend when ssb aura gets updated
                new LightingAppearance(new float[] { 0.38f, 0.24f, 1.25f })),
            new HairAppearance(Color.White), Color.Blue, new Color(65, 113, 153))
        {
        }
    }
}
