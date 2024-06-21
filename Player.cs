using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace IsometricRTS
{
    public class Player : Entity
    {
        public Player(Vector2 startPosition) : base(startPosition, 1, 0) 
        {
            
        }


        public override void Draw()
        {
            Vector2 scale = new Vector2(Globals.GameScale, Globals.GameScale);
            Globals.SpriteBatch.Draw(body.texture, new Vector2(body.position.X + 16, body.position.Y - 8), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }


    }
}
