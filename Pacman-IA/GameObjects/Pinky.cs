using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using Pacman_IA.Behaviour;

namespace Pacman_IA.GameObjects
{
    public class Pinky : Character
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
            spriteNormal = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pinky-Pink"), 4, 2);

            spriteNormal.animationAdd("right", 0, 2, 260.0f);
            spriteNormal.animationAdd("down", 2, 4, 260.0f);
            spriteNormal.animationAdd("left", 4, 6, 260.0f);
            spriteNormal.animationAdd("up", 6, 8, 260.0f);

            // Set the current sprite to Normal
            sprite = spriteNormal;

            normalSpeed = new Vector2(64);
            Speed = normalSpeed;

            // Scared Sprite
            spriteScared = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\BlueGhost"), 3, 2);

            spriteScared.animationAdd("run", 0, 2, 260.0f);
            spriteScared.animationAdd("flash", 0, 4, 200.0f);
            spriteScared.animationAdd("dead", 4, 6, 260.0f);

            base.LoadSprite();
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


        #region Behaviour

        protected override void InitBehaviour()
        {
            ghostBehaviour = new GhostBehaviour(this, new ChaseAmbush(this), new ScatterBehaviour(this, homeLocation), new FrightnedBehaviour(this));

            base.InitBehaviour();
        }

        protected override void CheckBehaviour()
        {
            ghostBehaviour.Behave(lastDirection);
        }

        #endregion

    }
}
