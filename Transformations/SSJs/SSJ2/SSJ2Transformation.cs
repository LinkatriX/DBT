using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJs.SSJ2
{
    public sealed class SSJ2Transformation : TransformationDefinition
    {
        public SSJ2Transformation(params TransformationDefinition[] parents) : base(
            "SSJ2", "Super Saiyan 2", typeof(SSJ2TransformationBuff),
            2.2f, 1.6f, 8, 
            new TransformationDrain(120f / Constants.TICKS_PER_SECOND, 60f / Constants.TICKS_PER_SECOND), 
            new SSJ2Appearance(),
            parents: parents)
        {
        }
    }

    public sealed class SSJ2TransformationBuff : TransformationBuff
    {
        public SSJ2TransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ2)
        {
        }
    }

    public sealed class SSJ2Appearance : TransformationAppearance
    {
        public SSJ2Appearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ2Transformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1.32f, 1.32f, 0f })),
            new HairAppearance(new Color(255, 255, 74)), Color.Yellow, Color.Turquoise)
        {
        }
    }
}
