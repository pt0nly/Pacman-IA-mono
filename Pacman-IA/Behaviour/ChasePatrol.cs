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
    public class ChasePatrol : ChaseBehaviour
    {
        private Vector2 pacmanLastPosition;
        private bool pacmanLocated;
        private List<Vector2> patrol;
        private int currPatrol;

        public ChasePatrol(Character person) : base(person)
        {
            pacmanLastPosition = GameVars.DIR.INVALID;
            pacmanLocated = false;
            patrol = person.PatrolLocation;
            currPatrol = 0;
        }

        public override Dictionary<string, int> Chase(Vector2 lastDirection)
        {
            if (ghost.GridLocation == GameMap.getGridLocation( patrol[currPatrol] )[0])
            {
                currPatrol++;
                if (currPatrol > (patrol.Count - 1))
                    currPatrol = 0;
            }

            List<int> indexes = new List<int>();

            if (lastDirection == GameVars.DIR.LEFT)
            {
                // Going LEFT
                int i = 0;
                foreach(var patLoc in patrol)
                {
                    Vector2 tmp = GameMap.getGridLocation(patLoc)[0];
                    if (tmp.X <= ghost.GridLocation.X)
                    {
                        indexes.Add(i);
                    }
                    i++;
                }
            }
            else if (lastDirection == GameVars.DIR.RIGHT)
            {
                // Going RIGHT
                int i = 0;
                foreach (var patLoc in patrol)
                {
                    Vector2 tmp = GameMap.getGridLocation(patLoc)[0];
                    if (tmp.X >= ghost.GridLocation.X)
                    {
                        indexes.Add(i);
                    }
                    i++;
                }
            }

            if (lastDirection == GameVars.DIR.DOWN)
            {
                // Going DOWN
                foreach(var ind in indexes)
                {
                    if (GameMap.getGridLocation(patrol[ind])[0].Y >= ghost.GridLocation.Y)
                    {
                        currPatrol = ind;
                    }
                }
            }
            else
            {
                // Going UP
                foreach (var ind in indexes)
                {
                    if (GameMap.getGridLocation(patrol[ind])[0].Y <= ghost.GridLocation.Y)
                    {
                        currPatrol = ind;
                    }
                }
            }

            //Dictionary<string, int> dirWeights = base.Chase(lastDirection);
            Dictionary<string, int> dirWeights = new Dictionary<string, int>();

            //if (dirWeights["left"] >= 4000 || dirWeights["right"] >= 4000 || dirWeights["down"] >= 4000 || dirWeights["up"] >= 4000)
            //{
                // Patrol the area
                ScatterBehaviour scatter = new ScatterBehaviour(ghost, patrol[currPatrol]);

                dirWeights = scatter.Scatter(lastDirection);
            //}

            return dirWeights;
        }

        protected override int checkChase(Point direction)
        {
            int retval = 0;
            Point location = ghost.GridLocation.ToPoint();

            if (location.Y >= 0 && location.X >= 0)
            {
                if (GameMap.wallLevel[location.Y + direction.Y, location.X + direction.X] == -1)
                {
                    // Not blocked
                    ScanLine scan = ScanMap(location, direction);

                    if (scan.pacmanFound)
                        retval = 4000;
                    else
                    {
                        // Does it have food?
                        if (scan.hasFood)
                        {
                            // Has food / PowerPellet
                            retval += scan.totalFood;
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

        private ScanLine ScanMap(Point location, Point direction)
        {
            ScanLine retval = new ScanLine();
            Point tmpLocation = new Point(location.X + direction.X, location.Y + direction.Y);

            // Scan selected direction
            while (tmpLocation.Y >= 0 && tmpLocation.Y < (retval.ySize - 1)
                && tmpLocation.X >= 0 && tmpLocation.X < (retval.xSize - 1)
                && GameMap.wallLevel[tmpLocation.Y, tmpLocation.X] == -1
            )
            {
                if (GameMap.pelletLevel[tmpLocation.Y, tmpLocation.X] != -1)
                {
                    retval.hasFood = true;
                    retval.totalFood += GameMap.pelletLevel[tmpLocation.Y, tmpLocation.X];
                }

                if (GameVars.Pacman.GridLocation == tmpLocation.ToVector2())
                {
                    // Found Pacman!
                    retval.pacmanFound = true;
                    pacmanLocated = true;
                    pacmanLastPosition = tmpLocation.ToVector2();

                    if (GameVars.Pacman.Direction != ReverseDirection(direction))
                    {
                        // Pacman is not running in your direction
                        // So we will try to ambush him
                        pacmanLastPosition = tmpLocation.ToVector2();
                    }
                }

                // Update scanning position
                tmpLocation.X += direction.X;
                tmpLocation.Y += direction.Y;
            }

            if (!pacmanLocated)
            {
                // Pacman was not located
                pacmanLastPosition = GameVars.DIR.INVALID;
            }

            return retval;
        }
    }
}
