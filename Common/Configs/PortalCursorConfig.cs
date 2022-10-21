using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Reflection.Emit;
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

		[DefaultValue(true)]
		[Label("Use Portal Indicator")]
		[Tooltip("Enable this to use the portal indicators")]
		public bool UsePortalIndicator;

		[DefaultValue(true)]
		[Label("Only Enable When Using Portals")]
		[Tooltip("With this enabled the above setting only apply when hovering over the portal station or holding the portal gun")]
		public bool OnlyEnableWhenUsingPortals;

		[DefaultValue(0)]
		[Label("Indicator Offset")]
		[Tooltip("How far from the center of the cursor to draw the portal indicator")]
		public int IndicatorOffset;
	}
}
