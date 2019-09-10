using DBT.Auras;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Kaiokens.Kaioken2x
{
    public sealed class Kaioken2xTransformation : TransformationDefinition
    {
        public Kaioken2xTransformation(params TransformationDefinition[] parents) : base("Kaioken2x", "Kaioken 2x", typeof(Kaioken2xTransformationBuff),
            1.05f, 1.025f, 1, new TransformationDrain(0f, 0f), new Kaioken2xTransformationAppearance(), new TransformationOverload(0, 0), 
            displaysInMenu: false, parents: parents)
        {
        }
    }

    public sealed class Kaioken2xTransformationBuff : TransformationBuff
    {
        public Kaioken2xTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken2x)
        {
        }
    }

    public sealed class Kaioken2xTransformationAppearance : TransformationAppearance
    {
        public Kaioken2xTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation("Auras/KaiokenAura", 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 0.35f, 0, 0 })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.Red, Main.LocalPlayer.eyeColor)
        {
        }
    }
}
