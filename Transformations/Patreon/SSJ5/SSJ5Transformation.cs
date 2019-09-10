using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Patreon.SSJ5
{
    public sealed class SSJ5Transformation : TransformationDefinition
    {
        public SSJ5Transformation(params TransformationDefinition[] parents) : base(
            "SSJ5", "Super Saiyan 5", typeof(SSJ5TransformationBuff),
            5.2f, 3.1f, 45,
            new TransformationDrain(260f / Constants.TICKS_PER_SECOND, 100f / Constants.TICKS_PER_SECOND),
            new SSJ5Appearance(),
            new TransformationOverload(0, 0), parents: parents)
        {
        }

        public override bool CheckPrePlayerConditions() => false; //SteamHelper.CanAccess(SteamHelper.Skipping, SteamHelper.Megawarrior101);
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
                new LightingAppearance(new float[] { 2.11f, 1.86f, 0.44f })),//1.475f, 1.475f, 1.475f white
            new HairAppearance(Color.White), Color.Yellow, new Color(211, 186, 44))
        {
        }
    }
}
