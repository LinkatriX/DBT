﻿using DBT.Commons;
using DBT.Dynamicity;

namespace DBT.Skills
{
    public class SkillDefinition : IHasUnlocalizedName, IHasParents<SkillDefinition>
    {
        protected SkillDefinition(string unlocalizedName, string displayName, string description, SkillCharacteristics characteristics, params SkillDefinition[] parents)
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

        public SkillCharacteristics Characteristics { get; }

        public SkillDefinition[] Parents { get; }
    }
}