using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman_IA.Classes
{
    public class GameGraphics
    {
        private const int SCREEN_WIDTH = 800;
        private const int SCREEN_HEIGHT = 600;

        private static Game game;

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        private static Texture2D background;
        private static Rectangle mainFrame;

        public static void Setup(Game curGame)
        {
            game = curGame;
            graphics = new GraphicsDeviceManager(game);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;
        }

        public static void Init()
        {
            game.IsMouseVisible = true;
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);

            GameVars.Pacman = new GameItem();
            GameVars.Blinky = new GameItem();
            GameVars.Pinky = new GameItem();
            GameVars.Inky = new GameItem();
            GameVars.Clyde = new GameItem();
        }

        public static void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            // Load the background content.
            background = game.Content.Load<Texture2D>(@"textures\background");

            GameVars.Pacman.Texture = game.Content.Load<Texture2D>(@"sprites\Pacman");
            GameVars.Blinky.Texture = game.Content.Load<Texture2D>(@"sprites\Blinky-Red");
            GameVars.Pinky.Texture = game.Content.Load<Texture2D>(@"sprites\Pinky-Pink");
            GameVars.Inky.Texture = game.Content.Load<Texture2D>(@"sprites\Inky-Cyan");
            GameVars.Clyde.Texture = game.Content.Load<Texture2D>(@"sprites\Clyde-Orange");
        }

        public static void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Yellow);


            // Draw Tiled Background
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(background, Vector2.Zero, mainFrame, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            spriteBatch.End();
        }
    }
}
