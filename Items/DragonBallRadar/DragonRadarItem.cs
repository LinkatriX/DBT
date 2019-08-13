using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.DragonBallRadar
{
    public abstract class DragonRadarItem : DBTItem
    {
        protected DragonRadarItem(string displayName, string tooltip, int width = 20, int height = 20, int maxStack = 1,
            int value = 0, int rarity = ItemRarityID.Expert) : base(displayName, tooltip, width, height, maxStack, value, rarity)
        {
        }
    }
}
