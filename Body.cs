using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace IsometricRTS
{
    public class Body
    {

        public float speed = 3f;
        public bool isCollidable = false;
        public Entity owner = null;
        public Texture2D texture;
        public Vector2 position;
        private Queue<Point> path;
        private Point tileSize;
        private Map map;

        public void SetPath(List<Point> path, Point tileSize, Map map)
        {
            try
            {
                Globals.UIManager.ClearAllElementsOfTypes(UIElement.UIElementType.MAP_POINTER);
                this.path = new Queue<Point>(path);
                this.tileSize = tileSize;
                this.map = map;
            }
            catch(System.ArgumentNullException ane) 
            {
                //
            }
            
        }

        public void Update()
        {
            if (path != null && path.Count > 0)
            {
                var nextTile = path.Peek();
                var targetPosition = Globals.MapToScreen(nextTile.X-1, nextTile.Y-1);

                if (Vector2.Distance(position, targetPosition) < speed)
                {
                    position = targetPosition;
                    path.Dequeue();
                    
                }
                else
                {
                    var direction = Vector2.Normalize(targetPosition - position);
                    position += direction * speed;
                }
            }
            else if(path != null && path.Count == 0)
            {
                Globals.UIManager.ClearAllElementsOfTypes(UIElement.UIElementType.MAP_POINTER);
            }

        }

    }
}
