using DBT.Auras;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Mystics.AwakenedMystic
{
    public sealed class AwakenedMysticTransformation : TransformationDefinition
    {
        public AwakenedMysticTransformation(params TransformationDefinition[] parents) : base(
            "AwakenedMystic", "Awakened Mystic", typeof(AwakenedMysticTransformationBuff),
            4.2f, 2.1f, 28, new TransformationDrain(80f / Constants.TICKS_PER_SECOND, 0f), new AwakenedMysticTransformationAppearance(),
            new TransformationOverload(0f, 0f), mastereable: false, parents: parents)
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
        public AwakenedMysticTransformationAppearance() : base(new AuraAppearance(new AuraAnimationInformation(typeof(AwakenedMysticTransformation), 8, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1f, 1f, 1f })),
            new HairAppearance(Main.LocalPlayer.hairColor), Main.LocalPlayer.hairColor, Main.LocalPlayer.eyeColor)
        {
        }
    }
}