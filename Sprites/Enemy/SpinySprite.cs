﻿using GameSpace.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameSpace.Machines;
using System.Diagnostics;

namespace GameSpace.Sprites
{
    public class SpinySprite : ISprite
    {
        private protected int currentFrame;
        private protected int totalFrames;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;

        private bool IsVisible;

        private int IsThrowing;
        public Texture2D Texture { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        protected Texture2D WhiteRect = SpriteBlockFactory.GetInstance().CreateBoundingBoxTexture();

        public int facingRight { get; set; }// left = 0, right = 1
        public SpriteEffects Facing { get; set; }
        private bool newState;

        /* Array Format is
         * First 34 is small, next 34 is big, next 34 is fire, then star, then dead
         * 0-8 and 18-25 is Left Actions and 9-17 and 26-34 is Right Actions
         * 0-8 [Death, Crouch, Jump, Turn Around/Skid, Fireball, Run/Walk x3, Idle] Flipped to Right Side [Idle, Run/Walk x3, Fireball, Turn Around/Skid, Jump, Crouch, Death] 9-12
         * 18-25 [Climb x2, Swim x6] Then Flipped to Right Side [Swim x6, Climb x2] 26-34
         * 
         * Small Mario does not have textures for crouching, fireball and Swim 5/6
         * Big Mario does not have textures for dying and fireball
         * Fire Mario does not have textures for dying
         *      Meaning the corresponding indexes will be 0's
         */
        private readonly int[] XFrame = new int[] { 0, 91, 180, 270 };
        private readonly int[] YFrame = new int[] { 5, 5, 5 ,5};
        private readonly int[] XWidth = new int[] { 50, 50, 50, 50 };
        private readonly int[] YHeight = new int[] { 50, 50, 50, 50 };
        private readonly int[] totalFramesAnimation = new int[] { 2, 2, 2, 2 };


        public void SetVisible() { IsVisible = !IsVisible; }
        public int invisibleCount = 0;

        public SpinySprite(Texture2D texture, int x, int y, int isFacingRight, int powerup, int action, bool isVisible)
        {
            //Debug.Print("Spiny SPRITE CREATED()");
            //Debug.Print("isFacingRight: {0} ", isFacingRight);
            Texture = texture;
            IsVisible = isVisible;
            currentFrame = 0;
            totalFrames = 0;
            facingRight = isFacingRight;
            newState = true;
            IsThrowing = action;

            Facing = SpriteEffects.None;

            #region time
            timeSinceLastFrame = 0;
            milliSecondsPerFrame = 275;
            #endregion
        }



        public void Update(GameTime gametime)
        {
            if (IsVisible)
            {
                int startingFrame = 0;
                totalFrames = totalFramesAnimation[currentFrame]; // gets previous frame's total frames in animation

                Facing = SpriteEffects.None;
                //startingFrame = (1 + 1 * (facingRight));
                
                //Debug.WriteLine("FACING RIGHT: {0},", facingRight);
                if (facingRight == 0)
                {
                    //currentFrame = 0;
                    startingFrame = 1;
                    //Debug.WriteLine("NOT FACING RIGHT," );
                }
                else if (facingRight == 1)
                {
                    //currentFrame = 2;
                    startingFrame = 2;
                    //Debug.WriteLine(" FACING RIGHT," );
                }
                //currentFrame = 0;
                //Debug.WriteLine(" currentFrame: {0},", currentFrame);
                /*else if (actionState == 2)//Walking
                {
                    startingFrame = (7 + 3 * (facingRight) + (34 * (marioPower)));
                }
                else if (actionState == 3)//Running
                {
                    startingFrame = (7 + 3 * (facingRight) + (34 * (marioPower)));
                }
                else if (actionState == 6)//Dying
                {
                    startingFrame = (0 + 17 * (facingRight));
                }*/


                if(newState == false)
                {
                    timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastFrame > milliSecondsPerFrame)
                    {
                        timeSinceLastFrame -= milliSecondsPerFrame;
                        if (facingRight == 0)
                        {
                            currentFrame = currentFrame - 1;
                        }
                        else
                        {
                            currentFrame = currentFrame + 1;
                        }
                    }

                    if (Math.Abs(currentFrame - startingFrame) >= totalFramesAnimation[startingFrame])
                    {
                        currentFrame = startingFrame;

                    }
                }
                else
                {
                    currentFrame = startingFrame;
                    newState = false;
                }

            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (IsVisible)
            {
                Width = XWidth[currentFrame];
                Height = YHeight[currentFrame];

                //Debug.WriteLine(" currentFrame {0},", currentFrame);
                Rectangle sourceRectangle = new Rectangle(XFrame[currentFrame], YFrame[currentFrame], Width, Height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 32, 32);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), Facing, 0);

            }
        }


        public void UpdateLocation(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Yellow * 0.4f);
        }

        /*public override virtual void UpdateCollisionBox(Vector2 location)
        {
            CollisionBox = new Rectangle((int)location.X, (int)location.Y,
                state.StateSprite.Texture.Width, state.StateSprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                state.StateSprite.Texture.Width, (state.StateSprite.Texture.Height * 2) + 4);
        }*/

        public Rectangle getCurrentSpriteRect()
        {
            return new Rectangle(XFrame[currentFrame], YFrame[currentFrame], XWidth[currentFrame], YHeight[currentFrame]);

        }
        public bool GetVisibleStatus()
        {
            return IsVisible;
        }

        public void FlipSprite()
        {

        }

        public void RevertSprite()
        {

        }
    }
}