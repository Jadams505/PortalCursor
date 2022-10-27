using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Reflection.Emit;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PortalCursor.Common.Configs
{
	public enum DisplayType
	{
		OnlyWhenUsingPortals,
		Always,
		Never
	}

	public class PortalCursorConfig : ModConfig
	{
		public static PortalCursorConfig Instance => ModContent.GetInstance<PortalCursorConfig>();

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[DrawTicks]
		[DefaultValue(DisplayType.OnlyWhenUsingPortals)]
		[Label("Use Portal Cursor")]
		[Tooltip("Enables when to use the custom portal crosshair")]
		public DisplayType UsePortalCursor;

		[DrawTicks]
		[DefaultValue(DisplayType.OnlyWhenUsingPortals)]
		[Label("Use Portal Indicator")]
		[Tooltip("Enables when to use the portal indicators")]
		public DisplayType UsePortalIndicator;

		[DefaultValue(0)]
		[Label("Indicator Offset")]
		[Tooltip("How far from the center of the cursor to draw the portal indicator")]
		public int IndicatorOffset;
	}
}
