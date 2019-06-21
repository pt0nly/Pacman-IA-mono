using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using Pacman_IA.Behaviour;

namespace Pacman_IA.GameObjects
{
    public class Blinky : Character
    {
        private ChaseBehaviour chaseBehaviour;
        private ScatterBehaviour scatterBehaviour;
        private FrightnedBehaviour frightnedBehaviour;

        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Blinky-Red"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            Speed = new Vector2(60, 60);
            Speed = Vector2.Zero;
        }

        protected override void InitBehaviour()
        {
            chaseBehaviour = new ChaseAggressive();
            scatterBehaviour = new ScatterBehaviour();
            frightnedBehaviour = new FrightnetWandering();
        }

        #region Constructor

        public Blinky() : base()
        {
            Init("Blinky", Vector2.Zero, "idle");
        }

        public Blinky(string startCommand) : base(startCommand)
        {
            Init("Blinky", Vector2.Zero, startCommand);
        }

        public Blinky(Vector2 location) : base(location)
        {
            Init("Blinky", location, "idle");
        }

        public Blinky(Vector2 location, string startCommand) : base(location, startCommand)
        {
            Init("Blinky", location, startCommand);
        }

        public Blinky(string name, Vector2 location) : base(name, location)
        {
            Init("Blinky", location, "idle");
        }

        public Blinky(string name, Vector2 location, string startCommand) : base(name, location, startCommand)
        {
            Init("Blinky", location, startCommand);
        }

        #endregion
    }
}
