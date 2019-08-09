using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class CyberneticParts : DBTMaterial
    {
        public CyberneticParts() : base("Cybernetic Parts", "Exquisite frieza force technology used to augment or repair the body.",
            42, 42, Item.buyPrice(silver: 3), ItemRarityID.Orange)
        {
        }
    }
}