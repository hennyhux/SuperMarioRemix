﻿using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class UsedBlock : IGameObjects
    {

        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private Boolean hasCollided;
        private Boolean drawBox;

        public UsedBlock(Vector2 initalPosition)
        {
            this.ObjectID = (int)BlockID.USEDBLOCK;
            this.state = new StateBlockIdle();
            this.Sprite = SpriteBlockFactory.GetInstance().ReturnUsedBlock();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }
        public void HandleCollision(IGameObjects entity)
        {
            
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}