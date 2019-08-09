using System.Collections.Generic;
using DBT.Transformations;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveMastery(TagCompound tag)
        {
            foreach (PlayerTransformation playerTransformation in AcquiredTransformations.Values)
                tag.Add(playerTransformation.ToSavableFormat());
        }

        private void LoadMastery(TagCompound tag)
        {
            foreach (KeyValuePair<string, object> kvp in tag)
                LoadMasteryEntry(kvp);
        }

        internal void LoadMasteryEntry(KeyValuePair<string, object> kvp)
        {
            if (kvp.Key.StartsWith(PlayerTransformation.MASTERY_PREFIX))
            {
                TransformationDefinition transformation = TransformationDefinitionManager.Instance[kvp.Key.Substring(PlayerTransformation.MASTERY_PREFIX.Length)];

                if (AcquiredTransformations.ContainsKey(transformation)) return;
                AcquiredTransformations.Add(transformation, new PlayerTransformation(transformation, float.Parse(kvp.Value.ToString())));
            }
        }
    }
}
