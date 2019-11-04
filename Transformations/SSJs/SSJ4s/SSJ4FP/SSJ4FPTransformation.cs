using DBT.Auras;
using DBT.Transformations.Appearance;
using DBT.Transformations.SSJs.SSJ4s.SSJ4;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJs.SSJ4s.SSJ4FP
{
    public sealed class SSJ4FPTransformation : TransformationDefinition
    {
        public SSJ4FPTransformation(params TransformationDefinition[] parents) : base(
                "SSJ4FP", "Super Saiyan 4 Full Power", typeof(SSJ4FPTransformationBuff),
                4.20f, 2.60f, 30,
                new TransformationDrain(250f / 60, 125f / 60),
                new SSJ4FPAppearance(),
                new TransformationOverload(0, 0),
                isManualLookup: true,
                manualHairLookup: "SSJ4",
                parents: parents)
        {
        }
    }

    public sealed class SSJ4FPTransformationBuff : TransformationBuff
    {
        public SSJ4FPTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ4FP)
        {
        }
    }

    public sealed class SSJ4FPAppearance : TransformationAppearance
    {
        public SSJ4FPAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ4Transformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1.60f, 1.40f, 0f })),
            new HairAppearance(new Color(37, 32, 35)), Color.Red, new Color(211, 186, 44), manualFur: new SSJ4Appearance())
        {
        }
    }
}
