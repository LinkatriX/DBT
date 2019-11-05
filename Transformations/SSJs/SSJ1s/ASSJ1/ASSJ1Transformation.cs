﻿using DBT.Players;
using DBT.Transformations.SSJs.SSJ1s.SSJ1;

namespace DBT.Transformations.SSJs.SSJ1s.ASSJ1
{
    public sealed class ASSJ1Transformation : TransformationDefinition
    {
        public ASSJ1Transformation(params TransformationDefinition[] parents) : base(
            "ASSJ1", "Ascended Super Saiyan", typeof(ASSJ1TransformationBuff),
            1.6f, 1.3f, 3, 
            new TransformationDrain(70f / Constants.TICKS_PER_SECOND, 35f / Constants.TICKS_PER_SECOND), 
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

    public sealed class ASSJ1TransformationBuff : TransformationBuff
    {
        public ASSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.ASSJ1)
        {
        }
    }
}
