﻿using Microsoft.Xna.Framework.Input;

namespace Zen
{
	public static class Input
	{
		private static KeyboardState _keyboardState;
		
		public static void Update()
		{
			_keyboardState = Keyboard.GetState();
		}

		public static bool IsKeyDown(Keys key)
		{
			return _keyboardState.IsKeyDown(key);
		}
	}
}