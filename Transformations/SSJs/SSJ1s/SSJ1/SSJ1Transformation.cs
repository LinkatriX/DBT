using DBT.Auras;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.SSJs.SSJ1s.SSJ1
{
    public sealed class SSJ1Transformation : TransformationDefinition
    {
        public SSJ1Transformation(params TransformationDefinition[] parents) : base(
            "SSJ1", "Super Saiyan", typeof(SSJ1TransformationBuff),
            1.5f, 1.25f, 2, 
            new TransformationDrain(60f / Constants.TICKS_PER_SECOND, 30f / Constants.TICKS_PER_SECOND),
            new SSJ1Appearance(),
            /* limitedToRaces: new RaceDefinition[] { RaceDefinitionManager.Instance.Saiyan }, */ parents: parents)
        {
            TabHoverText = "Normal Super Saiyan Transformations";
        }

        public override float GetMaxMastery(DBTPlayer dbtPlayer)
        {
            if (!dbtPlayer.AcquiredTransformations.ContainsKey(TransformationDefinitionManager.Instance.SSJG) ||
                dbtPlayer.AcquiredTransformations[TransformationDefinitionManager.Instance.SSJG].CurrentMastery < 1f)
                return BaseMaxMastery;

            return 2f;
        }

        public override void OnPlayerMasteryChanged(DBTPlayer dbtPlayer, float change, float currentMastery)
        {
            if (currentMastery >= 0.5f && !dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.ASSJ1))
                dbtPlayer.Acquire(TransformationDefinitionManager.Instance.ASSJ1);

            if (currentMastery >= 0.75f && !dbtPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.USSJ1))
                dbtPlayer.Acquire(TransformationDefinitionManager.Instance.USSJ1);
        }
    }

    public sealed class SSJ1TransformationBuff : TransformationBuff
    {
        public SSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ1)
        {
        }
    }

    public sealed class SSJ1Appearance : TransformationAppearance
    {
        public SSJ1Appearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ1Transformation), 4, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1.25f, 1.25f, 0f })), 
            new HairAppearance(new Color(255, 206, 20)), Color.Yellow, Color.Turquoise)
        {
        }
    }
}
