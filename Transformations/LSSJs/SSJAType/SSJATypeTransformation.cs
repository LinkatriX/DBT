using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.LSSJs.SSJAType
{
    public sealed class SSJATypeTransformation : TransformationDefinition
    {
        public SSJATypeTransformation(params TransformationDefinition[] parents) : base(
            "SSJAType", "Super Saiyan A-Type", typeof(SSJATypeTransformationBuff), 
            3.5f, 2.25f, 26,
            new TransformationDrain(200f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND), 
            new SSJATypeTransformationAppearance(), 
            new TransformationOverload(0.02f, 0.01f), 
            parents: parents) 
        {
        }
    }

    public sealed class SSJATypeTransformationBuff : TransformationBuff 
    {
        public SSJATypeTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJA) 
        {
        }
    }

    public sealed class SSJATypeTransformationAppearance : TransformationAppearance 
    {
        public SSJATypeTransformationAppearance() : base(
                new AuraAppearance(new AuraAnimationInformation(typeof(SSJATypeTransformation), 4, 3, BlendState.Additive, true),
                    new LightingAppearance(new float[] { 1.86f, 2.26f, 0f })),
                new HairAppearance(new Color(109, 206, 216)), new Color(109, 206, 216), new Color(19, 119, 145))
        {
        }
    }
}
