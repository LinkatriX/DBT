using DBT.Players;
using DBT.Transformations.SSJs.SSJ1s.SSJ1;

namespace DBT.Transformations.SSJs.SSJ1s.USSJ1
{
    public sealed class USSJ1Transformation : TransformationDefinition
    {
        public USSJ1Transformation(params TransformationDefinition[] parents) : base(
            "USSJ1", "Ultra Super Saiyan", typeof(USSJ1TransformationBuff),
            1.8f, 1.075f, 5, 
            new TransformationDrain(90f / Constants.TICKS_PER_SECOND, 45f / Constants.TICKS_PER_SECOND), 
            new SSJ1Appearance(),
            isManualLookup: true,
            manualHairLookup: "SSJ1",
            displaysInMenu: false, parents: parents)
        {
        }

        public override void OnPlayerMasteryChanged(DBTPlayer dbtPlayer, float change, float currentMastery)
        {
            dbtPlayer.ChangeMastery(TransformationDefinitionManager.Instance.SSJ1, change);
        }
    }

    public sealed class USSJ1TransformationBuff : TransformationBuff
    {
        public USSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.USSJ1)
        {
        }
    }
}
