using DBT.Players;
using DBT.Transformations;
using Microsoft.Xna.Framework;
using Terraria;

namespace DBT.HairStyles.Broly
{
    public sealed class BrolyHairStyle : HairStyle
    {
        public BrolyHairStyle() : base(xOffsetRight: -2, yOffsetRight: 2, xOffsetLeft: 2, yOffsetLeft: 2)
        {
        }

        /*public override int GetXOffsetRight(DBTPlayer dbtPlayer) => dbtPlayer.ActiveTransformations[0] == TransformationDefinitionManager.Instance.SSJ3 ? 
            XOffsetRight = 110 : XOffsetRight;

        /*public override int GetYOffsetRight(DBTPlayer dbtPlayer) => dbtPlayer.ActiveTransformations[0] == TransformationDefinitionManager.Instance.SSJ3 ?
            YOffsetRight = 110 : YOffsetRight;

        public override int GetXOffsetLeft(DBTPlayer dbtPlayer) => dbtPlayer.ActiveTransformations[0] == TransformationDefinitionManager.Instance.SSJ3 ?
            XOffsetLeft = 110 : XOffsetLeft;

        public override int GetYOffsetLeft(DBTPlayer dbtPlayer) => dbtPlayer.ActiveTransformations[0] == TransformationDefinitionManager.Instance.SSJ3 ?
            YOffsetLeft = 110 : YOffsetLeft;*/
    }
}
