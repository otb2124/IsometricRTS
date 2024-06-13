using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricRTS
{
    public class Tile
    {

        public readonly Texture2D _texture;
        public readonly Vector2 _position;
        private bool IsSelected;

        public Tile(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            IsSelected = false;
        }

        public void KeyBoardSelect()
        {
            IsSelected = true;
        }

        public void KeyBoardDeselect()
        {
            IsSelected = false;
        }


        public void Draw()
        {
            Vector2 scale = new Vector2(Globals.GameScale, Globals.GameScale);

            var color = Color.White;
            if (IsSelected) color = Color.Red;
            Globals.SpriteBatch.Draw(_texture, _position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

    }
}
