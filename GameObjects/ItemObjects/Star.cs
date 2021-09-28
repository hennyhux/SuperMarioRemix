﻿using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Star : IItemObjects
    {
        private IItemStates state;

        public Star(Game1 game)
        {
            state = new StarState(game);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            state.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            state.Trigger();
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }
}
