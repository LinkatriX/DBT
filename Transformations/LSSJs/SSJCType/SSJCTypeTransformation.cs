using DBT.Auras;
using DBT.Players;
using DBT.Transformations.LSSJs.LSSJ;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.Transformations.LSSJs.SSJCType
{
    public sealed class SSJCTypeTransformation : TransformationDefinition
    {
        public SSJCTypeTransformation(params TransformationDefinition[] parents) : base(
            "SSJCType", "Super Saiyan C-Type", typeof(SSJCTypeTransformationBuff),
            3.9f, 3.9f, 26, 
            new TransformationDrain(4, 2),
            new LSSJTransformationAppearance(), parents: parents)
        {
        }

        public sealed class SSJCTypeTransformationBuff : TransformationBuff
        {
            public SSJCTypeTransformationBuff() : base(TransformationDefinitionManager.Instance.LSSJ)
            {
            }
        }

        public sealed class SSJCTypeTransformationAppearance : TransformationAppearance
        {
            public SSJCTypeTransformationAppearance() : base(
                new AuraAppearance(new AuraAnimationInformation(typeof(SSJCTypeTransformation), 4, 3, BlendState.Additive, 1f, true),
                    new LightingAppearance(new float[] { })),
                new HairAppearance(Color.White), Color.Lime)
            {
            }
        }

        public sealed class SSJCTypeTransformationOverload : TransformationOverload
        {
            public SSJCTypeTransformationOverload() : base(10f / Constants.TICKS_PER_SECOND)
            {
            }

            public override bool OnPlayerOverloadUpdated(DBTPlayer dbtPlayer, float overload, float maxOverload)
            {
                if (overload >= maxOverload * 0.70)
                {

                }

                return true;
            }
        }
    }
}