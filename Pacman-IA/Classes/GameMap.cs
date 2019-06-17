
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.GameObjects;
using Pacman_IA.Sprites;
using System.Collections.Generic;

namespace Pacman_IA.Classes
{
    public static class GameMap
    {
        private static Game game;
        private static Sprite tileSprite;
        private static bool firstTime;

        private const int DOOR = 1000;
        private const int PACMAN = 100;
        private const int BLINKY = 200;
        private const int BLINKY_HOME = 210;
        private const int PINKY = 300;
        private const int PINKY_HOME = 310;
        private const int INKY = 400;
        private const int INKY_HOME = 410;
        private const int CLYDE = 500;
        private const int CLYDE_HOME = 510;

        private static Texture2D texture;
        private static int[,] level;
        private static int leftOffset;

        private static List<Wall> walls;
        private static List<Pellet> pellets;

        public static void Setup(Game gameMain)
        {
            game = gameMain;
        }

        public static void Init()
        {
            firstTime = true;
            leftOffset = 0;

            walls = new List<Wall>();
            pellets = new List<Pellet>();

            // 17(19) x 25
            int[,] level1 = new int[,] {
                {  4,      9,       6,  0,  8,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9, 10,  0,        4,       9,  6 },
                { 17, BLINKY, 17+DOOR,  0, 30, 30, 30, 30, 30, 30, 28, 30, 30, 30, 30, 30, 30,  0,  17+DOOR,   CLYDE, 17 },
                { 24, 9+DOOR,      26,  0,  8,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9, 10,  0,       24,  9+DOOR, 26 },
                {  0,      0,       0, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,        0,       0,  0 },

                {  7,     30,       7, 30,  4,  9, 10,  0,  8,  9,  5,  9, 10,  0,  8,  9,  6, 30,        7,      30,  7 },
                { 17,     30,      17, 30, 17, 30, 30, 30, 30, 30, 17, 30, 30, 30, 30, 30, 17, 30,       17,      30, 17 },
                { 17,     30,      17, 30, 27, 30,  4,  9, 10, 30, 27, 30,  8,  9,  6, 30, 27, 30,       17,      30, 17 },
                { 17,     30,      17, 30,  0, 30, 17, 30, 30, 30, 30, 30, 30, 30, 17, 30,  0, 30,       17,      30, 17 },
                { 17,     30,      17, 30,  7, 30, 27, 30,  4, 10,  0,  8,  6, 30, 27, 30,  7, 30,       17,      30, 17 },
                { 17,     30,      17, 30, 17, 30, 30, 30, 27,  0,  0,  0, 27, 30, 30, 30, 17, 30,       17,      30, 17 },
                
                { 17,     28,      17, 30, 14,  9, 10, 30,  0,  0, PACMAN,  0,  0, 30,  8,  9, 16, 30,       17,      28, 17 },

                { 17,     30,      17, 30, 17, 30, 30, 30,  7,  0,  0,  0,  7, 30, 30, 30, 17, 30,       17,      30, 17 },
                { 17,     30,      17, 30, 27, 30,  7, 30, 24, 10,  0,  8, 26, 30,  7, 30, 27, 30,       17,      30, 17 },
                { 17,     30,      17, 30,  0, 30, 17, 30, 30, 30, 30, 30, 30, 30, 17, 30,  0, 30,       17,      30, 17 },
                { 17,     30,      17, 30,  7, 30, 24,  9, 10, 30,  7, 30,  8,  9, 26, 30,  7, 30,       17,      30, 17 },
                { 17,     30,      17, 30, 17, 30, 30, 30, 30, 30, 17, 30, 30, 30, 30, 30, 17, 30,       17,      30, 17 },
                { 27,     30,      27, 30, 24,  9, 10,  0,  8,  9, 25,  9, 10,  0,  8,  9, 26, 30,       27,      30, 27 },

                {  0,      0,       0, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,        0,       0,  0 },
                {  4, 9+DOOR,       6,  0,  8,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9, 10,  0,        4,  9+DOOR,  6 },
                { 17,  PINKY, 17+DOOR,  0, 30, 30, 30, 30, 30, 30, 28, 30, 30, 30, 30, 30, 30,  0,  17+DOOR,    INKY, 17 },
                { 24,      9,      26,  0,  8,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9, 10,  0,       24,       9, 26 },
            };

            int[,] level2 = new int[,] {
                {  4,  9,  9,  9,  9,  5,  9,  9,  9,  9,  9,  9,  9,  9,  5,  9,  9,  9,  9,  6 },
                { 17, 28, 30, 30, 30, 17, 30, 30, 30, 30, 30, 30, 30, 30, 17, 30, 30, 30, 30, 17 },
                { 17, 30,  4, 10, 30, 27, 30,  8,  9,  9,  9,  9, 10, 30, 27, 30,  8,  6, 30, 17 },
                { 17, 30, 17, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 17, 30, 17 },
                
                { 17, 30, 27, 30,  8, 10, 30,  4,  9, 9+DOOR, 9+DOOR,  9,  6, 30,  8, 10, 30, 27, 30, 17 },

                { 17, 30, 30, 30, 30, 30, 30, 17, BLINKY, PINKY, INKY, CLYDE, 17, 30, 30, 30, 30, 30, 30, 17 },

                { 17, 30,  7, 30,  8, 10, 30, 24,  9,  9,  9,  9, 26, 30,  8, 10, 30,  7, 30, 17 },
                
                { 17, 30, 17, 30, 30, 30, 30,  0,  0,  0,  PACMAN,  0,  0, 30, 30, 30, 30, 17, 30, 17 },
                
                { 17, 30, 24, 10, 30,  7, 30,  8,  9,  9,  9,  9, 10, 30,  7, 30,  8, 26, 30, 17 },
                { 17, 30, 30, 30, 30, 17, 30, 30, 30, 30, 30, 30, 30, 30, 17, 30, 30, 30, 28, 17 },
                { 24,  9,  9,  9,  9, 25,  9,  9,  9,  9,  9,  9,  9,  9,  25, 9,  9,  9,  9, 26 },
            };

            level = level2;
        }

        public static void LoadContent()
        {
            texture = GameGraphics.Content.Load<Texture2D>(@"sprites\chompermazetiles");

            tileSprite = new Sprite(texture, 3, 10);
            tileSprite.animationAdd("idle", 0, 30, false, 0.0f);

            leftOffset = (int)(GameGraphics.graphics.PreferredBackBufferWidth / 2) - ((level.GetLength(1) * tileSprite.Width) / 2);

            LoadCharacterPosition();
        }

        private static void LoadCharacterPosition()
        {
            // Pacman Reset
            GameVars.Pacman.Location = Vector2.Zero;

            // Blinky Reset
            GameVars.Blinky.Location = Vector2.Zero;
            GameVars.Blinky.HomeLocation = Vector2.Zero;

            // Pinky Reset
            GameVars.Pinky.Location = Vector2.Zero;
            GameVars.Pinky.HomeLocation = Vector2.Zero;

            // Inky Reset
            GameVars.Inky.Location = Vector2.Zero;
            GameVars.Inky.HomeLocation = Vector2.Zero;

            // Clyde Reset
            GameVars.Clyde.Location = Vector2.Zero;
            GameVars.Clyde.HomeLocation = Vector2.Zero;

            for (int lin = 0; lin < level.GetLength(0); lin++)
            {
                for (int col = 0; col < level.GetLength(1); col++)
                {
                    int frame = level[lin, col];
                    Vector2 curLocation = new Vector2(leftOffset + col * tileSprite.Width, lin * tileSprite.Height);

                    if (frame == PACMAN)
                        GameVars.Pacman.Location = curLocation;
                    else if (frame == BLINKY_HOME)
                        GameVars.Blinky.HomeLocation = curLocation;
                    else if (frame == BLINKY)
                    {
                        GameVars.Blinky.Location = curLocation;
                        if (GameVars.Blinky.HomeLocation == Vector2.Zero)
                            GameVars.Blinky.HomeLocation = curLocation;
                    }
                    else if (frame == PINKY_HOME)
                        GameVars.Pinky.HomeLocation = curLocation;
                    else if (frame == PINKY)
                    {
                        GameVars.Pinky.Location = curLocation;
                        if (GameVars.Pinky.HomeLocation == Vector2.Zero)
                            GameVars.Pinky.HomeLocation = curLocation;
                    }
                    else if (frame == INKY_HOME)
                        GameVars.Inky.HomeLocation = curLocation;
                    else if (frame == INKY)
                    {
                        GameVars.Inky.Location = curLocation;
                        if (GameVars.Inky.HomeLocation == Vector2.Zero)
                            GameVars.Inky.HomeLocation = curLocation;
                    }
                    else if (frame == CLYDE_HOME)
                        GameVars.Clyde.HomeLocation = curLocation;
                    else if (frame == CLYDE)
                    {
                        GameVars.Clyde.Location = curLocation;
                        if (GameVars.Clyde.HomeLocation == Vector2.Zero)
                            GameVars.Clyde.HomeLocation = curLocation;
                    }
                    else
                    {
                        tileSprite.Update((frame > DOOR ? (frame - DOOR) : frame) - 1);
                        Sprite curSprite = new Sprite(texture, 3, 10);

                        if (frame < 28 || frame > DOOR)
                        {
                            // This will be treated as Walls
                            GameVars.WALL_TYPE wallType = (frame > DOOR ? GameVars.WALL_TYPE.DOOR : GameVars.WALL_TYPE.NORMAL);
                            frame = (frame > DOOR ? frame - DOOR : frame) - 1;

                            curSprite.animationAdd("idle", frame, frame, false, 0.0f);

                            walls.Add(new Wall(curSprite, curLocation, wallType));
                        }
                        else
                        {
                            // This will be treated as Pellets/Power-Pellets
                            GameVars.PELLET_TYPE pelletType = (frame == 30 ? GameVars.PELLET_TYPE.NORMAL : GameVars.PELLET_TYPE.POWER);
                            frame--;

                            pellets.Add(new Pellet(curSprite, curLocation, frame, pelletType));
                        }
                    }
                }
            }
        }

        public static void Update()
        {
            if (firstTime)
            {
                firstTime = false;
                tileSprite.animationPlay("idle");
            }

            foreach(var wall in walls)
            {
                wall.Update();
            }

            foreach(var pellet in pellets)
            {
                pellet.Update();
            }
        }

        public static void Draw()
        {
            foreach(var wall in walls)
            {
                wall.Draw();
            }

            foreach(var pellet in pellets)
            {
                pellet.Draw();
            }
            /*
            for(int lin=0; lin<level.GetLength(0); lin++)
            {
                for(int col=0; col<level.GetLength(1); col++)
                {
                    int frame = level[lin, col];
                    tileSprite.Update( (frame > DOOR ? (frame - DOOR) : frame) - 1 );
                    Vector2 tilePos = new Vector2(leftOffset + col * tileSprite.Width, lin * tileSprite.Height);


                    if (frame > DOOR)
                        tileSprite.Draw(tilePos, Color.SlateGray );
                    else
                        tileSprite.Draw(tilePos);
                }
            }
            */
        }
    }
}
