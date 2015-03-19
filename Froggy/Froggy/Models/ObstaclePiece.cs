using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Froggy.Models
{
    public class ObstaclePiece : GamePiece
    {
        protected int _movementRate;
        protected int _movementSpeed;
        protected GamePieceLocation _currentLocation;
        protected System.Timers.Timer _updateLocationTimer;
        protected GameModel _model; 

        public ObstaclePiece(GameModel myModel, int moveRate, int moveSpeed, GamePieceLocation myLocation)
        {
            _movementRate = moveRate;
            _movementSpeed = moveSpeed;
            _model = myModel;
            _currentLocation = myLocation;
        }
    }
}
