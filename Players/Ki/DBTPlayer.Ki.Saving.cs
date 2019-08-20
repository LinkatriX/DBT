using Terraria;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveKi(TagCompound tag)
        {
            tag.Add(nameof(Ki), Ki);
            tag.Add(nameof(BaseMaxKi), BaseMaxKi);
            tag.Add(nameof(MaxKiModifierPerm), MaxKiModifierPerm);
            tag.Add(nameof(BossesKilled), BossesKilled);
        }

        private void LoadKi(TagCompound tag)
        {
            Ki = tag.GetFloat(nameof(Ki));
            BaseMaxKi = tag.GetFloat(nameof(BaseMaxKi));
            MaxKiModifierPerm = tag.GetFloat(nameof(MaxKiModifierPerm));
            BossesKilled = tag.GetList<int>(nameof(BossesKilled));
        }

        private bool HasKi(float kiAmount)
        {
            return Ki >= kiAmount;
        }
    }
}
