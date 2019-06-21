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

        protected Vector4 outerBound;
        protected Vector4 innerBound;
        protected Rectangle outerRect;
        protected Rectangle innerRect;
        protected int boundRadius = 27;

        protected int deltaMove;
        protected Vector2 gridLocation;
        protected bool moving;

        protected bool toggleLeft;
        protected bool toggleRight;
        protected bool toggleUp;
        protected bool toggleDown;

        protected string teste = "";

        #region Properties

        public Vector2 Location {
            set {
                location = value;
            }
        }
        public Vector2 HomeLocation {
            get { return homeLocation; }
            set { homeLocation = value; }
        }

        protected Vector2 Speed { set { speed = value; } }

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

        protected bool IsMoving()
        {
            if (deltaMove > 0)
            {
                return true;
            }

            return false;
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
            moving = false;

            toggleLeft = false;
            toggleRight = false;
            toggleUp = false;
            toggleDown = false;

            //boundRadius /= 2;
            boundRadius = (int)boundRadius / 2;

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

            //innerRect = new Rectangle(center, new Point(boundRadius * 2));
            innerRect = new Rectangle(center, new Point(26));
            //outerRect = new Rectangle(outerPt, new Point(sprite.Width-4));
            outerRect = new Rectangle(location.ToPoint(), new Point(sprite.Width));
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
            Vector2 prevLocation = location;
            Point prevInnerLocation = innerRect.Location;
            Point prevOuterLocation = outerRect.Location;

            float xMovement = direction.X * speed.X * (float)GameVars.gameTime.ElapsedGameTime.TotalSeconds;
            float yMovement = direction.Y * speed.Y * (float)GameVars.gameTime.ElapsedGameTime.TotalSeconds;




            /*
            if (!IsMoving())
            {
                Vector2 ss = GameMap.getGridLocation(location)[0];
                int lin = ss.ToPoint().Y;
                int col = ss.ToPoint().X;

                destination = GameMap.posLevel[lin, col];
                if (direction.X < 0)
                {
                    if (location.X <= destination.X)
                    {
                        xMovement = 0;
                        location.X = destination.X;
                    }
                    else if (destination.X > (location.X + xMovement))
                    {
                        xMovement = 0;
                        location.X = destination.X;
                    }
                }
                else if (direction.X > 0)
                {
                    if (location.X >= destination.X)
                    {
                        xMovement = 0;
                        location.X = destination.X;
                    }
                    else if (destination.X < (location.X + xMovement))
                    {
                        xMovement = 0;
                        location.X = destination.X;
                    }
                }
                else if (direction.Y < 0)
                {
                    if (location.Y <= destination.Y)
                    {
                        yMovement = 0;
                        location.Y = destination.Y;
                    }
                    else if (destination.Y > (location.Y + yMovement))
                    {
                        yMovement = 0;
                        location.Y = destination.Y;
                    }
                }
                else if (direction.Y > 0)
                {
                    if (location.Y >= destination.Y)
                    {
                        yMovement = 0;
                        location.Y = destination.Y;
                    }
                }
            }

            deltaMove -= (int)xMovement;
            deltaMove -= (int)yMovement;

            if (xMovement > 0 && deltaMove < 0)
            {
                xMovement += deltaMove;
                deltaMove = 0;
            }
            else if (yMovement > 0 && deltaMove < 0)
            {
                yMovement += deltaMove;
                deltaMove = 0;
            }

            /**/

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
            /*
            Vector2 locInner = prevInnerLocation.ToVector2();
            locInner.X += xMovement;
            locInner.Y += yMovement;
            innerRect.Location = locInner.ToPoint();

            Vector2 locOuter = prevOuterLocation.ToVector2();
            locOuter.X += xMovement;
            locOuter.Y += yMovement;
            outerRect.Location = locOuter.ToPoint();
            */


            /**/
            bool collided = false;
            foreach(var wall in GameMap.walls)
            {
                if (wall.Collided(this))
                {
                    collided = true;
                    break;
                }
            }
            /**/

            if (!collided && !moving)
            {
                if (this is Pacman) {
                // Check if we can continue on this direction
                    gridLocation = GameMap.getGridLocation(location)[0];
                    int lin = gridLocation.ToPoint().Y + direction.ToPoint().Y;
                    int col = gridLocation.ToPoint().X + direction.ToPoint().X;

                    teste = GameMap.wallLevel[lin, col].ToString();

                    if (GameMap.wallLevel[lin, col] == -1)
                    {
                        moving = true;
                        destination = GameMap.posLevel[lin, col];
                    }
                }
            }
            else
            {
                teste = "";
            }


            /**/
            if (collided || !moving)
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
            }
            /**/

            // Update chosen animation
            sprite.animationPlay(command);
        }

        public void Draw()
        {
            sprite.Draw(location);

            
            var t = new Texture2D(GameGraphics.graphics.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });

            int bw = 2;
            /*
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


            if (this is Pacman)
            {
                string menuMessage = gridLocation.ToString();
                string menuMessage1 = direction.ToString();
                string menuMessage2 = location.ToString();
                string menuMessage3 = destination.ToString();

                // Grid Position
                Vector2 sizeOfText = GameGraphics.gameFont.MeasureString(menuMessage);
                Vector2 centerPosition = new Vector2(GameGraphics.graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, 350);
                GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, menuMessage, centerPosition, Color.White);

                /*
                // teste
                sizeOfText = GameGraphics.gameFont.MeasureString(teste);
                centerPosition = new Vector2(GameGraphics.graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, 300);
                GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, teste, centerPosition, Color.Orange);
                /**/

                // Direction
                sizeOfText = GameGraphics.gameFont.MeasureString(menuMessage1);
                centerPosition = new Vector2(GameGraphics.graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, 400);
                GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, menuMessage1, centerPosition, Color.Yellow);

                // Location
                sizeOfText = GameGraphics.gameFont.MeasureString(menuMessage2);
                centerPosition = new Vector2(GameGraphics.graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, 450);
                GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, menuMessage2, centerPosition, Color.Green);

                // Destination
                sizeOfText = GameGraphics.gameFont.MeasureString(menuMessage3);
                centerPosition = new Vector2(GameGraphics.graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, 500);
                GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, menuMessage3, centerPosition, Color.Blue);

            }

        }
    }
}
