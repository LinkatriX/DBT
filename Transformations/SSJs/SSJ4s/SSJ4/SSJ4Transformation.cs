using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJs.SSJ4s.SSJ4
{
    public sealed class SSJ4Transformation : TransformationDefinition
    {
        public SSJ4Transformation(params TransformationDefinition[] parents) : base(
            "SSJ4", "Super Saiyan 4", typeof(SSJ4TransformationBuff),
            3.30f, 2.15f, 22,
            new TransformationDrain(170f / 60, 80f / 60), 
            new SSJ4Appearance(),
            new TransformationOverload(0, 0), 
            parents: parents)
        {
        }
    }

    public sealed class SSJ4TransformationBuff : TransformationBuff
    {
        public SSJ4TransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ4)
        {
        }
    }

    public sealed class SSJ4Appearance : TransformationAppearance
    {
        public SSJ4Appearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ4Transformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1.60f, 1.40f, 0f })),
            new HairAppearance(new Color(37, 32, 35)), Color.Red, new Color(211, 186, 44))
        {
        }
    }
}
