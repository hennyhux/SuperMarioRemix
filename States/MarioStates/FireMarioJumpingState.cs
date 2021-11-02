﻿using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.States.MarioStates
{
    internal class FireMarioJumpingState : MarioActionStates//MarioPowerUpStates
    {
        public FireMarioJumpingState(Mario mario)
            : base(mario)
        {

        }

        public override void Enter(IMarioActionStates previousActionState)
        {
            Mario.marioActionState = this;
            this.previousActionState = previousActionState;
            // Mario.marioPowerUpState = new FireMarioState(Mario);
            Debug.WriteLine("MarioStandState(25) Enter, {0}", Mario.marioActionState);
            Debug.WriteLine("MarioWalkingState(25) facing:, {0}", Mario.Facing);

            //Set Proper velocity upon entering state
            Mario.Velocity = new Vector2(Mario.Velocity.X, -200);//NEW

            //AABB aabb = Mario.AABB;
            //eFacing Facing = MarioStandingState.Facing;
            eFacing Facing = Mario.Facing;
            Mario.Facing = Facing;
            //Mario.Sprite = MarioStandingState.SpriteFactory.CreateSprite(MarioSpriteFactory.MarioSpriteType(this, currentPowerUpState));
            Mario.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(this, Mario.marioPowerUpState));

            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(1);

            //play super jumping sound effect

        }

        public override void Exit() { }


        public override void StandingTransition()
        {//going to crouch for now(going to superstand
         //currentActionState.Exit();
            /// Debug.WriteLine("Fire Standtrans");

            Mario.marioActionState = new FireMarioStandingState(Mario);
            Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(2);
            Mario.marioActionState.Enter(this); // Changing states

        }
        public override void CrouchingTransition()
        {
            Exit();
            Mario.marioActionState = new FireMarioCrouchingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(3);
            Mario.marioActionState.Enter(this); // Changing states
        }//nothing
        public override void WalkingTransition()//Not Used Now, Used after Sprint2
        {
            Exit();

        }
        public override void RunningTransition()
        {
            Exit();
            Mario.marioActionState = new FireMarioRunningState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(4);
            Mario.marioActionState.Enter(this); // Changing states
        } //Longer you hold running you increase velocity and speed of animation
        public override void JumpingTransition()
        {

        }
        public override void FallingTransition()
        {
            Exit();
            Mario.marioActionState = new FireMarioFallingState(Mario);
            //Debug.WriteLine("MarioStandState(39) currentAState, {0}", Mario.marioActionState);
            //Mario.sprite = MarioFactory.GetInstance().CreateSprite(6);
            Mario.marioActionState.Enter(this); // Changing states
        }

        public override void FaceLeftTransition()
        {
            if (Mario.Facing == eFacing.LEFT)
            {
                RunningTransition();
            }
            // WalkingTransition(); bc no walking
            else
            {
                Mario.Facing = eFacing.LEFT;
            }
        }
        public override void FaceRightTransition()
        {

            if (Mario.Facing == eFacing.RIGHT)
            {
                RunningTransition();
            }
            // WalkingTransition();
            else
            {
                Mario.Facing = eFacing.RIGHT;
            }
        }

        public override void UpTransition()
        {

        }
        public override void DownTransition()
        {
            StandingTransition();
        }

        public override void SmallPowerUp()
        {
            Exit();
            Mario.marioActionState = new SmallMarioJumpingState(Mario);
            Mario.marioActionState.Enter(this);
        }
        public override void BigPowerUp()
        {
            Exit();
            Mario.marioActionState = new BigMarioJumpingState(Mario);
            Mario.marioActionState.Enter(this);
        }
        public override void FirePowerUp()
        {

        }
        public override void DeadPowerUp()
        {

        }

        public override void CrouchingDiscontinueTransition() { }//when you exit crouch, release down key
        public override void FaceLeftDiscontinueTransition() { }//generic entering walk and run, face left then start walking, then start running
        public override void FaceRightDiscontinueTransition() { }
        public override void WalkingDiscontinueTransition() { }//decelerata and go to standing
        public override void RunningDiscontinueTransition() { }//decelerate and go to walking dis
        public override void JumpingDiscontinueTransition() { }//abort jump or force jump to disc bc you reached apex of jump

        public override void Update(GameTime gametime)
        {
            Mario.Velocity += Mario.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Mario.Velocity = ClampVelocity(Mario.Velocity);
        }

        //void Update(GameTime gametime, GraphicsDeviceManager graphics);

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            return new Vector2(Mario.Velocity.X, Mario.Velocity.Y);//return the actualy velocity
        }
        // max velocity speed, clamp for each state speed
    }
}
