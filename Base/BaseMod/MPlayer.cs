using System;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT
{
    public class MPlayer : ModPlayer
    {
		public static bool useItem = false;

		public override void SetControls() 
		{
			if(useItem)
			{
				useItem = false;
				player.delayUseItem = false;
				player.controlUseItem = true;
			}
		}
    }
}