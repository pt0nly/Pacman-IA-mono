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
        private EatBehaviour eatBehaviour;
        private HuntBehaviour huntBehaviour;

        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pacman"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            Speed = new Vector2(60, 60);
        }

        protected override void InitBehaviour()
        {
            eatBehaviour = new EatBehaviour();
            huntBehaviour = new HuntBehaviour();
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


        public override void Update()
        {
            bool down = false;

            gridLocation = GameMap.getGridLocation(location)[0];

            //if (!IsMoving() || IsMoving())
            //{
                // Update which Animation/Movement
                KeyboardState state = Keyboard.GetState();



                if (state.IsKeyDown(Keys.Left) && !toggleLeft)
                {
                    toggleLeft = true;
                    moving = true;
                    command = "left";
                    direction = new Vector2(-1, 0);
                    //if (!IsMoving())
                    //deltaMove = 32;

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
                    down = true;
                    moving = true;
                    command = "right";
                    direction = new Vector2(1, 0);
                    //if (!IsMoving())
                    //deltaMove = 32;

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
                    down = true;
                    moving = true;
                    command = "down";
                    direction = new Vector2(0, 1);
                    //if (!IsMoving())
                    //deltaMove = 32;

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
                    down = true;
                    moving = true;
                    command = "up";
                    direction = new Vector2(0, -1);
                    //if (!IsMoving())
                    //deltaMove = 32;

                    int lin = gridLocation.ToPoint().Y - 1;
                    int col = gridLocation.ToPoint().X;
                    destination = GameMap.posLevel[lin, col];
                }
                else if (state.IsKeyUp(Keys.Up) && toggleUp)
                {
                    toggleUp = false;
                }
                else if (!moving)
                {
                    //command = "idle";
                    sprite.animationStop();
                    direction = Vector2.Zero;
                    deltaMove = 0;
                }
            //}

            base.Update();
        }
    }
}
