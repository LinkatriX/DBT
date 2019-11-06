using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Patreon.DemonSSJ
{
    public sealed class DemonSSJTransformation : TransformationDefinition
    {
        public DemonSSJTransformation(params TransformationDefinition[] parents) : base(
                "DemonSSJ", "Demon Super Saiyan", typeof(DemonSSJTransformationBuff),
                5.2f, 3.1f, 45,
                new TransformationDrain(260f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND),
                new DemonSSJAppearance(),
                new TransformationOverload(0, 0), parents: parents)
        {
        }

        public override bool CheckPrePlayerConditions() => false; //SteamHelper.CanAccess(SteamHelper.Skipping, SteamHelper.Megawarrior101);
    }

    public sealed class DemonSSJTransformationBuff : TransformationBuff
    {
        public DemonSSJTransformationBuff() : base(TransformationDefinitionManager.Instance.DemonSSJ)
        {
        }
    }

    public sealed class DemonSSJAppearance : TransformationAppearance
    {
        public DemonSSJAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(DemonSSJTransformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 0.14f, 0.24f, 0.93f })),
            new HairAppearance(Color.Purple), Color.Purple, Color.MediumPurple)
        {
        }
    }
}