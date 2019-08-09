using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.LSSJs.LSSJW
{
    public sealed class LSSJWTransformation : TransformationDefinition
    {
        public LSSJWTransformation(params TransformationDefinition[] parents) : base(
            "LSSJW", "Wrathful", typeof(LSSJWTransformationBuff),
            1.5f, 1.5f, 2,
            new TransformationDrain(1f, 1f),
            new LSSJWTransformationAppearance(),
            new TransformationOverload(0, 0),
            parents: parents)
        {
        }
    }

    public sealed class LSSJWTransformationBuff : TransformationBuff
    {
        public LSSJWTransformationBuff() : base(TransformationDefinitionManager.Instance.LSSJW)
        {
        }
    }

    public sealed class LSSJWTransformationAppearance : TransformationAppearance
    {
        public LSSJWTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(LSSJWTransformation), 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { })),
            new HairAppearance(Color.White), Main.LocalPlayer.hairColor)
        {
        }
    }
}
