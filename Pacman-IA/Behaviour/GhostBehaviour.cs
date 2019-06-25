using Microsoft.Xna.Framework;
using Pacman_IA.Classes;
using Pacman_IA.GameObjects;
using System;
using System.Collections.Generic;

namespace Pacman_IA.Behaviour
{
    public class GhostBehaviour : IBehaviour
    {
        private Character ghost = null;
        private GameVars.GHOST_MODE ghostMode;
        private byte wave;
        private byte waveMode;

        private ChaseBehaviour chaseBehaviour = null;
        private ScatterBehaviour scatterBehaviour = null;
        private FrightnedBehaviour wanderBehaviour = null;

        private Vector2 lstDirection;


        #region Properties

        public GameVars.GHOST_MODE GhostMode
        {
            get { return ghostMode; }
        }

        #endregion


        public GhostBehaviour(Character person, ChaseBehaviour chase, ScatterBehaviour scatter, FrightnedBehaviour wander)
        {
            ghost = person;

            // Set Ghost's behaviours
            chaseBehaviour = chase;
            scatterBehaviour = scatter;
            wanderBehaviour = wander;

            lstDirection = GameVars.DIR.EMPTY;

            // Initiates on Scatter mode
            ghostMode = GameVars.GHOST_MODE.SCATTER;

            wave = 1;
            waveMode = 1;
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
                        dirWeights.Remove("left");

                    if (dirWeights["down"] < 0)
                        dirWeights.Remove("down");

                    if (dirWeights["up"] < 0)
                        dirWeights.Remove("up");
                }
                else if (lstDirection == GameVars.DIR.RIGHT)
                {
                    // Going RIGHT
                    if (dirWeights["right"] < 0)
                        dirWeights.Remove("right");

                    if (dirWeights["down"] < 0)
                        dirWeights.Remove("down");

                    if (dirWeights["up"] < 0)
                        dirWeights.Remove("up");
                }
                else if (lstDirection == GameVars.DIR.DOWN)
                {
                    // Going DOWN
                    if (dirWeights["down"] < 0)
                        dirWeights.Remove("down");

                    if (dirWeights["left"] < 0)
                        dirWeights.Remove("left");

                    if (dirWeights["right"] < 0)
                        dirWeights.Remove("right");
                }
                else
                {
                    // Going UP
                    if (dirWeights["up"] < 0)
                        dirWeights.Remove("up");

                    if (dirWeights["left"] < 0)
                        dirWeights.Remove("left");

                    if (dirWeights["right"] < 0)
                        dirWeights.Remove("right");
                }
            }

            if (lstDirection == GameVars.DIR.LEFT)
            {
                // Going LEFT
                choice = "";

                if (dirWeights.ContainsKey("left"))
                    choice = "left";
                else if (dirWeights.ContainsKey("down"))
                    choice = "down";
                else if (dirWeights.ContainsKey("up"))
                    choice = "up";
                else if (dirWeights.ContainsKey("right"))
                    choice = "right";
            }
            else if (lstDirection == GameVars.DIR.RIGHT)
            {
                // Going RIGHT
                choice = "";

                if (dirWeights.ContainsKey("right"))
                    choice = "right";
                else if (dirWeights.ContainsKey("up"))
                    choice = "up";
                else if (dirWeights.ContainsKey("down"))
                    choice = "down";
                else if (dirWeights.ContainsKey("left"))
                    choice = "left";
            }
            else if (lstDirection == GameVars.DIR.UP)
            {
                // Going UP
                choice = "";

                if (dirWeights.ContainsKey("up"))
                    choice = "up";
                else if (dirWeights.ContainsKey("left"))
                    choice = "left";
                else if (dirWeights.ContainsKey("right"))
                    choice = "right";
                else if (dirWeights.ContainsKey("down"))
                    choice = "down";
            }
            else
            {
                // Going DOWN
                choice = "";

                if (dirWeights.ContainsKey("down"))
                    choice = "down";
                else if (dirWeights.ContainsKey("right"))
                    choice = "right";
                else if (dirWeights.ContainsKey("left"))
                    choice = "left";
                else if (dirWeights.ContainsKey("up"))
                    choice = "up";
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

        private bool CheckWaves()
        {
            if (ghost.IsDead)
            {
                if (ghostMode != GameVars.GHOST_MODE.DEAD)
                {
                    ghostMode = GameVars.GHOST_MODE.DEAD;
                    ghost.Timer = 0.0f;
                }
                else
                {
                    // Update timer
                    ghost.Timer += (float)(GameVars.gameTime.ElapsedGameTime.TotalMilliseconds / 100);

                    if (ghost.Timer >= 3.0f)
                    {
                        // Respawn and exit behaviour
                        ghost.Respawn();
                        return false;
                    }
                }
            }
            else if (GameVars.Pacman.PowerUp > 0.0f)
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

                if (wave <= 4)
                {
                    // Update timer
                    ghost.Timer += (float)(GameVars.gameTime.ElapsedGameTime.TotalMilliseconds / 100);
                }

                if (wave <= 2)
                {
                    if (waveMode == 1)
                    {
                        if (ghost.Timer <= 7.0f) // Seconds
                        {
                            // Go to Scatter Mode
                            ghostMode = GameVars.GHOST_MODE.SCATTER;
                        }
                        else
                        {
                            // Start second mode
                            waveMode = 2;
                            ghost.Timer = 0.0f;
                        }
                    }

                    if (waveMode == 2)
                    {
                        if (ghost.Timer <= 20.0f) // Seconds
                        {
                            // Go to Chase/Hunt Mode
                            ghostMode = GameVars.GHOST_MODE.CHASE;
                        }
                        else
                        {
                            // Start second mode
                            wave++;
                            waveMode = 1;
                            ghost.Timer = 0.0f;
                        }
                    }
                }
                
                if (wave > 2 && wave <= 4)
                {
                    if (waveMode == 1)
                    {
                        if (ghost.Timer <= 5) // Seconds
                        {
                            // Go to Scatter Mode
                            ghostMode = GameVars.GHOST_MODE.SCATTER;
                        }
                        else
                        {
                            // Start second mode
                            waveMode = 2;
                            ghost.Timer = 0.0f;
                        }
                    }
                    
                    if (waveMode == 2)
                    {
                        if (ghost.Timer <= 20) // Seconds
                        {
                            // Go to Chase/Hunt Mode
                            ghostMode = GameVars.GHOST_MODE.CHASE;
                        }
                        else
                        {
                            // Start second mode
                            wave++;
                            waveMode = 1;
                            if (wave < 4)
                                ghost.Timer = 0.0f;
                        }
                    }
                }

                if (wave == 5)
                {
                    // Go to Chase/Hunt Mode
                    ghostMode = GameVars.GHOST_MODE.CHASE;
                    wave++;
                }
            }

            return true;
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
                    if (!CheckWaves())
                        return;

                    // Update LastDirection
                    lastDirection = lstDirection;

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

                        case GameVars.GHOST_MODE.DEAD:
                        case GameVars.GHOST_MODE.WANDER:
                            dirWeights = wanderBehaviour.Wander(lastDirection);
                            break;
                    }

                    Move( ConfirmDirections(dirWeights, firstRun) );
                }
            }
        }
    }
}
