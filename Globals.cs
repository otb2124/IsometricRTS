using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace IsometricRTS
{
    public static class Globals
    {
        public static float TotalGameTime { get; private set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static int GameScale = 2;
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public static GameTime GameTime { get; set; }
        public static Player Player { get; set; }
        public static Camera Camera { get; set; }
        public static AssetSetter AssetSetter { get; set; }
        public static UIManager UIManager { get; set; }
        public static InputManager InputManager { get; set; }

        public static Map currentMap { get; set; }

        public static AStarPathfinding AStarPathfinding { get; set; }

        public enum GameState
        {
            playstate,
            silencestate,
        };

        public static Globals.GameState gameState;

        public enum GameMode
        {
            playmode,
            debugmode,
        };

        public static Globals.GameMode gameMode;

        public static void Update(GameTime gameTime)
        {
            TotalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameTime = gameTime;
        }





        public static Vector2 MapToScreen(int mapX, int mapY)
        {
            var screenX = (mapX * currentMap.TILE_SIZE.X / 2 - mapY * currentMap.TILE_SIZE.X / 2);
            var screenY = (mapY * currentMap.TILE_SIZE.Y / 2 + mapX * currentMap.TILE_SIZE.Y / 2);
            return new Vector2(screenX, screenY);
        }

        public static Point MouseScreenToMap(Point mousePos)
        {
            float zoom = Globals.Camera.zoom;
            Vector2 cameraPosition = Globals.Camera.position;
            Viewport viewport = Globals.GraphicsDeviceManager.GraphicsDevice.Viewport;

            Vector2 adjustedMousePos = mousePos.ToVector2();

            Vector2 cursor = new(
                (adjustedMousePos.X + cameraPosition.X - (viewport.Width / 2)),
                (adjustedMousePos.Y + cameraPosition.Y - (viewport.Height / 2))
            );

            var x = cursor.X + (2 * cursor.Y) - (currentMap.TILE_SIZE.X / 2);
            int mapX = (x < 0) ? -1 : (int)(x / currentMap.TILE_SIZE.X);
            var y = -cursor.X + (2 * cursor.Y) + (currentMap.TILE_SIZE.X / 2);
            int mapY = (y < 0) ? -1 : (int)(y / currentMap.TILE_SIZE.X);

            return new(mapX, mapY);
        }


        public static Point ScreenToMap(Point objectPos)
        {
            var x = objectPos.X + (2 * objectPos.Y) - (currentMap.TILE_SIZE.X / 2);
            int mapX = (x < 0) ? -1 : (int)(x / currentMap.TILE_SIZE.X);
            var y = -objectPos.X + (2 * objectPos.Y) + (currentMap.TILE_SIZE.X / 2);
            int mapY = (y < 0) ? -1 : (int)(y / currentMap.TILE_SIZE.X);

            mapX += 2;
            mapY += 1;

            return new(mapX, mapY);
        }

    }
}
