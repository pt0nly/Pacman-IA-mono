using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;

namespace Pacman_IA.Sprites
{
    public class Animation
    {
        private const float defaultSpeed = 45.0f;
        private const bool defaultLoop = true;

        private Texture2D texture;
        private int rows;
        private int columns;
        private int width;
        private int height;

        private string animKey;
        private int startFrame;
        private int endFrame;
        private int currentFrame;
        private bool loop;
        private bool playing;
        private float speed;
        private float elapsedTime;

        private Rectangle srcRect;
        private Rectangle dstRect;
        private Color tint;

        #region Properties

        private int CurrentRow { get { return (int)((float)currentFrame / (float)columns); } }

        private int CurrentColumn { get { return currentFrame % columns; } }

        public bool IsPlaying
        {
            get { return playing; }
            set { playing = value; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }

        #endregion


        #region Constructor

        public Animation(Texture2D texture, int width, int height, int rows, int columns, string animKey, int startIndex, int endIndex)
        {
            Add(texture, width, height, rows, columns, animKey, startIndex, endIndex, defaultLoop, defaultSpeed);
        }

        public Animation(Texture2D texture, int width, int height, int rows, int columns, string animKey, int startIndex, int endIndex, bool loop)
        {
            Add(texture, width, height, rows, columns, animKey, startIndex, endIndex, loop, defaultSpeed);
        }

        public Animation(Texture2D texture, int width, int height, int rows, int columns, string animKey, int startIndex, int endIndex, float speed)
        {
            Add(texture, width, height, rows, columns, animKey, startIndex, endIndex, defaultLoop, speed);
        }

        public Animation(Texture2D texture, int width, int height, int rows, int columns, string animKey, int startIndex, int endIndex, bool loop, float speed)
        {
            Add(texture, width, height, rows, columns, animKey, startIndex, endIndex, loop, speed);
        }

        private void Add(Texture2D texture, int width, int height, int rows, int columns, string animKey, int startIndex, int endIndex, bool loop, float speed)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.rows = rows;
            this.columns = columns;

            this.animKey = animKey;
            this.startFrame = startIndex;
            this.endFrame = endIndex;
            this.loop = loop;
            this.speed = speed;
            this.currentFrame = this.startFrame;
            this.elapsedTime = 0.0f;
        }

        #endregion


        public void Start()
        {
            IsPlaying = true;
            currentFrame = startFrame;
            elapsedTime = 0.0f;
        }

        public void Stop()
        {
            IsPlaying = false;
            currentFrame = startFrame;
            elapsedTime = 0.0f;
        }

        public void Update()
        {
            if (IsPlaying)
            {
                // Check Elapsed time
                elapsedTime += (float)GameVars.gameTime.ElapsedGameTime.TotalMilliseconds;

                if (speed > 0.0f && elapsedTime >= speed)
                {
                    currentFrame++;
                    if (currentFrame >= endFrame)
                    {
                        if (!loop) Stop();
                        else currentFrame = startFrame;
                    }

                    elapsedTime = 0.0f;
                }

                srcRect = new Rectangle(width * CurrentColumn, height * CurrentRow, width, height);
                tint = Color.White;
            }
        }

        public void Play(Vector2 location)
        {
            if (IsPlaying)
            {
                dstRect = new Rectangle((int)location.X, (int)location.Y, width, height);

                GameGraphics.spriteBatch.Draw(texture, dstRect, srcRect, tint);
            }
        }

        public void Play(Vector2 location, Color newTint)
        {
            tint = newTint;
            Play(location);
        }
    }
}
