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
    public class ChaseAmbush : ChaseBehaviour
    {
        private Vector2 pacmanLastPosition;
        private bool pacmanLocated;

        public ChaseAmbush(Character person) : base(person)
        {
            pacmanLastPosition = GameVars.DIR.INVALID;
            pacmanLocated = false;
        }

        public override Dictionary<string, int> Chase(Vector2 lastDirection)
        {
            bool pacLocated = pacmanLocated;
            Dictionary<string, int> dirWeights = base.Chase(lastDirection);

            if (pacmanLocated && !pacLocated)
            {
                if (ghost.GridLocation == pacmanLastPosition)
                {
                    pacmanLocated = false;
                }
                else
                {
                    // Pacman located! Keep trying to ambush Pacman.
                    ScatterBehaviour scatter = new ScatterBehaviour(ghost, pacmanLastPosition);

                    dirWeights = scatter.Scatter(lastDirection);
                }
            }

            return dirWeights;
        }

        protected override int checkChase(Point direction)
        {
            int retval = 0;
            int lin = ghost.GridLocation.ToPoint().Y;
            int col = ghost.GridLocation.ToPoint().X;

            if (GameMap.wallLevel[lin + direction.Y, col + direction.X] == -1)
            {
                // Not blocked
                if (pacmanLastPosition == GameVars.DIR.INVALID)
                    pacmanLocated = false;

                if (pacmanLocated)
                {
                    // Pacman located
                    if (!ghost.GridLocation.Equals(pacmanLastPosition))
                    {
                        // Pacman located! Keep trying to ambush Pacman.
                        ScatterBehaviour scatter = new ScatterBehaviour(ghost, pacmanLastPosition);

                        retval = scatter.checkScatter(direction);
                    }
                }

                if (!pacmanLocated || ghost.GridLocation.Equals(pacmanLastPosition))
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
                            retval = 4000;
                            pacmanLocated = true;
                            pacmanLastPosition = posCheck;

                            // Search for Pacman food
                            if (GameMap.pelletLevel[lin, col] != -1)
                            {
                                hasFood = true;
                                totalFood += GameMap.pelletLevel[lin, col];
                            }

                            if (GameVars.Pacman.Direction != new Vector2(direction.X * -1, direction.Y * -1))
                            {
                                // Pacman is not running in your direction
                                // So we will try to ambush him
                                pacmanLastPosition = posCheck;
                            }
                        }

                        // Update scanning position
                        tmpLin += direction.Y;
                        tmpCol += direction.X;
                    }

                    if (!pacmanLocated)
                    {
                        // Pacman was not located
                        pacmanLastPosition = GameVars.DIR.INVALID;

                        // Does it have food?
                        if (hasFood)
                        {
                            // Has food / PowerPellet
                            retval += totalFood;
                        }
                    }
                }
            }
            else
            {
                // Blocked
                retval = -1;
            }

            return retval;
        }
    }
}
