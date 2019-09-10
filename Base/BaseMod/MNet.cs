using Terraria;

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