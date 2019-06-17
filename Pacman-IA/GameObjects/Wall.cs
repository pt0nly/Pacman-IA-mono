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

        public Wall(Sprite newSprite, Vector2 newLocation)
        {
            sprite = newSprite;
            location = newLocation;
            type = GameVars.WALL_TYPE.NORMAL;
        }

        public Wall(Sprite newSprite, Vector2 newLocation, GameVars.WALL_TYPE newType)
        {
            sprite = newSprite;
            location = newLocation;
            type = newType;
        }

        public void Update()
        {
            sprite.animationPlay("idle");
        }

        public void Draw()
        {
            sprite.Draw(location, type == GameVars.WALL_TYPE.NORMAL ? Color.White : Color.SlateGray);
        }
    }
}
