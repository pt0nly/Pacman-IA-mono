using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Character
    {
        protected string name;
        protected Sprite sprite;
        protected Vector2 location;
        protected Vector2 homeLocation;

        protected string command;
        protected Vector2 speed;
        protected Vector2 direction;

        #region Properties

        public Vector2 Location { set { location = value; } }
        public Vector2 HomeLocation {
            get { return homeLocation; }
            set { homeLocation = value; }
        }

        protected Vector2 Speed { set { speed = value; } }

        #endregion

        protected virtual void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\ExtraGhost-Purple"), 4, 2);

            sprite.animationAdd("right", 0, 2, 100.0f);
            sprite.animationAdd("down", 2, 4, 100.0f);
            sprite.animationAdd("left", 4, 6, 100.0f);
            sprite.animationAdd("up", 6, 8, 100.0f);
        }

        protected void Init(string name, Vector2 location, string startCommand)
        {
            this.name = name;

            Location = location;
            HomeLocation = Vector2.Zero;
            speed = new Vector2(150, 150);
            direction = Vector2.Zero;

            command = startCommand;

            if (command == "left")
                direction = new Vector2(-1, 0);
            else if (command == "right")
                direction = new Vector2(1, 0);
            else if (command == "up")
                direction = new Vector2(0, -1);
            else if (command == "down")
                direction = new Vector2(0, 1);

            LoadSprite();
        }

        #region Constructor

        public Character()
        {
            Init("player", Vector2.Zero, "idle");
        }

        public Character(string startCommand)
        {
            Init("player", Vector2.Zero, startCommand);
        }

        public Character(Vector2 location)
        {
            Init("player", location, "idle");
        }

        public Character(Vector2 location, string startCommand)
        {
            Init("player", location, startCommand);
        }

        public Character(string name, Vector2 location)
        {
            Init(name, location, "idle");
        }

        public Character(string name, Vector2 location, string startCommand)
        {
            Init(name, location, startCommand);
        }

        #endregion

        public virtual void Update()
        {
            /*
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
            */

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
