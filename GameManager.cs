using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricRTS
{
    public class GameManager
    {

        private readonly Map map = new Map();
        private Player player;

        public void init()
        {
            player = new Player(Globals.LoadTexture("Content/player/player0.png"), Vector2.Zero);
            Globals.Player = player;
        }

        public void Update()
        {
            InputManager.Update();
            map.Update();
        }



        public void Draw()
        {
            map.Draw();
            player.Draw();
        }
    }
}
