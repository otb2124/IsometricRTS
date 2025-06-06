﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IsometricRTS
{
    public class AssetSetter
    {

        public Texture2D[][][] textures;
        public SpriteFont[] fonts;

        public AssetSetter()
        {
            textures = new Texture2D[10][][];

            textures[0] = new Texture2D[10][];
            textures[1] = new Texture2D[10][];
            textures[2] = new Texture2D[10][];
            textures[3] = new Texture2D[10][];

            fonts = new SpriteFont[10];
        }


        public void SetAssets()
        {
            SetTextures();
            SetFonts();
        }


        public void SetTextures()
        {
            LoadTextures(0, "res/tiles/tile");    // Load tile textures
            LoadTextures(1, "res/player/player"); // Load player textures
            LoadTextures(2, "res/objects/object");// Load object textures
            LoadTextures(3, "res/ui/uielement");// Load ui textures
        }

        private void LoadTextures(int index, string basePath)
        {
            for (int i = 0; i < textures[index].Length; i++)
            {
                string directoryPath = Path.GetDirectoryName(basePath);
                string searchPattern = $"{Path.GetFileName(basePath)}{i}*.png";
                string[] files = Directory.GetFiles(directoryPath, searchPattern);

                if (files.Length > 0)
                {
                    textures[index][i] = files.Select(filePath => LoadTexture(filePath)).ToArray();
                }
            }
        }

        public Texture2D LoadTexture(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return Texture2D.FromStream(Globals.GraphicsDeviceManager.GraphicsDevice, fileStream);
            }
        }


        public void SetFonts()
        {
            string fontsDirectory = Path.Combine(Environment.CurrentDirectory, "Content", "fonts");

            if (!Directory.Exists(fontsDirectory))
            {
                Debug.WriteLine("Fonts directory not found.");
                return;
            }

            string[] fontFiles = Directory.GetFiles(fontsDirectory, "*.xnb");

            fonts = new SpriteFont[fontFiles.Length];

            for (int i = 0; i < fontFiles.Length; i++)
            {
                string fontPath = Path.Combine("fonts", Path.GetFileNameWithoutExtension(fontFiles[i]));
                fonts[i] = Globals.Content.Load<SpriteFont>(fontPath);
            }
        }
    }
}
