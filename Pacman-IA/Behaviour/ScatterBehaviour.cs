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

                    

                    /*
                    Point tmpLocation = location;
                    int xSize = GameMap.wallLevel.GetLength(1);
                    int ySize = GameMap.wallLevel.GetLength(0);

                    int totalSpaces = 0;

                    //Vector2 gridDestination = GameMap.getGridLocation(destination)[0];
                    Vector2 gridDestination = destination;

                    int yDist = Math.Abs(location.Y - gridDestination.ToPoint().Y);
                    int xDist = Math.Abs(location.X - gridDestination.ToPoint().X);

                    // Scan selected direction
                    while (tmpLocation.Y >= 0 && tmpLocation.Y < (ySize - 1)
                        && tmpLocation.X >= 0 && tmpLocation.X < (xSize - 1)
                        && GameMap.wallLevel[tmpLocation.Y, tmpLocation.X] == -1
                    )
                    {
                        totalSpaces++;

                        // Update scanning position
                        tmpLocation.Y += direction.Y;
                        tmpLocation.X += direction.X;
                    }

                    totalSpaces = 1;
                    int spaceValue = 10;
                    int sameDirection = 30;
                    int opositeValue = 25;
                    int forceDirection = 300;
                    /**/


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



                    /*
                    if (direction == GameVars.DIR.LEFT.ToPoint())
                    {
                        // Going LEFT
                        if (ghost.LastDirection == GameVars.DIR.RIGHT)
                        {
                            // This is the opposite direction, check if this is only route possible
                            if (GameMap.wallLevel[location.Y - 1, location.X] < 0 || GameMap.wallLevel[location.Y + 1, location.X] < 0 || GameMap.wallLevel[location.Y, location.X + 1] < 0)
                            {
                                // Give it the lowest priority, or just block it
                                retval = -1;
                            }
                        }

                        if (retval != -1)
                        {
                            if (location.X > gridDestination.X)
                            {
                                if (GameMap.wallLevel[location.Y - 1, location.X] == -1 || GameMap.wallLevel[location.Y + 1, location.X] == -1 && xDist > yDist)
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }

                                retval += sameDirection;
                            }

                            retval += spaceValue * totalSpaces;
                        }
                    }
                    else if (direction == GameVars.DIR.RIGHT.ToPoint())
                    {
                        // Going RIGHT
                        if (ghost.LastDirection == GameVars.DIR.LEFT)
                        {
                            // This is the opposite direction, check if this is only route possible
                            if (GameMap.wallLevel[location.Y - 1, location.X] < 0 || GameMap.wallLevel[location.Y + 1, location.X] < 0 || GameMap.wallLevel[location.Y, location.X - 1] < 0)
                            {
                                // Give it the lowest priority, or just block it
                                retval = -1;
                            }
                        }

                        if (retval != -1)
                        {
                            if (location.X < gridDestination.X)
                            {
                                if (GameMap.wallLevel[location.Y - 1, location.X] == -1 || GameMap.wallLevel[location.Y + 1, location.X] == -1 && xDist > yDist)
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }

                                retval += sameDirection;
                            }

                            retval += spaceValue * totalSpaces;
                        }
                    }
                    else if (direction == GameVars.DIR.DOWN.ToPoint())
                    {
                        // Going DOWN
                        if (ghost.LastDirection == GameVars.DIR.UP)
                        {
                            // This is the opposite direction, check if this is only route possible
                            if (GameMap.wallLevel[location.Y, location.X - 1] < 0 || GameMap.wallLevel[location.Y, location.X + 1] < 0 || GameMap.wallLevel[location.Y + 1, location.X] < 0)
                            {
                                // Give it the lowest priority, or just block it
                                retval = -1;
                            }
                        }

                        if (retval != -1)
                        {
                            if (location.Y > gridDestination.Y)
                            {
                                if (GameMap.wallLevel[location.Y - 1, location.X] == -1 || GameMap.wallLevel[location.Y + 1, location.X] == -1 && yDist > xDist)
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }

                                retval += sameDirection;
                            }

                            retval += spaceValue * totalSpaces;
                        }
                    }
                    else
                    {
                        // Going UP
                        if (ghost.LastDirection == GameVars.DIR.DOWN)
                        {
                            // This is the opposite direction, check if this is only route possible
                            if (GameMap.wallLevel[location.Y, location.X - 1] < 0 || GameMap.wallLevel[location.Y, location.X + 1] < 0 || GameMap.wallLevel[location.Y - 1, location.X] < 0)
                            {
                                // Give it the lowest priority, or just block it
                                retval = -1;
                            }
                        }

                        if (retval != -1)
                        {
                            if (location.Y < gridDestination.Y)
                            {
                                if (GameMap.wallLevel[location.Y - 1, location.X] == -1 || GameMap.wallLevel[location.Y + 1, location.X] == -1 && yDist > xDist)
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }

                                retval += sameDirection;
                            }

                            retval += spaceValue * totalSpaces;
                        }
                    }


                    /**/


                    /*
                    if (direction.Y == 0)
                    {
                        if (direction == GameVars.DIR.LEFT.ToPoint())
                        {
                            // Going LEFT
                            if (ghost.LastDirection == direction.ToVector2() && col > gridDestination.X)
                            {
                                // Already going LEFT

                                if ((GameMap.wallLevel[lin - 1, col] == -1 || GameMap.wallLevel[lin + 1, col] == -1)
                                    && xDist > yDist
                                )
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }
                            }
                            else
                            {
                                if (col > gridDestination.X)
                                {
                                    // Testing direction towards goal
                                    retval += sameDirection;
                                }

                                retval += spaceValue * totalSpaces;

                                if (ghost.LastDirection == direction.ToVector2())
                                {
                                    // Is LEFT
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.RIGHT)
                                {
                                    // Is RIGHT
                                    retval -= opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.DOWN && gridDestination.Y < lin)
                                {
                                    // Is DOWN
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.UP && gridDestination.Y > lin)
                                {
                                    // Is UP
                                    retval += opositeValue;
                                }
                            }
                        }
                        else
                        {
                            // Going RIGHT
                            if (ghost.LastDirection == direction.ToVector2() && col < gridDestination.X)
                            {
                                // Already going RIGHT
                                if ((GameMap.wallLevel[lin - 1, col] == -1 || GameMap.wallLevel[lin + 1, col] == -1)
                                    && xDist > yDist
                                )
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }
                            }
                            else
                            {
                                if (col < gridDestination.X)
                                {
                                    // Testing direction towards goal
                                    retval += sameDirection;
                                }

                                retval = spaceValue * totalSpaces;

                                if (ghost.LastDirection == direction.ToVector2())
                                {
                                    // Is RIGHT
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.LEFT)
                                {
                                    // Is LEFT
                                    retval -= opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.DOWN && gridDestination.Y < lin)
                                {
                                    // Is DOWN
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.UP && gridDestination.Y > lin)
                                {
                                    // Is UP
                                    retval += opositeValue;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (direction == GameVars.DIR.UP.ToPoint())
                        {
                            // Going UP
                            if (ghost.LastDirection == direction.ToVector2() && lin < gridDestination.Y)
                            {
                                // Already going UP
                                if ((GameMap.wallLevel[lin, col - 1] == -1 || GameMap.wallLevel[lin, col + 1] == -1)
                                    && yDist > xDist
                                )
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }
                            }
                            else
                            {
                                if (lin > gridDestination.Y)
                                {
                                    // Testing direction towards goal
                                    retval += sameDirection;
                                }

                                retval = spaceValue * totalSpaces;

                                if (ghost.LastDirection == direction.ToVector2())
                                {
                                    // Is UP
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.DOWN)
                                {
                                    // Is DOWN
                                    retval -= opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.LEFT && gridDestination.X < col)
                                {
                                    // Is LEFT
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.RIGHT && gridDestination.X > col)
                                {
                                    // Is RIGHT
                                    retval += opositeValue;
                                }
                            }
                        }
                        else
                        {
                            // Going DOWN
                            if (ghost.LastDirection == direction.ToVector2() && lin > gridDestination.Y)
                            {
                                // Already going DOWN
                                if ((GameMap.wallLevel[lin, col - 1] == -1 || GameMap.wallLevel[lin, col + 1] == -1)
                                    && yDist > xDist
                                )
                                {
                                    retval = 0;
                                }
                                else
                                {
                                    retval = forceDirection;
                                }
                            }
                            else
                            {
                                if (lin < gridDestination.Y)
                                {
                                    // Testing direction towards goal
                                    retval += sameDirection;
                                }

                                retval = spaceValue * totalSpaces;

                                if (ghost.LastDirection == direction.ToVector2())
                                {
                                    // Is DOWN
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.UP)
                                {
                                    // Is UP
                                    retval -= opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.LEFT && gridDestination.X < col)
                                {
                                    // Is LEFT
                                    retval += opositeValue;
                                }
                                else if (ghost.LastDirection == GameVars.DIR.RIGHT && gridDestination.X > col)
                                {
                                    // Is RIGHT
                                    retval += opositeValue;
                                }
                            }
                        }
                    }
                    /**/
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
