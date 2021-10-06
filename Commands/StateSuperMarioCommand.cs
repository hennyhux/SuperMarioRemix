﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class StateSuperMarioCommand : ICommand
    {
        private protected GameRoot game;

        public StateSuperMarioCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.GetMario.Big();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
