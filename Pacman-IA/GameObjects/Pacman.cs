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

        private const double POWERUP_TIME = 10.0d; // 10 Seconds of PowerUp
        private double PowerUpTime = 0.0d; // Time left for PowerUp


        #region Properties

        public double PowerUp
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

            Speed = new Vector2(55);
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

        protected override void InitBehaviour()
        {
            pacmanBehaviour = new PacmanBehaviour(this, new EatBehaviour(this), new HuntBehaviour(this));
        }

        protected override void CheckBehaviour()
        {
            pacmanBehaviour.Behave(lastDirection);
        }

        #endregion

        /*
        public override void Update()
        {
            gridLocation = GameMap.getGridLocation(location)[0];

            // Update which Animation/Movement
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left) && !toggleLeft)
            {
                toggleLeft = true;
                moving = true;
                command = "left";
                direction = new Vector2(-1, 0);
                lastDirection = direction;

                int lin = gridLocation.ToPoint().Y;
                int col = gridLocation.ToPoint().X - 1;
                destination = GameMap.posLevel[lin, col];
            }
            else if (state.IsKeyUp(Keys.Left) && toggleLeft)
            {
                toggleLeft = false;
            }
            else if (state.IsKeyDown(Keys.Right) && !toggleRight)
            {
                toggleRight = true;
                moving = true;
                command = "right";
                direction = new Vector2(1, 0);
                lastDirection = direction;

                int lin = gridLocation.ToPoint().Y;
                int col = gridLocation.ToPoint().X + 1;
                destination = GameMap.posLevel[lin, col];
            }
            else if (state.IsKeyUp(Keys.Right) && toggleRight)
            {
                toggleRight = false;
            }
            else if (state.IsKeyDown(Keys.Down) && !toggleDown)
            {
                toggleDown = true;
                moving = true;
                command = "down";
                direction = new Vector2(0, 1);
                lastDirection = direction;

                int lin = gridLocation.ToPoint().Y + 1;
                int col = gridLocation.ToPoint().X;
                destination = GameMap.posLevel[lin, col];

            }
            else if (state.IsKeyUp(Keys.Down) && toggleDown)
            {
                toggleDown = false;
            }
            else if (state.IsKeyDown(Keys.Up) && !toggleUp)
            {
                toggleUp = true;
                moving = true;
                command = "up";
                direction = new Vector2(0, -1);
                lastDirection = direction;

                int lin = gridLocation.ToPoint().Y - 1;
                int col = gridLocation.ToPoint().X;
                destination = GameMap.posLevel[lin, col];
            }
            else if (state.IsKeyUp(Keys.Up) && toggleUp)
            {
                toggleUp = false;
            }
            //else if (state.IsKeyDown(Keys.F6))
            //{
                //sprite.animationPause();
                //direction = Vector2.Zero;
                //deltaMove = 0;
            //}
            else if (!moving)
            {
                //command = "idle";
                //sprite.animationStop();
                //direction = Vector2.Zero;
                //deltaMove = 0;
            }

            base.Update();
        }
        /**/
    }
}
