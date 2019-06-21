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
            GameMap.Setup(this);
        }

        protected override void Initialize()
        {
            GameVars.Game_Loaded = false;
            GameGraphics.Init();
            GameMap.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameGraphics.LoadContent();
            GameMap.LoadContent();

            GameVars.Game_Loaded = true;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameVars.gameTime = gameTime;
            GameGraphics.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameVars.gameTime = gameTime;

            GameGraphics.Draw();

            base.Draw(gameTime);
        }
    }
}
