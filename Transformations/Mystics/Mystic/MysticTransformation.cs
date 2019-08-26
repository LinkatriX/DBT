using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Mystics.Mystic
{
    public sealed class MysticTransformation : TransformationDefinition
    {
        public MysticTransformation(params TransformationDefinition[] parents) : base("Mystic", "Mystic", typeof(MysticTransformationBuff), 1.6f, 1.6f, 3,
            new TransformationDrain(25f / Constants.TICKS_PER_SECOND, 0f), new MysticTransformationAppearance(), 
            new TransformationOverload(0, 0), mastereable: false,
            parents: parents)
        {
        }
    }

    public sealed class MysticTransformationBuff : TransformationBuff
    {
        public MysticTransformationBuff() : base(TransformationDefinitionManager.Instance.Mystic)
        {
        }
    }

    public sealed class MysticTransformationAppearance : TransformationAppearance
    {
        public MysticTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation("Auras/BaseAura", 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1f, 1f, 1f })),
            new HairAppearance(Main.LocalPlayer.hairColor), Main.LocalPlayer.hairColor, Main.LocalPlayer.eyeColor)
        {
        }
    }
}