using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_IA.Classes;
using Pacman_IA.GameObjects;
using Pacman_IA.Sprites;

namespace Pacman_IA.UI
{
    public class InGameHud
    {
        private int worldCenterX;
        private int worldCenterY;

        // Buttons
        private GuiButton soundButton;
        private bool toggleSound;

        private GuiButton pauseButton;
        private bool togglePause;

        private PacmanLives pacmanLives;
        private CharacterScore pacmanScore;
        private CharacterScore blinkyScore;
        private CharacterScore pinkyScore;
        private CharacterScore inkyScore;
        private CharacterScore clydeScore;

        #region Classes

        private class PacmanLives
        {
            private Sprite sprite;
            private Vector2 location;

            public PacmanLives(Vector2 newLocation) {
                sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pacman"), 4, 2);
                sprite.animationAdd("lives", 0, 1, 0.0f);

                location = new Vector2(newLocation.X, newLocation.Y - sprite.Height);
            }

            public void Update()
            {
                sprite.animationPlay("lives");
            }

            public void Draw()
            {
                Vector2 currLocation;

                for (int i=0; i<GameVars.PacmanLives; i++)
                {
                    currLocation = location;
                    currLocation.X += i * (sprite.Width + 3);

                    sprite.Draw(currLocation);
                }
            }
        }

        private class CharacterScore
        {
            private Sprite sprite;
            private Sprite spriteScore;
            private Vector2 location;
            private Character person;
            private int score;

            public CharacterScore(Vector2 newLocation, Character newPerson)
            {
                person = newPerson;
                score = 0;

                if (person is Blinky)
                    sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Blinky-Red"), 4, 2);
                else if (person is Pinky)
                    sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pinky-Pink"), 4, 2);
                else if (person is Inky)
                    sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Inky-Cyan"), 4, 2);
                else if (person is Clyde)
                    sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Clyde-Orange"), 4, 2);
                else
                    sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pacman"), 4, 2);

                sprite.animationAdd("person", 0, 1, 0.0f);


                if (person is Pacman)
                    spriteScore = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\BlueGhost"), 3, 2);
                else
                    spriteScore = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Pacman"), 4, 2);

                spriteScore.animationAdd("score", 0, 1, 0.0f);


                location = new Vector2(newLocation.X, newLocation.Y - sprite.Height);
            }

            public void Update()
            {
                sprite.animationPlay("person");
                spriteScore.animationPlay("score");

                if (person is Pacman)
                    score = GameVars.PacmanScore;
                else if (person is Blinky)
                    score = GameVars.BlinkyScore;
                else if (person is Pinky)
                    score = GameVars.PinkyScore;
                else if (person is Inky)
                    score = GameVars.InkyScore;
                else if (person is Clyde)
                    score = GameVars.ClydeScore;
            }

            public void Draw()
            {
                sprite.Draw(location);

                Vector2 currLocation = location;
                currLocation.X += sprite.Width + 10;
                currLocation.Y -= 8;

                string text = "x";
                if (score == 0)
                    text += " " + score;

                GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, text, currLocation, Color.Yellow);

                currLocation.X -= 12;
                currLocation.Y = location.Y;
                for (int i = 0; i < score; i++)
                {
                    currLocation.X += (spriteScore.Width + 6);

                    spriteScore.Draw(currLocation);
                }
            }
        }

        #endregion

        public InGameHud()
        {
            setWorldCenter();

            pacmanLives = new PacmanLives(new Vector2(5, GameGraphics.SCREEN_HEIGHT - 5));

            int height = 32;
            int step = 5;
            int startX = 300;
            int startY = GameGraphics.SCREEN_HEIGHT - 0 - (5 * (height + step));

            pacmanScore = new CharacterScore(new Vector2(startX, startY), GameVars.Pacman);

            startY += height + step;
            blinkyScore = new CharacterScore(new Vector2(startX, startY), GameVars.Blinky);

            startY += height + step;
            pinkyScore = new CharacterScore(new Vector2(startX, startY), GameVars.Pinky);

            startY += height + step;
            inkyScore = new CharacterScore(new Vector2(startX, startY), GameVars.Inky);

            startY += height + step;
            clydeScore = new CharacterScore(new Vector2(startX, startY), GameVars.Clyde);
        }

        private void setWorldCenter()
        {
            worldCenterX = (int)GameGraphics.spriteBatch.GraphicsDevice.Viewport.Width / 2;
            worldCenterY = (int)GameGraphics.spriteBatch.GraphicsDevice.Viewport.Height / 2;
        }

        public void Update()
        {
            pacmanLives.Update();

            pacmanScore.Update();

            blinkyScore.Update();
            pinkyScore.Update();
            inkyScore.Update();
            clydeScore.Update();
        }

        public void Draw()
        {
            GameGraphics.spriteBatch.DrawString(GameGraphics.gameFont, "Score: " + GameVars.Score, new Vector2(10, GameGraphics.SCREEN_HEIGHT - 80), Color.WhiteSmoke);

            pacmanLives.Draw();

            pacmanScore.Draw();

            blinkyScore.Draw();
            pinkyScore.Draw();
            inkyScore.Draw();
            clydeScore.Draw();

            if (GameVars.GameOver)
            {
                string gameOver = "GameOver";
                string gameOverMsg = "You lost!";
                Color cor = Color.Red;

                if (GameVars.PacmanLives > 0 && GameMap.pellets.Count == 0)
                {
                    gameOverMsg = "You won!";
                    cor = Color.Green;
                }

                GameGraphics.spriteBatch.DrawString(GameGraphics.gameOverFont, gameOver, new Vector2(worldCenterX - 158, worldCenterY / 4), Color.Yellow);
                GameGraphics.spriteBatch.DrawString(GameGraphics.gameOverFont, gameOverMsg, new Vector2(worldCenterX - 130, worldCenterY / 4 + 80), cor);
            }
        }
    }
}
