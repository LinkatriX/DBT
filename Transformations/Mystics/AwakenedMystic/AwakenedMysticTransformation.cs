using DBT.Auras;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Mystics.AwakenedMystic
{
    public sealed class AwakenedMysticTransformation : TransformationDefinition
    {
        public AwakenedMysticTransformation(params TransformationDefinition[] parents) : base(
            "AwakenedMystic", "Awakened Mystic", typeof(AwakenedMysticTransformationBuff),
            4.2f, 2.6f, 28, new TransformationDrain(80f / Constants.TICKS_PER_SECOND), new AwakenedMysticTransformationAppearance(),
            mastereable: false, isManualLookup: true, manualHairLookup: "Wrathful", parents: parents)
        {
        }
    }

    public sealed class AwakenedMysticTransformationBuff : TransformationBuff
    {
        public AwakenedMysticTransformationBuff() : base(TransformationDefinitionManager.Instance.AwakenedMystic)
        {
        }
    }

    public sealed class AwakenedMysticTransformationAppearance : TransformationAppearance
    {
        public AwakenedMysticTransformationAppearance() : base(new AuraAppearance(new AuraAnimationInformation(typeof(AwakenedMysticTransformation), 8, 3, BlendState.Additive, true),
                new LightingAppearance(new float[] { 1f, 1f, 1f })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.LightBlue, null)
        {
        }
    }
}