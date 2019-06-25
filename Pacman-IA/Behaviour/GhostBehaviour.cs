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
    public class GhostBehaviour : IBehaviour
    {
        private Character ghost = null;
        private GameVars.GHOST_MODE ghostMode;
        private byte wave;
        private byte waveMode;
        private float timer;

        private ChaseBehaviour chaseBehaviour = null;
        private ScatterBehaviour scatterBehaviour = null;
        private FrightnedBehaviour wanderBehaviour = null;

        private Vector2 lstDirection;

        public GhostBehaviour(Character person, ChaseBehaviour chase, ScatterBehaviour scatter, FrightnedBehaviour wander)
        {
            ghost = person;

            // Set Ghost's behaviours
            chaseBehaviour = chase;
            scatterBehaviour = scatter;
            wanderBehaviour = wander;

            lstDirection = GameVars.DIR.EMPTY;

            ghostMode = GameVars.GHOST_MODE.CHASE;

            wave = 1;
            waveMode = 1;
            timer = 0;
        }

        private string ConfirmDirections(Dictionary<string, int> dirWeights, bool firstRun)
        {
            string choice = "";

            if (!firstRun)
            {

                if (lstDirection == GameVars.DIR.LEFT)
                {
                    // Going LEFT
                    if (dirWeights["left"] < 0)
                    {
                        dirWeights.Remove("left");
                    }
                    if (dirWeights["down"] < 0)
                    {
                        dirWeights.Remove("down");
                    }
                    if (dirWeights["up"] < 0)
                    {
                        dirWeights.Remove("up");
                    }
                }
                else if (lstDirection == GameVars.DIR.RIGHT)
                {
                    // Going RIGHT
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
                }
                else if (lstDirection == GameVars.DIR.DOWN)
                {
                    // Going DOWN
                    if (dirWeights["down"] < 0)
                    {
                        dirWeights.Remove("down");
                    }
                    if (dirWeights["left"] < 0)
                    {
                        dirWeights.Remove("left");
                    }
                    if (dirWeights["right"] < 0)
                    {
                        dirWeights.Remove("right");
                    }
                }
                else
                {
                    // Going UP
                    if (dirWeights["up"] < 0)
                    {
                        dirWeights.Remove("up");
                    }
                    if (dirWeights["left"] < 0)
                    {
                        dirWeights.Remove("left");
                    }
                    if (dirWeights["right"] < 0)
                    {
                        dirWeights.Remove("right");
                    }
                }
            }

            /*
            if (dirWeights["left"] < 0) //|| (lstDirection == GameVars.DIR.RIGHT && !firstRun))
            {
                dirWeights.Remove("left");
            }
            if (dirWeights["right"] < 0 //|| (lstDirection == GameVars.DIR.LEFT && !firstRun))
            {
                dirWeights.Remove("right");
            }
            if (dirWeights["down"] < 0 //|| (lstDirection == GameVars.DIR.UP && !firstRun))
            {
                dirWeights.Remove("down");
            }
            if (dirWeights["up"] < 0 //|| (lstDirection == GameVars.DIR.DOWN && !firstRun))
            {
                dirWeights.Remove("up");
            }
            /**/

            if (lstDirection == GameVars.DIR.LEFT)
            {
                // Going LEFT
                choice = "";

                if (dirWeights.ContainsKey("left"))
                {
                    // Contains LEFT
                    choice = "left";
                }
                else if (dirWeights.ContainsKey("down"))
                {
                    // Contains DOWN
                    choice = "down";
                }
                else if (dirWeights.ContainsKey("up"))
                {
                    // Contains UP
                    choice = "up";
                }
                else if (dirWeights.ContainsKey("right"))
                {
                    // Contains RIGHT, but must not change direction unless blocked
                    choice = "right";
                }
            }
            else if (lstDirection == GameVars.DIR.RIGHT)
            {
                // Going RIGHT
                choice = "";

                if (dirWeights.ContainsKey("right"))
                {
                    // Contains RIGHT
                    choice = "right";
                }
                else if (dirWeights.ContainsKey("up"))
                {
                    // Contains UP
                    choice = "up";
                }
                else if (dirWeights.ContainsKey("down"))
                {
                    // Contains DOWN
                    choice = "down";
                }
                else if (dirWeights.ContainsKey("left"))
                {
                    // Contains LEFT, but must not change direction unless blocked
                    choice = "left";
                }
            }
            else if (lstDirection == GameVars.DIR.UP)
            {
                // Going UP
                choice = "";

                if (dirWeights.ContainsKey("up"))
                {
                    // Contains UP
                    choice = "up";
                }
                else if (dirWeights.ContainsKey("left"))
                {
                    // Contains LEFT
                    choice = "left";
                }
                else if (dirWeights.ContainsKey("right"))
                {
                    // Contains RIGHT
                    choice = "right";
                }
                else if (dirWeights.ContainsKey("down"))
                {
                    // Contains DOWN, but must not reverse direction
                    choice = "down";
                }
            }
            else
            {
                // Going DOWN
                choice = "";

                if (dirWeights.ContainsKey("down"))
                {
                    // Contains DOWN
                    choice = "down";
                }
                else if (dirWeights.ContainsKey("right"))
                {
                    // Contains RIGHT
                    choice = "right";
                }
                else if (dirWeights.ContainsKey("left"))
                {
                    // Contains LEFT
                    choice = "left";
                }
                else if (dirWeights.ContainsKey("up"))
                {
                    // Contains UP, but must not reverse direction
                    choice = "up";
                }
            }

            // Check remaining direction weights
            foreach (var weight in dirWeights)
            {
                if (choice == "")
                    choice = weight.Key;

                if (weight.Key != choice)
                {
                    if (weight.Value > dirWeights[choice])
                    {
                        choice = weight.Key;
                    }
                    else if (weight.Value == dirWeights[choice])
                    {
                        Random rnd = new Random();
                        int tmp = rnd.Next(1, 2);

                        if (tmp == 2)
                            choice = weight.Key;
                    }
                }
            }

            return choice;
        }

        private void Move(string choice)
        {
            if (choice == "left")
                ghost.MoveLeft();
            else if (choice == "right")
                ghost.MoveRight();
            else if (choice == "up")
                ghost.MoveUp();
            else if (choice == "down")
                ghost.MoveDown();
            else
            {
                string a = "say what??";
            }
        }

        protected Vector2 ReverseDirection(Point direction)
        {
            return new Vector2(direction.X * -1, direction.Y * -1);
        }

        private void CheckWaves()
        {
            if (GameVars.Pacman.PowerUp > 0.0f)
            {
                // Pacman is Energized!! Must flee!!
                ghost.Speed = ghost.SpeedScared;
                ghost.CurrSprite = ghost.SpriteScared;
                lstDirection = ReverseDirection(lstDirection.ToPoint());
                ghost.LastDirection = lstDirection;
            }
            else
            {
                // Pacman is about to be Hunted!!
                ghost.Speed = ghost.SpeedNormal;
                ghost.CurrSprite = ghost.SpriteNormal;

                if (wave <= 2)
                {
                    if (waveMode == 1)
                    {
                        if (timer <= 7) // Seconds
                        {
                            // Go to Scatter Mode
                            ghostMode = GameVars.GHOST_MODE.SCATTER;
                        }
                        else
                        {
                            waveMode = 2;
                        }
                    }
                    else
                    {
                        if (timer <= 20) // Seconds
                        {
                            // Go to Chase/Hunt Mode
                            ghostMode = GameVars.GHOST_MODE.CHASE;
                        }
                        else
                        {
                            wave++;
                            waveMode = 1;
                        }
                    }
                }
                else
                {
                    if (wave <= 4)
                    {
                        if (waveMode == 1)
                        {
                            if (timer <= 5) // Seconds
                            {
                                // Go to Scatter Mode
                                ghostMode = GameVars.GHOST_MODE.SCATTER;
                            }
                            else
                            {
                                waveMode = 2;
                            }
                        }
                        else
                        {
                            if (timer <= 20) // Seconds
                            {
                                // Go to Chase/Hunt Mode
                                ghostMode = GameVars.GHOST_MODE.CHASE;
                            }
                            else
                            {
                                wave++;
                                waveMode = 1;
                            }
                        }
                    }
                    else
                    {
                        // Go to Chase/Hunt Mode
                        ghostMode = GameVars.GHOST_MODE.CHASE;
                    }
                }
            }
        }

        public void Behave(Vector2 lastDirection)
        {
            if (ghost != null)
            {
                bool firstRun = false;

                if (lastDirection == GameVars.DIR.EMPTY)
                {
                    Random rndDir = new Random();
                    int choice = rndDir.Next(1, 4);
                    firstRun = true;

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

                    ghost.Direction = lastDirection;
                    ghost.LastDirection = lastDirection;
                    ghost.IsMoving = false;
                }

                lstDirection = lastDirection;

                if (!ghost.IsMoving || ghost.HasCollided)
                {
                    CheckWaves();
                    lastDirection = lstDirection;

                    /*
                    if ((ghost is Clyde || ghost is Blinky || ghost is Pinky) && !ghost.InSpawn)
                        ghostMode = GameVars.GHOST_MODE.CHASE;
                    else
                        ghostMode = GameVars.GHOST_MODE.SCATTER;

                    if (!ghost.InSpawn)
                        ghostMode = GameVars.GHOST_MODE.CHASE;

                    ghostMode = GameVars.GHOST_MODE.SCATTER;
                    /**/



                    Dictionary<string, int> dirWeights = new Dictionary<string, int>();

                    switch (ghostMode)
                    {
                        default:
                        case GameVars.GHOST_MODE.CHASE:
                            dirWeights = chaseBehaviour.Chase(lastDirection);
                            break;

                        case GameVars.GHOST_MODE.SCATTER:
                            dirWeights = scatterBehaviour.Scatter(lastDirection);
                            break;

                        case GameVars.GHOST_MODE.WANDER:
                            //dirWeights = wanderBehaviour.Wander(lastDirection);
                            break;
                    }

                    Move( ConfirmDirections(dirWeights, firstRun) );








                    /**
                    if (lastDirection == GameVars.DIR.LEFT)
                    {
                        // Going LEFT (default)
                        if (left >= 0 && left == down && left == up)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 3);

                            if (tmp == 1)
                                ghost.MoveLeft();
                            else if (tmp == 2)
                                ghost.MoveDown();
                            else
                                ghost.MoveUp();
                        }
                        else if (left >= 0 && (left == down || left == up))
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveLeft();
                            else if (left == down)
                                ghost.MoveDown();
                            else
                                ghost.MoveUp();
                        }
                        else if (down >= 0 && down == up)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveDown();
                            else
                                ghost.MoveUp();
                        }
                        else if (left >= down && left >= up)
                            ghost.MoveLeft();
                        else if (down > left && down >= up)
                            ghost.MoveDown();
                        else if (up > left && up > down)
                            ghost.MoveUp();
                        else
                            ghost.MoveRight();
                    }
                    else if (lastDirection == GameVars.DIR.RIGHT)
                    {
                        // Going RIGHT
                        if (right >= 0 && right == up && right == down)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 3);

                            if (tmp == 1)
                                ghost.MoveRight();
                            else if (tmp == 2)
                                ghost.MoveUp();
                            else
                                ghost.MoveDown();
                        }
                        else if (right >= 0 && (right == up || right == down))
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveRight();
                            else if (right == up)
                                ghost.MoveUp();
                            else
                                ghost.MoveDown();
                        }
                        else if (up >= 0 && up == down)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveUp();
                            else
                                ghost.MoveDown();
                        }
                        else if (right >= up && right >= down)
                            ghost.MoveRight();
                        else if (up > right && up >= down)
                            ghost.MoveUp();
                        else if (down > right && down > up)
                            ghost.MoveDown();
                        else
                            ghost.MoveLeft();
                    }
                    else if (lastDirection == GameVars.DIR.UP)
                    {
                        // Going UP
                        if (up >= 0 && up == left && up == right)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 3);

                            if (tmp == 1)
                                ghost.MoveUp();
                            else if (tmp == 2)
                                ghost.MoveLeft();
                            else
                                ghost.MoveRight();
                        }
                        else if (up >= 0 && (up == left || up == right))
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveUp();
                            else if (up == left)
                                ghost.MoveLeft();
                            else
                                ghost.MoveRight();
                        }
                        else if (left >= 0 && left == right)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveLeft();
                            else
                                ghost.MoveRight();
                        }
                        else if (up >= left && up >= right)
                            ghost.MoveUp();
                        else if (left > up && left >= right)
                            ghost.MoveLeft();
                        else if (right > up && right > left)
                            ghost.MoveRight();
                        else
                            ghost.MoveDown();
                    }
                    else
                    {
                        // Going DOWN
                        if (down >= 0 && down == right && down == left)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 3);

                            if (tmp == 1)
                                ghost.MoveDown();
                            else if (tmp == 2)
                                ghost.MoveRight();
                            else
                                ghost.MoveLeft();
                        }
                        else if (down >= 0 && (down == right || down == left))
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveDown();
                            else if (down == right)
                                ghost.MoveRight();
                            else
                                ghost.MoveLeft();
                        }
                        else if (right >= 0 && right == left)
                        {
                            Random rnd = new Random();
                            int tmp = rnd.Next(1, 2);

                            if (tmp == 1)
                                ghost.MoveRight();
                            else
                                ghost.MoveLeft();
                        }
                        else if (down >= right && down >= left)
                            ghost.MoveDown();
                        else if (right > down && right >= left)
                            ghost.MoveRight();
                        else if (left > down && left > right)
                            ghost.MoveLeft();
                        else
                            ghost.MoveUp();
                    }

                    /**/


                    // Avança posição


                    // Modo Susto ?

                    // Colidiu ?

                    // Espera 3 segundos para fazer respawn



                }



            }
        }
    }
}
