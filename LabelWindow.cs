using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace IsometricRTS
{
    public class LabelWindow : UIComposite

    {
        public Vector2 position;
        public bool StickToCursor;

        public LabelWindow(string text, Vector2 position, bool StickToCursor) 
        {
            this.position = position;
            this.StickToCursor = StickToCursor;
            this.type = UICompositeType.ITEMWINDOW;

            UIElement cursor = Globals.UIManager.GetElementSByType(UIElement.UIElementType.MOUSE_CURSOR)[0];


            position.X += cursor.texture.Width * Globals.GameScale;
            position.Y += cursor.texture.Height * Globals.GameScale;


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

            UIElement topCenter = new UIElement(new Vector2(centredX, position.Y), Globals.AssetSetter.textures[3][2][1], UIElement.UIElementType.ITEMWINDOW_FRAME_PART);
            topCenter.scale.X *= horizontalScale;
            UIElement bottomCenter = new UIElement(new Vector2(centredX, position.Y + padding.Y), Globals.AssetSetter.textures[3][2][1], UIElement.UIElementType.ITEMWINDOW_FRAME_PART);
            bottomCenter.scale.X *= horizontalScale;
            bottomCenter.flip = 1;

            UIElement leftTopCorner = new UIElement(new Vector2(position.X - padding.X, position.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW_FRAME_PART);
            leftTopCorner.flip = -1;

            UIElement leftBottomCorner = new UIElement(new Vector2(position.X - padding.X, position.Y + padding.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW_FRAME_PART);
            leftBottomCorner.flip = 1;

            UIElement rightTopCorner = new UIElement(new Vector2(position.X + padding.X, position.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW_FRAME_PART);
            rightTopCorner.flip = 0;

            UIElement rightBottomCorner = new UIElement(new Vector2(position.X + padding.X, position.Y + padding.Y), Globals.AssetSetter.textures[3][2][0], UIElement.UIElementType.ITEMWINDOW_FRAME_PART);
            rightBottomCorner.flip = 2;

            topCenter.scale /= rescale;
            bottomCenter.scale /= rescale;
            components.Add(topCenter);
            components.Add(bottomCenter);
            // Add UI elements to the list
            leftTopCorner.scale /= rescale;
            leftBottomCorner.scale /= rescale;
            rightBottomCorner.scale /= rescale;
            rightTopCorner.scale /= rescale;

            components.Add(leftTopCorner);
            components.Add(leftBottomCorner);
            components.Add(rightTopCorner);
            components.Add(rightBottomCorner);

            // Text
            UIElement textElement = new UIElement(new Vector2(position.X - padding.X + Globals.AssetSetter.textures[3][2][0].Width, position.Y + Globals.AssetSetter.textures[3][2][0].Height - textSize.Y), null, UIElement.UIElementType.TEXT_LABEL);
            textElement.text = text;
            textElement.fontID = fontID;
            textElement.scale /= rescale;
            components.Add(textElement);



            if (StickToCursor)
            {
                foreach (var component in components)
                {
                    component.StickToCursor = true;
                    component.StickToCamera = true;
                }
            }
            else
            {
                foreach (var component in components)
                {
                    component.StickToCursor = false;
                    component.StickToCamera = false;
                }
            }
        
        }



    }
}
