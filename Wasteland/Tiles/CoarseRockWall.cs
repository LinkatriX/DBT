using DBT.Wasteland.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Wasteland.Tiles
{
	public class CoarseRockWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = false;

            drop = mod.ItemType<CoarseRockWallItem>();
            AddMapEntry(new Color(204, 130, 85));
		}
	}
}