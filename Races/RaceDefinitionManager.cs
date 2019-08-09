using DBT.Managers;

namespace DBT.Races
{
    public sealed class RaceDefinitionManager : SingletonManager<RaceDefinitionManager, RaceDefinition>
    {
        internal override void DefaultInitialize()
        {
            Terrarian = Add(new TerrarianRace()) as TerrarianRace;
            Saiyan = Add(new SaiyanRace()) as SaiyanRace;

            base.DefaultInitialize();
        }

        public TerrarianRace Terrarian { get; internal set; }
        public SaiyanRace Saiyan { get; internal set; }
    }
}