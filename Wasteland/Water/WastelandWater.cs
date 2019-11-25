using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Wasteland.Water
{
	public class WastelandWater : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == mod.GetSurfaceBgStyleSlot("WastelandSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("WastelandWaterfall");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("WastelandWaterSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Wasteland/Water/WastelandWaterDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 0.40f;
			g = 0.20f;
			b = 0.43f;
		}

		public override Color BiomeHairColor()
		{
            return new Color(188, 151, 3);
		}
	}
}