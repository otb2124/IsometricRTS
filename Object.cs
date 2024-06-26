﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;

namespace IsometricRTS
{
    public class Object : Entity
    {


        public bool GlowUpWhenHoverOn = false;


        private bool IsGlownUp = false;
        private bool ItemBoxShown = false;
        private bool SubMenuOn = false;


        public Object(Vector2 startPosition, bool HasCollision, int TextureId) : base(startPosition, Int32.Parse(TextureId.ToString().Substring(0, 1)), Int32.Parse(TextureId.ToString().Substring(1, 1)))
        {

            if (HasCollision)
            {
                Globals.currentMap._tiles[(int)startPosition.X, (int)startPosition.Y].HasCollision = true;
                Globals.currentMap._tiles[(int)startPosition.X, (int)startPosition.Y]._texture = Globals.AssetSetter.textures[0][6][0];
            }
        }


        public override void Update()
        {


            if (Globals.InputManager.HoversOn(body)) //onhover
            {

                if (GlowUpWhenHoverOn)
                {
                    IsGlownUp = true;
                }

                if (!SubMenuOn)
                {
                    Globals.UIManager.DrawItemWindow(name, new Vector2(0, 0), true);
                }
                ItemBoxShown = true;



                if (Globals.InputManager.IsLeftMouseClick()) //left click
                {
                    Globals.UIManager.DrawItemWindow(name, new Vector2(0, 0), false);
                    Globals.UIManager.DrawItemWindow(name, new Vector2(50, 0), false);
                    Globals.UIManager.DrawItemWindow(name, new Vector2(0, 200), false);
                    Globals.UIManager.DrawItemWindow("dick", new Vector2(100, 0), false);
                    SubMenuOn = true;
                }
            }
            else  //unhover
            {
                if (IsGlownUp)
                {
                    IsGlownUp = false;
                }

                if (ItemBoxShown)
                {
                    Globals.UIManager.ClearAllCompositesOfTypes(UIComposite.UICompositeType.ITEMWINDOW);
                    ItemBoxShown = false;
                    SubMenuOn = false;
                }
                
            }



        }
        public override void Draw()
        {
            // Determine stroke parameters
            int strokeSize = 1;
            Color strokeColor = Color.LightBlue;
            StrokeType strokeType = StrokeType.OutlineAndTexture;


            // Create stroke texture only if IsGlownUp is true
            Texture2D textureToDraw = body.texture;
            if (IsGlownUp)
            {
                textureToDraw = StrokeEffect.CreateStroke(body.texture, strokeSize, strokeColor, Globals.GraphicsDeviceManager.GraphicsDevice, strokeType);
            }

            drawposition = new Vector2(body.position.X - body.texture.Width + (Globals.currentMap.TILE_SIZE.X / Globals.GameScale), body.position.Y - body.texture.Height + (Globals.currentMap.TILE_SIZE.Y / Globals.GameScale) * 2 - body.texture.Height);



            // Draw the texture (with or without stroke)
            Vector2 scale = new Vector2(Globals.GameScale, Globals.GameScale);
            Globals.SpriteBatch.Draw(textureToDraw, drawposition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);



            if (Globals.gameMode == Globals.GameMode.debugmode)
            {
                Texture2D whiteRectangle = new Texture2D(Globals.GraphicsDeviceManager.GraphicsDevice, body.texture.Width, body.texture.Height);
                Color[] data = new Color[body.texture.Width * body.texture.Height];
                for (int i = 0; i < data.Length; ++i) data[i] = new Color(0, 75, 0, 0);
                whiteRectangle.SetData(data);

                Globals.SpriteBatch.Draw(whiteRectangle, drawposition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

        }
    }
}
