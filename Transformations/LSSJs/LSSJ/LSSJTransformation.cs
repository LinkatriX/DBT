using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.LSSJs.LSSJ
{
    public sealed class LSSJTransformation : TransformationDefinition
    {
        public LSSJTransformation(params TransformationDefinition[] parents) : base(
            "LSSJ", "Legendary Super Saiyan", typeof(LSSJTransformationBuff),
            4.9f, 2.80f, 36, 
            new TransformationDrain(340f / 60, 170f / 60),
            new LSSJTransformationAppearance(),
            new TransformationOverload(0.1f, 0.05f), // 0.2f, 0.1f
            parents: parents)
        {
        }
    }

    public sealed class LSSJTransformationBuff : TransformationBuff
    {
        public LSSJTransformationBuff() : base(TransformationDefinitionManager.Instance.LSSJ)
        {
        }
    }

    public sealed class LSSJTransformationAppearance : TransformationAppearance
    {
        public LSSJTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(LSSJTransformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
            new HairAppearance(new Color(161, 253, 70)), Color.Lime, new Color(103, 219, 50))
        {
        }
    }
}