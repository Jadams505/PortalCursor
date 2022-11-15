using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PortalCursor.Common.Configs;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace PortalCursor
{
	public static class ModUtil
	{
		public static bool IsUsingCustomCursor()
		{
			PortalCursorConfig config = PortalCursorConfig.Instance;
			return config.UsePortalCursor == DisplayType.Always || config.UsePortalCursor == DisplayType.OnlyWhenUsingPortals && IsUsingPortals();
		}

		public static bool CanDisplayIndicator()
		{
			PortalCursorConfig config = PortalCursorConfig.Instance;
			return config.UsePortalIndicator == DisplayType.Always || (IsUsingPortals() && config.UsePortalIndicator == DisplayType.OnlyWhenUsingPortals);
		}

		public static bool IsUsingPortals()
		{
			return (Main.LocalPlayer.HeldItem.type == ItemID.PortalGun || IsHoveringOverPortalStation()) && !Main.gameMenu && !Main.InGameUI.IsVisible && !Main.ingameOptionsWindow && !Main.hideUI && !Main.mapFullscreen && !Main.inFancyUI;
		}

		public static bool IsHoveringOverPortalStation()
		{
			Point pos = GetMousePos();
			Tile tile = Framing.GetTileSafely(pos);

			return tile.TileType == TileID.Cannon && tile.TileFrameX >= 216;
		}

		public static bool IsCursorBeingDrawn()
		{
			return !((Main.gameMenu && Main.alreadyGrabbingSunOrMoon) 
				|| (PlayerInput.SettingsForUI.ShowGamepadCursor && ((Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && !Main.gameMenu)
				|| PlayerInput.InvisibleGamepadInMenus)));
		}

		public static Point GetMousePos()
		{
			int targetX = Player.tileTargetX;
			int targetY = Player.tileTargetY;
			if (Main.SmartInteractShowingGenuine)
			{
				targetX = Main.SmartInteractX;
				targetY = Main.SmartInteractY;
				return new Point(targetX, targetY);
			}
			if (Main.SmartCursorShowing)
			{
				targetX = Main.SmartCursorX;
				targetY = Main.SmartCursorY;
				return new Point(targetX, targetY);
			}
			return new Point(targetX, targetY);
		}

		public static bool InBounds(int targetX, int targetY)
		{
			if (targetX < (Main.screenPosition.X - 16) / 16) // left
			{
				return false;
			}
			if (16 * targetX > PlayerInput.RealScreenWidth + Main.screenPosition.X) // right
			{
				return false;
			}
			if (targetY < (Main.screenPosition.Y - 16) / 16) // top
			{
				return false;
			}
			if (16 * targetY > PlayerInput.RealScreenHeight + Main.screenPosition.Y) // bottom
			{
				return false;
			}
			return true;
		}

		public static Vector2 GetScaledCursorCenter(int cursorSize)
		{
			return new Vector2(Main.mouseX - cursorSize * Main.cursorScale / 2, Main.mouseY - cursorSize * Main.cursorScale / 2);
		}

		public static Vector2 GetCursorCenter(int cursorSize)
		{
			return new Vector2(Main.mouseX - cursorSize / 2, Main.mouseY - cursorSize / 2);
		}

		public static bool HasPlayerPlacedPortal(int portal)
		{
			int[,] lookup = typeof(PortalHelper).GetField("FoundPortals", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null) as int[,];
			if (lookup != null)
			{
				return lookup[Main.myPlayer, portal] != -1;
			}
			return false;
		}

		public static Asset<Texture2D> RequestTexture(ref Asset<Texture2D> asset, string path, AssetRequestMode mode)
		{
			asset ??= ModContent.Request<Texture2D>(PortalCursor.Instance.Name + "/Assets/" + path, mode);
			return asset;
		}
	}
}
