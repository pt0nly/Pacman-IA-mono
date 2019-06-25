using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.UI
{
    public class GuiButton
    {
        private SpriteBatch spriteBatch;
        private Sprite sprite;
        private Vector2 location;

        #region Properties

        public Vector2 Location
        {
            get { return location; }
        }

        #endregion

        public GuiButton(Sprite newSprite, Vector2 newLocation)
        {
            location = newLocation;
            sprite = newSprite;
        }

        public void Update()
        {
            sprite.Update();
        }

        public void Draw()
        {
            // Draw the Button
            sprite.Draw(location);
        }
    }
}
