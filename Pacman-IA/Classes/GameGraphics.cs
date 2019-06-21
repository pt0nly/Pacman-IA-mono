using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.GameObjects;
using Pacman_IA.Sprites;

namespace Pacman_IA.Classes
{
    public class GameGraphics
    {
        private const int SCREEN_WIDTH = 800;
        private const int SCREEN_HEIGHT = 600;

        private static Game game;

        public static GraphicsDeviceManager graphics;
        private static ContentManager content;
        public static SpriteBatch spriteBatch;
        public static SpriteFont gameFont;

        private static Texture2D background;
        private static Rectangle mainFrame;

        #region Properties

        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        #endregion

        public static void Setup(Game curGame)
        {
            game = curGame;
            graphics = new GraphicsDeviceManager(game);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;

            graphics.SynchronizeWithVerticalRetrace = true;

            game.IsMouseVisible = true;
            game.IsFixedTimeStep = true;

            Content = game.Content;
            Content.RootDirectory = "Content";
        }

        public static void Init()
        {
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
        }

        public static void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            // Load the background content.
            background = Content.Load<Texture2D>(@"textures\background");

            GameVars.Pacman = new Pacman(new Vector2(300, 300), "left");
            GameVars.Blinky = new Blinky(new Vector2(300, 350), "left");
            GameVars.Pinky = new Pinky(new Vector2(300, 400), "left");
            GameVars.Inky = new Inky(new Vector2(300, 450), "left");
            GameVars.Clyde = new Clyde(new Vector2(300, 500), "left");

            gameFont = Content.Load<SpriteFont>("gameFont");

            // Load map content.
            GameMap.LoadContent();
        }

        public static void Update()
        {
            // Check for Fullscreen HotKey
            if (Keyboard.GetState().IsKeyDown(Keys.F10))
                graphics.ToggleFullScreen();

            GameVars.Pacman.Update();
            GameVars.Blinky.Update();
            GameVars.Pinky.Update();
            GameVars.Inky.Update();
            GameVars.Clyde.Update();

            GameMap.Update();
        }

        public static void Draw()
        {
            game.GraphicsDevice.Clear(Color.Black);


            // Draw Tiled Background
            /**
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(background, Vector2.Zero, mainFrame, Color.White);
            spriteBatch.End();
            /**/

            spriteBatch.Begin();

            GameMap.Draw();

            GameVars.Pacman.Draw();
            GameVars.Blinky.Draw();
            GameVars.Pinky.Draw();
            GameVars.Inky.Draw();
            GameVars.Clyde.Draw();

            spriteBatch.End();
        }
    }
}
