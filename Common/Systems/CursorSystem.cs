﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PortalCursor.Common.Configs;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace PortalCursor.Common.Systems
{
	public class CursorSystem : ModSystem
	{
		public const int LEFT_PORTAL = 0;
		public const int RIGHT_PORTAL = 1;

		public static CursorSystem Instance => ModContent.GetInstance<CursorSystem>();

		public override void Load()
		{
			On_Main.DrawCursor += On_Main_DrawCursor;
			On_Main.DrawThickCursor += On_Main_DrawThickCursor;
		}

		private Vector2 On_Main_DrawThickCursor(On_Main.orig_DrawThickCursor orig, bool smart)
		{
			if (ModUtil.IsUsingCustomCursor())
			{
				return Vector2.Zero;
			}
			return orig(smart);
		}

		private void On_Main_DrawCursor(On_Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
		{
			if (ModUtil.IsCursorBeingDrawn())
			{
				if (ModUtil.CanDisplayIndicator())
				{
					DrawIndicators(Main.spriteBatch);
				}
				if (ModUtil.IsUsingCustomCursor())
				{
					DrawPortalCursor();
					return;
				}
			}
			orig(bonus, smart);
		}

		public override void Unload()
		{
			On_Main.DrawCursor -= On_Main_DrawCursor;
			On_Main.DrawThickCursor -= On_Main_DrawThickCursor;
		}

		private static void DrawPortalCursor()
		{
			Texture2D cursor = PortalCursor.CursorTexture.Value;
			Vector2 center = new Vector2(cursor.Width / 2, cursor.Height / 2);
			Main.spriteBatch.Draw(cursor, Main.MouseScreen, cursor.Frame(), PortalCursorConfig.Instance.CursorColor.Color, 0, center, Main.cursorScale, 0, 0);
		}

		private static void DrawIndicators(SpriteBatch spriteBatch)
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
			PortalCursorConfig config = PortalCursorConfig.Instance;
			Vector2 cursorCenter = ModUtil.GetScaledCursorCenter(cursorSize);

			Vector2 leftCenterOffset = new Vector2(config.LeftIndicatorOffset.X, config.LeftIndicatorOffset.Y);
			DrawLeftHalf(spriteBatch, cursorCenter + leftCenterOffset, ModUtil.HasPlayerPlacedPortal(LEFT_PORTAL));

			Vector2 rightCenterOffset = new Vector2(config.RightIndicatorOffset.X, config.RightIndicatorOffset.Y);
			DrawRightHalf(spriteBatch, cursorCenter + rightCenterOffset, ModUtil.HasPlayerPlacedPortal(RIGHT_PORTAL));
		}

		private static void DrawIndicatorAroundCustomCursor(SpriteBatch spriteBatch)
		{
			int customCursorSize = 22;
			int magicOffset = -3;
			PortalCursorConfig config = PortalCursorConfig.Instance;
			Vector2 cursorCenter = ModUtil.GetScaledCursorCenter(customCursorSize);

			Vector2 leftCenterOffset = new Vector2(config.LeftIndicatorOffset.X + magicOffset, config.LeftIndicatorOffset.Y + magicOffset);
			DrawLeftHalf(spriteBatch, cursorCenter + leftCenterOffset, ModUtil.HasPlayerPlacedPortal(LEFT_PORTAL));

			Vector2 rightCenterOffset = new Vector2(config.RightIndicatorOffset.X + magicOffset, config.RightIndicatorOffset.Y + magicOffset);
			DrawRightHalf(spriteBatch, cursorCenter + rightCenterOffset, ModUtil.HasPlayerPlacedPortal(RIGHT_PORTAL));
		}

		private static void DrawLeftHalf(SpriteBatch spriteBatch, Vector2 pos, bool full)
		{
			Asset<Texture2D> leftHalf = full ? PortalCursor.LeftPortalFullTexture : PortalCursor.LeftPortalEmptyTexture;
			spriteBatch.Draw(leftHalf.Value, pos, leftHalf.Frame(), GetIndicatorColor(LEFT_PORTAL), 0, Vector2.Zero, Main.cursorScale, 0, 0);
		}

		private static void DrawRightHalf(SpriteBatch spriteBatch, Vector2 pos, bool full)
		{
			Asset<Texture2D> rightHalf = full ? PortalCursor.RightPortalFullTexture : PortalCursor.RightPortalEmptyTexture;
			spriteBatch.Draw(rightHalf.Value, pos, rightHalf.Frame(), GetIndicatorColor(RIGHT_PORTAL), 0, Vector2.Zero, Main.cursorScale, 0, 0);
		}

		private static Color GetIndicatorColor(int portal)
		{
			PortalCursorConfig config = PortalCursorConfig.Instance;
			Color leftColor = config.UseCustomIndicatorColors ? config.LeftIndicatorColor.Color : PortalHelper.GetPortalColor(Main.myPlayer, LEFT_PORTAL);
			Color rightColor = config.UseCustomIndicatorColors ? config.RightIndicatorColor.Color : PortalHelper.GetPortalColor(Main.myPlayer, RIGHT_PORTAL);
			if (portal == LEFT_PORTAL)
			{
				return config.InvertIndicatorColors ? rightColor : leftColor; 
			}
			else if(portal == RIGHT_PORTAL)
			{
				return config.InvertIndicatorColors ? leftColor : rightColor;
			}
			return Color.White;
		}
	}
}
