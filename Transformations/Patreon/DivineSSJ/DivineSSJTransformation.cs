using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Patreon.DivineSSJ
{
    public sealed class DivineSSJTransformation : TransformationDefinition
    {
        public DivineSSJTransformation(params TransformationDefinition[] parents) : base(
                "DivineSSJ", "Divine Super Saiyan", typeof(DivineSSJTransformationBuff),
                5.2f, 3.1f, 45,
                new TransformationDrain(260f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND),
                new DivineSSJAppearance(),
                new TransformationOverload(0, 0), parents: parents)
        {
        }

        public override bool CheckPrePlayerConditions() => false; //SteamHelper.CanAccess(SteamHelper.Skipping, SteamHelper.Megawarrior101);
    }

    public sealed class DivineSSJTransformationBuff : TransformationBuff
    {
        public DivineSSJTransformationBuff() : base(TransformationDefinitionManager.Instance.DivineSSJ)
        {
        }
    }

    public sealed class DivineSSJAppearance : TransformationAppearance
    {
        public DivineSSJAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(DivineSSJTransformation), 8, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1.41f, 0f, 0.49f })),
            new HairAppearance(Color.White), new Color(141, 0, 49), Color.White)
        {
        }
    }
}
