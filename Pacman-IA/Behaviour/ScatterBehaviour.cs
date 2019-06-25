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
    public class ScatterBehaviour : IScatterBehaviour
    {
        private Vector2 destination;
        private Character ghost = null;
        private int loseSpawnLine = -1;
        private int loseSpawnCol = -1;

        public ScatterBehaviour(Character person, Vector2 destLocation)
        {
            ghost = person;
            destination = destLocation;

            loseSpawnLine = -1;
            loseSpawnCol = -1;
        }

        public Dictionary<string, int> Scatter(Vector2 lastDirection)
        {
            Dictionary<string, int> dirWeights = new Dictionary<string, int>();
            dirWeights["left"] = checkScatter(GameVars.DIR.LEFT.ToPoint());
            dirWeights["right"] = checkScatter(GameVars.DIR.RIGHT.ToPoint());
            dirWeights["down"] = checkScatter(GameVars.DIR.DOWN.ToPoint());
            dirWeights["up"] = checkScatter(GameVars.DIR.UP.ToPoint());

            return dirWeights;
        }

        public int checkScatter(Point direction)
        {
            //int retval = 20;
            int retval = 0;
            Point location = ghost.GridLocation.ToPoint();
            Vector2 gridDestination = GameMap.getGridLocation(destination)[0];

            if (location.Y >= 0 && location .X >= 0)
            {
                if ((loseSpawnLine != -1 && loseSpawnLine == location.Y) || (loseSpawnCol != -1 && loseSpawnCol == location.X))
                {
                    ghost.InSpawn = false;
                    loseSpawnLine = -1;
                    loseSpawnCol = -1;
                }

                if (ghost.InSpawn && GameMap.wallLevel[location.Y + direction.Y, location .X + direction.X] == -2)
                {
                    // Not blocked, this is a door to exit the spawn site
                    //retval = 9000;

                    if (loseSpawnCol == -1 && loseSpawnLine == -1)
                    {
                        retval = 9000;

                        if (direction.Y != 0)
                            loseSpawnLine = location.Y + direction.Y * 2;
                        else if (direction.X != 0)
                            loseSpawnCol = location.X + direction.X * 2;
                    }
                    else if (ghost.LastDirection == direction.ToVector2())
                    {
                        retval = 9000;
                    }
                }
                else if (GameMap.wallLevel[location.Y + direction.Y, location .X + direction.X] == -1)
                {
                    // Not blocked, empty space
                   if (direction == GameVars.DIR.LEFT.ToPoint())
                    {
                        // Going LEFT
                        if (ghost.LastDirection == ReverseDirection(direction))
                        {
                            retval = -1;
                        }
                        else
                        {
                            if (gridDestination.X < location.X)
                            {
                                retval = 4;
                            }
                            else if (gridDestination.X > location.X)
                            {
                                retval = 2;
                            }
                        }
                    }
                    else if (direction == GameVars.DIR.RIGHT.ToPoint())
                    {
                        // Going RIGHT
                        if (ghost.LastDirection == ReverseDirection(direction))
                        {
                            retval = -1;
                        }
                        else
                        {
                            if (gridDestination.X > location.X)
                            {
                                retval = 4;
                            }
                            else if (gridDestination.X < location.X)
                            {
                                retval = 2;
                            }
                        }
                    }
                    else if (direction == GameVars.DIR.DOWN.ToPoint())
                    {
                        // Going DOWN
                        if (ghost.LastDirection == ReverseDirection(direction))
                        {
                            retval = -1;
                        }
                        else
                        {
                            if (gridDestination.Y > location.Y)
                            {
                                retval = 4;
                            }
                            else if (gridDestination.Y < location.Y)
                            {
                                retval = 2;
                            }
                        }
                    }
                    else
                    {
                        // Going UP
                        if (ghost.LastDirection == ReverseDirection(direction))
                        {
                            retval = -1;
                        }
                        else
                        {
                            if (gridDestination.Y < location.Y)
                            {
                                retval = 4;
                            }
                            else if (gridDestination.Y > location.Y)
                            {
                                retval = 2;
                            }
                        }
                    }
                }
                else
                {
                    // Blocked
                    retval = -1;
                }
            }
            else
            {
                retval = -1;
            }

            return retval;
        }

        private Vector2 ReverseDirection(Point direction)
        {
            return new Vector2(direction.X * -1, direction.Y * -1);
        }

    }
}
