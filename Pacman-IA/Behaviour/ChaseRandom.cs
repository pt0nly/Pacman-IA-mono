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
    public class ChaseRandom : ChaseBehaviour
    {
        private Vector2 pacmanLastPosition;
        private bool pacmanLocated;
        private bool runToPacman;

        public ChaseRandom(Character person) : base(person)
        {
            pacmanLastPosition = GameVars.DIR.INVALID;
            pacmanLocated = false;
            runToPacman = true;
        }

        public ChaseRandom(Character person, bool runAfterPacman) : base(person)
        {
            pacmanLastPosition = GameVars.DIR.INVALID;
            pacmanLocated = false;
            runToPacman = runAfterPacman;
        }

        public override Dictionary<string, int> Chase(Vector2 lastDirection)
        {
            Dictionary<string, int> dirWeights = base.Chase(lastDirection);

            // Remove unwanted directions
            if (dirWeights["left"] <= 0)
                dirWeights.Remove("left");

            if (dirWeights["right"] <= 0)
                dirWeights.Remove("right");

            if (dirWeights["down"] <= 0)
                dirWeights.Remove("down");

            if (dirWeights["up"] <= 0)
                dirWeights.Remove("up");

            int total = dirWeights.Count();
            if (total > 0)
            {
                // Remove all directions except the chosen one
                string choice = "";
                string pacmanChoice = "";

                if (total > 1)
                {
                    // Only remove oposite direction if more than one selected
                    foreach(var weight in dirWeights)
                    {
                        if (weight.Value == 1)
                        {
                            choice = weight.Key;
                            total--;
                        }

                        if (weight.Value == 4)
                            pacmanChoice = weight.Key;
                    }
                }

                if (choice == "")
                    choice = "up";
                else
                {
                    dirWeights.Remove(choice);
                }


                if (pacmanChoice == "")
                {
                    if (total < 1)
                        total = 1;
                    int tmp = GameVars.rand.Next(1, total);

                    if (tmp == 1)
                    {
                        if (dirWeights.ContainsKey("left"))
                            choice = "left";
                        else if (dirWeights.ContainsKey("right"))
                            choice = "right";
                        else if (dirWeights.ContainsKey("down"))
                            choice = "down";
                        else if (dirWeights.ContainsKey("up"))
                            choice = "up";
                    }
                    else if (tmp == 2)
                    {
                        if (dirWeights.ContainsKey("right"))
                            choice = "right";
                        else if (dirWeights.ContainsKey("down"))
                            choice = "down";
                        else if (dirWeights.ContainsKey("up"))
                            choice = "up";
                    }
                    else if (tmp == 3)
                    {
                        if (dirWeights.ContainsKey("down"))
                            choice = "down";
                        else if (dirWeights.ContainsKey("up"))
                            choice = "up";
                    }
                }
                else
                {
                    choice = pacmanChoice;
                }

                if (choice != "left" && dirWeights.ContainsKey("left"))
                    dirWeights["left"] = -1;
                if (!dirWeights.ContainsKey("left"))
                    dirWeights.Add("left", -1);

                if (choice != "right" && dirWeights.ContainsKey("right"))
                    dirWeights["right"] = -1;
                if (!dirWeights.ContainsKey("right"))
                    dirWeights.Add("right", -1);

                if (choice != "down" && dirWeights.ContainsKey("down"))
                    dirWeights["down"] = -1;
                if (!dirWeights.ContainsKey("down"))
                    dirWeights.Add("down", -1);

                if (choice != "up" && dirWeights.ContainsKey("up"))
                    dirWeights["up"] = -1;
                if (!dirWeights.ContainsKey("up"))
                    dirWeights.Add("up", -1);
            }

            return dirWeights;
        }

        protected override int checkChase(Point direction)
        {
            int retval = 0;
            int lin = ghost.GridLocation.ToPoint().Y;
            int col = ghost.GridLocation.ToPoint().X;

            if (lin >= 0 && col >= 0)
            {
                if (GameMap.wallLevel[lin + direction.Y, col + direction.X] == -1)
                {
                    // Not blocked

                    // Don't know where pacman is
                    int tmpLin = lin;
                    int tmpCol = col;
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
                            pacmanLocated = true;
                            pacmanLastPosition = posCheck;
                        }

                        // Update scanning position
                        tmpLin += direction.Y;
                        tmpCol += direction.X;
                    }

                    // Check direction
                    if (direction == GameVars.DIR.LEFT.ToPoint())
                    {
                        // Going LEFT
                        if (ghost.LastDirection.X == 1)
                        {
                            // Last going RIGHT
                            retval = 1;
                        }
                        else
                        {
                            if (pacmanLocated)
                                retval = runToPacman ? 4 : 1;
                            else
                                retval = runToPacman ? 2 : 1;
                        }
                    }
                    else if (direction == GameVars.DIR.RIGHT.ToPoint())
                    {
                        // Going RIGHT
                        if (ghost.LastDirection.X == -1)
                        {
                            // Last going LEFT
                            retval = 1;
                        }
                        else
                        {
                            if (pacmanLocated)
                                retval = runToPacman ? 4 : 1;
                            else
                                retval = runToPacman ? 2 : 1;
                        }
                    }
                    else if (direction == GameVars.DIR.UP.ToPoint())
                    {
                        // Going UP
                        if (ghost.LastDirection.Y == 1)
                        {
                            // Last going DOWN
                            retval = 1;
                        }
                        else
                        {
                            if (pacmanLocated)
                                retval = runToPacman ? 4 : 1;
                            else
                                retval = runToPacman ? 2 : 1;
                        }
                    }
                    else
                    {
                        // Going DOWN
                        if (ghost.LastDirection.Y == -1)
                        {
                            // Last going UP
                            retval = 1;
                        }
                        else
                        {
                            if (pacmanLocated)
                                retval = runToPacman ? 4 : 1;
                            else
                                retval = runToPacman ? 2 : 1;
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
    }
}
