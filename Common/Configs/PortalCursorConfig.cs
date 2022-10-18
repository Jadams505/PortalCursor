using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PortalCursor.Common.Configs
{
	public class PortalCursorConfig : ModConfig
	{
		public static PortalCursorConfig Instance => ModContent.GetInstance<PortalCursorConfig>();

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[DefaultValue(true)]
		[Label("Use Portal Cursor")]
		[Tooltip("Enable this to use the custom portal crosshair")]
		public bool UsePortalCursor;

		[DefaultValue(0)]
		[Label("Indicator Offset")]
		[Tooltip("How far from the center of the cursor to draw the portal indicator")]
		public int IndicatorOffset;
	}
}
