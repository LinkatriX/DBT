using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveGuardian(TagCompound tag)
        {
            tag.Add(nameof(BaseHealingBonus), BaseHealingBonus);
        }

        private void LoadGuardian(TagCompound tag)
        {
            BaseHealingBonus = tag.GetInt(nameof(BaseHealingBonus));
        }
    }
}
