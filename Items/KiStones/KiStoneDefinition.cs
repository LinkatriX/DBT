using DBT.Commons;
using DBT.Dynamicity;
using System;

namespace DBT.Items.KiStones
{
    public sealed class KiStoneDefinition : IHasUnlocalizedName, IHasParents<KiStoneDefinition>
    {
        public KiStoneDefinition(float requiredKi, Type itemType, params KiStoneDefinition[] parents)
        {
            RequiredKi = requiredKi;
            ItemType = itemType;
            Parents = parents;
        }

        public string UnlocalizedName => ItemType.FullName;

        public float RequiredKi { get; }

        public Type ItemType { get; }

        public KiStoneDefinition[] Parents { get; }
    }
}