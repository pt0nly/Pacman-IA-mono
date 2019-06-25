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

        private Vector2 ReverseDirection(Point direction)
        {
            return new Vector2(direction.X * -1, direction.Y * -1);
        }

        private void RevertDirection(Vector2 lastDirection)
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

        private string ConfirmDirections(Dictionary<string, int> dirWeights, Vector2 lastDirection)
        {
            string choice = "";

            if (dirWeights["left"] < 0)
            {
                dirWeights.Remove("left");
            }
            if (dirWeights["right"] < 0)
            {
                dirWeights.Remove("right");
            }
            if (dirWeights["down"] < 0)
            {
                dirWeights.Remove("down");
            }
            if (dirWeights["up"] < 0)
            {
                dirWeights.Remove("up");
            }

            // Check remaining direction weights
            foreach (var weight in dirWeights)
            {
                if (choice == "")
                    choice = weight.Key;

                if (weight.Key != choice)
                {
                    if (weight.Value > dirWeights[choice])
                        choice = weight.Key;
                    else if (weight.Value == dirWeights[choice])
                    {
                        int tmp = GameVars.rand.Next(1, 2);

                        if (tmp == 2)
                            choice = weight.Key;
                    }
                }
            }

            if (choice == "")
            {
                int tmp = GameVars.rand.Next(1, 4);

                choice = "up";

                if (tmp == 1)
                    choice = "left";
                else if (tmp == 2)
                    choice = "right";
                else if (tmp == 3)
                    choice = "down";
            }

            return choice;
        }

        public void Eat(Vector2 lastDirection)
        {
            if (pac != null)
            {
                Dictionary<string, int> dirWeights = new Dictionary<string, int>();
                dirWeights["left"] = checkFuga(GameVars.DIR.LEFT.ToPoint());
                dirWeights["right"] = checkFuga(GameVars.DIR.RIGHT.ToPoint());
                dirWeights["down"] = checkFuga(GameVars.DIR.DOWN.ToPoint());
                dirWeights["up"] = checkFuga(GameVars.DIR.UP.ToPoint());

                switch( ConfirmDirections(dirWeights, lastDirection) )
                {
                    case "left":
                        pac.MoveLeft();
                        break;

                    case "right":
                        pac.MoveRight();
                        break;

                    case "down":
                        pac.MoveDown();
                        break;

                    case "up":
                    default:
                        pac.MoveUp();
                        break;
                }
            }
        }

        private int checkFuga(Point direction)
        {
            int retval = 0;
            Point location = pac.GridLocation.ToPoint();

            if (GameMap.wallLevel[location.Y + direction.Y, location.X + direction.X] == -1)
            {
                // Not blocked
                Point tmpLocation = new Point(location.X + direction.X, location.Y + direction.Y);
                bool ghostFound = false;
                bool ghostCollisionCourse = false;
                bool hasFood = false;
                int totalFood = 0;
                int foodDistance = 0;
                int xSize = GameMap.wallLevel.GetLength(1);
                int ySize = GameMap.wallLevel.GetLength(0);
                bool hole = false;

                // Scan selected direction
                while (tmpLocation.Y >= 0 && tmpLocation.Y < (ySize - 1)
                    && tmpLocation.X >= 0 && tmpLocation.X < (xSize - 1)
                    && GameMap.wallLevel[tmpLocation.Y, tmpLocation.X] == -1
                )
                {
                    if (GameMap.pelletLevel[tmpLocation.Y, tmpLocation.X] != -1 && !hole)
                    {
                        hasFood = true;
                        totalFood += GameMap.pelletLevel[tmpLocation.Y, tmpLocation.X];
                    }
                    else
                    {
                        foodDistance++;
                        if (hasFood)
                            hole = true;
                    }

                    if (GameVars.Blinky.GridLocation == tmpLocation.ToVector2() || GameVars.Pinky.GridLocation == tmpLocation.ToVector2()
                        || GameVars.Inky.GridLocation == tmpLocation.ToVector2() || GameVars.Clyde.GridLocation == tmpLocation.ToVector2()
                    )
                    {
                        // Found a Ghost!
                        ghostFound = true;

                        if ((tmpLocation.X == location.X && tmpLocation.Y == location.Y)
                            || GameVars.Blinky.Direction == ReverseDirection(direction)
                            || GameVars.Pinky.Direction == ReverseDirection(direction)
                            || GameVars.Inky.Direction == ReverseDirection(direction)
                            || GameVars.Clyde.Direction == ReverseDirection(direction)
                        )
                        {
                            // Ghost is running in your direction
                            ghostCollisionCourse = true;
                        }
                    }

                    // Update scanning position
                    tmpLocation.X += direction.X;
                    tmpLocation.Y += direction.Y;
                }

                if (ghostFound)
                {
                    // Ghost have been found

                    retval = -1;

                    if (ghostCollisionCourse)
                        retval = -2;
                }

                if (retval >= -1)
                {
                    // Does it have food?
                    if (hasFood)
                    {
                        // Has food / PowerPellet
                        retval += 1 + totalFood;
                    }
                }
            }
            // Blocked
            else
                retval = -1;

            return retval;
        }
    }
}
