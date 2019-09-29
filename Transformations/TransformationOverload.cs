namespace DBT.Transformations
{
    public struct TransformationOverload
    {
        public readonly float
            baseOverloadGrowthRate,
            masteredOverloadGrowthRate;


        public TransformationOverload(float baseOverloadGrowthRate, float masteredOverloadGrowthRate)
        {
            this.baseOverloadGrowthRate = baseOverloadGrowthRate;
            this.masteredOverloadGrowthRate = masteredOverloadGrowthRate;
        }


        public static TransformationOverload Zero { get; } = new TransformationOverload(0, 0);
    }
}