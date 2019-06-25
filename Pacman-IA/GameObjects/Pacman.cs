using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using Pacman_IA.Behaviour;
using System;
using System.Collections.Generic;

namespace Pacman_IA.GameObjects
{
    public class Pacman : Character
    {
        private PacmanBehaviour pacmanBehaviour;

        private const float POWERUP_TIME = 10.0f; // 10 Seconds of PowerUp
        private float PowerUpTime = 0.0f; // Time left for PowerUp


        #region Properties

        public float PowerUp
        {
            get { return PowerUpTime; }
        }

        #endregion

        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pacman"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            normalSpeed = new Vector2(61);
            Speed = normalSpeed;
        }


        #region Constructor

        public Pacman() : base()
        {
            Init("Pacman", Vector2.Zero, "idle");
        }

        public Pacman(string startCommand) : base(startCommand)
        {
            Init("Pacman", Vector2.Zero, startCommand);
        }

        public Pacman(Vector2 location) : base(location)
        {
            Init("Pacman", location, "idle");
        }

        public Pacman(Vector2 location, string startCommand) : base(location, startCommand)
        {
            Init("Pacman", location, startCommand);
        }

        public Pacman(string name, Vector2 location) : base(name, location)
        {
            Init("Pacman", location, "idle");
        }

        public Pacman(string name, Vector2 location, string startCommand) : base(name, location, startCommand)
        {
            Init("Pacman", location, startCommand);
        }

        #endregion


        #region Behaviour

        public void StartPowerUpTime()
        {
            PowerUpTime = POWERUP_TIME;
        }

        private void CheckPowerUpTime()
        {
            // Check Elapsed time
            if (PowerUpTime > 0.0f)
            {
                PowerUpTime -= (float)(GameVars.gameTime.ElapsedGameTime.TotalMilliseconds / 1000);

                if (PowerUpTime < 0.0f)
                    PowerUpTime = 0.0f;
            }
        }

        protected override void InitBehaviour()
        {
            pacmanBehaviour = new PacmanBehaviour(this, new EatBehaviour(this), new HuntBehaviour(this));

            base.InitBehaviour();
        }

        protected override void CheckBehaviour()
        {
            if (okToBehave)
            {
                pacmanBehaviour.Behave(lastDirection);
            }
        }

        public override bool CharacterCollision(Character person)
        {
            if (GameVars.PacmanLives > 0 && PowerUpTime <= 0.0f)
            {
                if (this.innerRect.Intersects(person.InnerBound))
                {
                    GameVars.PacmanLives--;

                    if (person is Blinky)
                        GameVars.BlinkyScore++;
                    else if (person is Pinky)
                        GameVars.PinkyScore++;
                    else if (person is Inky)
                        GameVars.InkyScore++;
                    else if (person is Clyde)
                        GameVars.ClydeScore++;

                    if (GameVars.PacmanLives <= 0)
                        GameVars.GameOver = true;
                    else
                    {
                        GameVars.Blinky.Respawn();
                        GameVars.Pinky.Respawn();
                        GameVars.Inky.Respawn();
                        GameVars.Clyde.Respawn();

                        // Respawn Pacman
                        Respawn();
                    }

                    return true;
                }
            }

            return base.CharacterCollision(person);
        }

        #endregion

        public override void Update()
        {
            // Check PowerUp Timer
            CheckPowerUpTime();

            if (!CharacterCollision(GameVars.Blinky))
                if (!CharacterCollision(GameVars.Pinky))
                    if (!CharacterCollision(GameVars.Inky))
                        CharacterCollision(GameVars.Clyde);

            base.Update();
        }

    }
}
