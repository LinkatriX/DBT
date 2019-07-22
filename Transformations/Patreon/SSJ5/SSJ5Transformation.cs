using DBT.Auras;
using DBT.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Patreon.SSJ5
{
    public sealed class SSJ5Transformation : TransformationDefinition
    {
        public SSJ5Transformation(params TransformationDefinition[] parents) : base(
            "SSJ5", "Super Saiyan 5", typeof(SSJ5TransformationBuff),
            5.2f, 5.2f, 40,
            new TransformationDrain(260f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND),
            new SSJ5Appearance(), parents: parents)
        {
        }

        public override bool CheckPrePlayerConditions() => SteamHelper.CanAccess(SteamHelper.Skipping, SteamHelper.Megawarrior101);
    }

    public sealed class SSJ5TransformationBuff : TransformationBuff
    {
        public SSJ5TransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ5)
        {
        }
    }

    public sealed class SSJ5Appearance : TransformationAppearance
    {
        public SSJ5Appearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ5Transformation), 8, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1.475f, 1.475f, 1.475f })),
            new HairAppearance(Color.White), Color.Red)
        {
        }
    }
}
