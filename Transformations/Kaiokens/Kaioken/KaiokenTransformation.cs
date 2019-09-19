/*using DBT.Auras;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.Kaiokens.Kaioken
{
    public abstract class KaiokenTransformation : TransformationDefinition
    {
        public KaiokenTransformation(params TransformationDefinition[] parents) : base("Kaioken", "Kaioken", typeof(KaiokenTransformationBuff),
            1.05f, 1.025f, 1, new TransformationDrain(0f, 0f), new KaiokenTransformationAppearance(), new TransformationOverload(0, 0), 
            displaysInMenu: false, parents: parents)
        {

        }
    }

    public sealed class KaiokenTransformationBuff : TransformationBuff
    {
        public KaiokenTransformationBuff() : base(TransformationDefinitionManager.Instance.Kaioken)
        {
        }
    }

    public sealed class KaiokenTransformationAppearance : TransformationAppearance
    {
        public KaiokenTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation("Auras/KaiokenAura", 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 0.35f, 0, 0 })),
            new HairAppearance(Main.LocalPlayer.hairColor), Color.Red, Main.LocalPlayer.eyeColor)
        {
        }
    }

    public int kaiokenLevel { get; private set; } = 2;
}

public float GetAuraScale(MyPlayer modPlayer)
{
    // universal scale handling
    // scale is based on kaioken level, which gets set to 0
    var baseScale = 1.0f;

    // special scaling for Kaioken auras only
    if (isKaiokenAura)
    {
        return baseScale * (0.9f + 0.1f * modPlayer.kaiokenLevel);
    }
    else
    {
        return baseScale;
    }
}*/
