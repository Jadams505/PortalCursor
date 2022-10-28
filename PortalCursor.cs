using Terraria.ModLoader;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace PortalCursor
{
	public class PortalCursor : Mod
	{
		public static PortalCursor Instance => ModContent.GetInstance<PortalCursor>();

		private static Asset<Texture2D> _cursorTexture;
		public static Asset<Texture2D> CursorTexture => ModUtil.RequestTexture(ref _cursorTexture, "Portal_Cursor", AssetRequestMode.AsyncLoad);

		private static Asset<Texture2D> _leftPortalFullTexture;
		public static Asset<Texture2D> LeftPortalFullTexture => ModUtil.RequestTexture(ref _leftPortalFullTexture, "Portal_Left_Half_Full", AssetRequestMode.AsyncLoad);

		private static Asset<Texture2D> _rightPortalFullTexture;
		public static Asset<Texture2D> RightPortalFullTexture => ModUtil.RequestTexture(ref _rightPortalFullTexture, "Portal_Right_Half_Full", AssetRequestMode.AsyncLoad);

		private static Asset<Texture2D> _leftPortalEmptyTexture;
		public static Asset<Texture2D> LeftPortalEmptyTexture => ModUtil.RequestTexture(ref _leftPortalEmptyTexture, "Portal_Left_Half_Empty", AssetRequestMode.AsyncLoad);
		
		private static Asset<Texture2D> _rightPortalEmptyTexture;
		public static Asset<Texture2D> RightPortalEmptyTexture => ModUtil.RequestTexture(ref _rightPortalEmptyTexture, "Portal_Right_Half_Empty", AssetRequestMode.AsyncLoad);

		public override void Unload()
		{
			_cursorTexture = null;
			_leftPortalFullTexture = null;
			_rightPortalFullTexture = null;
			_leftPortalEmptyTexture = null;
			_rightPortalEmptyTexture = null;
		}
	}
}