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
        protected Vector2 destination;

        protected string command;
        protected Vector2 speed;
        protected Vector2 direction;
        protected Vector2 lastDirection;

        protected Vector4 outerBound;
        protected Vector4 innerBound;
        protected Rectangle outerRect;
        protected Rectangle innerRect;
        protected int boundRadius = 27;

        protected int deltaMove;
        protected Vector2 gridLocation;
        protected bool moving;
        protected bool collided;

        protected bool toggleLeft;
        protected bool toggleRight;
        protected bool toggleUp;
        protected bool toggleDown;

        protected bool inSpawn;

        public string teste = "";

        #region Properties

        public Vector2 Location {
            get { return location; }
            set { location = value; }
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

        public Vector2 Speed { set { speed = value; } }

        public Rectangle OuterBound { get { return outerRect; } }
        public Rectangle InnerBound { get { return innerRect; } }

        #endregion

        public void SetLocation(Vector2 newLocation)
        {
            location = newLocation;
            calculateBoundaries();
        }

        protected virtual void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\ExtraGhost-Purple"), 4, 2);

            sprite.animationAdd("right", 0, 2, 100.0f);
            sprite.animationAdd("down", 2, 4, 100.0f);
            sprite.animationAdd("left", 4, 6, 100.0f);
            sprite.animationAdd("up", 6, 8, 100.0f);
        }

        protected virtual void InitBehaviour()
        {

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
            speed = new Vector2(150, 150);
            direction = Vector2.Zero;
            deltaMove = 0;
            gridLocation = Vector2.Zero;
            collided = false;
            moving = false;

            toggleLeft = false;
            toggleRight = false;
            toggleUp = false;
            toggleDown = false;

            //boundRadius /= 2;
            boundRadius = (int)boundRadius / 2;

            command = startCommand;

            /*
            if (command == "left")
                direction = new Vector2(-1, 0);
            else if (command == "right")
                direction = new Vector2(1, 0);
            else if (command == "up")
                direction = new Vector2(0, -1);
            else if (command == "down")
                direction = new Vector2(0, 1);
            */

            LoadSprite();

            calculateBoundaries();

            InitBehaviour();
        }

        protected void calculateBoundaries()
        {
            // Find center point
            Point center = location.ToPoint();
            Point outerPt = location.ToPoint();

            // Calculate innerRect location and size
            center.X += ((sprite.Width / 2) - 14);
            center.Y += ((sprite.Height / 2) - 14);

            outerPt.X += 2;
            outerPt.Y += 2;

            innerRect = new Rectangle(center, new Point(26));
            outerRect = new Rectangle(outerPt, new Point(sprite.Width-4));
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

        public void MoveLeft()
        {
            command = "left";
            direction = GameVars.DIR.LEFT;

            Move();
        }

        public void MoveRight()
        {
            command = "right";
            direction = GameVars.DIR.RIGHT;

            Move();
        }

        public void MoveUp()
        {
            command = "up";
            direction = GameVars.DIR.UP;

            Move();
        }

        public void MoveDown()
        {
            command = "down";
            direction = GameVars.DIR.DOWN;

            Move();
        }

        private void Move()
        {
            int lin = GridLocation.ToPoint().Y;
            int col = GridLocation.ToPoint().X;

            moving = true;
            lastDirection = direction;

            // Set destination
            Destination = GameMap.posLevel[lin + direction.ToPoint().Y, col + direction.ToPoint().X];
        }

        public virtual void Update()
        {
            gridLocation = GameMap.getGridLocation(location)[0];

            if (this is Pacman && gridLocation == new Vector2(16, 5))
            {
                string aa = "asda";
            }

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
            }

            /**/
            if (collided /*|| !moving*/)
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

                /*
                if (this is Pacman)
                {
                    int lin = GridLocation.ToPoint().Y;
                    int col = GridLocation.ToPoint().X;

                    if (lastDirection == GameVars.DIR.LEFT)
                    {
                        // Going LEFT

                        if (GameMap.wallLevel[lin + GameVars.DIR.DOWN.ToPoint().Y, col + GameVars.DIR.DOWN.ToPoint().X] == -1)
                        {
                            // DOWN is OK
                            direction = GameVars.DIR.DOWN;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.UP.ToPoint().Y, col + GameVars.DIR.UP.ToPoint().X] == -1)
                        {
                            // UP is OK
                            direction = GameVars.DIR.UP;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.RIGHT.ToPoint().Y, col + GameVars.DIR.RIGHT.ToPoint().X] == -1)
                        {
                            // RIGHT is OK
                            direction = GameVars.DIR.RIGHT;
                        }
                    }
                    else if (lastDirection == GameVars.DIR.RIGHT) {
                        // Going RIGHT

                        if (GameMap.wallLevel[lin + GameVars.DIR.DOWN.ToPoint().Y, col + GameVars.DIR.DOWN.ToPoint().X] == -1)
                        {
                            // DOWN is OK
                            direction = GameVars.DIR.DOWN;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.UP.ToPoint().Y, col + GameVars.DIR.UP.ToPoint().X] == -1)
                        {
                            // UP is OK
                            direction = GameVars.DIR.UP;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.LEFT.ToPoint().Y, col + GameVars.DIR.LEFT.ToPoint().X] == -1)
                        {
                            // LEFT is OK
                            direction = GameVars.DIR.LEFT;
                        }
                    }
                    else if (lastDirection == GameVars.DIR.DOWN)
                    {
                        // Going DOWN

                        if (GameMap.wallLevel[lin + GameVars.DIR.LEFT.ToPoint().Y, col + GameVars.DIR.LEFT.ToPoint().X] == -1)
                        {
                            // LEFT is OK
                            direction = GameVars.DIR.LEFT;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.RIGHT.ToPoint().Y, col + GameVars.DIR.RIGHT.ToPoint().X] == -1)
                        {
                            // RIGHT is OK
                            direction = GameVars.DIR.RIGHT;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.UP.ToPoint().Y, col + GameVars.DIR.UP.ToPoint().X] == -1)
                        {
                            // UP is OK
                            direction = GameVars.DIR.UP;
                        }
                    }
                    else
                    {
                        // Going UP

                        if (GameMap.wallLevel[lin + GameVars.DIR.LEFT.ToPoint().Y, col + GameVars.DIR.LEFT.ToPoint().X] == -1)
                        {
                            // LEFT is OK
                            direction = GameVars.DIR.LEFT;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.RIGHT.ToPoint().Y, col + GameVars.DIR.RIGHT.ToPoint().X] == -1)
                        {
                            // RIGHT is OK
                            direction = GameVars.DIR.RIGHT;
                        }
                        else if (GameMap.wallLevel[lin + GameVars.DIR.DOWN.ToPoint().Y, col + GameVars.DIR.DOWN.ToPoint().X] == -1)
                        {
                            // DOWN is OK
                            direction = GameVars.DIR.DOWN;
                        }
                    }

                    lastDirection = direction;
                    collided = false;
                }/**/
                moving = true;
            }
            /**/

            /*
            if (!moving)
                sprite.animationPause();
            */


            // Update chosen animation
            //if (moving || !(this is Pacman))
            sprite.animationPlay(command);
            /**/
        }

        public void Draw()
        {
            sprite.Draw(location);

            
            var t = new Texture2D(GameGraphics.graphics.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });

            int bw = 2;
            /**/
            // Left
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Left, innerRect.Top, bw, innerRect.Height), Color.Blue);
            // Right
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Right, innerRect.Top, bw, innerRect.Height), Color.Blue);
            // Top
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Left, innerRect.Top, innerRect.Width, bw), Color.Blue);
            // Bottom
            GameGraphics.spriteBatch.Draw(t, new Rectangle(innerRect.Left, innerRect.Bottom, innerRect.Width, bw), Color.Blue);
            /**/

            /**/
            // Left
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Top, bw, outerRect.Height), Color.Green);
            // Right
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Right, outerRect.Top, bw, outerRect.Height), Color.Green);
            // Top
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Top, outerRect.Width, bw), Color.Green);
            // Bottom
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Bottom, outerRect.Width, bw), Color.Green);
            /**/

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
                menuMessage5 = "Moving: " + IsMoving.ToString();
                menuMessage6 = "Collided: " + collided.ToString();

                cor = Color.Red;

            float y1 = 346;
            float y2 = 476;
            float x1 = 50;
            float x2 = 300;
            float x3 = 600;

            if (this is Pacman)
            {
                menuMessage4 = "";
                centerPosition = new Vector2(x3, y1);
                cor = Color.Yellow;
            }
            else if (this is Blinky)
            {
                centerPosition = new Vector2(x2, y1);
                cor = Color.Red;
            }
            else if (this is Pinky)
            {
                centerPosition = new Vector2(x1, y1);
                cor = Color.Purple;
            }
            else if (this is Inky)
            {
                centerPosition = new Vector2(x2, y2);
                cor = Color.Cyan;
            }
            else if (this is Clyde)
            {
                centerPosition = new Vector2(x1, y2);
                cor = Color.Orange;
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

        }
    }
}
