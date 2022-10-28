using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace PortalCursor.Common.Systems
{
	public class CursorSystem : ModSystem
	{
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

		private void Main_DrawCursor(On.Terraria.Main.orig_DrawCursor orig, Microsoft.Xna.Framework.Vector2 bonus, bool smart)
		{
			if (ModUtil.IsUsingCustomCursor())
			{
				Texture2D cursor = PortalCursor.CursorTexture.Value;
				Vector2 center = new Vector2(cursor.Width / 2, cursor.Height / 2);
				Main.spriteBatch.Draw(cursor, Main.MouseScreen, cursor.Frame(), Main.mouseColor, 0, center, Main.cursorScale, 0, 0);
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
	}
}
