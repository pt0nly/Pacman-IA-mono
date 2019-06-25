using Microsoft.Xna.Framework;
using Pacman_IA.GameObjects;

namespace Pacman_IA.Classes
{
    public static class GameVars
    {
        public const int STARTING_LIVES = 3;
        public static bool Game_Loaded = false;

        public static Pacman Pacman;
        public static Blinky Blinky;
        public static Pinky Pinky;
        public static Inky Inky;
        public static Clyde Clyde;

        public static GameTime gameTime;

        public static string MSG1 = "";
        public static string MSG2 = "";
        public static string MSG3 = "";
        public static string MSG4 = "";

        public static class DIR
        {
            public static Vector2 LEFT = new Vector2(-1, 0);
            public static Vector2 RIGHT = new Vector2(1, 0);
            public static Vector2 DOWN = new Vector2(0, 1);
            public static Vector2 UP = new Vector2(0, -1);

            public static Vector2 INVALID = new Vector2(-1, -1);
            public static Vector2 EMPTY = Vector2.Zero;
        }

        public enum WALL_TYPE
        {
            NORMAL = 0,
            DOOR = 100
        };

        public enum PELLET_TYPE
        {
            NORMAL = 0,
            POWER = 2
        }

        public enum MAP_ITEM
        {
            EMPTY = 0,

            WALL_CLOSE_TOP_LEFT = 1,
            WALL_CLOSE_TOP = 2,
            WALL_CLOSE_TOP_RIGHT = 3,
            WALL_CLOSE_LEFT = 11,
            WALL_CLOSE = 12,
            WALL_CLOSE_RIGHT = 13,
            WALL_CLOSE_BOTTOM_LEFT = 21,
            WALL_CLOSE_BOTTOM = 22,
            WALL_CLOSE_BOTTOM_RIGHT = 23,

            WALL_OPEN_TOP_LEFT = 4,
            WALL_OPEN_TOP = 5,
            WALL_OPEN_TOP_RIGHT = 6,
            WALL_OPEN_LEFT = 14,
            WALL_OPEN = 15,
            WALL_OPEN_RIGHT = 16,
            WALL_OPEN_BOTTOM_LEFT = 24,
            WALL_OPEN_BOTTOM = 25,
            WALL_OPEN_BOTTOM_RIGHT = 26,

            WALL_VERT_TOP = 7,
            WALL_VERT = 17,
            WALL_VERT_BOTTOM = 27,

            WALL_HORIZ_LEFT = 8,
            WALL_HORIZ = 9,
            WALL_HORIZ_RIGHT = 10,

            WALL = 18,

            PELLET = 30,
            POWER_PELLET = 28,


            DOOR = 1000,

            PACMAN = 100,

            BLINKY = 200,
            BLINKY_HOME = 210,

            PINKY = 300,
            PINKY_HOME = 310,

            INKY = 400,
            INKY_HOME = 410,
            INKY_PATROL = 420,

            CLYDE = 500,
            CLYDE_HOME = 510
        }

        public enum GHOST_MODE
        {
            CHASE,
            SCATTER,
            WANDER
        }

        public enum PACMAN_MODE
        {
            EAT,
            HUNT
        }

    }
}
