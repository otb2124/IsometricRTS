using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace IsometricRTS
{
    public class GameManager
    {

        private Map map;
        public List<List<Entity>> entities;
        public List<Entity> overDraw;
        public List<Entity> underDraw;
        private Player player;



        public void init()
        {
            entities = new List<List<Entity>>();
            entities.Add(new List<Entity>());
            entities.Add(new List<Entity>());

            overDraw = new List<Entity>();
            underDraw = new List<Entity>();

            AssetSetter assetSetter = new AssetSetter();
            Globals.AssetSetter = assetSetter;

            InputManager inputManager = new InputManager();
            Globals.InputManager = inputManager;

            Globals.AssetSetter.SetAssets();
            map = new Map();
            AStarPathfinding aStarPathfinding = new AStarPathfinding();
            Globals.AStarPathfinding = aStarPathfinding;
            UIManager uIManager = new UIManager();
            Globals.UIManager = uIManager;

            player = new Player(new Vector2(1, 1));
            Globals.Player = player;

            

            entities[0] = new List<Entity>();
            entities[0].Add(player);

            Object door = new Object(new Vector2(5, 5), true, 21);
            Object door1 = new Object(new Vector2(10, 15), true, 20);
            Object door2 = new Object(new Vector2(15, 15), true, 22);
            Object door3 = new Object(new Vector2(15, 20), true, 23);

            door.name = "tree";
            door1.name = "small log";
            door2.name = "big log";
            door3.name = "wisp tree";

            entities[1] = new List<Entity>();
            entities[1].Add(door);
            entities[1].Add(door1);
            entities[1].Add(door2);
            entities[1].Add(door3);

            Globals.gameState = Globals.GameState.playstate;
            Globals.gameMode = Globals.GameMode.playmode;

        }

        public void Update()
        {
            Globals.InputManager.Update();
            map.Update();


            //entities
            for (int i = 0; i < entities.Count; i++)
            {
                foreach (var entity in entities[i])//all
                {
                    entity.Update();
                    if (entity.body.position.Y - Globals.currentMap.TILE_SIZE.Y <= player.body.position.Y && !(entity is Player))
                    {
                        underDraw.Add(entity);
                    }
                    else
                    {
                        overDraw.Add(entity);
                    }
                }
            }



            //rest
            Globals.Camera.Update();
            Globals.UIManager.Update();
        }


        public void Draw()
        {


            map.Draw();

            Globals.UIManager.DrawUnder();

            foreach (var entity in underDraw)
            {
                entity.Draw();
            }

            foreach (var entity in overDraw)
            {
                entity.Draw();
            }

            underDraw.Clear();
            overDraw.Clear();


            Globals.UIManager.DrawOver();

        }
    }
}
