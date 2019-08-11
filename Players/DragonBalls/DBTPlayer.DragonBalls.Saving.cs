using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void SaveWishes(TagCompound tag)
        {
            tag.Add(nameof(PowerWishesLeft), PowerWishesLeft);
            tag.Add(nameof(ImmortalityWishesLeft), ImmortalityWishesLeft);
            tag.Add(nameof(SkillWishesLeft), SkillWishesLeft);
            tag.Add(nameof(AwakeningWishesLeft), AwakeningWishesLeft);
            tag.Add(nameof(ImmortalityRevivesLeft), ImmortalityRevivesLeft);
        }

        private void LoadWishes(TagCompound tag)
        {
            PowerWishesLeft = tag.GetInt(nameof(PowerWishesLeft));
            ImmortalityWishesLeft = tag.GetInt(nameof(ImmortalityWishesLeft));
            SkillWishesLeft = tag.GetInt(nameof(SkillWishesLeft));
            AwakeningWishesLeft = tag.GetInt(nameof(AwakeningWishesLeft));
            ImmortalityRevivesLeft = tag.GetInt(nameof(ImmortalityRevivesLeft));
        }
    }
}
