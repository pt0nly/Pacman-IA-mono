using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using Pacman_IA.Behaviour;

namespace Pacman_IA.GameObjects
{
    public class Inky : Character
    {
        private GhostBehaviour ghostBehaviour;

        #region Properties

        public GameVars.GHOST_MODE GhostMode
        {
            get { return ghostBehaviour.GhostMode; }
        }

        #endregion

        protected override void LoadSprite()
        {
            spriteNormal = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Inky-Cyan"), 4, 2);

            spriteNormal.animationAdd("right", 0, 2, 260.0f);
            spriteNormal.animationAdd("down", 2, 4, 260.0f);
            spriteNormal.animationAdd("left", 4, 6, 260.0f);
            spriteNormal.animationAdd("up", 6, 8, 260.0f);

            // Set the current sprite to Normal
            sprite = spriteNormal;

            normalSpeed = new Vector2(63);
            Speed = normalSpeed;

            // Scared Sprite
            spriteScared = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\BlueGhost"), 3, 2);

            spriteScared.animationAdd("run", 0, 2, 260.0f);
            spriteScared.animationAdd("flash", 0, 4, 200.0f);
            spriteScared.animationAdd("dead", 4, 6, 260.0f);

            base.LoadSprite();
        }

        protected override void InitBehaviour()
        {
            ghostBehaviour = new GhostBehaviour(this, new ChasePatrol(this), new ScatterBehaviour(this, homeLocation), new FrightnedBehaviour(this));

            base.InitBehaviour();
        }

        protected override void CheckBehaviour()
        {
            ghostBehaviour.Behave(lastDirection);
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
