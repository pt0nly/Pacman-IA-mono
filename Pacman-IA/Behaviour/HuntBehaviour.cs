using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pacman_IA.Classes;
using Pacman_IA.GameObjects;

namespace Pacman_IA.Behaviour
{
    public class HuntBehaviour : IHuntBehaviour
    {
        private Pacman pac = null;

        public HuntBehaviour(Character person)
        {
            if (person is Pacman)
                pac = (Pacman)person;
        }

        public void Hunt(Vector2 lastDirection)
        {
            if (pac != null)
            {
                int left = checkHunt(GameVars.DIR.LEFT.ToPoint());
                int right = checkHunt(GameVars.DIR.RIGHT.ToPoint());
                int down = checkHunt(GameVars.DIR.DOWN.ToPoint());
                int up = checkHunt(GameVars.DIR.UP.ToPoint());

                if (lastDirection == GameVars.DIR.LEFT)
                {
                    if (left >= up && left >= down && left >= right)
                    {
                        // Go LEFT
                        pac.MoveLeft();
                    }
                    else if (up > left && up >= down && up >= right)
                    {
                        // Go UP
                        pac.MoveUp();
                    }
                    else if (down > left && down > up && down >= right)
                    {
                        // Go DOWN
                        pac.MoveDown();
                    }
                    else
                    {
                        // Go RIGHT
                        pac.MoveRight();
                    }
                }
                else if (lastDirection == GameVars.DIR.RIGHT)
                {
                    if (right >= down && right >= up && right >= left)
                    {
                        // Go RIGHT
                        pac.MoveRight();
                    }
                    else if (down > right && down >= up && down >= left)
                    {
                        // Go DOWN
                        pac.MoveDown();
                    }
                    else if (up > right && up > down && up >= left)
                    {
                        // Go UP
                        pac.MoveUp();
                    }
                    else
                    {
                        // Go LEFT
                        pac.MoveLeft();
                    }
                }
                else if (lastDirection == GameVars.DIR.UP)
                {
                    if (up >= right && up >= left && up >= down)
                    {
                        // Go UP
                        pac.MoveUp();
                    }
                    else if (right > up && right >= left && right >= down)
                    {
                        // Go RIGHT
                        pac.MoveRight();
                    }
                    else if (left > up && left > right && left >= down)
                    {
                        // Go LEFT
                        pac.MoveLeft();
                    }
                    else
                    {
                        // Go DOWN
                        pac.MoveDown();
                    }
                }
                else
                {
                    if (down >= left && down >= right && down >= up)
                    {
                        // Go DOWN
                        pac.MoveDown();
                    }
                    else if (left > down && left >= right && left >= up)
                    {
                        // Go LEFT
                        pac.MoveLeft();
                    }
                    else if (right > down && right > left && right >= up)
                    {
                        // Go RIGHT
                        pac.MoveRight();
                    }
                    else
                    {
                        // Go UP
                        pac.MoveUp();
                    }
                }

            }
        }

        private int checkHunt(Point direction)
        {
            int retval = 0;
            Point location = pac.GridLocation.ToPoint();

            if (GameMap.wallLevel[location.Y + direction.Y, location.X + direction.X] == -1)
            {
                // Not blocked
                ScanLine scan = ScanMap(location, direction);

                if (scan.ghostFound)
                {
                    // Ghost have been found
                    retval += 200;

                    if (scan.ghostCollisionCourse)
                    {
                        // Ghost in collision course
                        retval += 260;
                    }
                }

                // Does it have food?
                if (scan.hasFood)
                {
                    // Has food / PowerPellet
                    int tmp = (scan.totalFood - 3 * scan.foodDistance);

                    retval += (tmp > 0 ? tmp : 1);
                }
            }
            else
            {
                // Blocked
                retval = -2;
            }

            return retval;
        }

        private ScanLine ScanMap(Point location, Point direction)
        {
            ScanLine retval = new ScanLine();
            Point tmpLocation = new Point(location.X + direction.X, location.Y + direction.Y);

            // Scan selected direction
            while(tmpLocation.Y >= 0 && tmpLocation.Y < (retval.ySize - 1)
                && tmpLocation.X >= 0 && tmpLocation.X < (retval.xSize - 1)
                && GameMap.wallLevel[tmpLocation.Y, tmpLocation.X] == -1
            )
            {
                if (GameMap.pelletLevel[tmpLocation.Y, tmpLocation.X] != -1)
                {
                    retval.hasFood = true;
                    retval.totalFood += GameMap.pelletLevel[tmpLocation.Y, tmpLocation.X];
                }
                else
                {
                    retval.foodDistance++;
                }

                if ((GameVars.Blinky.GridLocation == tmpLocation.ToVector2() && !GameVars.Blinky.IsDead)
                    || (GameVars.Pinky.GridLocation == tmpLocation.ToVector2() && !GameVars.Pinky.IsDead)
                    || (GameVars.Inky.GridLocation == tmpLocation.ToVector2() && !GameVars.Inky.IsDead)
                    || (GameVars.Clyde.GridLocation == tmpLocation.ToVector2() && !GameVars.Clyde.IsDead)
                )
                {
                    // Found a Ghost!
                    retval.ghostFound = true;

                    if (tmpLocation == location
                        || GameVars.Blinky.Direction == ReverseDirection(direction)
                        || GameVars.Pinky.Direction == ReverseDirection(direction)
                        || GameVars.Inky.Direction == ReverseDirection(direction)
                        || GameVars.Clyde.Direction == ReverseDirection(direction)
                    )
                    {
                        // Ghost is running in your direction
                        retval.ghostCollisionCourse = true;
                    }
                }

                // Update scanning position
                tmpLocation.X += direction.X;
                tmpLocation.Y += direction.Y;
            }

            return retval;
        }

        private Vector2 ReverseDirection(Point direction)
        {
            return new Vector2(direction.X * -1, direction.Y * -1);
        }
    }
}
