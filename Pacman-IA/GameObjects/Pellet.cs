using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;
using System;

namespace Pacman_IA.GameObjects
{
    public class Pellet
    {
        private Sprite sprite;
        private Vector2 location;
        private GameVars.PELLET_TYPE type;
        public Color tint;
        private int boundRadius = 12;
        private Rectangle innerRect;
        public bool removed;

        private void Init(Sprite newSprite, Vector2 newLocation, GameVars.PELLET_TYPE newType, int frame)
        {
            removed = false;
            sprite = newSprite;
            location = newLocation;
            type = newType;

            boundRadius /= 2;

            if (type == GameVars.PELLET_TYPE.POWER)
            {
                sprite.animationAdd("idle", frame, frame + 2, true, 250.0f);
            }
            else
                sprite.animationAdd("idle", frame, frame, false, 0.0f);

            calculateBoundaries();

            tint = Color.White;
        }

        private void calculateBoundaries()
        {
            // Find center point
            Point center = new Point(location.ToPoint().X + 32 / 2, location.ToPoint().Y + 32 / 2);

            // Calculate innerRect location and size
            center.X -= boundRadius - 2;
            center.Y -= boundRadius - 2;
            innerRect = new Rectangle(center, new Point(boundRadius));
        }


        #region Constructor

        public Pellet(Sprite newSprite, Vector2 newLocation, int frame)
        {
            Init(newSprite, newLocation, GameVars.PELLET_TYPE.NORMAL, frame);
        }

        public Pellet(Sprite newSprite, Vector2 newLocation, int frame, GameVars.PELLET_TYPE newType)
        {
            Init(newSprite, newLocation, newType, frame);
        }

        #endregion

        public void Update()
        {
            if (GameVars.Game_Loaded)
            {
                if (this.innerRect.Intersects(GameVars.Pacman.InnerBound))
                    removed = true;
                else
                    sprite.animationPlay("idle");
            }
        }

        public void Draw()
        {
                sprite.Draw(location, tint);
        }
    }
}
