using Froggy.Characters;
using Froggy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Froggy.ViewModels
{
    public abstract class SinglePlayerGameViewModel : GameViewModel
    {
        protected GamePieceLocation _startLocation = new GamePieceLocation(5, 3);
        protected Character _userCharacter;
        protected UserPiece _userPiece;

        public SinglePlayerGameViewModel(GameModel model): base(model)
        {

        }

        abstract public void MoveMainCharacter(GamePieceMovementDirection direction);

        abstract public void AddMainCharacter(UserPiece piece, GamePieceLocation location);

        abstract public void AddObstacle(MovingObstaclePiece piece);

    }
}
