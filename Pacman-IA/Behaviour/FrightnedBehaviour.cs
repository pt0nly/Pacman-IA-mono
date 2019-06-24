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
            Dictionary<string, int> dirWeight = new Dictionary<string, int>();

            // Calculate direction weights
            dirWeight.Add("left", checkWandering(GameVars.DIR.LEFT.ToPoint()));
            dirWeight.Add("right", checkWandering(GameVars.DIR.RIGHT.ToPoint()));
            dirWeight.Add("down", checkWandering(GameVars.DIR.DOWN.ToPoint()));
            dirWeight.Add("up", checkWandering(GameVars.DIR.UP.ToPoint()));

            return dirWeight;
        }

        private int checkWandering(Point direction)
        {
           int retval = 0;

           if (!modeStarted)
           {
               modeStarted = true;
               timer = 20;
               ghost.Speed = new Vector2(7);
               // Color = darkBlue;
               retval = -1;
           }
           else
           {
               int lin = ghost.GridLocation.ToPoint().Y;
               int col = ghost.GridLocation.ToPoint().X;

               if (GameMap.wallLevel[lin + direction.Y, col + direction.X] != -1)
               {
                   // Blocked
                   retval = -1;
               }

               if (timer <= 0)
               {
                   //ghost.mode = prevMode;
                   modeStarted = false;
                   ghost.Speed = new Vector2(10);
                   //Color = Pink
               }
               else
               {
                   // Don't know where pacman is
                   int tmpLin = lin;
                   int tmpCol = col;
                   bool hasFood = false;
                   int totalFood = 0;
                   int xSize = GameMap.wallLevel.GetLength(1);
                   int ySize = GameMap.wallLevel.GetLength(0);

                   // Scan selected direction (-1 means no wall)
                   while (tmpLin >= 0 && tmpLin < (ySize - 1)
                       && tmpCol >= 0 && tmpCol < (xSize - 1)
                       && GameMap.wallLevel[tmpLin, tmpCol] == -1
                   )
                   {
                       Vector2 posCheck = new Vector2(tmpCol, tmpLin);

                       if (GameVars.Pacman.GridLocation == posCheck)
                       {
                           // Found Pacman!
                           retval -= 4000;
                       }

                       // Update scanning position
                       tmpLin += direction.Y;
                       tmpCol += direction.X;
                   }
               }
           }

           timer--;

           return retval;
        }
    }
}
