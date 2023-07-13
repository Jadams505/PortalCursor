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
		OnlyWhenUsingPortals,
		Always,
		Never
	}

	public class PortalCursorConfig : ModConfig
	{
		public static PortalCursorConfig Instance => ModContent.GetInstance<PortalCursorConfig>();

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("MainSettings")]

		[DrawTicks]
		[DefaultValue(DisplayType.OnlyWhenUsingPortals)]
		public DisplayType UsePortalCursor;

		[DrawTicks]
		[DefaultValue(DisplayType.OnlyWhenUsingPortals)]
		public DisplayType UsePortalIndicator;

		[SeparatePage]
		public Point LeftIndicatorOffset = new Point(-3, -8);

		[SeparatePage]
		public Point RightIndicatorOffset = new Point(8, 6);

		[Header("ColorSettings")]

		[SeparatePage]
		public ColorWrapper CursorColor = new ColorWrapper(Main.mouseColor);

		[DefaultValue(true)]
		public bool SyncCursorColor;

		[DefaultValue(false)]
		public bool InvertIndicatorColors;

		[DefaultValue(false)]
		public bool UseCustomIndicatorColors;

		[SeparatePage]
		public ColorWrapper LeftIndicatorColor = new ColorWrapper(Color.White);

		[SeparatePage]
		public ColorWrapper RightIndicatorColor = new ColorWrapper(Color.White);

		public class ColorWrapper
		{
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
