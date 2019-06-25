using Microsoft.Xna.Framework;
using Pacman_IA.GameObjects;
using System.Collections.Generic;

namespace Pacman_IA.Behaviour
{
    public class FrightnedBehaviour : IFrightnedBehaviour
    {
        private Character ghost = null;
        private float timer = 20;
        private bool modeStarted = false;

        public FrightnedBehaviour(Character person)
        {
            ghost = person;

            timer = 20;
            modeStarted = false;
        }

        public Dictionary<string, int> Wander(Vector2 lastDirection)
        {
            ScatterBehaviour rand = new ScatterBehaviour(ghost, ghost.HomeLocation);
            Dictionary<string, int> dirWeight = rand.Scatter(lastDirection);

            return dirWeight;
        }
    }
}
