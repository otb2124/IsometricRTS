using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace IsometricRTS
{
    public class UIManager
    {

        public List<UIElement> elements;
        public List<UIComposite> composites;

        public UIManager()
        {
            elements = new List<UIElement>();
            composites = new List<UIComposite>();
            DrawCursor();
        }



        public void DrawItemWindow(string text, Vector2 position, bool StickToCursor)
        {
            
            ClearLastCompositeWithType(UIComposite.UICompositeType.ITEMWINDOW);
            LabelWindow labelWindow = new LabelWindow(text, position, StickToCursor);
            composites.Add(labelWindow);
            elements.AddRange(labelWindow.components);
        }

        public void DrawCursor()
        {
            UIElement cursor = new UIElement(Globals.InputManager.GetCursorPos(), Globals.AssetSetter.textures[3][1][0], UIElement.UIElementType.MOUSE_CURSOR);
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




        public void ClearAllCompositesOfTypes(params UIComposite.UICompositeType[] types)
        {
            foreach (var type in types)
            {
                foreach (var composite in composites.Where(c => c.type == type).ToList())
                {
                    ClearComposite(composite);
                }
            }
        }

        public void ClearLastCompositeWithType(UIComposite.UICompositeType type)
        {

            for (int i = 0; i < composites.Count; i++)
            {
                if (composites[i].type == type)
                {
                    ClearComposite(composites[i]);
                    break;
                }
            }
        }


        public void ClearComposite(UIComposite composite)
        {
            foreach (var component in composite.components)
            {
                if (elements.Contains(component))
                {
                    elements.Remove(component);
                }
            }
            composites.Remove(composite);
        }

        public UIComposite[] GetCompositesByType(UIComposite.UICompositeType type)
        {
            return composites.Where(e => e.type == type).ToArray();
        }

        public void ClearAllElementsOfTypes(params UIElement.UIElementType[] elementTypes)
        {
            for (int i = 0; i < elementTypes.Length; i++)
            {
                elements.RemoveAll(e => e.type == elementTypes[i]);
            }
        }


        public bool ContainsElementsOfTypes(params UIElement.UIElementType[] elementTypes)
        {
            return elements.Any(e => elementTypes.Contains(e.type));
        }

        public UIElement[] GetElementSByType(UIElement.UIElementType type)
        {
            return elements.Where(e => e.type == type).ToArray();
        }

        public void Update()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].type == UIElement.UIElementType.MOUSE_CURSOR)
                {
                    elements[i].position = Globals.InputManager.GetCursorPos();
                }

            }

            Debug.WriteLine(composites.Count);
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
