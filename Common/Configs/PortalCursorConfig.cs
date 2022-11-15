using Terraria;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Reflection.Emit;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System;

namespace PortalCursor.Common.Configs
{
	public enum DisplayType
	{
		[Label("Only When Using Portals")]
		OnlyWhenUsingPortals,
		[Label("Always")]
		Always,
		[Label("Never")]
		Never
	}

	[Label("Portal Cursor Config")]
	public class PortalCursorConfig : ModConfig
	{
		public static PortalCursorConfig Instance => ModContent.GetInstance<PortalCursorConfig>();

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("Main Settings")]

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

		[Label("Left Indicator Offset")]
		[Tooltip("How far from the center of the cursor to draw the left portal indicator")]
		[SeparatePage]
		public Point LeftIndicatorOffset = new Point(-3, -8);

		[Label("Right Indicator Offset")]
		[Tooltip("How far from the center of the cursor to draw the right portal indicator")]
		[SeparatePage]
		public Point RightIndicatorOffset = new Point(8, 6);

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
		[Tooltip("Whether or not to use your custom indicator colors")]
		public bool UseCustomIndicatorColors;

		[Label("Left Indicator Color")]
		[Tooltip("The color to make the left portal indicator (does not change your actual portal colors)")]
		[SeparatePage]
		public ColorWrapper LeftIndicatorColor = new ColorWrapper(Color.White);

		[Label("Right Indicator Color")]
		[Tooltip("The color to make the right portal indicator (does not change your actual portal colors)")]
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

		public class Point
		{
			[Range(-999, 999)]
			public int X;
			[Range(-999, 999)]
			public int Y;

			public Point()
			{
				X = 0;
				Y = 0;
			}

			public Point(int x, int y)
			{
				X = x;
				Y = y;
			}

			public override bool Equals(object obj)
			{
				if(obj is Point point)
				{
					return X == point.X && Y == point.Y;
				}
				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(X, Y);
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
