using Microsoft.Xna.Framework;
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
			On.Terraria.Main.DrawCursor += Main_DrawCursor;
			On.Terraria.Main.DrawThickCursor += Main_DrawThickCursor;
		}

		public override void Unload()
		{
			On.Terraria.Main.DrawCursor -= Main_DrawCursor;
			On.Terraria.Main.DrawThickCursor -= Main_DrawThickCursor;
		}

		private void Main_DrawCursor(On.Terraria.Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
		{
			Draw(Main.spriteBatch);
			if (ModUtil.IsUsingCustomCursor())
			{
				Texture2D cursor = PortalCursor.CursorTexture.Value;
				Vector2 center = new Vector2(cursor.Width / 2, cursor.Height / 2);
				Main.spriteBatch.Draw(cursor, Main.MouseScreen, cursor.Frame(), PortalCursorConfig.Instance.CursorColor.Color, 0, center, Main.cursorScale, 0, 0);
				return;
			}
			orig(bonus, smart);
		}

		private Vector2 Main_DrawThickCursor(On.Terraria.Main.orig_DrawThickCursor orig, bool smart)
		{
			if (ModUtil.IsUsingCustomCursor())
			{
				return Vector2.Zero;
			}
			return orig(smart);
		}

		private static void Draw(SpriteBatch spriteBatch)
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
			Vector2 cursorCenter = ModUtil.GetScaledCursorCenter(cursorSize);

			Vector2 leftCenterOffset = new Vector2(-3, -8);
			Vector2 leftPos = cursorCenter;
			leftPos += leftCenterOffset;
			leftPos -= offset;
			DrawLeftHalf(spriteBatch, leftPos, ModUtil.HasPlayerPlacedPortal(LEFT_PORTAL));

			Vector2 rightCenterOffset = new Vector2(8, 6);
			Vector2 rightPos = cursorCenter;
			rightPos += rightCenterOffset;
			rightPos += offset;
			DrawRightHalf(spriteBatch, rightPos, ModUtil.HasPlayerPlacedPortal(RIGHT_PORTAL));
		}

		private static void DrawIndicatorAroundCustomCursor(SpriteBatch spriteBatch)
		{
			int customCursorSize = 22;
			Vector2 offset = new Vector2(PortalCursorConfig.Instance.IndicatorOffset);
			Vector2 cursorCenter = ModUtil.GetScaledCursorCenter(customCursorSize);

			Vector2 leftCenterOffset = new Vector2(-6, -11);
			Vector2 leftPos = cursorCenter;
			leftPos += leftCenterOffset;
			leftPos -= offset;
			DrawLeftHalf(spriteBatch, leftPos, ModUtil.HasPlayerPlacedPortal(LEFT_PORTAL));


			Vector2 rightCenterOffset = new Vector2(5, 3);
			Vector2 rightPos = cursorCenter;
			rightPos += rightCenterOffset;
			rightPos += offset;
			DrawRightHalf(spriteBatch, rightPos, ModUtil.HasPlayerPlacedPortal(RIGHT_PORTAL));
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
