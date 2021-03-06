﻿using DBT.Dynamicity;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WebmilioCommons.Extensions;
using WebmilioCommons.Managers;

namespace DBT.Skills
{
    public class SkillDefinition : IHasUnlocalizedName, IHasParents<SkillDefinition>
    {
        protected SkillDefinition(string unlocalizedName, string displayName, string description, Type item, SkillCharacteristics characteristics, Vector2 menuPosition = default, string unlockDescription = null, params SkillDefinition[] parents)
        {
            UnlocalizedName = unlocalizedName;

            DisplayName = displayName;
            Description = description;
            UnlockDescription = unlockDescription;

            Item = item;

            Characteristics = characteristics;
            MenuPosition = menuPosition;
            Parents = parents;
        }

        public bool DoesDisplayInTechniqueMenu(DBTPlayer dbtPlayer) => CheckPrePlayerConditions();

        public virtual bool CheckPrePlayerConditions() => true;

        public const string DEFAULT_BEAM_INSTRUCTIONS = "Hold Right-Click to Charge\nLeft-Click to Fire";

        public string UnlocalizedName { get; }

        public string DisplayName { get; }
        public string Description { get; }
        public string UnlockDescription { get; }
        public Type Item { get; }
        public virtual Texture2D SkillIcon => Item.GetTexture();

        public SkillCharacteristics Characteristics { get; }
        public Vector2 MenuPosition { get; }
        public SkillDefinition[] Parents { get; }
    }
}