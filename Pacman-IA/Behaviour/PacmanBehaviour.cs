using Microsoft.Xna.Framework;
using Pacman_IA.Classes;
using Pacman_IA.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IA.Behaviour
{
    public class PacmanBehaviour : IBehaviour
    {
        private Pacman pacman = null;
        private GameVars.PACMAN_MODE pacmanMode;

        private EatBehaviour eatBehaviour = null;
        private HuntBehaviour huntBehaviour = null;

        public PacmanBehaviour(Pacman person, EatBehaviour eat, HuntBehaviour hunt)
        {
            pacman = person;

            // Set Pacman's behaviours
            eatBehaviour = eat;
            huntBehaviour = hunt;

            pacmanMode = GameVars.PACMAN_MODE.EAT;
        }

        public void Behave(Vector2 lastDirection)
        {
            // Check if we can continue on this direction
            if (lastDirection == GameVars.DIR.EMPTY)
            {
                Random rndDir = new Random();
                int choice = rndDir.Next(1, 4);

                if (choice == 1)
                {
                    // Go LEFT
                    lastDirection = GameVars.DIR.LEFT;
                }
                else if (choice == 2)
                {
                    // Go RIGHT
                    lastDirection = GameVars.DIR.RIGHT;
                }
                else if (choice == 3)
                {
                    // Go DOWN
                    lastDirection = GameVars.DIR.DOWN;
                }
                else
                {
                    // Go UP
                    lastDirection = GameVars.DIR.UP;
                }
            }

            if (pacman.PowerUp > 0.0f)
            {
                // Energized
                pacmanMode = GameVars.PACMAN_MODE.HUNT;
            }
            else
            {
                // Not Energized
                pacmanMode = GameVars.PACMAN_MODE.EAT;
            }


            if (pacmanMode == GameVars.PACMAN_MODE.EAT)
                eatBehaviour.Eat(lastDirection);
            else
                huntBehaviour.Hunt(lastDirection);
        }
    }
}
