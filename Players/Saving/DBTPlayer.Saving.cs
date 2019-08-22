using DBT.Transformations;
using Microsoft.Xna.Framework;
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
            SaveNPCs(tag);
            SaveHair(tag);
            SaveWishes(tag);

            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerSaving(this, tag));
            ForAllAcquiredTransformations(t => t.Definition.OnPlayerSaving(this, tag));

            // added to store the player's original eye color if possible
            if (originalEyeColor != null)
            {
                tag.Add("OriginalEyeColorR", originalEyeColor.Value.R);
                tag.Add("OriginalEyeColorG", originalEyeColor.Value.G);
                tag.Add("OriginalEyeColorB", originalEyeColor.Value.B);
            }

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
            LoadNPCs(tag);
            LoadHair(tag);
            LoadWishes(tag);

            // load the player's original eye color if possible
            if (tag.ContainsKey("OriginalEyeColorR") && tag.ContainsKey("OriginalEyeColorG") && tag.ContainsKey("OriginalEyeColorB"))
            {
                originalEyeColor = new Color(tag.Get<byte>("OriginalEyeColorR"), tag.Get<byte>("OriginalEyeColorG"), tag.Get<byte>("OriginalEyeColorB"));
            }

            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerLoading(this, tag));
            ForAllAcquiredTransformations(t => t.Definition.OnPlayerLoading(this, tag));
        }
    }
}
