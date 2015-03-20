using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Froggy.Models
{
    public class UserPiece : GamePiece
    {
        protected GameModel _model;
        protected GamePieceLocation _location;

        public UserPiece(GameModel myModel, GamePieceLocation startingLocation)
        {
            _model = myModel;
            _location = startingLocation;
        }

        public GamePieceLocation Move(GamePieceMovementDirection direction)
        {
            if (GamePieceMovementDirection.Up.Equals(direction) && _location.row > 0)
                _location.row--;
            else if (GamePieceMovementDirection.Left.Equals(direction) && _location.column > 0)
                _location.column--;
            else if (GamePieceMovementDirection.Right.Equals(direction) && _location.column < _model.BoardWidth)
                _location.column++;
            else if (GamePieceMovementDirection.Down.Equals(direction) && _location.row < _model.BoardHeight)
                _location.row++;

            return _location;
        }
    }
}
