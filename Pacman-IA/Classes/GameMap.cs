
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pacman_IA.Classes
{
    public static class GameMap
    {
        private static Game game;
        //private static Texture2D tilemap;
        private static GameItem tile;

        public static void Setup(Game gameMain)
        {
            game = gameMain;
        }

        public static void Init()
        {
            tile = new GameItem();
            tile.Velocity = Point.Zero;
            tile.Position = Point.Zero;
        }

        public static void LoadContent()
        {
            //tilemap = game.Content.Load<Texture2D>(@"sprites\chompermazetiles");
            tile.Texture = game.Content.Load<Texture2D>(@"sprites\chompermazetiles");
        }

        public static void Update()
        {
            tile.Update();
        }

        public static void Draw()
        {
            /*
            Rectangle srcRect = new Rectangle(0, 0, 32, 32);
            Rectangle dstRect = new Rectangle(0, 0, 32, 32);

            GameGraphics.spriteBatch.Draw(tilemap, dstRect, srcRect, Color.White);
            */
            tile.Draw();
        }
    }
}
