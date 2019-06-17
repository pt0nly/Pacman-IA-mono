using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Pellet
    {
        private Sprite sprite;
        private Vector2 location;
        private GameVars.PELLET_TYPE type;

        private void Init(Sprite newSprite, Vector2 newLocation, GameVars.PELLET_TYPE newType, int frame)
        {
            sprite = newSprite;
            location = newLocation;
            type = newType;

            if (type == GameVars.PELLET_TYPE.POWER)
            {
                sprite.animationAdd("idle", frame, frame + 2, true, 250.0f);
            }
            else
                sprite.animationAdd("idle", frame, frame, false, 0.0f);
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
            sprite.animationPlay("idle");
        }

        public void Draw()
        {
            sprite.Draw(location, Color.White);
        }
    }
}
