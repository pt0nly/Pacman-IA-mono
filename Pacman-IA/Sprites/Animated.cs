using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;

namespace Pacman_IA.Sprites
{
    public class Animated
    {
        protected string name;
        protected Texture2D texture;
        protected int rows;
        protected int columns;
        protected int width;
        protected int height;
        protected int currentFrame;
        protected int totalFrames;

        protected Vector2 location;
        protected Rectangle srcRect;
        protected Rectangle dstRect;


        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public int Rows
        {
            get
            {
                return this.rows;
            }

            set
            {
                this.rows = value;
                this.height = this.texture.Height / this.rows;
            }
        }

        public int Columns
        {
            get
            {
                return this.columns;
            }

            set
            {
                this.columns = value;
                this.width = this.texture.Width / this.columns;
            }
        }

        #endregion

        protected void InitSetup(Texture2D texture, int rows, int columns, string name)
        {
            this.name = name;
            this.texture = texture;
            Rows = rows;
            Columns = columns;

            this.currentFrame = 0;
            this.totalFrames = this.rows * this.columns;
        }

        public Animated(Texture2D texture, int rows, int columns)
        {
            InitSetup(texture, rows, columns, "");
        }

        public Animated(Texture2D texture, int rows, int columns, string name)
        {
            InitSetup(texture, rows, columns, name);
        }

        protected int getCurrentRow()
        {
            return (int)((float)currentFrame / (float)columns);
        }

        protected int getCurrentColumn()
        {
            return currentFrame % columns;
        }

        public void Update(Vector2 location)
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;

            this.location = location;
            srcRect = new Rectangle(width * getCurrentColumn(), height * getCurrentRow(), width, height);
            dstRect = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public void Draw()
        {
            GameGraphics.spriteBatch.Draw(texture, dstRect, srcRect, Color.White);
        }
    }
}
