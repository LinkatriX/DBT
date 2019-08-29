using DBT.Players;
using DBT.Transformations.SSJs.SSJ1s.SSJ1;

namespace DBT.Transformations.SSJs.SSJ1s.ASSJ1
{
    public sealed class ASSJ1Transformation : TransformationDefinition
    {
        public ASSJ1Transformation(params TransformationDefinition[] parents) : base(
            "ASSJ1", "Ascended Super Saiyan", typeof(ASSJ1TransformationBuff),
            1.75f, 1.125f, 3, 
            new TransformationDrain(70f / 60, 35f / 60), 
            new SSJ1Appearance(),
            new TransformationOverload(0, 0),
            displaysInMenu: false, parents: parents)
        {
        }

        public override void OnPlayerMasteryGain(DBTPlayer dbtPlayer, float gain, float currentMastery)
        {
            dbtPlayer.GainMastery(TransformationDefinitionManager.Instance.SSJ1, gain);
        }
    }

    public sealed class ASSJ1TransformationBuff : TransformationBuff
    {
        public ASSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.ASSJ1)
        {
        }
    }
}
