using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using PortalCursor.Common.Configs;

namespace PortalCursor
{
	public class PortalCursor : Mod
	{
		public static PortalCursor Instance => ModContent.GetInstance<PortalCursor>();

		public static readonly Asset<Texture2D> CursorTexture = RequestTexture("Portal_Cursor", AssetRequestMode.AsyncLoad);
		public static readonly Asset<Texture2D> LeftPortalFullTexture = RequestTexture("Portal_Left_Half_Full", AssetRequestMode.AsyncLoad);
		public static readonly Asset<Texture2D> RightPortalFullTexture = RequestTexture("Portal_Right_Half_Full", AssetRequestMode.AsyncLoad);
		public static readonly Asset<Texture2D> LeftPortalEmptyTexture = RequestTexture("Portal_Left_Half_Empty", AssetRequestMode.AsyncLoad);
		public static readonly Asset<Texture2D> RightPortalEmptyTexture = RequestTexture("Portal_Right_Half_Empty", AssetRequestMode.AsyncLoad);

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

		private void Main_DrawCursor(On.Terraria.Main.orig_DrawCursor orig, Microsoft.Xna.Framework.Vector2 bonus, bool smart)
		{
			if (Main.LocalPlayer?.HeldItem?.type == ItemID.PortalGun && PortalCursorConfig.Instance.UsePortalCursor)
			{
				Texture2D cursor = CursorTexture.Value;
				Vector2 center = new Vector2(cursor.Width / 2, cursor.Height / 2);
				Main.spriteBatch.Draw(cursor, Main.MouseScreen, cursor.Frame(), Main.mouseColor, 0, center, Main.cursorScale, 0, 0);
			}
			else
			{
				orig(bonus, smart);
			}
		}

		private Vector2 Main_DrawThickCursor(On.Terraria.Main.orig_DrawThickCursor orig, bool smart)
		{
			if (Main.LocalPlayer?.HeldItem?.type == ItemID.PortalGun && PortalCursorConfig.Instance.UsePortalCursor)
			{
				return Vector2.Zero;
			}
			else
			{
				return orig(smart);
			}
		}

		public static Asset<Texture2D> RequestTexture(string path, AssetRequestMode mode)
		{
			return ModContent.Request<Texture2D>(Instance.Name + "/Assets/" + path, mode);
		}
	}
}