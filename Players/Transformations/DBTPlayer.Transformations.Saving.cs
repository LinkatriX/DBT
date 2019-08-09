using System.Collections.Generic;
using DBT.Transformations;
using DBT.UserInterfaces.CharacterMenus;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void InitializeTransformations()
        {
            AcquiredTransformations = new Dictionary<TransformationDefinition, PlayerTransformation>();
            ActiveTransformations = new List<TransformationDefinition>();
            SelectedTransformations = new List<TransformationDefinition>();
        }

        private void SaveTransformations(TagCompound tag)
        {
            string[] transformationNames = new string[SelectedTransformations.Count];

            for (int i = 0; i < SelectedTransformations.Count; i++)
                transformationNames[i] = SelectedTransformations[i].UnlocalizedName;

            tag.Add(nameof(SelectedTransformations), string.Join(",", transformationNames));

            if (CharacterTransformationsMenu.LastActiveTransformationTab != null)
                tag.Add(nameof(CharacterTransformationsMenu.LastActiveTransformationTab), CharacterTransformationsMenu.LastActiveTransformationTab.UnlocalizedName);
        }

        private void LoadTransformations(TagCompound tag)
        {
            string[] transformationNames = tag.GetString(nameof(SelectedTransformations)).Split(',');

            for (int i = 0; i < transformationNames.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(transformationNames[i])) continue;
                SelectedTransformations.Add(TransformationDefinitionManager.Instance[transformationNames[i]]);
            }

            if (tag.ContainsKey(nameof(CharacterTransformationsMenu.LastActiveTransformationTab)))
                CharacterTransformationsMenu.LastActiveTransformationTab = TransformationDefinitionManager.Instance[tag.GetString(nameof(CharacterTransformationsMenu.LastActiveTransformationTab))];
        }
    }
}