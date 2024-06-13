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

        public static void Update(GameTime gameTime)
        {
            TotalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameTime = gameTime;
        }

        public static Texture2D LoadTexture(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
            }
        }
    }
}
