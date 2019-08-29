using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJGs.SSJBs.SSJBE
{
    public sealed class SSJBETransformation : TransformationDefinition
    {
        public SSJBETransformation(params TransformationDefinition[] parents) : base(
            "SSJBE", "Super Saiyan Blue Evolved", typeof(SSJBETransformationBuff),
            5.25f, 2.625f, 42,
            new TransformationDrain(280f / Constants.TICKS_PER_SECOND, 140f / Constants.TICKS_PER_SECOND),
            new SSJBEAppearance(),
            new TransformationOverload(0, 0), 
            parents: parents)          
        {
        }
    }

    public sealed class SSJBETransformationBuff : TransformationBuff
    {
        public SSJBETransformationBuff() : base(TransformationDefinitionManager.Instance.SSJBE)
        {
        }
    }

    public sealed class SSJBEAppearance : TransformationAppearance
    {
        public SSJBEAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJBETransformation), 8, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 0f, 0.33f, 1.32f })),
            new HairAppearance(Color.White), Color.Blue, new Color(27, 56, 170))
        {
        }
    }
}
