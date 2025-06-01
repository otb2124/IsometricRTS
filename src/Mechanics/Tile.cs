using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricRTS
{
    public class Tile
    {

        public Texture2D _texture;
        public readonly Vector2 _position;
        public bool HasCollision = false;

        public Tile(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public void Draw()
        {
            Vector2 scale = new Vector2(Globals.GameScale, Globals.GameScale);
            Globals.SpriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

    }
}
