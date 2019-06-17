﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_IA.Classes;
using Pacman_IA.Sprites;

namespace Pacman_IA.GameObjects
{
    public class Clyde : Character
    {
        protected override void LoadSprite()
        {
            sprite = new Sprite(GameGraphics.Content.Load<Texture2D>(@"sprites\Clyde-Orange"), 4, 2);

            sprite.animationAdd("right", 0, 2, 260.0f);
            sprite.animationAdd("down", 2, 4, 260.0f);
            sprite.animationAdd("left", 4, 6, 260.0f);
            sprite.animationAdd("up", 6, 8, 260.0f);

            Speed = new Vector2(60, 60);
        }


        #region Constructor

        public Clyde() : base()
        {
            Init("Clyde", Vector2.Zero, "idle");
        }

        public Clyde(string startCommand) : base(startCommand)
        {
            Init("Clyde", Vector2.Zero, startCommand);
        }

        public Clyde(Vector2 location) : base(location)
        {
            Init("Clyde", location, "idle");
        }

        public Clyde(Vector2 location, string startCommand) : base(location, startCommand)
        {
            Init("Clyde", location, startCommand);
        }

        public Clyde(string name, Vector2 location) : base(name, location)
        {
            Init("Clyde", location, "idle");
        }

        public Clyde(string name, Vector2 location, string startCommand) : base(name, location, startCommand)
        {
            Init("Clyde", location, startCommand);
        }

        #endregion
    }
}