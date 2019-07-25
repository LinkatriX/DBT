using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DBT.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DBT.UserInterfaces.KiAttackUI
{
	public class KiSlotButtonDefinition : UIImageButton //requires some accessebility parameter that will be making the attack dark/light
	{
		public KiSlotButtonDefinition(KiBrowserUIMenu.Call methodDelegate, Texture2D texture, 
			string textHover, UIPanel attPanel, bool attackIsUnlocked) : base(Main.magicPixel)
		{
			STexture = texture;

			HoverText = textHover;

			AttachPanel = attPanel;

			MethodCalling = methodDelegate;

			AttackAccessed = attackIsUnlocked;
		}

		public override void OnInitialize() //Only takes care of drawing the BackGround of the button.
		{
			Button = new UIImageButton(Main.magicPixel);
			Button.Width.Set(16, 0f);
			Button.Height.Set(16, 0f);
			Button.Left.Set(AttachPanel.Width.Pixels / 2f + 200, 0f);
			Button.Top.Set(AttachPanel.Height.Pixels / 2f - 240, 0f);
			Button.OnClick += new MouseEvent(MethodCalling.Invoke);
			DrawPos = new Vector2(Button.Left.Pixels / 2f, Button.Top.Pixels / 2);
			AttachPanel.Append(Button);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
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
			spriteBatch.Draw(STexture, DrawPos, source, AttackAccessed ? new Color(105, 105, 105) : default);
		}


		public virtual string GetHoverText(DBTPlayer DbtPlayer) => HoverText;

		private string HoverText { get; }
		private UIPanel AttachPanel { get; }
		private UIImageButton Button { get; set; }//Keep as a pannel since the button requires a texture, but we need to draw it manually.
		private Texture2D STexture { get; set; }
		private bool AttackAccessed { get; set; }

		Color FillBlack = new Color(105, 105, 105);

		Vector2 DrawPos;

		KiBrowserUIMenu.Call MethodCalling;
	}
}
