using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Pacman : Character
    {
        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pacman"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            Speed = new Vector2(60, 60);
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
            // Update which Animation/Movement
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                command = "left";
                direction = new Vector2(-1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                command = "right";
                direction = new Vector2(1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                command = "down";
                direction = new Vector2(0, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                command = "up";
                direction = new Vector2(0, -1);
            }
            else
            {
                //command = "idle";
                sprite.animationStop();
                direction = Vector2.Zero;
            }

            base.Update();
        }
    }
}
