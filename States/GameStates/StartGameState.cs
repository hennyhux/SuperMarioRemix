﻿using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.GameStates
{
    public abstract class Component
    {

    }

    public class StartGameState : State
    {
        private List<Component> components;

        private Texture2D menuBackGroundTexture;
        private List<IController> controllers;

        public StartGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            HUDHandler.GetInstance().DrawStartingPanel(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
        HUDHandler.GetInstance().LoadContent(content);
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
        }

        public override void Restart(Vector2 position)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
        }
    }
}
