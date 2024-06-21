using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace IsometricRTS
{
    public class UIManager
    {

        public List<UIElement> elements;
        

        public UIManager() 
        {
            elements = new List<UIElement>();

            DrawCursor();
        }



        public void DrawItemWindow(string text)
        {

            Vector2 position = new Vector2(0, 0);

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].type == UIElement.UIElementType.MOUSE_CURSOR)
                {
                    position.X = elements[i].position.X + elements[i].texture.Width * Globals.GameScale + Globals.Camera.position.X;
                    position.Y = elements[i].position.Y + elements[i].texture.Height*Globals.GameScale + Globals.Camera.position.Y;
                    break;
                }
            }

           

            int fontID = 4;
            Vector2 textSize = Globals.AssetSetter.fonts[fontID].MeasureString(text);
            Vector2 padding = new Vector2(textSize.X + 32, textSize.Y);
            int rescale = 2;
            padding /= rescale;
            // Calculate scaling factors
            float singleCharacterWidth = textSize.X / text.Length;
            float horizontalScale = singleCharacterWidth / Globals.AssetSetter.textures[3][2][1].Width * text.Length;
            float verticalScale = textSize.Y;

            // Create and position the UI elements with scaling

            float centredX = position.X - padding.X + Globals.AssetSetter.textures[3][2][0].Width;

            UIElement topCenter = new UIElement(new Vector2(centredX, position.Y), Globals.AssetSetter.textures[3][2][1], UIElement.UIElementType.ITEMWINDOW);
            topCenter.scale.X *= horizontalScale;
            UIElement bottomCenter = new UIElement(new Vector2(centredX, position.Y + padding.Y), Globals.AssetSetter.textures[3][2][1], UIElement.UIElementType.ITEMWINDOW);
            bottomCenter.scale.X *= horizontalScale;
            bottomCenter.flip = 1;

            UIElement leftTopCorner = new UIElement(new Vector2(position.X - padding.X, position.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW);
            leftTopCorner.flip = -1;

            UIElement leftBottomCorner = new UIElement(new Vector2(position.X - padding.X, position.Y + padding.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW);
            leftBottomCorner.flip = 1;

            UIElement rightTopCorner = new UIElement(new Vector2(position.X + padding.X, position.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW);
            rightTopCorner.flip = 0;

            UIElement rightBottomCorner = new UIElement(new Vector2(position.X + padding.X, position.Y + padding.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW);
            rightBottomCorner.flip = 2;

            topCenter.scale /= rescale;
            bottomCenter.scale /= rescale;
            elements.Add(topCenter);
            elements.Add(bottomCenter);
            // Add UI elements to the list
            leftTopCorner.scale /= rescale;
            leftBottomCorner.scale /= rescale;
            rightBottomCorner.scale /= rescale;
            rightTopCorner.scale /= rescale;

            elements.Add(leftTopCorner);
            elements.Add(leftBottomCorner);
            elements.Add(rightTopCorner);
            elements.Add(rightBottomCorner);

            // Text
            UIElement textElement = new UIElement(new Vector2(position.X - padding.X + Globals.AssetSetter.textures[3][2][0].Width, position.Y + Globals.AssetSetter.textures[3][2][0].Height - textSize.Y), null, UIElement.UIElementType.TEXT_LABEL);
            textElement.text = text;
            textElement.fontID = fontID;
            textElement.scale /= rescale;
            elements.Add(textElement);
        }

        public void ClearItemWindow()
        {
            elements.RemoveAll(e => e.type == UIElement.UIElementType.ITEMWINDOW || e.type == UIElement.UIElementType.TEXT_LABEL);
        }

        public void DrawCursor()
        {
            UIElement cursor = new UIElement(Globals.InputManager.MousePosition.ToVector2(), Globals.AssetSetter.textures[3][1][0], UIElement.UIElementType.MOUSE_CURSOR);
            cursor.StickToCamera = true;
            elements.Add(cursor);
        }

        public void DrawMapPointer(Vector2 position, int id)
        {
            
            UIElement pic = new UIElement(position, Globals.AssetSetter.textures[3][0][1], UIElement.UIElementType.MAP_POINTER);
            if (id == 1)
            {
                pic.texture = Globals.AssetSetter.textures[3][0][0];
            }
            pic.UnderDrawn = true;
            elements.Add(pic);
        }

        public void ClearAllMapPointers()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].type == UIElement.UIElementType.MAP_POINTER)
                {
                    elements.Remove(elements[i]);
                }
            }
        } 

        public void Update()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].type == UIElement.UIElementType.MOUSE_CURSOR)
                {
                    elements[i].position = new Vector2((Globals.InputManager.MousePosition.X - Globals.Camera.viewport.Width / 2), (Globals.InputManager.MousePosition.Y - Globals.Camera.viewport.Height / 2));
                }
            }
        }

        public void DrawOver()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].UnderDrawn && elements[i].type != UIElement.UIElementType.MOUSE_CURSOR)
                {
                    elements[i].Draw();
                }
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].type == UIElement.UIElementType.MOUSE_CURSOR)
                {
                    elements[i].Draw();
                }
            }

        }

        public void DrawUnder()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].UnderDrawn)
                {
                    elements[i].Draw();
                }
            }
        }
    }
}
