using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using ReLogic.Content;
using Terraria.ModLoader;

namespace PortalCursor.Common.UI
{
	public class CursorUI : UIState
	{
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Main.LocalPlayer.HeldItem.type == ItemID.PortalGun)
			{
				int cursorSize = 14;
				int offset = 8;
				Vector2 mouseCenter = new Vector2((Main.mouseX + cursorSize * Main.cursorScale / 2), Main.mouseY + cursorSize * Main.cursorScale / 2);

				Asset<Texture2D> leftHalf = ModContent.Request<Texture2D>("PortalCursor/Assets/Portal_Left_Half", AssetRequestMode.ImmediateLoad);
				Vector2 leftCenterOffset = new Vector2(11, 15);
				Vector2 leftPos = mouseCenter -= leftCenterOffset + new Vector2(offset);
				spriteBatch.Draw(leftHalf.Value, leftPos, leftHalf.Frame(), Color.Orange, 0, Vector2.Zero, Main.cursorScale, 0, 0);

				Asset<Texture2D> rightHalf = ModContent.Request<Texture2D>("PortalCursor/Assets/Portal_Right_Half", AssetRequestMode.ImmediateLoad);
				Vector2 rightCenterOffset = new Vector2(0, 0);
				Vector2 rightPos = mouseCenter -= rightCenterOffset - new Vector2(offset * 2);
				spriteBatch.Draw(rightHalf.Value, rightPos, rightHalf.Frame(), Color.SkyBlue, 0, Vector2.Zero, Main.cursorScale, 0, 0);
			}
		}
	}
}
