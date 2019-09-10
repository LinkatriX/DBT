using DBT.Players;
using WebmilioCommons.Managers;

namespace DBT.Traits
{
    public abstract class TraitDefinition : IHasUnlocalizedName
    {
        protected TraitDefinition(string unlocalizedName)
        {
            UnlocalizedName = unlocalizedName;
        }

        public virtual void OnPlayerMasteryGained(DBTPlayer dbtPlayer, ref float gain, float currentMastery) { }

        public string UnlocalizedName { get; }
    }
}
