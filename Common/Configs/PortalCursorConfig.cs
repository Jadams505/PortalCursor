using Terraria;
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
		[Range(-99, 99)]
		[Label("Indicator Offset")]
		[Tooltip("How far from the center of the cursor to draw the portal indicator")]
		public int IndicatorOffset;

		[Header("Color Settings")]

		[Label("Cursor Color")]
		[Tooltip("Changes what color to make the custom portal crosshair")]
		[SeparatePage]
		public ColorWrapper CursorColor = new ColorWrapper(Main.mouseColor);

		[DefaultValue(true)]
		[Label("Sync Cursor Color")]
		[Tooltip("Enable this to make the custom portal crosshair the same color as your main cursor")]
		public bool SyncCursorColor;

		[DefaultValue(false)]
		[Label("Invert Indicator Colors")]
		[Tooltip("Switches the colors of the portal indicators")]
		public bool InvertIndicatorColors;

		[DefaultValue(false)]
		[Label("Use Custom Indicator Colors")]
		[Tooltip("Whether or not to use the colors specified in the config")]
		public bool UseCustomIndicatorColors;

		[Label("Left Indicator Color")]
		[Tooltip("The color to make the left portal indicator")]
		[SeparatePage]
		public ColorWrapper LeftIndicatorColor = new ColorWrapper(Color.White);

		[Label("Right Indicator Color")]
		[Tooltip("The color to make the right portal indicator")]
		[SeparatePage]
		public ColorWrapper RightIndicatorColor = new ColorWrapper(Color.White);

		public class ColorWrapper
		{
			[Label("Color")]
			public Color Color;

			public ColorWrapper(byte r, byte g, byte b, byte a)
			{
				Color = new Color(r, g, b, a);
			}

			public ColorWrapper(Color color)
			{
				this.Color = color;
			}

			public ColorWrapper()
			{
				Color = new Color(0, 0, 0, 0);
			}

			public override bool Equals(object obj)
			{
				if (obj is Color other)
				{
					return Color.Equals(other);
				}
				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return Color.GetHashCode();
			}
		}

		public override void OnChanged()
		{
			if (SyncCursorColor)
			{
				CursorColor.Color = Main.mouseColor;
			}
		}
	}
}
