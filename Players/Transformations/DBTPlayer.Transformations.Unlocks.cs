using DBT.Transformations;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void UpdateConditions()
        {
            if (!HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ3) && CheckKiRequirement(10000) && HasMastered(TransformationDefinitionManager.Instance.SSJ1) && HasMastered(TransformationDefinitionManager.Instance.SSJ2) && IsTransformed(TransformationDefinitionManager.Instance.SSJ2) && player.statLife <= (player.statLifeMax + player.statLifeMax2) * 0.30f && DBTMod.Instance.transformUpKey.JustPressed)
                AcquireAndTransform(TransformationDefinitionManager.Instance.SSJ3);
        }

        public void UpdateSpecialConditions()
        {
            //SSJ1
            if (!HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ1) && CheckKiRequirement(2500) && CheckFriendshipRequirement(25))
                AcquireAndTransform(TransformationDefinitionManager.Instance.SSJ1);
            //SSJ2
            if (!HasAcquiredTransformation(TransformationDefinitionManager.Instance.SSJ2) && CheckKiRequirement(5000) && CheckFriendshipRequirement(50) && HasMastered(TransformationDefinitionManager.Instance.SSJ1))
                AcquireAndTransform(TransformationDefinitionManager.Instance.SSJ2);
        }

        public bool CheckKiRequirement(int amount)
        {
            if (BaseMaxKi >= amount)
                return true;

            return false;
        }

        public bool CheckFriendshipRequirement(int amount)
        {
            if (AliveTownNPCs.ContainsValue(amount))
            {
                return true;
            }
                

            return false;
        }

        public void DeathTriggers()
        {
            UpdateSpecialConditions();
        }
    }
}
