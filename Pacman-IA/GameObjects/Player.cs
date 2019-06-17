using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Player
    {
        private string name;
        private Sprite sprite;
        private Vector2 location;

        private string command;
        private Vector2 speed;
        private Vector2 direction;

        #region Properties

        public Vector2 Location { set { location = value; } }

        #endregion


        private void Init(string name, Sprite sprite, Vector2 location, string startCommand)
        {
            this.name = name;
            this.sprite = sprite;
            Location = location;
            speed = new Vector2(150, 150);
            direction = Vector2.Zero;

            command = startCommand;
        }


        #region Constructor

        public Player(Sprite sprite)
        {
            Init("player", sprite, new Vector2(0, 0), "idle");
        }
        public Player(Sprite sprite, string startCommand)
        {
            Init("player", sprite, new Vector2(0, 0), startCommand);
        }

        public Player(Sprite sprite, Vector2 location, string startCommand)
        {
            Init("player", sprite, location, startCommand);
        }

        public Player(Sprite sprite, Vector2 location)
        {
            Init("player", sprite, location, "idle");
        }

        public Player(string name, Sprite sprite, Vector2 location, string startCommand)
        {
            Init(name, sprite, location, startCommand);
        }
        public Player(string name, Sprite sprite, Vector2 location)
        {
            Init(name, sprite, location, "idle");
        }

        #endregion


        public void Update()
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

            // Update location
            location.X += direction.X * speed.X * (float)GameVars.gameTime.ElapsedGameTime.TotalSeconds;
            location.Y += direction.Y * speed.Y * (float)GameVars.gameTime.ElapsedGameTime.TotalSeconds;

            // Update chosen animation
            sprite.animationPlay(command);
        }

        public void Draw()
        {
            sprite.Draw(location);
        }
    }
}
