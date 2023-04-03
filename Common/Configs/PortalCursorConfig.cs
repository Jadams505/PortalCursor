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
		[Label("$Mods.PortalCursor.Enums.OnlyWhenUsingPortals")]
		OnlyWhenUsingPortals,
		[Label("$Mods.PortalCursor.Enums.Always")]
		Always,
		[Label("$Mods.PortalCursor.Enums.Never")]
		Never
	}

	[Label("$Mods.PortalCursor.PortalCursorConfig")]
	public class PortalCursorConfig : ModConfig
	{
		public static PortalCursorConfig Instance => ModContent.GetInstance<PortalCursorConfig>();

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("$Mods.PortalCursor.MainSettings")]

		[DrawTicks]
		[DefaultValue(DisplayType.OnlyWhenUsingPortals)]
		[Label("$Mods.PortalCursor.UsePortalCursor.Label")]
		[Tooltip("$Mods.PortalCursor.UsePortalCursor.Tooltip")]
		public DisplayType UsePortalCursor;

		[DrawTicks]
		[DefaultValue(DisplayType.OnlyWhenUsingPortals)]
		[Label("$Mods.PortalCursor.UsePortalIndicator.Label")]
		[Tooltip("$Mods.PortalCursor.UsePortalIndicator.Tooltip")]
		public DisplayType UsePortalIndicator;

		[Label("$Mods.PortalCursor.LeftIndicatorOffset.Label")]
		[Tooltip("$Mods.PortalCursor.LeftIndicatorOffset.Tooltip")]
		[SeparatePage]
		public Point LeftIndicatorOffset = new Point(-3, -8);

		[Label("$Mods.PortalCursor.RightIndicatorOffset.Label")]
		[Tooltip("$Mods.PortalCursor.RightIndicatorOffset.Tooltip")]
		[SeparatePage]
		public Point RightIndicatorOffset = new Point(8, 6);

		[Header("$Mods.PortalCursor.ColorSettings")]

		[Label("$Mods.PortalCursor.CursorColor.Label")]
		[Tooltip("$Mods.PortalCursor.CursorColor.Tooltip")]
		[SeparatePage]
		public ColorWrapper CursorColor = new ColorWrapper(Main.mouseColor);

		[DefaultValue(true)]
		[Label("$Mods.PortalCursor.SyncCursorColor.Label")]
		[Tooltip("$Mods.PortalCursor.SyncCursorColor.Tooltip")]
		public bool SyncCursorColor;

		[DefaultValue(false)]
		[Label("$Mods.PortalCursor.InvertIndicatorColors.Label")]
		[Tooltip("$Mods.PortalCursor.InvertIndicatorColors.Tooltip")]
		public bool InvertIndicatorColors;

		[DefaultValue(false)]
		[Label("$Mods.PortalCursor.UseCustomIndicatorColors.Label")]
		[Tooltip("$Mods.PortalCursor.UseCustomIndicatorColors.Tooltip")]
		public bool UseCustomIndicatorColors;

		[Label("$Mods.PortalCursor.LeftIndicatorColor.Label")]
		[Tooltip("$Mods.PortalCursor.LeftIndicatorColor.Tooltip")]
		[SeparatePage]
		public ColorWrapper LeftIndicatorColor = new ColorWrapper(Color.White);

		[Label("$Mods.PortalCursor.RightIndicatorColor.Label")]
		[Tooltip("$Mods.PortalCursor.RightIndicatorColor.Tooltip")]
		[SeparatePage]
		public ColorWrapper RightIndicatorColor = new ColorWrapper(Color.White);

		public class ColorWrapper
		{
			[Label("$Mods.PortalCursor.Color")]
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
			[Label("$Mods.PortalCursor.X.Label")]
			public int X;
			[Range(-999, 999)]
			[Label("$Mods.PortalCursor.Y.Label")]
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
