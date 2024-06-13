using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace IsometricRTS
{
    public static class InputManager
    {
        private static Point _direction;
        public static Point Direction => _direction;

        private static MouseState _lastMouseState;
        public static Point MousePosition => Mouse.GetState().Position;

        public static bool IsLeftMouseClick()
        {
            var mouseState = Mouse.GetState();
            bool isClick = mouseState.LeftButton == ButtonState.Pressed;
            _lastMouseState = mouseState;
            return isClick;
        }
        public static void Update()
        {
            _lastMouseState = Mouse.GetState();
        }
    }
}
