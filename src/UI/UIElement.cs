using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricRTS
{
    public class UIElement
    {

        public enum UIElementType
        {
            MAP_POINTER,
            MOUSE_CURSOR,
            ITEMWINDOW_FRAME_PART,
            TEXT_LABEL,
        }

        public UIElementType type { get; set; }

        public Vector2 position;
        public Vector2 scale;
        public Vector2 origin;
        public Texture2D texture;


        public bool StickToCamera;
        public bool StickToZoom;
        public bool StickToCursor;
        public bool HalfedOrigin;
        public bool UnderDrawn = false;
        public int flip = -1;
        public int fontID = 0;

        public string text = string.Empty;

        public bool IsShown = false;

        public UIElement(Vector2 position, Texture2D texture, UIElementType type)
        {
            this.position = position;
            this.scale = new Vector2(Globals.GameScale, Globals.GameScale);
            this.origin = Vector2.Zero;
            this.texture = texture;
            this.StickToCamera = false;
            this.StickToZoom = false;
            this.StickToCursor = false;
            this.HalfedOrigin = false;
            this.type = type;
        }

        public void Draw()
        {
            Vector2 adjustedPosition = position;

            if (StickToCamera)
            {
                adjustedPosition += Globals.Camera.position;
            }

            if (StickToCursor)
            {
                adjustedPosition += Globals.InputManager.GetCursorPos();
            }

            Vector2 adjustedScale = scale;

            if (StickToZoom)
            {
                adjustedPosition /= Globals.Camera.zoom;
                adjustedScale /= Globals.Camera.zoom;
            }

            Vector2 adjustedOrigin = origin;


            if (HalfedOrigin)
            {
                if(type == UIElementType.TEXT_LABEL)
                {
                    adjustedOrigin = new Vector2(Globals.AssetSetter.fonts[fontID].MeasureString(text).X / 2, Globals.AssetSetter.fonts[fontID].MeasureString(text).Y / 2);
                }
                else
                {
                    adjustedOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
                }
            }

            SpriteEffects spriteEffects = SpriteEffects.None;

            switch (flip)
            {
                case 0:
                    spriteEffects = SpriteEffects.FlipHorizontally; break;
                case 1:
                    spriteEffects = SpriteEffects.FlipVertically; break;
                case 2:
                    spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally; break;
                case -1:
                    break;
            }


            if (type != UIElementType.TEXT_LABEL)
            {
                Globals.SpriteBatch.Draw(texture, adjustedPosition, null, Color.White, 0f, adjustedOrigin, adjustedScale, spriteEffects, 0f);
            }
            else
            {
                Globals.SpriteBatch.DrawString(Globals.AssetSetter.fonts[fontID], text, adjustedPosition, Color.White, 0f, adjustedOrigin, adjustedScale, spriteEffects, 0f);
            }
            
        }
    }
}
