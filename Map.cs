using Microsoft.Xna.Framework;
using System;

namespace IsometricRTS
{
    public class Map
    {
        public readonly Point MAP_SIZE = new(100, 100);
        public readonly Point TILE_SIZE;
        public Tile[,] _tiles;

        public Map()
        {

            Globals.currentMap = this;

            _tiles = new Tile[MAP_SIZE.X, MAP_SIZE.Y];


            TILE_SIZE.X = Globals.AssetSetter.textures[0][0][0].Width * Globals.GameScale;
            TILE_SIZE.Y = Globals.AssetSetter.textures[0][0][0].Height * Globals.GameScale / 2;

            Random random = new();

            for (int y = 0; y < MAP_SIZE.Y; y++)
            {
                for (int x = 0; x < MAP_SIZE.X; x++)
                {
                    int r = random.Next(0, Globals.AssetSetter.textures[0].Length);

                        if (Globals.AssetSetter.textures[0][r] == null || r == 6)
                        {
                            r = 0;
                        }
                    
                        

                    _tiles[x, y] = new Tile(Globals.AssetSetter.textures[0][r][0], Globals.MapToScreen(x, y));
                    if (_tiles[x, y]._texture == Globals.AssetSetter.textures[0][5][0])
                    {
                        _tiles[x, y].HasCollision = true;
                    }
                }
            }
        }

        public void Update()
        {
            var map = Globals.MouseScreenToMap(Globals.InputManager.MousePosition);

            if (map.X >= 0 && map.Y >= 0 && map.X < MAP_SIZE.X && map.Y < MAP_SIZE.Y)
            { 
                if (Globals.InputManager.IsRightMouseClick())
                {
                    if (_tiles[map.X, map.Y].HasCollision)
                    {

                    }
                    else
                    {
                        var playerMapPos = Globals.ScreenToMap(Globals.Player.body.position.ToPoint());
                        var path = Globals.AStarPathfinding.FindPath(playerMapPos, new Point(map.X, map.Y));
                        Globals.Player.body.SetPath(path, TILE_SIZE, this);
                        for (int i = 0; i < path.Count; i++)
                        {
                            Globals.UIManager.DrawMapPointer(Globals.MapToScreen(path[i].X, path[i].Y), 0);
                        }
                        Globals.UIManager.DrawMapPointer(Globals.MapToScreen(path[path.Count - 1].X, path[path.Count-1].Y), 1);
                    }
                    
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
