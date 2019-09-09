using DBT.Transformations;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void ChangeMastery(float change)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                ChangeMastery(ActiveTransformations[i], change);
        }

        public void ChangeMastery(TransformationDefinition definition, float change)
        {
            PlayerTransformation playerTransformation = AcquiredTransformations[definition];
            float maxMastery = playerTransformation.Definition.GetMaxMastery(this);

            if (playerTransformation.CurrentMastery >= maxMastery)
                return;

            if (playerTransformation.CurrentMastery + change > maxMastery)
                change = maxMastery - playerTransformation.CurrentMastery;

            if (Trait != null)
                Trait.OnPlayerMasteryGained(this, ref change, playerTransformation.CurrentMastery);

            playerTransformation.ChangeMastery(this, change);
            definition.OnPlayerMasteryChanged(this, change, playerTransformation.CurrentMastery);
        }
    }
}
