using Microsoft.Xna.Framework;
using WebmilioCommons.Managers;

namespace DBT.Races
{
    public abstract class RaceDefinition : IHasUnlocalizedName
    {
        protected RaceDefinition(string unlocalizedName, Color associatedColor)
        {
            UnlocalizedName = unlocalizedName;

            AssociatedColor = associatedColor;
        }
        
        public string UnlocalizedName { get; }

        public Color AssociatedColor { get; }
    }
}