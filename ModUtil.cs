using Microsoft.Xna.Framework.Graphics;
using PortalCursor.Common.Configs;
using ReLogic.Content;
using Terraria;
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
			return Main.LocalPlayer.HeldItem.type == ItemID.PortalGun && !Main.gameMenu && !Main.InGameUI.IsVisible && !Main.ingameOptionsWindow && !Main.hideUI && !Main.mapFullscreen;
		}

		public static Asset<Texture2D> RequestTexture(ref Asset<Texture2D> asset, string path, AssetRequestMode mode)
		{
			asset ??= ModContent.Request<Texture2D>(PortalCursor.Instance.Name + "/Assets/" + path, mode);
			return asset;
		}
	}
}
