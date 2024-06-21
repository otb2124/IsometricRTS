using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IsometricRTS
{
    public class Camera
    {
        public Vector2 position;
        public float zoom;
        private float rotation;
        private Matrix transform;
        public Viewport viewport;
        public bool followPlayer;
        public readonly float DEFAULT_ZOOM = 1.0f;

        private float lastZoom;
        private bool zoomingIn = false;
        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            position = Vector2.Zero;
            zoom = DEFAULT_ZOOM;
            rotation = 0f;
            followPlayer = false;
            UpdateTransform();
        }


        public void Update()
        {
            FollowPlayer(Globals.Player.body.position);

            if (Globals.gameState == Globals.GameState.playstate)
            {
                lastZoom = zoom;
            }
            else if (Globals.gameState == Globals.GameState.silencestate)
            {

                if (zoomingIn)
                {
                    Zoom(0.001f);
                    if (zoom >= lastZoom+0.2f)
                    {
                        zoomingIn = false;
                    }
                }
                else
                {
                    Zoom(-0.001f);
                    if (zoom <= lastZoom)
                    {
                        zoomingIn = true;
                    }
                }
            }

        }
        public void Move(Vector2 delta)
        {
            position += delta;
            followPlayer = false;
            UpdateTransform();
        }

        public void Zoom(float delta)
        {
            zoom += delta;
            if (zoom < 0.5f) zoom = 0.5f;
            if (zoom > 10f) zoom = 10f;
            UpdateTransform();
        }

        public void FollowPlayer(Vector2 playerPosition)
        {
            if (followPlayer)
            {
                position = playerPosition;
                UpdateTransform();
            }
        }

        public Matrix Transform => transform;

        private void UpdateTransform()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(zoom) *
                Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
        }
    }
}
