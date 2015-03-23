using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Froggy.Models;
using Froggy.Characters;

namespace Froggy.ViewModels
{
    public class GameViewModel
    {
        protected GameModel _model;

        public GameViewModel(GameModel model)
        {
            _model = model;
        }


    }
}
