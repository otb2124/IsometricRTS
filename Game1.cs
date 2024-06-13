using IsometricRTS;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace IsometricRTS
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameManager manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            Globals.GraphicsDeviceManager = graphics;
            IsMouseVisible = true;


           
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;


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
            GraphicsDevice.Clear(Color.White);

            Globals.SpriteBatch.Begin();
            manager.Draw();
            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}