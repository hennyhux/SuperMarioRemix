﻿using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class AbstractBlock : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }

        internal bool drawBox;
        internal bool hasCollided;
        internal IBlockStates state;
        internal AbstractItem item;
        internal bool revealedItem;

        public AbstractBlock()
        {
            drawBox = false;
            revealedItem = false;
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        public virtual void UpdateCollisionBox(Vector2 location, GameTime gametime)
        {
            //Block does not move
        }
        public virtual bool IsCurrentlyColliding()
        {
            return false;
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void Trigger()
        {
            //bump and reveal the item when triggered
        }

        public virtual void HandleCollision(IGameObjects entity)
        {
            hasCollided = true;
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().BlockToMarioCollision(this);
                    break;
            }
        }

        public abstract bool RevealItem();
    }
}
