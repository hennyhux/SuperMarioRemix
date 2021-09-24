﻿using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects
{
    public class StairBlock : IBlockObjects
    {
        private IBlockStates state;

        public StairBlock(Game1 game)
        {
            this.state = new StairBlockState(game);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            state.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            state.Initiate();
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }
}
