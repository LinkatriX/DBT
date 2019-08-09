using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DBT.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DBT.UserInterfaces.KiAttackUI
{
	public class KiSlotButtonDefinition : UIImageButton //requires some accessebility parameter that will be making the attack text dark/light
	{
		public KiSlotButtonDefinition(Texture2D texture, 
			string textHover, bool attackIsUnlocked) : base(Main.magicPixel)
		{
			STexture = texture;

			HoverText = textHover;

			AttackAccessed = attackIsUnlocked;
		}

		/*protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			if (IsMouseHovering)
			{
				 DBTPlayer DbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

				if (DbtPlayer == null)
					Main.hoverItemName = HoverText;
				else
					Main.hoverItemName = GetHoverText(DbtPlayer);
			}

			Rectangle source = new Rectangle(0,0, STexture.Width, STexture.Height);
			spriteBatch.Draw(STexture, DrawPos, source, AttackAccessed ? default : new Color(105, 105, 105));
		}*/


		public virtual string GetHoverText(DBTPlayer DbtPlayer) => HoverText;

		private string HoverText { get; }
		private Texture2D STexture { get; set; }
		private bool AttackAccessed { get; set; }

		Vector2 DrawPos;
	}
}
