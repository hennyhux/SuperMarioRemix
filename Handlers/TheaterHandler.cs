﻿using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.Sprites.ExtraItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameSpace.EntityManaging
{
    public class TheaterHandler : Handler
    {
        private static readonly TheaterHandler instance = new TheaterHandler();

        private IGameObjects addItem;
        public static TheaterHandler GetInstance()
        {
            return instance;
        }

        private TheaterHandler()
        {
            addItem = null;
        }

        public void LoadData(List<IGameObjects> objectList)
        {

            gameEntityList = objectList;

            foreach (IGameObjects entity in objectList)
            {
                if (entity is WarpPipeHead)
                {
                    listOfWarpPipes.Add(entity);
                }

                if (entity.ObjectID == (int)ItemID.WARPPIPEROOM)
                {
                    listOfWarpRoomPipes.Add(entity);
                }
            }

            mario = (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);
        }

        public void ResetStaticMembers()
        {
            gameEntityList = new List<IGameObjects>();
            prunedList = new List<IGameObjects>();
            copyPrunedList = new List<IGameObjects>();
            animationList = new List<IObjectAnimation>();
            listOfWarpPipes = new List<IGameObjects>();
            listOfWarpRoomPipes = new List<IGameObjects>();
            musicList = new List<SoundEffect>();
            GameTime internalGametime = new GameTime();
            GameRoot gameRootCopy = new GameRoot();

            currentWarpLocation = 0;
            marioScores = 0;
            MarioHandler.marioLives = 3;
            damageTakenScale = 32;
            experinceScale = 1f;
            currentMarioLevel = 0;
        }

        public void RestartStaticMembers()
        {
            animationList = new List<IObjectAnimation>();
            listOfWarpPipes = new List<IGameObjects>();
            listOfWarpRoomPipes = new List<IGameObjects>();
            musicList = new List<SoundEffect>();
            GameTime internalGametime = new GameTime();
            GameRoot gameRootCopy = new GameRoot();

            currentWarpLocation = 0;
            marioScores = 0;
        }

        public void InitializeGameroot(GameRoot copy)
        {
            gameRoot = copy;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObjects entity in gameEntityList)
            {
                entity.Draw(spriteBatch);
            }

            foreach (IObjectAnimation animation in animationList)
            {
                animation.Draw(spriteBatch);
            }

        }

        public void Update(GameTime gametime)
        {

            internalGametime = gametime;

            if (addItem != null)
            {
                gameEntityList.Add(addItem);
                addItem = null;
            }

            foreach (IGameObjects entity in gameEntityList)
            {
                entity.Update(gametime);
            }

            foreach (IObjectAnimation animation in animationList)
            {
                animation.Update(gametime);
            }

            CollisionHandler.GetInstance().UpdateCollision();
        }

        public void ToggleCollisionBox()
        {
            foreach (IGameObjects entity in gameEntityList)
            {
                entity.ToggleCollisionBoxes();
            }
        }

        public void AddItemToStage(IGameObjects item)
        {
            gameEntityList.Add(item);
        }

        public void QueueItemAddToStage(IGameObjects item)
        {
            addItem = item;
        }

        public void ChangeStageToPlaying()
        {
            gameRoot.ChangeToPlayState();
        }

    }
}
