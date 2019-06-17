using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;

namespace Pacman_IA.Sprites
{
    public class Sprite
    {
        private Texture2D texture;
        private int rows;
        private int columns;
        private int width;
        private int height;
        private Dictionary<string, Animation> animations;
        private string currentAnimation;
        private string previousAnimation;

        #region Properties

        private int Rows
        {
            get { return rows; }
            set {
                rows = value;
                height = texture.Height / rows;
            }
        }

        private int Columns
        {
            get { return columns; }
            set {
                columns = value;
                width = texture.Width / columns;
            }
        }

        public bool IsPlaying
        {
            get { return animations[currentAnimation].IsPlaying; }
        }

        #endregion


        #region Constructor

        public Sprite(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            Rows = rows;
            Columns = columns;

            this.animations = new Dictionary<string, Animation>();
            this.currentAnimation = "";
            this.previousAnimation = "";
        }

        #endregion


        #region animationAdd

        public void animationAdd(string animKey, int startIndex, int endIndex)
        {
            animations.Add(animKey, new Animation(texture, width, height, rows, columns, animKey, startIndex, endIndex));
        }

        public void animationAdd(string animKey, int startIndex, int endIndex, bool loop)
        {
            animations.Add(animKey, new Animation(texture, width, height, rows, columns, animKey, startIndex, endIndex, loop));
        }

        public void animationAdd(string animKey, int startIndex, int endIndex, float speed)
        {
            animations.Add(animKey, new Animation(texture, width, height, rows, columns, animKey, startIndex, endIndex, speed));
        }

        public void animationAdd(string animKey, int startIndex, int endIndex, bool loop, float speed)
        {
            animations.Add(animKey, new Animation(texture, width, height, rows, columns, animKey, startIndex, endIndex, loop, speed));
        }

        #endregion

        public void animationStop()
        {
            currentAnimation = "";
            previousAnimation = "";
        }

        public void animationPlay(string animKey)
        {
            currentAnimation = animKey;

            animationPlay();
        }

        private void animationPlay()
        {
            if (currentAnimation != previousAnimation)
            {
                if (previousAnimation != "")
                {
                    animations[previousAnimation].Stop();
                }
                animations[currentAnimation].Start();

                previousAnimation = currentAnimation;
            }

            if (currentAnimation != "")
                animations[currentAnimation].Update();
        }

        public void Update()
        {
            if (currentAnimation != "")
            {
                animationPlay();
            }
        }

        public void Draw(Vector2 location)
        {
            if (currentAnimation != "")
                animations[currentAnimation].Play(location);
        }
    }
}
