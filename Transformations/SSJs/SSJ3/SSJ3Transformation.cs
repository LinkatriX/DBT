using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJs.SSJ3
{
    public sealed class SSJ3Transformation : TransformationDefinition
    {
        public SSJ3Transformation(params TransformationDefinition[] parents) : base(
            "SSJ3", "Super Saiyan 3", typeof(SSJ3TransformationBuff),
            2.7f, 1.85f, 16, 
            new TransformationDrain(160f / Constants.TICKS_PER_SECOND, 80f / Constants.TICKS_PER_SECOND), 
            new SSJ3Appearance(),
            parents: parents)
        {
        }
    }

    public sealed class SSJ3TransformationBuff : TransformationBuff
    {
        public SSJ3TransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ3)
        {
        }
    }

    public sealed class SSJ3Appearance : TransformationAppearance
    {
        public SSJ3Appearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ3Transformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1.50f, 1.50f, 0f })),
            new HairAppearance(Color.Gold), Color.Yellow, Color.Turquoise)
        {
        }
    }
}
