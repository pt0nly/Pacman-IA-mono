using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;

namespace Pacman_IA.Sprites
{
    public class Animated
    {
        private const float DEFAULT_SPEED = 45.0f;

        protected string name;
        protected Texture2D texture;
        protected int rows;
        protected int columns;
        protected int width;
        protected int height;
        protected int currentFrame;
        protected int totalFrames;

        private float elapsedTime;

        protected Vector2 location;
        protected Rectangle srcRect;
        protected Rectangle dstRect;


        #region Properties

        public string Name {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Rows
        {
            get { return this.rows; }

            set {
                this.rows = value;
                this.height = this.texture.Height / this.rows;
            }
        }

        public int Columns
        {
            get { return this.columns; }

            set {
                this.columns = value;
                this.width = this.texture.Width / this.columns;
            }
        }

        protected int CurrentRow
        {
            get { return (int)((float)currentFrame / (float)Columns); }
        }

        protected int CurrentColumn
        {
            get { return currentFrame % Columns; }
        }

        #endregion

        #region Constructor

        protected void InitSetup(Texture2D texture, int rows, int columns, string name)
        {
            this.name = name;
            this.texture = texture;
            Rows = rows;
            Columns = columns;

            currentFrame = 0;
            totalFrames = this.rows * this.columns;
            elapsedTime = 0.0f;
        }

        public Animated(Texture2D texture, int rows, int columns)
        {
            InitSetup(texture, rows, columns, "");
        }

        public Animated(Texture2D texture, int rows, int columns, string name)
        {
            InitSetup(texture, rows, columns, name);
        }

        #endregion


        public void Update(Vector2 location)
        {
            // Check Elapsed time
            elapsedTime += (float)GameVars.gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= 0f)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;

                elapsedTime = 0.0f;
            }

            this.location = location;
            srcRect = new Rectangle(width * CurrentColumn, height * CurrentRow, width, height);
            dstRect = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public void Draw()
        {
            GameGraphics.spriteBatch.Draw(texture, dstRect, srcRect, Color.White);
        }
    }
}
