using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.LSSJs.Wrathful
{
    public sealed class WrathfulTransformation : TransformationDefinition
    {
        public WrathfulTransformation(params TransformationDefinition[] parents) : base(
            "Wrathful", "Wrathful", typeof(WrathfulTransformationBuff),
            1.5f, 1.5f, 2,
            new TransformationDrain(1f, 1f),
            new WrathfulTransformationAppearance(),
            new TransformationOverload(0, 0),
            parents: parents)
        {
        }
    }

    public sealed class WrathfulTransformationBuff : TransformationBuff
    {
        public WrathfulTransformationBuff() : base(TransformationDefinitionManager.Instance.Wrathful)
        {
        }
    }

    public sealed class WrathfulTransformationAppearance : TransformationAppearance
    {
        public WrathfulTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(WrathfulTransformation), 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
            new HairAppearance(Color.White), Main.LocalPlayer.hairColor)
        {
        }
    }
}