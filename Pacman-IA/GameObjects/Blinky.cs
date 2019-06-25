using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using Pacman_IA.Behaviour;

namespace Pacman_IA.GameObjects
{
    public class Blinky : Character
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
            // Normal Sprite
            spriteNormal = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Blinky-Red"), 4, 2);

            spriteNormal.animationAdd("right", 0, 2, 260.0f);
            spriteNormal.animationAdd("down", 2, 4, 260.0f);
            spriteNormal.animationAdd("left", 4, 6, 260.0f);
            spriteNormal.animationAdd("up", 6, 8, 260.0f);

            // Set the current sprite to Normal
            sprite = spriteNormal;

            normalSpeed = new Vector2(62);
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
            ghostBehaviour = new GhostBehaviour(this, new ChaseAggressive(this), new ScatterBehaviour(this, homeLocation), new FrightnedBehaviour(this));

            base.InitBehaviour();
        }

        protected override void CheckBehaviour()
        {
            ghostBehaviour.Behave(lastDirection);
        }

        public override bool CharacterCollision(Character person)
        {
            if (!isDead && person is Pacman && (GhostMode == GameVars.GHOST_MODE.WANDER || GameVars.Pacman.PowerUp > 0.0f))
            {
                if (this.innerRect.Intersects(person.InnerBound))
                {
                    isDead = true;
                    GameVars.Score += 200;
                    GameVars.PacmanScore++;

                    return true;
                }
            }

            return base.CharacterCollision(person);
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

        public override void Update()
        {
            CharacterCollision(GameVars.Pacman);

            base.Update();
        }
    }
}
