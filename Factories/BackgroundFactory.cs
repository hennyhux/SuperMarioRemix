﻿using GameSpace.Sprites.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Factories
{
    public class BackgroundFactory
    {
        private Texture2D RegularBackground;
        private Texture2D Mountain;
        private Texture2D Clouds;


        private static readonly BackgroundFactory instance = new BackgroundFactory();
        public static BackgroundFactory GetInstance()
        {
            return instance;
        }

        private BackgroundFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            //RegularBackground = content.Load<Texture2D>("Background/Background");
            RegularBackground = content.Load<Texture2D>("Background/small_BG");
            Mountain = content.Load<Texture2D>("Background/mountain");
            Clouds = content.Load<Texture2D>("Background/cloudsmall");
        }

        public BackgroundSprite CreateRegularBackground()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(RegularBackground, new Vector2(0, 334));
        }

        public BackgroundSprite CreateBGMountainSprite()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Mountain, new Vector2(0, 100));
        }

        public BackgroundSprite CreateCloudsSprite()//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Clouds, new Vector2(0, 0));
        }

        public BackgroundSprite CreateRegularBackground(Vector2 position)//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(RegularBackground, position);
        }

        public BackgroundSprite CreateBGMountainSprite(Vector2 position)//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Mountain, position);
        }

        public BackgroundSprite CreateCloudsSprite(Vector2 position)//mario.SmallMarioJumpingSprite
        {
            return new BackgroundSprite(Clouds, position);
        }

    }
}
