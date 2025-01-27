﻿using GameSpace.Abstracts;
using GameSpace.Camera2D;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Level;
using GameSpace.Machines;
using GameSpace.TileMapDefinition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSpace.States.GameStates
{
    public class PlayingGameState : GameState
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;
        private readonly LevelRestart levelRestart;
        private bool startOfGame = true;

        // DeathTimer timer;
        public Color FontColor { get; set; } = Color.DarkBlue;


        //Camera Stuff
        private Camera camera;
        private Vector2 parallax = new Vector2(1f);


        //Scrolling Background, Manually Setting
        private List<Layer> layers;
        // Background texture

        #region Lists
        private List<IController> controllers;
        private List<IGameObjects> objects;
        public List<SoundEffect> soundEffects;
        #endregion

        //private readonly string xmlFileName = "../../../Levels/Level1.xml";
        private readonly string xmlFileName = "../../../Levels/TestingLevel.xml";

        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);

        public PlayingGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            levelRestart = new LevelRestart(game, 0);
        }

        public override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }
        public override void Reset()
        {
            TheaterHandler.GetInstance().ResetStaticMembers();
            TheaterHandler.GetInstance().InitializeGameroot(game);
            startOfGame = true;
            Debug.Print("Lives Reset in Reset()");
            MarioHandler.GetInstance().ResetMarioLives(); //set lives to 3
            Initialize();
        }
        public override void Restart()
        {
            TheaterHandler.GetInstance().RestartStaticMembers();
            TheaterHandler.GetInstance().InitializeGameroot(game);
            startOfGame = false;
            Initialize();
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            SpriteBlockFactory.GetInstance().LoadContent(content);
            MarioFactory.GetInstance(game).LoadContent(content);
            SpriteEnemyFactory.GetInstance().LoadContent(content);
            SpriteExtraItemsFactory.GetInstance().LoadContent(content);
            BackgroundFactory.GetInstance().LoadContent(content);
            SpriteItemFactory.GetInstance().LoadContent(content);
            BackgroundFactory.GetInstance().LoadContent(content);
            AudioFactory.GetInstance().LoadContent(content);

            MusicHandler.GetInstance().LoadMusicIntoList(AudioFactory.GetInstance().loadList());
            #region Loading Lists

            if (startOfGame)
            {
                objects = Loader.Load(game, xmlFileName, new Vector2(0, 0), startOfGame);
            }
            else
            {
                objects = Loader.Load(game, xmlFileName, levelRestart.GetPosition(), startOfGame);
            }
            #endregion

            #region Loading Handlers
            HUDHandler.GetInstance().LoadContent(content, game);
            TheaterHandler.GetInstance().LoadData(objects);
            #endregion

            #region Loading Controllers
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
            #endregion

            //Camera Stuff
            camera = new Camera(graphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 480) };//Should be set to level's max X and Y

            CameraHandler.GetInstance().LoadCamera(camera);

            //Scrolling Background, Manually Setting
            layers = new List<Layer>
            {
                new Layer(camera, BackgroundFactory.GetInstance().CreateCloudsSprite(), new Vector2(2.0f, 1.0f)),
                //new Layer(camera, BackgroundFactory.GetInstance().CreateBGMountainSprite(), new Vector2(1.5f, 1.0f)),
                new Layer(camera, BackgroundFactory.GetInstance().CreateRegularBackground1(), new Vector2(1.4f, 1.0f)),
                new Layer(camera, BackgroundFactory.GetInstance().CreateBlackWindow2(), new Vector2(1.0f, 0.0f)),
            };

            //Play Song
            MusicHandler.GetInstance().LoadSong(AudioFactory.GetInstance().CreateSong());
            MusicHandler.GetInstance().PlaySong();
        }

        public override void Update(GameTime gameTime)
        {
            HUDHandler.GetInstance().LoadGameTime(gameTime);
            HUDHandler.GetInstance().UpdateTimer();
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            TheaterHandler.GetInstance().Update(gameTime);
            //Camera Stuff- Centered Mario
            camera.LookAt(new Vector2(GetMario.Position.X + GetMario.CollisionBox.Width / 2, graphicsDevice.Viewport.Height / 2));
            CameraHandler.GetInstance().DebugCameraFindLimits();
            levelRestart.Restart();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            //Background/Scrolling Stuff
            foreach (Layer layer in layers)
            {
                layer.Draw(spriteBatch, camera.Position);
            }
            //Normal Sprites
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(parallax));
            TheaterHandler.GetInstance().Draw(spriteBatch);
            HUDHandler.GetInstance().Draw(spriteBatch);
            spriteBatch.End();
        }

    }

}