using DBT.Players;
using Terraria;

namespace DBT.Transformations
{
    public abstract class TransformationOverload
    {
        protected TransformationOverload(float baseOverloadGrowthRate)
        {
            BaseOverloadGrowthRate = baseOverloadGrowthRate;
        }

        /// <summary></summary>
        /// <param name="dbtPlayer"></param>
        /// <param name="overload"></param>
        /// <param name="maxOverload"></param>
        /// <returns>Should the player be kicked out of the transformation.</returns>
        public virtual bool OnPlayerOverloadUpdated(DBTPlayer dbtPlayer, float overload, float maxOverload)
        {
            Main.NewText("Overload is being updated");
            return true;
        }

        public virtual float GetOverloadGrowthRate() => BaseOverloadGrowthRate;


        public virtual float BaseOverloadGrowthRate { get; } = 0;
    }
}