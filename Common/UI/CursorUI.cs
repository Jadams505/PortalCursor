using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using ReLogic.Content;
using PortalCursor.Common.Configs;
using Terraria.GameContent;
using System.Reflection;

namespace PortalCursor.Common.UI
{
	public class CursorUI : UIState
	{
		public const int LEFT_PORTAL = 0;
		public const int RIGHT_PORTAL = 1;

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (ModUtil.CanDisplayIndicator())
			{
				if (ModUtil.IsUsingCustomCursor())
				{
					DrawIndicatorAroundCustomCursor(spriteBatch);
				}
				else
				{
					DrawIndicatorAroundVanillaCursor(spriteBatch);
				}
			}
		}

		private static void DrawIndicatorAroundVanillaCursor(SpriteBatch spriteBatch)
		{
			int cursorSize = 14;
			Vector2 offset = new Vector2(PortalCursorConfig.Instance.IndicatorOffset);
			Vector2 cursorCenter = GetScaledCursorCenter(cursorSize);

			Vector2 leftCenterOffset = new Vector2(-3, -8);
			Vector2 leftPos = cursorCenter;
			leftPos += leftCenterOffset;
			leftPos -= offset;
			DrawLeftHalf(spriteBatch, leftPos, HasPlayerPlacedPortal(LEFT_PORTAL));

			Vector2 rightCenterOffset = new Vector2(8, 6);
			Vector2 rightPos = cursorCenter;
			rightPos += rightCenterOffset;
			rightPos += offset;
			DrawRightHalf(spriteBatch, rightPos, HasPlayerPlacedPortal(RIGHT_PORTAL));
		}

		private static void DrawIndicatorAroundCustomCursor(SpriteBatch spriteBatch)
		{
			int customCursorSize = 22;
			Vector2 offset = new Vector2(PortalCursorConfig.Instance.IndicatorOffset);
			Vector2 cursorCenter = GetScaledCursorCenter(customCursorSize);

			Vector2 leftCenterOffset = new Vector2(-6, -11);
			Vector2 leftPos = cursorCenter;
			leftPos += leftCenterOffset;
			leftPos -= offset;
			DrawLeftHalf(spriteBatch, leftPos, HasPlayerPlacedPortal(LEFT_PORTAL));
			

			Vector2 rightCenterOffset = new Vector2(5, 3);
			Vector2 rightPos = cursorCenter;
			rightPos += rightCenterOffset;
			rightPos += offset;
			DrawRightHalf(spriteBatch, rightPos, HasPlayerPlacedPortal(RIGHT_PORTAL));			
		}

		private static void DrawLeftHalf(SpriteBatch spriteBatch, Vector2 pos, bool full)
		{
			Asset<Texture2D> leftHalf = full ? PortalCursor.LeftPortalFullTexture : PortalCursor.LeftPortalEmptyTexture;
			spriteBatch.Draw(leftHalf.Value, pos, leftHalf.Frame(), PortalHelper.GetPortalColor(Main.myPlayer, LEFT_PORTAL), 0, Vector2.Zero, Main.cursorScale, 0, 0);
		}

		private static void DrawRightHalf(SpriteBatch spriteBatch, Vector2 pos, bool full)
		{
			Asset<Texture2D> rightHalf = full ? PortalCursor.RightPortalFullTexture : PortalCursor.RightPortalEmptyTexture;
			spriteBatch.Draw(rightHalf.Value, pos, rightHalf.Frame(), PortalHelper.GetPortalColor(Main.myPlayer, RIGHT_PORTAL), 0, Vector2.Zero, Main.cursorScale, 0, 0);
		}

		private static Vector2 GetScaledCursorCenter(int cursorSize)
		{
			return new Vector2(Main.mouseX - cursorSize * Main.cursorScale / 2, Main.mouseY - cursorSize * Main.cursorScale / 2);
		}

		private static Vector2 GetCursorCenter(int cursorSize)
		{
			return new Vector2(Main.mouseX - cursorSize / 2, Main.mouseY - cursorSize / 2);
		}

		private static bool HasPlayerPlacedPortal(int portal)
		{
			int[,] lookup = typeof(PortalHelper).GetField("FoundPortals", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null) as int[,];
			if(lookup != null)
			{
				return lookup[Main.myPlayer, portal] != -1;
			}
			return false;
		}
	}
}
