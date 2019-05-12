﻿using DBT.Auras;
using DBT.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.Patreon.SSJ5
{
    public sealed class SSJ5Transformation : TransformationDefinition
    {
        public SSJ5Transformation(params TransformationDefinition[] parents) : base(
            "SSJ5", "Super Saiyan 5", typeof(SSJ5TransformationBuff),
            5f, 5f, 40,
            new TransformationDrain(260f / 60, 130f / 60),
            new SSJ5Appearance(), parents: parents)
        {
        }

        public override bool CheckPrePlayerConditions() => SteamHelper.CanUserAccess(true, 
            76561193979609866, // Skipping#7613 - 450018452103757835
            76561193783431419); // Megawarrior_101#0616 - 405844470584836117
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
                new LightingAppearance(new float[] { 1.60f, 1.40f, 0f })),
            new HairAppearance(Color.White), Color.Red)
        {
        }
    }
}