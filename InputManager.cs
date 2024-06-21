using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Buffers.Binary;
using System.Diagnostics;

namespace IsometricRTS
{
    public class InputManager
    {
        private static Point _direction;
        public static Point Direction => _direction;

        private static MouseState _lastMouseState;
        public Point MousePosition => Mouse.GetState().Position;


        private int silenceStateCounter = 0;

        public bool HoversOn(Body body)
        {
            // Calculate the position of the object's texture relative to the camera and zoom
            float zoomFactor = Globals.Camera.zoom; // Assuming Zoom is a float value indicating the zoom level

            // Adjusted position considering zoom
            float adjustedX = body.position.X - body.texture.Width / 2 - Globals.Camera.position.X + Globals.Camera.viewport.Width / 2 - body.texture.Width/4;
            float adjustedY = body.position.Y - body.texture.Height - Globals.Camera.position.Y + Globals.Camera.viewport.Height / 2 - body.texture.Height/2;

            // Create a Rectangle representing the bounds of the object's texture
            Rectangle textureBounds = new Rectangle((int)adjustedX, (int)adjustedY, (int)(body.texture.Width*Globals.GameScale), (int)(body.texture.Height * Globals.GameScale));

            // Check if the mouse position is within the bounds of the object's texture
            if (textureBounds.Contains(MousePosition))
            {
                return true;
            }

            return false;
        }

        public Vector2 GetCursorPos() 
        {
            return new Vector2(Globals.InputManager.MousePosition.X - Globals.Camera.viewport.Width / 2, Globals.InputManager.MousePosition.Y - Globals.Camera.viewport.Height / 2);
        }
        public bool IsRightMouseClick()
        {
            var mouseState = Mouse.GetState();
            bool isClick = mouseState.RightButton == ButtonState.Pressed;
            _lastMouseState = mouseState;
            return isClick;
        }

        public bool IsLeftMouseClick()
        {
            var mouseState = Mouse.GetState();
            bool isClick = mouseState.LeftButton == ButtonState.Pressed;
            _lastMouseState = mouseState;
            return isClick;
        }


        public void Update()
        {
            _lastMouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) Globals.Camera.Move(new Vector2(-5, 0));
            if (keyboardState.IsKeyDown(Keys.Right)) Globals.Camera.Move(new Vector2(5, 0));
            if (keyboardState.IsKeyDown(Keys.Up)) Globals.Camera.Move(new Vector2(0, -5));
            if (keyboardState.IsKeyDown(Keys.Down)) Globals.Camera.Move(new Vector2(0, 5));
            if (keyboardState.IsKeyDown(Keys.OemPlus)) Globals.Camera.Zoom(0.05f);
            if (keyboardState.IsKeyDown(Keys.OemMinus)) Globals.Camera.Zoom(-0.05f);
            if (keyboardState.IsKeyDown(Keys.OemTilde)) Globals.Camera.followPlayer = true;

            bool userInteracted = false;

            

            // Detect if any key is pressed
            if (keyboardState.GetPressedKeys().Length > 0)
            {
                userInteracted = true;
            }

            // Detect if mouse button is clicked
            if (_lastMouseState.LeftButton == ButtonState.Pressed || _lastMouseState.RightButton == ButtonState.Pressed)
            {
                userInteracted = true;
            }


            if (userInteracted)
            {
                silenceStateCounter = 0;

                if (Globals.gameState == Globals.GameState.silencestate)
                {
                    Globals.Camera.zoom = Globals.Camera.DEFAULT_ZOOM;
                    Globals.gameState = Globals.GameState.playstate;
                }
            }
            else
            {
                if (Globals.gameState == Globals.GameState.playstate)
                {
                    if (silenceStateCounter > 10 * 60) // 10 seconds at 60 FPS
                    {
                        Globals.gameState = Globals.GameState.silencestate;
                        silenceStateCounter = 0;
                    }
                    else
                    {
                        silenceStateCounter++;
                    }
                }
            }
        }
    }
}
