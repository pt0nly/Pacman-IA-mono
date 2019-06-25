using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using System.Collections.Generic;

namespace Pacman_IA.GameObjects
{
    public class Character
    {
        protected string name;
        protected Sprite sprite;
        protected Sprite spriteNormal;
        protected Sprite spriteScared;
        protected Vector2 location;
        protected Vector2 homeLocation;
        protected Vector2 destination;
        protected Vector2 spawnLocation;

        protected List<Vector2> patrolLocation;

        protected string command;
        protected Vector2 speed;
        protected Vector2 normalSpeed;
        protected Vector2 scaredSpeed;
        protected Vector2 direction;
        protected Vector2 lastDirection;

        protected float timer;

        protected Vector4 outerBound;
        protected Vector4 innerBound;
        protected Rectangle outerRect;
        protected Rectangle innerRect;
        protected int boundRadius = 27;

        protected int deltaMove;
        protected Vector2 gridLocation;
        protected bool moving;
        protected bool collided;
        protected bool okToBehave;

        protected bool toggleLeft;
        protected bool toggleRight;
        protected bool toggleUp;
        protected bool toggleDown;

        protected bool inSpawn;
        protected bool isDead;

        #region Properties

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public float Timer {
            get { return timer; }
            set { timer = value; }
        }

        public Vector2 Location {
            get { return location; }
            set { location = value; }
        }

        public Vector2 SpawnLocation
        {
            get { return spawnLocation; }
            private set { spawnLocation = value; }
        }

        public List<Vector2> PatrolLocation
        {
            get { return patrolLocation; }
            set { patrolLocation = value; }
        }

        public bool InSpawn
        {
            get { return inSpawn; }
            set { inSpawn = value; }
        }

        public Vector2 HomeLocation {
            get { return homeLocation; }
            set {
                homeLocation = value;
                InitBehaviour();
            }
        }

        public Vector2 GridLocation
        {
            get { return gridLocation; }
            set { gridLocation = value; }
        }

        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public Vector2 Direction
        {
            get { return lastDirection; }
            set { lastDirection = value; }
        }

        public Vector2 LastDirection
        {
            get { return lastDirection; }
            set { lastDirection = value; }
        }

        public Vector2 Speed {
            get { return speed; }
            set { speed = value; }
        }
        public Vector2 SpeedNormal { get { return normalSpeed; } }
        public Vector2 SpeedScared { get { return scaredSpeed; } }

        public Sprite CurrSprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Sprite SpriteNormal { get { return spriteNormal; } }
        public Sprite SpriteScared { get { return spriteScared; } }

        public bool OkToBehave
        {
            get { return okToBehave; }
            set { okToBehave = value; }
        }

        public Rectangle OuterBound { get { return outerRect; } }
        public Rectangle InnerBound { get { return innerRect; } }

        #endregion

        public void Respawn()
        {
            Location = spawnLocation;
            direction = Vector2.Zero;
            deltaMove = 0;
            gridLocation = Vector2.Zero;
            collided = false;
            moving = false;
            isDead = false;
            inSpawn = true;

            toggleLeft = false;
            toggleRight = false;
            toggleUp = false;
            toggleDown = false;

            boundRadius = (int)boundRadius / 2;

            calculateBoundaries();
            InitBehaviour();
        }

        public void SetLocation(Vector2 newLocation)
        {
            location = newLocation;
            spawnLocation = newLocation;
            calculateBoundaries();
        }

        protected virtual void LoadSprite()
        {
            scaredSpeed = new Vector2(40);
        }

        protected virtual void InitBehaviour()
        {
            okToBehave = true;
            timer = 0.0f;
        }

        public bool IsMoving
        {
            get { return moving; }
            set { moving = value; }
        }

        public bool HasCollided
        {
            get { return collided; }
            set { collided = value; }
        }

        protected void Init(string name, Vector2 location, string startCommand)
        {
            this.name = name;

            Location = location;
            HomeLocation = Vector2.Zero;
            patrolLocation = new List<Vector2>();
            speed = new Vector2(150, 150);
            direction = Vector2.Zero;
            deltaMove = 0;
            gridLocation = Vector2.Zero;
            collided = false;
            moving = false;
            isDead = false;

            toggleLeft = false;
            toggleRight = false;
            toggleUp = false;
            toggleDown = false;

            boundRadius = (int)boundRadius / 2;

            command = startCommand;

            sprite = null;
            spriteNormal = null;
            spriteScared = null;

            LoadSprite();

            calculateBoundaries();

            InitBehaviour();
        }

        protected void calculateBoundaries()
        {
            int width = sprite != null ? sprite.Width : 32;
            int height = sprite != null ? sprite.Height : 32;

            // Find center point
            Point center = location.ToPoint();
            Point outerPt = location.ToPoint();

            // Calculate innerRect location and size
            center.X += ((width / 2) - 14 + 6);
            center.Y += ((height / 2) - 14 + 6);

            outerPt.X += 2;
            outerPt.Y += 2;

            innerRect = new Rectangle(center, new Point(14));
            outerRect = new Rectangle(location.ToPoint(), new Point(width));
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

        protected virtual void CheckBehaviour()
        {
        }

        public virtual void MoveLeft()
        {
            if (!(this is Pacman) && GameVars.Pacman.PowerUp > 0.0f)
                if (isDead)
                    command = "dead";
                else if (GameVars.Pacman.PowerUp <= 3.0f)
                    command = "flash";
                else
                    command = "run";
            else
                command = "left";

            direction = GameVars.DIR.LEFT;

            Move();
        }

        public void MoveRight()
        {
            if (!(this is Pacman) && GameVars.Pacman.PowerUp > 0.0f)
                if (isDead)
                    command = "dead";
                else if (GameVars.Pacman.PowerUp <= 3.0f)
                    command = "flash";
                else
                    command = "run";
            else
                command = "right";

            direction = GameVars.DIR.RIGHT;

            Move();
        }

        public void MoveUp()
        {
            if (!(this is Pacman) && GameVars.Pacman.PowerUp > 0.0f)
                if (isDead)
                    command = "dead";
                else if (GameVars.Pacman.PowerUp <= 3.0f)
                    command = "flash";
                else
                    command = "run";
            else
                command = "up";

            direction = GameVars.DIR.UP;

            Move();
        }

        public void MoveDown()
        {
            if (!(this is Pacman) && GameVars.Pacman.PowerUp > 0.0f)
                if (isDead)
                    command = "dead";
                else if (GameVars.Pacman.PowerUp <= 3.0f)
                    command = "flash";
                else
                    command = "run";
            else
                command = "down";

            direction = GameVars.DIR.DOWN;

            Move();
        }

        private void Move()
        {
            Point tmpLocation = gridLocation.ToPoint();
            
            moving = true;
            lastDirection = direction;

            // Set destination
            Destination = GameMap.posLevel[tmpLocation.Y + direction.ToPoint().Y, tmpLocation.X + direction.ToPoint().X];

            okToBehave = false;
        }

        public virtual bool CharacterCollision(Character person)
        {
            return false;
        }

        public virtual void Update()
        {
            // Update current gridLocation
            gridLocation = GameMap.getGridLocation(location)[0];

            CheckBehaviour();

            Vector2 prevLocation = location;
            Point prevInnerLocation = innerRect.Location;
            Point prevOuterLocation = outerRect.Location;

            float xMovement = direction.X * speed.X * (float)GameVars.gameTime.ElapsedGameTime.TotalSeconds;
            float yMovement = direction.Y * speed.Y * (float)GameVars.gameTime.ElapsedGameTime.TotalSeconds;

            // Update location
            location.X += xMovement;
            location.Y += yMovement;

            if (direction.X < 0 && location.X < destination.X)
            {
                location.X = destination.X;
                moving = false;
            }
            else if (direction.X > 0 && location.X > destination.X)
            {
                location.X = destination.X;
                moving = false;
            }
            else if (direction.Y < 0 && location.Y < destination.Y)
            {
                location.Y = destination.Y;
                moving = false;
            }
            else if (direction.Y > 0 && location.Y > destination.Y)
            {
                location.Y = destination.Y;
                moving = false;
            }

            calculateBoundaries();

            collided = false;
            // Check for wall collisions
            foreach(var wall in GameMap.walls)
            {
                if (wall.Collided(this))
                {
                    collided = true;
                    break;
                }
            }

            CheckBehaviour();

            if (!moving)
            {
                if ((location.Y - 1) == destination.Y)
                {
                    // Bug boundary fix
                    location.Y = destination.Y;
                    innerRect.Location = new Point(location.ToPoint().X, location.ToPoint().Y - 1);
                    outerRect.Location = new Point(location.ToPoint().X, location.ToPoint().Y - 1);
                }
                okToBehave = true;
            }

            if (collided)
            {
                if ((location.Y - 1) == destination.Y)
                {
                    // Bug boundary fix
                    location.Y = destination.Y;
                    innerRect.Location = new Point(location.ToPoint().X, location.ToPoint().Y - 1);
                    outerRect.Location = new Point(location.ToPoint().X, location.ToPoint().Y - 1);
                }
                else
                {
                    location = prevLocation;
                    innerRect.Location = prevInnerLocation;
                    outerRect.Location = prevOuterLocation;
                }
                okToBehave = true;

                moving = true;
            }

            if (!(this is Pacman))
            {
                if (sprite == spriteNormal)
                {
                    if (command == "run" || command == "flash" || command == "dead")
                        command = "right";
                }
                else
                {
                    if (command != "run" && command != "flash" && command != "dead")
                        command = "run";
                }
            }

            // Update chosen animation
            sprite.animationPlay(command);
        }

        public void Draw()
        {
            sprite.Draw(location);

            
            var t = new Texture2D(GameGraphics.graphics.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });

            int bw = 2;
            // Uncomment to see boundary rectangles
            /**
            // Left
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Left, innerRect.Top, bw, innerRect.Height), Color.Blue);
            // Right
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Right, innerRect.Top, bw, innerRect.Height), Color.Blue);
            // Top
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Left, innerRect.Top, innerRect.Width, bw), Color.Blue);
            // Bottom
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Left, innerRect.Bottom, innerRect.Width, bw), Color.Blue);
            /**/

            /**
            // Left
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Top, bw, outerRect.Height), Color.Green);
            // Right
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Right, outerRect.Top, bw, outerRect.Height), Color.Green);
            // Top
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Top, outerRect.Width, bw), Color.Green);
            // Bottom
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Bottom, outerRect.Width, bw), Color.Green);
            /**/


            // Uncomment to see debug
            /**
            string menuMessage = "";
            string menuMessage1 = "";
            string menuMessage2 = "";
            string menuMessage3 = "";
            string menuMessage4 = "";
            string menuMessage5 = "";
            string menuMessage6 = "";
            Color cor = Color.White;
            Vector2 centerPosition = Vector2.Zero;

            menuMessage = "Grid: " + gridLocation.ToString();
            menuMessage1 = "Dir: " + direction.ToString();
            menuMessage2 = "Loc: " + location.ToString();
            menuMessage3 = "Dest: " + destination.ToString();
            menuMessage4 = "Home: " + GameMap.getGridLocation(homeLocation)[0].ToString();
            menuMessage5 = "Ok To Behave: " + OkToBehave.ToString();
            menuMessage6 = "Collided: " + collided.ToString();

            menuMessage4 = "Timer: " + timer.ToString();

            cor = Color.Red;

            float y1 = 346;
            float y2 = 476;
            float x1 = 50;
            float x2 = 300;
            float x3 = 600;

            if (this is Pacman)
            {
                menuMessage4 = "PowerUp: " + ((Pacman)this).PowerUp.ToString();
                centerPosition = new Vector2(x3, y1);
                cor = Color.Yellow;
            }
            else if (this is Blinky)
            {
                centerPosition = new Vector2(x2, y1);
                cor = Color.Red;
                menuMessage6 = "Speed: " + speed.ToString();
                menuMessage5 = "Ghost Mode: " + ((Blinky)this).GhostMode;
            }
            else if (this is Pinky)
            {
                centerPosition = new Vector2(x1, y1);
                cor = Color.Purple;
                menuMessage6 = "Speed: " + speed.ToString();
                menuMessage5 = "Ghost Mode: " + ((Pinky)this).GhostMode;
            }
            else if (this is Inky)
            {
                centerPosition = new Vector2(x2, y2);
                cor = Color.Cyan;
                menuMessage6 = "Speed: " + speed.ToString();
                menuMessage5 = "Ghost Mode: " + ((Inky)this).GhostMode;
            }
            else if (this is Clyde)
            {
                centerPosition = new Vector2(x1, y2);
                cor = Color.Orange;
                menuMessage6 = "Speed: " + speed.ToString();
                menuMessage5 = "Ghost Mode: " + ((Clyde)this).GhostMode;
            }

            float yStep = 16;

            // Grid Position
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage, centerPosition, cor);

            // Direction
            centerPosition.Y += yStep;
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage1, centerPosition, cor);

            // Location
            centerPosition.Y += yStep;
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage2, centerPosition, cor);

            // Destination
            centerPosition.Y += yStep;
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage3, centerPosition, cor);

            // Home Location
            centerPosition.Y += yStep;
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage4, centerPosition, cor);

            // Is Moving
            centerPosition.Y += yStep;
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage5, centerPosition, cor);

            // Has Collided
            centerPosition.Y += yStep;
            GameGraphics.spriteBatch.DrawString(GameGraphics.infoFont, menuMessage6, centerPosition, cor);
            /**/
        }
    }
}
