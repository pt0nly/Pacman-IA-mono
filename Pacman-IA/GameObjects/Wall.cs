using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Wall
    {
        private Sprite sprite;
        private Vector2 location;
        private GameVars.WALL_TYPE type;
        private Rectangle outerRect;
        private int boundRadius = 32;

        public Wall(Sprite newSprite, Vector2 newLocation)
        {
            boundRadius /= 2;
            sprite = newSprite;
            location = newLocation;
            type = GameVars.WALL_TYPE.NORMAL;

            calculateBoundaries();
        }

        public Wall(Sprite newSprite, Vector2 newLocation, GameVars.WALL_TYPE newType)
        {
            sprite = newSprite;
            location = newLocation;
            type = newType;

            calculateBoundaries();
        }

        private void calculateBoundaries()
        {
            //outerRect = new Rectangle(location.ToPoint(), new Point(sprite.Width, sprite.Height));
            outerRect = new Rectangle(location.ToPoint(), new Point(32, 32));
        }

        public bool Collided(object other)
        {
            if (other is Pacman && this.outerRect.Intersects( ((Pacman)other).OuterBound) )
            {
                return true;
            }
            else if (other is Blinky && this.outerRect.Intersects( ((Blinky)other).OuterBound) )
            {
                return true;
            }
            else if (other is Pinky && this.outerRect.Intersects(((Pinky)other).OuterBound))
            {
                return true;
            }
            else if (other is Inky && this.outerRect.Intersects(((Inky)other).OuterBound))
            {
                return true;
            }
            else if (other is Clyde && this.outerRect.Intersects(((Clyde)other).OuterBound))
            {
                return true;
            }

            return false;
        }

        public void Update()
        {
            sprite.animationPlay("idle");
        }

        public void Draw()
        {
            sprite.Draw(location, type == GameVars.WALL_TYPE.NORMAL ? Color.White : Color.SlateGray);


            var t = new Texture2D(GameGraphics.graphics.GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White });

            int bw = 2;

            /**
            // Left
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Top, bw, outerRect.Height), Color.Red);
            // Right
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Right, outerRect.Top, bw, outerRect.Height), Color.Red);
            // Top
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Top, outerRect.Width, bw), Color.Red);
            // Bottom
            GameGraphics.spriteBatch.Draw(t, new Rectangle(outerRect.Left, outerRect.Bottom, outerRect.Width, bw), Color.Red);
            /**/


        }
    }
}
