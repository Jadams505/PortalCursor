using Terraria;
using Terraria.ModLoader;
using PortalCursor.Common.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PortalCursor.Common.Systems
{
	public class UISystem : ModSystem
	{
		public static UISystem Instance => ModContent.GetInstance<UISystem>();

		public CursorUI CursorUI;
		private UserInterface _interface;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				CursorUI = new CursorUI();

				_interface = new UserInterface();
				_interface.SetState(CursorUI);
			}
		}

		public override void Unload()
		{
			CursorUI = null;
			_interface = null;
		}

		public override void UpdateUI(GameTime gameTime)
		{
			_interface?.Update(gameTime);
		}

		public bool DrawGUI()
		{
			_interface?.Draw(Main.spriteBatch, new GameTime());
			return true;
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Cursor"));
			if (inventoryLayer != -1)
			{
				layers.Add(new LegacyGameInterfaceLayer(
					$"{Mod.Name}: Cursor Layer", DrawGUI, InterfaceScaleType.UI));
			}
		}
	}
}
