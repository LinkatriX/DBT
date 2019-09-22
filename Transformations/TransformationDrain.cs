namespace DBT.Transformations
{
    public struct TransformationDrain
    {
        public readonly float
            baseUnmasteredKiDrain,
            baseMasteredKiDrain,
            baseUnmasteredHealthDrain,
            baseMasteredHealthDrain,

            maxTransformationKiDrainMultiplier,
            kiMultiplierPerStep,

            maxTransformationHealthDrainMultiplier,
            healthMultiplierPerStep;

        public readonly int transformationStepDelay;

        public TransformationDrain(float baseUnmasteredKiDrain, float baseMasteredKiDrain = 0f, float baseUnmasteredHealthDrain = 0f, float baseMasteredHealthDrain = 0f, 
            int transformationStepDelay = 300, float maxTransformationKiDrainMultiplier = 3f, float kiMultiplierPerStep = 0.5f, float maxTransformationHealthDrainMultiplier = 3f, float healthMultiplierPerStep = 0)
        {
            this.baseUnmasteredKiDrain = baseUnmasteredKiDrain;
            this.baseMasteredKiDrain = baseMasteredKiDrain;
            this.baseUnmasteredHealthDrain = baseUnmasteredHealthDrain;
            this.baseMasteredHealthDrain = baseMasteredHealthDrain;

            this.transformationStepDelay = transformationStepDelay;
            this.maxTransformationKiDrainMultiplier = maxTransformationKiDrainMultiplier;
            this.kiMultiplierPerStep = kiMultiplierPerStep;

            this.maxTransformationHealthDrainMultiplier = maxTransformationHealthDrainMultiplier;
            this.healthMultiplierPerStep = healthMultiplierPerStep;
        }
    }
}