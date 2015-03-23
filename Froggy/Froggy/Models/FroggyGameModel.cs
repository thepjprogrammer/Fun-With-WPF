using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Legacy;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;


namespace Froggy.Models
{
    public class FroggyGameModel : GameModel
    {
        public FroggyGameModel(int boardWidth, int boardHeight): base(boardWidth, boardHeight) { }

    }
}
