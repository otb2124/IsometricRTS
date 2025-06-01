using System.Collections.Generic;

namespace IsometricRTS
{
    public class UIComposite
    {

        public enum UICompositeType
        {
            ITEMWINDOW,
        }

        public UICompositeType type { get; set; }



        public List<UIElement> components;

        public UIComposite() { components = new List<UIElement>(); }

        public void Draw()
        {
            foreach (var component in components)
            {
                component.Draw();
            }


        }
    }
}
