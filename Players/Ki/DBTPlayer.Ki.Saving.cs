using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveKi(TagCompound tag)
        {
            tag.Add(nameof(Ki), Ki);
            tag.Add(nameof(BaseMaxKi), BaseMaxKi);
        }

        private void LoadKi(TagCompound tag)
        {
            Ki = tag.GetFloat(nameof(Ki));
            BaseMaxKi = tag.GetFloat(nameof(BaseMaxKi));
        }
    }
}
