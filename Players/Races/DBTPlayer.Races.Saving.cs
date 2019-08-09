using DBT.Races;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveRace(TagCompound tag)
        {
            if (Race == null)
                Race = RaceDefinitionManager.Instance.Terrarian;

            tag.Add(nameof(Race), Race.UnlocalizedName);
        }

        private void LoadRace(TagCompound tag)
        {
            if (!tag.ContainsKey(nameof(Race)))
                Race = RaceDefinitionManager.Instance.Terrarian;
            else
                Race = RaceDefinitionManager.Instance[tag.GetString(nameof(Race))];
        }
    }
}
