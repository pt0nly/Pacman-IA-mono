using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.Classes;

namespace Pacman_IA
{
    public class Game1 : Game
    {
        public Game1()
        {
            GameGraphics.Setup(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameGraphics.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameGraphics.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameGraphics.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
