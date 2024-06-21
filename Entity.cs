using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricRTS
{
    public class Entity
    {
        public Body body;
        public string name = "blankName";
        public Vector2 drawposition;

        public Entity(Vector2 startPosition, int textureCategory, int textureID)
        {
            body = new Body();
            body.texture = Globals.AssetSetter.textures[textureCategory][textureID][0];
            body.position = Globals.MapToScreen((int)startPosition.X, (int)startPosition.Y);
            body.owner = this;
        }


        public virtual void Update()
        {
            
            body.Update();
            
            
        }

        public virtual void Draw()
        {

            drawposition = new Vector2(body.position.X - body.texture.Width + (Globals.currentMap.TILE_SIZE.X / Globals.GameScale), body.position.Y - body.texture.Height + (Globals.currentMap.TILE_SIZE.Y / Globals.GameScale) * 2 - body.texture.Height);



            // Draw the texture (with or without stroke)
            Vector2 scale = new Vector2(Globals.GameScale, Globals.GameScale);
            Globals.SpriteBatch.Draw(body.texture, drawposition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
