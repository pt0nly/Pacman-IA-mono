using Microsoft.Xna.Framework;
using Pacman_IA.GameObjects;

namespace Pacman_IA.Classes
{
    public static class GameVars
    {
        public const float COLLISION_DISTANCE = 20.0f;

        public const int STARTING_LIVES = 3;

        public static Pacman Pacman;
        public static Blinky Blinky;
        public static Pinky Pinky;
        public static Inky Inky;
        public static Clyde Clyde;

        public static Player Player;

        public static GameTime gameTime;
    }
}
