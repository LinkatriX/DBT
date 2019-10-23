using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Patreon.LSSJG
{
    public sealed class LSSGTransformation : TransformationDefinition
    {
        public LSSGTransformation(params TransformationDefinition[] parents) : base(
                "LSSG", "Legendary Super Saiyan God", typeof(LSSGTransformationBuff),
                5.2f, 3.1f, 45,
                new TransformationDrain(260f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND),
                new LSSGAppearance(),
                new TransformationOverload(0, 0), parents: parents)
        {
        }

        public override bool CheckPrePlayerConditions() => false; //SteamHelper.CanAccess(SteamHelper.Skipping, SteamHelper.Megawarrior101);
    }

    public sealed class LSSGTransformationBuff : TransformationBuff
    {
        public LSSGTransformationBuff() : base(TransformationDefinitionManager.Instance.LSSG)
        {
        }
    }

    public sealed class LSSGAppearance : TransformationAppearance
    {
        public LSSGAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(LSSGTransformation), 8, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
            new HairAppearance(new Color(161, 253, 70)), Color.Lime, new Color(103, 219, 50))
        {
        }
    }
}
