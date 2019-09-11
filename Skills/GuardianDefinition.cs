using DBT.Dynamicity;
using WebmilioCommons.Managers;

namespace DBT.Skills
{
    public class GuardianDefinition : IHasUnlocalizedName, IHasParents<GuardianDefinition>
    {
        protected GuardianDefinition(string unlocalizedName, string displayName, string description, GuardianCharacteristics characteristics, params GuardianDefinition[] parents)
        {
            UnlocalizedName = unlocalizedName;

            DisplayName = displayName;
            Description = description;

            Characteristics = characteristics;

            Parents = parents;
        }

        public string UnlocalizedName { get; }

        public string DisplayName { get; }
        public string Description { get; }

        public GuardianCharacteristics Characteristics { get; }

        public GuardianDefinition[] Parents { get; }
    }
}