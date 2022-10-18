﻿using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using ReLogic.Content;
using PortalCursor.Common.Configs;

namespace PortalCursor.Common.UI
{
	public class CursorUI : UIState
	{
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Main.LocalPlayer.HeldItem.type == ItemID.PortalGun)
			{
				if (PortalCursorConfig.Instance.UsePortalCursor)
				{
					DrawAroundCustomCursor(spriteBatch);
				}
				else
				{
					DrawAroundVanillaCursor(spriteBatch);
				}
			}
		}

		private static void DrawAroundVanillaCursor(SpriteBatch spriteBatch)
		{
			int cursorSize = 14;
			Vector2 offset = new Vector2(PortalCursorConfig.Instance.IndicatorOffset);
			Vector2 cursorCenter = GetScaledCursorCenter(cursorSize);

			Vector2 leftCenterOffset = new Vector2(-3, -8);
			Vector2 leftPos = cursorCenter;
			leftPos += leftCenterOffset;
			leftPos -= offset;
			DrawLeftHalf(spriteBatch, leftPos);

			Vector2 rightCenterOffset = new Vector2(8, 6);
			Vector2 rightPos = cursorCenter;
			rightPos += rightCenterOffset;
			rightPos += offset;
			DrawRightHalf(spriteBatch, rightPos);
		}

		private static void DrawAroundCustomCursor(SpriteBatch spriteBatch)
		{
			int customCursorSize = 22;
			Vector2 offset = new Vector2(PortalCursorConfig.Instance.IndicatorOffset);
			Vector2 cursorCenter = GetScaledCursorCenter(customCursorSize);

			Vector2 leftCenterOffset = new Vector2(-6, -11);
			Vector2 leftPos = cursorCenter;
			leftPos += leftCenterOffset;
			leftPos -= offset;
			DrawLeftHalf(spriteBatch, leftPos);

			Vector2 rightCenterOffset = new Vector2(5, 3);
			Vector2 rightPos = cursorCenter;
			rightPos += rightCenterOffset;
			rightPos += offset;
			DrawRightHalf(spriteBatch, rightPos);
		}

		private static void DrawLeftHalf(SpriteBatch spriteBatch, Vector2 pos)
		{
			Asset<Texture2D> leftHalf = PortalCursor.LeftPortalTexture;
			spriteBatch.Draw(leftHalf.Value, pos, leftHalf.Frame(), Color.Orange, 0, Vector2.Zero, Main.cursorScale, 0, 0);
		}

		private static void DrawRightHalf(SpriteBatch spriteBatch, Vector2 pos)
		{
			Asset<Texture2D> rightHalf = PortalCursor.RightPortalTexture;
			spriteBatch.Draw(rightHalf.Value, pos, rightHalf.Frame(), Color.SkyBlue, 0, Vector2.Zero, Main.cursorScale, 0, 0);
		}

		private static Vector2 GetScaledCursorCenter(int cursorSize)
		{
			return new Vector2(Main.mouseX - cursorSize * Main.cursorScale / 2, Main.mouseY - cursorSize * Main.cursorScale / 2);
		}

		private static Vector2 GetCursorCenter(int cursorSize)
		{
			return new Vector2(Main.mouseX - cursorSize / 2, Main.mouseY - cursorSize / 2);
		}
	}
}