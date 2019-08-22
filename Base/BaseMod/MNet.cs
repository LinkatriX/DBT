using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT
{
	public class MNet
	{
		public static void SendBaseNetMessage(int msg, params object[] param)
		{
			if (Main.netMode == 0) { return; } //nothing to sync in SP
            BaseNet.WriteToPacket(DBTMod.Instance.GetPacket(), (byte)msg, param).Send();
		}
	}
}