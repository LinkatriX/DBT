using DBT.Transformations;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public override TagCompound Save()
        {
            ClearTransformations();

            TagCompound tag = new TagCompound();

            tag.Add(nameof(PlayerInitialized), PlayerInitialized);

            SaveRace(tag);
            SaveMastery(tag);
            SaveTransformations(tag);
            SaveKi(tag);
            SaveOverload(tag);
            SaveGuardian(tag);

            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerSaving(this, tag));
            ForAllAcquiredTransformations(t => t.Definition.OnPlayerSaving(this, tag));

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            PlayerInitialized = tag.GetBool(nameof(PlayerInitialized));

            LoadRace(tag);
            LoadMastery(tag);
            LoadTransformations(tag);
            LoadKi(tag);
            LoadKi(tag);
            LoadGuardian(tag);

            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerLoading(this, tag));
            ForAllAcquiredTransformations(t => t.Definition.OnPlayerLoading(this, tag));
        }
    }
}
