﻿using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.Level
{
    public class LevelRestart
    {
        private protected GameRoot MyGame;
        public int lastCheckPoint;
        // public Vector2 positionBeforeDead;
        public LevelRestart(GameRoot game, int checkPoint)
        {
            MyGame = game;
            lastCheckPoint = 0;
            // positionBeforeDead = new Vector2(64, 402);
        }

        public void FindCheckPoint()
        {

            if (MyGame.GetMario.Position.X >= 5120 || lastCheckPoint == 2) //Checkpoint 2 - Randomly assigned
            {
                lastCheckPoint = 2;
            }
            else if (MyGame.GetMario.Position.X >= 2336 || lastCheckPoint == 1) //Checkpoint 1 - Randomly assigned
            {
                lastCheckPoint = 1;
            }
            else //Starting position
            {
                lastCheckPoint = 0;
            }
        }
        public Vector2 GetPosition()
        {
            Vector2 positionBeforeDead = new Vector2(64, 402);
            FindCheckPoint();
            if (lastCheckPoint == 2)
            {
                positionBeforeDead = new Vector2(5120, 402); //Checkpoint 2 - Randomly assigned
            }
            else if (lastCheckPoint == 1) //Checkpoint 1 - Randomly assigned
            {
                positionBeforeDead = new Vector2(2336, 402);
            }
            else //Starting position
            {
                positionBeforeDead = new Vector2(64, 402);
            }
            return positionBeforeDead;
        }

        public void Restart()
        {
            IMarioActionStates currentState = MyGame.GetMario.marioActionState;
            Vector2 positionBeforeDead = new Vector2(64, 402);
            FindCheckPoint();
            if (currentState is GameSpace.States.MarioStates.DeadMarioState)
            {
                MyGame.Restart();
            }
        }

    }
}
