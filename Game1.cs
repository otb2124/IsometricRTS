using IsometricRTS;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace IsometricRTS
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;

        GameManager manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            Globals.GraphicsDeviceManager = graphics;
            Globals.GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;

            graphics.PreferredBackBufferWidth = 1920; // Set to desired width
            graphics.PreferredBackBufferHeight = 1080; // Set to desired height
            graphics.ApplyChanges(); // Apply the changes to the graphics device
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;

            camera = new Camera(GraphicsDevice.Viewport);
            Globals.Camera = camera;

            manager = new GameManager();
            manager.init();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            manager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            Globals.SpriteBatch.Begin(transformMatrix: camera.Transform);
            
            manager.Draw();
            GraphicsDevice.Clear(Color.White);
            Globals.SpriteBatch.End();

            base.Draw(gameTime);
            
        }
    }
}