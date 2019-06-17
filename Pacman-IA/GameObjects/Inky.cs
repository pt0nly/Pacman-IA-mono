using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Inky : Character
    {
        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Inky-Cyan"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            Speed = new Vector2(60, 60);
            Speed = Vector2.Zero;
        }


        #region Constructor

        public Inky() : base()
        {
            Init("Inky", Vector2.Zero, "idle");
        }

        public Inky(string startCommand) : base(startCommand)
        {
            Init("Inky", Vector2.Zero, startCommand);
        }

        public Inky(Vector2 location) : base(location)
        {
            Init("Inky", location, "idle");
        }

        public Inky(Vector2 location, string startCommand) : base(location, startCommand)
        {
            Init("Inky", location, startCommand);
        }

        public Inky(string name, Vector2 location) : base(name, location)
        {
            Init("Inky", location, "idle");
        }

        public Inky(string name, Vector2 location, string startCommand) : base(name, location, startCommand)
        {
            Init("Inky", location, startCommand);
        }

        #endregion
    }
}
