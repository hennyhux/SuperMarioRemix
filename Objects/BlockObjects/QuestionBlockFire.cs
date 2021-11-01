﻿using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.BlockObjects
{
    public class QuestionBlockFire : AbstractItemBlock
    {
        private IGameObjects fire;
        public QuestionBlockFire(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.QUESTIONBLOCK;
            state = new StateQuestionBlockIdle();
            Sprite = SpriteBlockFactory.GetInstance().ReturnQuestionBlock();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, (Sprite.Texture.Width * 2) / 3, Sprite.Texture.Height * 2);
            drawBox = false;
        }

        public override void Trigger()
        {
            state = new StateQuestionBlockBump(this);
            fire = ObjectFactory.GetInstance().CreateFireFlowerObject(new Vector2(Position.X - 4, Position.Y - Sprite.Texture.Height * 2 - 4));
            EntityManager.AddEntity(fire);
            revealedItem = true;
        }
    }
}
