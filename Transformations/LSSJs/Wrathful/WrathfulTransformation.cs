using DBT.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBT.Transformations.LSSJs.Wrathful
{
    public partial class WrathfulTransformation : TransformationDefinition
    {
        public readonly float _damageMulti, _speedMulti, _kiDrain;
        public readonly int _defense;

        public WrathfulTransformation(float damageMulti, float speedMulti, float kiDrain, int defense, params TransformationDefinition[] parents)
            : base("Wrathful", "Wrathful", typeof(WrathfulTransformationBuff),
            damageMulti, speedMulti, defense,
            new TransformationDrain(kiDrain, kiDrain),
            new WrathfulTransformationAppearance(),
            new TransformationOverload(0, 0),
            parents: parents)
        {
            _damageMulti = damageMulti;
            _speedMulti = speedMulti;
            _defense = defense;
            _kiDrain = kiDrain;
        }
    }

    public sealed class WrathfulTransformationBuff : TransformationBuff
    {
        public WrathfulTransformationBuff() : base(TransformationDefinitionManager.Instance.Wrathful)
        {
        }
    }

    public sealed class WrathfulTransformationAppearance : TransformationAppearance
    {
        public WrathfulTransformationAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(WrathfulTransformation), 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 0f, 1.25f, 0f })),
            new HairAppearance(Color.White), Main.LocalPlayer.hairColor, new Color(211, 186, 44))
        {
        }
    }

    public class WrathfulTransformationAct1 : WrathfulTransformation
    {
        public WrathfulTransformationAct1() : base(1.5f, 1.5f, 1f, 3)
        {
        }
    }

    public class WrathfulTransformationAct2 : WrathfulTransformation
    {
        public WrathfulTransformationAct2() : base(2.25f, 2.25f, 120f / Constants.TICKS_PER_SECOND, 10)
        {
        }
    }

    public class WrathfulTransformationAct3 : WrathfulTransformation
    {
        public WrathfulTransformationAct3() : base(2.9f, 2.9f, 160f / Constants.TICKS_PER_SECOND, 20)
        {
        }
    }
}