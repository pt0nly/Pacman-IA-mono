using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Pinky : Character
    {
        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pinky-Pink"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            Speed = new Vector2(60, 60);
            Speed = Vector2.Zero;
        }


        #region Constructor

        public Pinky() : base()
        {
            Init("Pinky", Vector2.Zero, "idle");
        }

        public Pinky(string startCommand) : base(startCommand)
        {
            Init("Pinky", Vector2.Zero, startCommand);
        }

        public Pinky(Vector2 location) : base(location)
        {
            Init("Pinky", location, "idle");
        }

        public Pinky(Vector2 location, string startCommand) : base(location, startCommand)
        {
            Init("Pinky", location, startCommand);
        }

        public Pinky(string name, Vector2 location) : base(name, location)
        {
            Init("Pinky", location, "idle");
        }

        public Pinky(string name, Vector2 location, string startCommand) : base(name, location, startCommand)
        {
            Init("Pinky", location, startCommand);
        }

        #endregion
    }
}
