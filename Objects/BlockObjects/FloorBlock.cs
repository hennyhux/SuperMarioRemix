﻿using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class FloorBlock : IGameObjects
    {

        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle Rect { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public int ObjectID {get; set; }

        public FloorBlock(Vector2 initalPosition)
        {
            this.ObjectID = (int)BLOCKID.FlOORBLOCK;
            this.state = new StateBlockIdle();
            this.Sprite = SpriteBlockFactory.GetInstance().ReturnFloorBlock();
            this.Position = initalPosition;
            this.Rect = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            Debug.WriteLine("FLOOR BLOCK AT " + "(" + this.Position.X + ", " + this.Position.Y + ")");
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            //block does nothing 
        }

        public void SetPosition(Vector2 location)
        {
            //block doesnt move 
        }

        public void HandleCollision(IGameObjects entity)
        {
            
        }
    }
}
