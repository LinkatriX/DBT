using DBT.HairStyles;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveHair(TagCompound tag)
        {
            if (ChosenHairStyle == null)
                ChosenHairStyle = HairStyleManager.Instance.NoChoice;

            tag.Add(nameof(HairChecked), HairChecked);
            tag.Add(nameof(ChosenHairStyle), ChosenHairStyle.UnlocalizedName);
        }

        private void LoadHair(TagCompound tag)
        {
            if (!tag.ContainsKey(nameof(ChosenHairStyle)))
                ChosenHairStyle = HairStyleManager.Instance.NoChoice;
            else
                ChosenHairStyle = HairStyleManager.Instance[tag.GetString(nameof(ChosenHairStyle))];

            HairChecked = tag.GetBool(nameof(HairChecked));
        }
    }
}
