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
    }
}