
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.GameObjects;
using Pacman_IA.Sprites;
using System;
using System.Collections.Generic;

namespace Pacman_IA.Classes
{
    public static class GameMap
    {
        private static Game game;
        private static Sprite tileSprite;
        private static bool firstTime;

        private static Texture2D texture;
        private static List<GameVars.MAP_ITEM>[,] level;
        public static object[,] objLevel;
        public static int[,] pelletLevel;
        public static int[,] wallLevel;
        public static Vector2[,] posLevel;
        private static int leftOffset;

        public static List<Wall> walls;
        public static List<Pellet> pellets;

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

            List<GameVars.MAP_ITEM> Empty = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.EMPTY };

            List<GameVars.MAP_ITEM> WallCloseTopLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_TOP_LEFT };
            List<GameVars.MAP_ITEM> WallCloseTop = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_TOP };
            List<GameVars.MAP_ITEM> WallCloseTopRiht = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_TOP_RIGHT };
            List<GameVars.MAP_ITEM> WallCloseLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_LEFT };
            List<GameVars.MAP_ITEM> WallClose = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE };
            List<GameVars.MAP_ITEM> WallCloseRight = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_RIGHT };
            List<GameVars.MAP_ITEM> WallCloseBottomLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_BOTTOM_LEFT };
            List<GameVars.MAP_ITEM> WallCloseBottom = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_BOTTOM };
            List<GameVars.MAP_ITEM> WallCloseBottomRight = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_CLOSE_BOTTOM_RIGHT };

            List<GameVars.MAP_ITEM> WallOpenTopLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_TOP_LEFT };
            List<GameVars.MAP_ITEM> WallOpenTop = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_TOP };
            List<GameVars.MAP_ITEM> WallOpenTopRight = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_TOP_RIGHT };

            List<GameVars.MAP_ITEM> WallOpenLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_LEFT };

            List<GameVars.MAP_ITEM> WallOpen = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN };
            List<GameVars.MAP_ITEM> WallOpenRight = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_RIGHT };
            List<GameVars.MAP_ITEM> WallOpenBottomLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_BOTTOM_LEFT };
            List<GameVars.MAP_ITEM> WallOpenBottom = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_BOTTOM };
            List<GameVars.MAP_ITEM> WallOpenBottomRight = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_OPEN_BOTTOM_RIGHT };

            List<GameVars.MAP_ITEM> WallVertTop = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT_TOP };
            List<GameVars.MAP_ITEM> WallVert = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT };
            List<GameVars.MAP_ITEM> WallVertBottom = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT_BOTTOM };

            List<GameVars.MAP_ITEM> WallHorizLeft = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ_LEFT };
            List<GameVars.MAP_ITEM> WallHoriz = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ };
            List<GameVars.MAP_ITEM> WallHorizRight = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ_RIGHT };

            List<GameVars.MAP_ITEM> Wall = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL };

            List<GameVars.MAP_ITEM> Pellet = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PELLET };
            List<GameVars.MAP_ITEM> PowerPellet = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.POWER_PELLET };

            List<GameVars.MAP_ITEM> Door = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.DOOR };

            List<GameVars.MAP_ITEM> Pacman = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PACMAN };

            List<GameVars.MAP_ITEM> Blinky = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.BLINKY };
            List<GameVars.MAP_ITEM> BlinkyHome = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.BLINKY_HOME };
            List<GameVars.MAP_ITEM> Pinky = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PINKY };
            List<GameVars.MAP_ITEM> PinkyHome = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PINKY_HOME };
            List<GameVars.MAP_ITEM> Inky = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.INKY };
            List<GameVars.MAP_ITEM> InkyHome = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.INKY_HOME };
            List<GameVars.MAP_ITEM> Clyde = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.CLYDE };
            List<GameVars.MAP_ITEM> ClydeHome = new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.CLYDE_HOME };

            // 17(19) x 25
            List<GameVars.MAP_ITEM>[,] level1 = new List<GameVars.MAP_ITEM>[,] {
                {        WallOpenTop,      WallHoriz,    WallOpenTopRight,  Empty,      WallHorizLeft, WallHoriz,          WallHoriz, WallHoriz,          WallHoriz,      WallHoriz,      WallHoriz,     WallHoriz,           WallHoriz, WallHoriz,           WallHoriz, WallHoriz,      WallHorizRight,  Empty,    WallOpenTopLeft,      WallHoriz,    WallOpenTopRight },
                {           WallVert,         Blinky,       new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.DOOR },  Empty,             Pellet,    Pellet,             Pellet,    Pellet,             Pellet,         Pellet,    PowerPellet,        Pellet,              Pellet,    Pellet,              Pellet,    Pellet,              Pellet,  Empty,      new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.DOOR },          Clyde,            WallVert },
                { WallOpenBottomLeft, new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ, GameVars.MAP_ITEM.DOOR }, WallOpenBottomRight,  Empty,      WallHorizLeft, WallHoriz,          WallHoriz, WallHoriz,          WallHoriz,      WallHoriz,      WallHoriz,     WallHoriz,           WallHoriz, WallHoriz,           WallHoriz, WallHoriz,      WallHorizRight,  Empty, WallOpenBottomLeft, new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ, GameVars.MAP_ITEM.DOOR }, WallOpenBottomRight },
                {              Empty,          Empty,               Empty, Pellet,             Pellet,    Pellet,             Pellet,    Pellet,             Pellet,         Pellet,         Pellet,        Pellet,              Pellet,    Pellet,              Pellet,    Pellet,              Pellet, Pellet,              Empty,          Empty,               Empty },

                {        WallVertTop,         Pellet,         WallVertTop, Pellet,    WallOpenTopLeft, WallHoriz,     WallHorizRight,     Empty,      WallHorizLeft,      WallHoriz,    WallOpenTop,     WallHoriz,      WallHorizRight,     Empty,       WallHorizLeft, WallHoriz,    WallOpenTopRight, Pellet,        WallVertTop,         Pellet,         WallVertTop },
                {           WallVert,         Pellet,            WallVert, Pellet,           WallVert,    Pellet,             Pellet,    Pellet,             Pellet,         Pellet,       WallVert,        Pellet,              Pellet,    Pellet,              Pellet,    Pellet,            WallVert, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,     WallVertBottom,    Pellet,    WallOpenTopLeft, WallHoriz,     WallHorizRight,         Pellet, WallVertBottom,        Pellet,       WallHorizLeft, WallHoriz,    WallOpenTopRight,    Pellet,      WallVertBottom, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,              Empty,    Pellet,           WallVert,    Pellet,             Pellet,         Pellet,         Pellet,        Pellet,              Pellet,    Pellet,            WallVert,    Pellet,               Empty, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,        WallVertTop,    Pellet,     WallVertBottom,    Pellet,    WallOpenTopLeft, WallHorizRight,          Empty, WallHorizLeft,    WallOpenTopRight,    Pellet,      WallVertBottom,    Pellet,         WallVertTop, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,           WallVert,    Pellet,             Pellet,    Pellet,     WallVertBottom,          Empty,          Empty,         Empty,      WallVertBottom,    Pellet,              Pellet,    Pellet,            WallVert, Pellet,           WallVert,         Pellet,            WallVert },
                
                {           WallVert,    PowerPellet,            WallVert, Pellet,       WallOpenLeft, WallHoriz,     WallHorizRight,    Pellet,              Empty,          Empty,         Pacman,         Empty,               Empty,    Pellet,       WallHorizLeft, WallHoriz,       WallOpenRight, Pellet,           WallVert,    PowerPellet,            WallVert },

                {           WallVert,         Pellet,            WallVert, Pellet,           WallVert,    Pellet,             Pellet,    Pellet,        WallVertTop,          Empty,          Empty,         Empty,         WallVertTop,    Pellet,              Pellet,    Pellet,            WallVert, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,     WallVertBottom,    Pellet,        WallVertTop,    Pellet, WallOpenBottomLeft, WallHorizRight,          Empty, WallHorizLeft, WallOpenBottomRight,    Pellet,         WallVertTop,    Pellet,      WallVertBottom, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,              Empty,    Pellet,           WallVert,    Pellet,             Pellet,         Pellet,         Pellet,        Pellet,              Pellet,    Pellet,            WallVert,    Pellet,               Empty, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,        WallVertTop,    Pellet, WallOpenBottomLeft, WallHoriz,     WallHorizRight,         Pellet,    WallVertTop,        Pellet,       WallHorizLeft, WallHoriz, WallOpenBottomRight,    Pellet,         WallVertTop, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,            WallVert, Pellet,           WallVert,    Pellet,             Pellet,    Pellet,             Pellet,         Pellet,       WallVert,        Pellet,              Pellet,    Pellet,              Pellet,    Pellet,            WallVert, Pellet,           WallVert,         Pellet,            WallVert },
                {           WallVert,         Pellet,      WallVertBottom, Pellet, WallOpenBottomLeft, WallHoriz,     WallHorizRight,     Empty,      WallHorizLeft,      WallHoriz, WallOpenBottom,     WallHoriz,      WallHorizRight,     Empty,       WallHorizLeft, WallHoriz, WallOpenBottomRight, Pellet,     WallVertBottom,         Pellet,      WallVertBottom },

                {              Empty,          Empty,               Empty, Pellet,             Pellet,    Pellet,             Pellet,    Pellet,             Pellet,         Pellet,         Pellet,        Pellet,              Pellet,    Pellet,              Pellet,    Pellet,              Pellet, Pellet,              Empty,          Empty,               Empty },
                {    WallOpenTopLeft, new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ, GameVars.MAP_ITEM.DOOR },    WallOpenTopRight,  Empty,      WallHorizLeft, WallHoriz,          WallHoriz, WallHoriz,          WallHoriz,      WallHoriz,      WallHoriz,     WallHoriz,           WallHoriz, WallHoriz,           WallHoriz, WallHoriz,      WallHorizRight,  Empty,    WallOpenTopLeft, new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ, GameVars.MAP_ITEM.DOOR },    WallOpenTopRight },
                {           WallVert,          Pinky,       new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.DOOR },  Empty,             Pellet,    Pellet,             Pellet,    Pellet,             Pellet,         Pellet,    PowerPellet,        Pellet,              Pellet,    Pellet,              Pellet,    Pellet,              Pellet,  Empty,      new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.DOOR },           Inky,            WallVert },
                { WallOpenBottomLeft,      WallHoriz, WallOpenBottomRight,  Empty,      WallHorizLeft, WallHoriz,          WallHoriz, WallHoriz,          WallHoriz,      WallHoriz,      WallHoriz,     WallHoriz,           WallHoriz, WallHoriz,           WallHoriz, WallHoriz,      WallHorizRight,  Empty, WallOpenBottomLeft,      WallHoriz, WallOpenBottomRight },
            };
            
            List<GameVars.MAP_ITEM>[,] level2 = new List<GameVars.MAP_ITEM>[,] {
                {    WallOpenTopLeft,   WallHoriz,          WallHoriz,      WallHoriz,     WallHoriz,    WallOpenTop, WallHoriz,          WallHoriz, WallHoriz,      WallHoriz,      WallHoriz, WallHoriz,           WallHoriz, WallHoriz,    WallOpenTop,      WallHoriz,     WallHoriz,           WallHoriz,   WallHoriz,    WallOpenTopRight },
                {           new List<GameVars.MAP_ITEM> {GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.PINKY_HOME}, PowerPellet,             Pellet,         Pellet,        Pellet,       WallVert,    Pellet,             Pellet,    Pellet,         Pellet,         Pellet,    Pellet,              Pellet,    Pellet,       WallVert,         Pellet,        Pellet,              Pellet,      Pellet,            WallVert },
                {           WallVert,      Pellet,    WallOpenTopLeft, WallHorizRight,        Pellet, WallVertBottom,    Pellet,      WallHorizLeft, WallHoriz,      WallHoriz,      WallHoriz, WallHoriz,      WallHorizRight,    Pellet, WallVertBottom,         Pellet, WallHorizLeft,    WallOpenTopRight,      Pellet,            new List<GameVars.MAP_ITEM> {GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.BLINKY_HOME } },
                {           WallVert,      Pellet,           WallVert,         Pellet,        Pellet,         Pellet,    new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PELLET, GameVars.MAP_ITEM.INKY_PATROL },             Pellet,    Pellet,         Pellet,         Pellet,    Pellet,              Pellet,    Pellet,         Pellet,         Pellet,        new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PELLET, GameVars.MAP_ITEM.INKY_PATROL },            WallVert,      Pellet,            WallVert },
                {           WallVert,      Pellet,     WallVertBottom,         Pellet, WallHorizLeft, WallHorizRight,    Pellet,    WallOpenTopLeft, WallHoriz, new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ, GameVars.MAP_ITEM.DOOR }, new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.WALL_HORIZ, GameVars.MAP_ITEM.DOOR }, WallHoriz,    WallOpenTopRight,    Pellet,  WallHorizLeft, WallHorizRight,        Pellet,      WallVertBottom,      Pellet,            WallVert },
                {           WallVert,      Pellet,             Pellet,         Pellet,        Pellet,         Pellet,    Pellet,           WallVert,    Blinky,          Pinky,           Inky,     Clyde,            WallVert,    Pellet,         Pellet,         Pellet,        Pellet,              Pellet,      Pellet,            WallVert },

                {           WallVert,      Pellet,        WallVertTop,         Pellet, WallHorizLeft, WallHorizRight,    Pellet, WallOpenBottomLeft, WallHoriz,      WallHoriz,      WallHoriz, WallHoriz, WallOpenBottomRight,    Pellet,  WallHorizLeft, WallHorizRight,        Pellet,         WallVertTop,      Pellet,            WallVert },

                {           WallVert,      Pellet,           WallVert,         Pellet,        Pellet,         Pellet,    new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PELLET, GameVars.MAP_ITEM.INKY_PATROL },              Empty,     Empty,          Empty,         Pacman,     Empty,               Empty,    Pellet,         Pellet,         Pellet,        new List<GameVars.MAP_ITEM> { GameVars.MAP_ITEM.PELLET, GameVars.MAP_ITEM.INKY_PATROL },            WallVert,      Pellet,            WallVert },

                {           WallVert,      Pellet, WallOpenBottomLeft, WallHorizRight,        Pellet,    WallVertTop,    Pellet,      WallHorizLeft, WallHoriz,      WallHoriz,      WallHoriz, WallHoriz,      WallHorizRight,    Pellet,    WallVertTop,         Pellet, WallHorizLeft, WallOpenBottomRight,      Pellet,            WallVert },
                {           WallVert,      new List<GameVars.MAP_ITEM> {GameVars.MAP_ITEM.PELLET, GameVars.MAP_ITEM.CLYDE_HOME },             Pellet,         Pellet,        Pellet,       WallVert,    Pellet,             Pellet,    Pellet,         Pellet,         Pellet,    Pellet,              Pellet,    Pellet,       WallVert,         Pellet,        Pellet,              Pellet, PowerPellet,            new List<GameVars.MAP_ITEM> {GameVars.MAP_ITEM.WALL_VERT, GameVars.MAP_ITEM.INKY_HOME } },
                { WallOpenBottomLeft,   WallHoriz,          WallHoriz,      WallHoriz,     WallHoriz, WallOpenBottom, WallHoriz,          WallHoriz, WallHoriz,      WallHoriz,      WallHoriz, WallHoriz,           WallHoriz, WallHoriz, WallOpenBottom,      WallHoriz,     WallHoriz,           WallHoriz,   WallHoriz, WallOpenBottomRight },
            };

            level = level2;
            objLevel = new object[level.GetLength(0), level.GetLength(1)];
            posLevel = new Vector2[level.GetLength(0), level.GetLength(1)];
        }

        public static void LoadContent()
        {
            texture = GameGraphics.Content.Load<Texture2D>(@"sprites\chompermazetiles");

            tileSprite = new Sprite(texture, 3, 10);
            tileSprite.animationAdd("idle", 0, 30, false, 0.0f);

            leftOffset = (int)(GameGraphics.graphics.PreferredBackBufferWidth / 2) - ((level.GetLength(1) * tileSprite.Width) / 2);

            LoadCharacterPosition();
        }

        public static List<Vector2> getGridLocation(Vector2 location)
        {
            List<Vector2> gridLocation = new List<Vector2>();
            float locX = location.X - leftOffset;
            int Y = (int)Math.Ceiling(location.Y / 32);

            int floorY = (int)Math.Floor(location.Y / 32);
            int ceilY = (int)Math.Ceiling(location.Y / 32);

            int floorX = (int)Math.Floor(locX / 32);
            int ceilX = (int)Math.Ceiling(locX / 32);

            gridLocation.Add(new Vector2((float)Math.Floor(locX / 32), (float)Math.Floor(location.Y / 32)));
            if (locX % 32 > 0)
            {
                if (location.Y % 32 > 0 && Y < objLevel.GetLength(1))
                    gridLocation.Add(new Vector2((float)Math.Ceiling(locX / 32), (float)Math.Ceiling(location.Y / 32)) );
                else
                    gridLocation.Add(new Vector2((float)Math.Ceiling(locX / 32), (float)Math.Floor(location.Y / 32)) );
            }
            else if (location.Y % 32 > 0 && Y < objLevel.GetLength(1))
            {
                gridLocation.Add(new Vector2((float)Math.Floor(locX / 32), (float)Math.Ceiling(location.Y / 32)) );
            }

            return gridLocation;
        }

        private static void LoadCharacterPosition()
        {
            // Pacman Reset
            GameVars.Pacman.Location = Vector2.Zero;
            GameVars.Pacman.InSpawn = false;

            // Blinky Reset
            GameVars.Blinky.Location = Vector2.Zero;
            GameVars.Blinky.HomeLocation = Vector2.Zero;
            GameVars.Blinky.InSpawn = true;

            // Pinky Reset
            GameVars.Pinky.Location = Vector2.Zero;
            GameVars.Pinky.HomeLocation = Vector2.Zero;
            GameVars.Pinky.InSpawn = true;

            // Inky Reset
            GameVars.Inky.Location = Vector2.Zero;
            GameVars.Inky.HomeLocation = Vector2.Zero;
            GameVars.Inky.InSpawn = true;

            // Clyde Reset
            GameVars.Clyde.Location = Vector2.Zero;
            GameVars.Clyde.HomeLocation = Vector2.Zero;
            GameVars.Clyde.InSpawn = true;

            pelletLevel = new int[level.GetLength(0), level.GetLength(1)];
            wallLevel = new int[level.GetLength(0), level.GetLength(1)];
            objLevel = new object[level.GetLength(0), level.GetLength(1)];
            walls = new List<Wall>();
            pellets = new List<Pellet>();

            for (int lin = 0; lin < level.GetLength(0); lin++)
            {
                for (int col = 0; col < level.GetLength(1); col++)
                {
                    List<GameVars.MAP_ITEM> curItem = level[lin, col];
                    Vector2 curLocation = new Vector2(leftOffset + col * tileSprite.Width, lin * tileSprite.Height);
                    posLevel[lin, col] = curLocation;

                    pelletLevel[lin, col] = -1;
                    wallLevel[lin, col] = -1;



                    if (curItem.Contains(GameVars.MAP_ITEM.PACMAN))
                    {
                        GameVars.Pacman.SetLocation(curLocation);
                        curItem.Remove(GameVars.MAP_ITEM.PACMAN);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.BLINKY_HOME))
                    {
                        GameVars.Blinky.HomeLocation = curLocation;
                        curItem.Remove(GameVars.MAP_ITEM.BLINKY_HOME);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.BLINKY))
                    {
                        GameVars.Blinky.SetLocation(curLocation);
                        curItem.Remove(GameVars.MAP_ITEM.BLINKY);

                        if (GameVars.Blinky.HomeLocation == Vector2.Zero)
                            GameVars.Blinky.HomeLocation = curLocation;
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.PINKY_HOME))
                    {
                        GameVars.Pinky.HomeLocation = curLocation;
                        curItem.Remove(GameVars.MAP_ITEM.PINKY_HOME);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.PINKY))
                    {
                        GameVars.Pinky.SetLocation(curLocation);
                        curItem.Remove(GameVars.MAP_ITEM.PINKY);

                        if (GameVars.Pinky.HomeLocation == Vector2.Zero)
                            GameVars.Pinky.HomeLocation = curLocation;
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.INKY_HOME))
                    {
                        GameVars.Inky.HomeLocation = curLocation;
                        curItem.Remove(GameVars.MAP_ITEM.INKY_HOME);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.INKY))
                    {
                        GameVars.Inky.SetLocation(curLocation);
                        curItem.Remove(GameVars.MAP_ITEM.INKY);

                        if (GameVars.Inky.HomeLocation == Vector2.Zero)
                            GameVars.Inky.HomeLocation = curLocation;
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.INKY_PATROL))
                    {
                        GameVars.Inky.PatrolLocation.Add(curLocation);
                        curItem.Remove(GameVars.MAP_ITEM.INKY_PATROL);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.CLYDE_HOME))
                    {
                        GameVars.Clyde.HomeLocation = curLocation;
                        curItem.Remove(GameVars.MAP_ITEM.CLYDE_HOME);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.CLYDE))
                    {
                        GameVars.Clyde.SetLocation(curLocation);
                        curItem.Remove(GameVars.MAP_ITEM.CLYDE);

                        if (GameVars.Clyde.HomeLocation == Vector2.Zero)
                            GameVars.Clyde.HomeLocation = curLocation;
                    }

                    if (curItem.Count == 0)
                        curItem.Add(GameVars.MAP_ITEM.EMPTY);

                    int curFrame = 0;
                    Sprite curSprite = new Sprite(texture, 3, 10);
                    bool updateSprite = false;

                    if (curItem.Contains(GameVars.MAP_ITEM.PELLET) || curItem.Contains(GameVars.MAP_ITEM.POWER_PELLET))
                    {
                        updateSprite = true;

                        // This will be treated as Pellets/Power-Pellets
                        GameVars.PELLET_TYPE pelletType = curItem.Contains(GameVars.MAP_ITEM.PELLET)
                                                            ? GameVars.PELLET_TYPE.NORMAL
                                                            : GameVars.PELLET_TYPE.POWER;

                        if (pelletType == GameVars.PELLET_TYPE.NORMAL)
                            curFrame = (int)GameVars.MAP_ITEM.PELLET;
                        else
                            curFrame = (int)GameVars.MAP_ITEM.POWER_PELLET;
                        curFrame--;

                        pellets.Add(new Pellet(curSprite, curLocation, curFrame, pelletType));
                        pelletLevel[lin, col] = (pelletType == GameVars.PELLET_TYPE.NORMAL ? 10 : 50);
                    }
                    else if (curItem.Contains(GameVars.MAP_ITEM.DOOR))
                    {
                        updateSprite = true;

                        // This will be treated as Wall/Door
                        GameVars.WALL_TYPE wallType = GameVars.WALL_TYPE.DOOR;

                        curItem.Remove(GameVars.MAP_ITEM.DOOR);
                        curFrame = (int)curItem[0] - 1;

                        curSprite.animationAdd("idle", curFrame, curFrame, false, 0.0f);

                        walls.Add(new Wall(curSprite, curLocation, wallType));
                        wallLevel[lin, col] = -2;
                    }
                    else if (!curItem.Contains(GameVars.MAP_ITEM.EMPTY))
                    {
                        updateSprite = true;

                        // This will be treated as Walls
                        GameVars.WALL_TYPE wallType = GameVars.WALL_TYPE.NORMAL;
                        curFrame = (int)curItem[0] - 1;

                        curSprite.animationAdd("idle", curFrame, curFrame, false, 0.0f);

                        walls.Add(new Wall(curSprite, curLocation, wallType));
                        wallLevel[lin, col] = walls.Count - 1;
                    }

                    if (updateSprite)
                        tileSprite.Update(curFrame);
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
            pellets.RemoveAll(a => a.removed);
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

        }
    }
}
