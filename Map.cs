using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.IO;

namespace IsometricRTS
{
    public class Map
    {
        private readonly Point MAP_SIZE = new(30, 30);
        private readonly Point TILE_SIZE;
        private readonly Tile[,] _tiles;
        private readonly Vector2 MAP_OFFSET = new(2.5f * Globals.GameScale, 2 * Globals.GameScale);

        private Tile lastSelectedTile;

        public Map()
        {
            _tiles = new Tile[MAP_SIZE.X, MAP_SIZE.Y];

            Debug.WriteLine(Globals.Content.ToString());

            Texture2D[] textures =
            {
                Globals.LoadTexture("Content/tiles/tile0.png"),
                Globals.LoadTexture("Content/tiles/tile1.png"),
                Globals.LoadTexture("Content/tiles/tile2.png"),
                Globals.LoadTexture("Content/tiles/tile3.png"),
                Globals.LoadTexture("Content/tiles/tile4.png"),
            };

            TILE_SIZE.X = textures[0].Width * Globals.GameScale;
            TILE_SIZE.Y = textures[0].Height * Globals.GameScale / 2;

            Random random = new();

            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    int r = random.Next(0, textures.Length);
                    _tiles[x, y] = new Tile(textures[r], MapToScreen(x, y));
                }
            }
        }

        private Vector2 MapToScreen(int mapX, int mapY)
        {
            var screenX = mapX * TILE_SIZE.X / 2 - mapY * TILE_SIZE.X / 2 + (MAP_OFFSET.X * TILE_SIZE.X);
            var screenY = mapY * TILE_SIZE.Y / 2 + mapX * TILE_SIZE.Y / 2 + (MAP_OFFSET.Y * TILE_SIZE.Y);
            return new Vector2(screenX, screenY);
        }

        private Point ScreenToMap(Point mousePos)
        {
            Vector2 cursor = new(mousePos.X - (int)(MAP_OFFSET.X * TILE_SIZE.X), mousePos.Y - (int)(MAP_OFFSET.Y * TILE_SIZE.Y));

            var x = cursor.X + (2 * cursor.Y) - (TILE_SIZE.X / 2);
            int mapX = (x < 0) ? -1 : (int)(x / TILE_SIZE.X);
            var y = -cursor.X + (2 * cursor.Y) + (TILE_SIZE.X / 2);
            int mapY = (y < 0) ? -1 : (int)(y / TILE_SIZE.X);

            return new(mapX, mapY);
        }

        public Vector2 GetTileScreenPosition(Point tilePos)
        {
            return MapToScreen(tilePos.X, tilePos.Y);
        }

        public void Update()
        {
            lastSelectedTile?.KeyBoardDeselect();

            var map = ScreenToMap(InputManager.MousePosition);

            if (map.X >= 0 && map.Y >= 0 && map.X < MAP_SIZE.X && map.Y < MAP_SIZE.Y)
            {
                lastSelectedTile = _tiles[map.X, map.Y];
                lastSelectedTile.KeyBoardSelect();


                if (InputManager.IsLeftMouseClick())
                {
                    Globals.Player.MoveTo(MapToScreen(map.X-1, map.Y-1));
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    _tiles[x, y].Draw();
                }
            }
        }
    }
}
