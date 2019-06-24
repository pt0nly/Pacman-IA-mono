using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman_IA.GameObjects;
using Pacman_IA.Classes;
using Microsoft.Xna.Framework;

namespace Pacman_IA.Behaviour
{
    public class EatBehaviour : IEatBehaviour
    {
        private Pacman pac = null;


        public EatBehaviour(Character person)
        {
            if (person is Pacman)
                pac = (Pacman)person;
        }

        private void ReverseDirection(Vector2 lastDirection)
        {
            Point reverseDir = new Point(lastDirection.ToPoint().Y * -1, lastDirection.ToPoint().X * -1);
            int lin = pac.GridLocation.ToPoint().Y;
            int col = pac.GridLocation.ToPoint().X;

            if (GameMap.wallLevel[lin + reverseDir.Y, col + reverseDir.X] == -1)
            {
                lastDirection = reverseDir.ToVector2();

                if (lastDirection == GameVars.DIR.LEFT)
                {
                    // Go LEFT
                    pac.MoveLeft();
                }
                else if (lastDirection == GameVars.DIR.RIGHT)
                {
                    // Go RIGHT
                    pac.MoveRight();
                }
                else if (lastDirection == GameVars.DIR.UP)
                {
                    // Go UP
                    pac.MoveUp();
                }
                else
                {
                    // Go DOWN
                    pac.MoveDown();
                }
            }
        }

        public void Eat(Vector2 lastDirection)
        {
            if (pac != null)
            {
                /*
                if (pac.IsMoving)
                {
                    int dir = 0;
                    //dir = checkFuga(lastDirection.ToPoint());

                    if (dir < -1)
                    {
                        //ReverseDirection(lastDirection);
                    }
                }
                /**/
                if (!pac.IsMoving)
                {
                    int left = checkFuga(GameVars.DIR.LEFT.ToPoint());
                    int right = checkFuga(GameVars.DIR.RIGHT.ToPoint());
                    int down = checkFuga(GameVars.DIR.DOWN.ToPoint());
                    int up = checkFuga(GameVars.DIR.UP.ToPoint());

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
                        // Direction RIGHT
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
                        // Direction UP
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
                        // Direction DOWN
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
        }

        private int checkFuga(Point direction)
        {
            int retval = 0;
            int lin = pac.GridLocation.ToPoint().Y;
            int col = pac.GridLocation.ToPoint().X;


            /*
            if (pac.IsMoving)
            {
                int tmpLin = lin;
                int tmpCol = col;
                bool ghostFound = false;
                bool ghostCollisionCourse = false;
                int xSize = GameMap.wallLevel.GetLength(1);
                int ySize = GameMap.wallLevel.GetLength(0);

                // Scan selected direction
                while (tmpLin >= 0 && tmpLin < (ySize - 1)
                    && tmpCol >= 0 && tmpCol < (xSize - 1)
                    && GameMap.wallLevel[tmpLin, tmpCol] == -1
                )
                {
                    Vector2 posCheck = new Vector2(tmpCol, tmpLin);

                    if (GameVars.Blinky.GridLocation == posCheck || GameVars.Pinky.GridLocation == posCheck
                        || GameVars.Inky.GridLocation == posCheck || GameVars.Clyde.GridLocation == posCheck
                    )
                    {
                        // Found a Ghost!
                        ghostFound = true;
                        if (GameVars.Blinky.Direction == new Vector2(direction.X * -1, direction.Y * -1) || (tmpCol == col && tmpLin == lin))
                        {
                            // Ghost is running in your direction
                            ghostCollisionCourse = true;
                        }
                    }

                    // Update scanning position
                    tmpLin += direction.Y;
                    tmpCol += direction.X;
                }

                if (ghostFound)
                {
                    // Ghost have been found
                    if (ghostCollisionCourse)
                    {
                        // Ghost in collision course
                        retval = -2;
                    }
                    else
                    {
                        // No collision course
                        retval = 0;
                    }
                }


            }
            /**/
            if (!pac.IsMoving)
            {
                if (lin == 5 && col == 17)
                {
                    string aa = "asda";
                }

                if (GameMap.wallLevel[lin + direction.Y, col + direction.X] == -1)
                {
                    // Not blocked
                    int tmpLin = lin + direction.Y;
                    int tmpCol = col + direction.X;
                    bool ghostFound = false;
                    bool ghostCollisionCourse = false;
                    bool hasFood = false;
                    int totalFood = 0;
                    int xSize = GameMap.wallLevel.GetLength(1);
                    int ySize = GameMap.wallLevel.GetLength(0);

                    // Scan selected direction
                    while (tmpLin >= 0 && tmpLin < (ySize - 1)
                        && tmpCol >= 0 && tmpCol < (xSize - 1)
                        && GameMap.wallLevel[tmpLin, tmpCol] == -1
                    )
                    {
                        Vector2 posCheck = new Vector2(tmpCol, tmpLin);

                        if (GameMap.pelletLevel[tmpLin, tmpCol] != -1)
                        {
                            hasFood = true;
                            totalFood += GameMap.pelletLevel[tmpLin, tmpCol];
                        }

                        if (GameVars.Blinky.GridLocation == posCheck || GameVars.Pinky.GridLocation == posCheck
                            || GameVars.Inky.GridLocation == posCheck || GameVars.Clyde.GridLocation == posCheck
                        )
                        {
                            // Found a Ghost!
                            ghostFound = true;
                            if (GameVars.Blinky.Direction == new Vector2(direction.X * -1, direction.Y * -1) || (tmpCol == col && tmpLin == lin))
                            {
                                // Ghost is running in your direction
                                ghostCollisionCourse = true;
                            }
                        }

                        // Update scanning position
                        tmpLin += direction.Y;
                        tmpCol += direction.X;
                    }

                    if (ghostFound)
                    {
                        // Ghost have been found
                        if (ghostCollisionCourse)
                        {
                            // Ghost in collision course
                            retval = -2;
                        }
                        else
                        {
                            // No collision course
                            retval = 0;
                        }
                    }

                    if (retval >= -1)
                    {
                        // Does it have food?
                        if (hasFood)
                        {
                            // Has food / PowerPellet
                            retval += totalFood;
                        }

                        if (hasFruit())
                        {
                            // Has fruit
                            retval += 100;
                        }
                    }
                }
                else
                {
                    // Blocked
                    retval = -1;
                }
            }

            return retval;
        }

        private bool hasFruit()
        {
            return false;
        }
    }
}
