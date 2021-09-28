﻿using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.ItemStates
{
    public class SuperShroomState : IItemStates
    {
        private ISprite sprite;
        private ItemSpriteFactory itemFactory;
        private bool triggered;

        public SuperShroomState(Game1 game)
        {
            itemFactory = game.ItemSpriteFactory;
            this.sprite = itemFactory.ReturnSuperShrooom();
            triggered = false;
        }


        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            sprite.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
