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
using ReactiveUI.Legacy;


namespace Froggy.Models
{
    public class GameModel : ReactiveObject
    {
        protected GamePiece[,] _playingBoard;
        protected int _boardHeight;
        protected int _boardWidth;

        public GameModel(int boardWidth, int boardHeight)
        {
            _boardHeight = boardHeight;
            _boardWidth = boardWidth;
            _playingBoard = new GamePiece[boardWidth, boardHeight];
        }

        public int BoardHeight
        {
            get { return _boardHeight; }
        }

        public int BoardWidth
        {
            get { return _boardWidth; }
        }

        public MovingObstaclePiece AddObstacle(int speed, GamePieceLocation location)
        {
            MovingObstaclePiece newPiece = new MovingObstaclePiece(this, 1, speed, location);
            _playingBoard[location.row, location.column] = newPiece;
            return newPiece;
        }

        public MovingObstaclePiece AddObstacle(GamePieceLocation location)
        {
            MovingObstaclePiece newPiece = new MovingObstaclePiece(this, 1, GamePieceMovementSpeed.SLOW, location);
            _playingBoard[location.row, location.column] = newPiece;
            return newPiece;
        }

        public UserPiece AddMainCharacter(GamePieceLocation location)
        {
            UserPiece newPiece = new UserPiece(this, location);
            _playingBoard[location.row, location.column] = newPiece;
            return newPiece;
        }

        public bool LocationIsOccupied(GamePieceLocation location)
        {
            return _playingBoard[location.row, location.column] != null;
        }

        public bool CollideWithUserPiece(GamePieceLocation location)
        {
            bool collision = false;
            var pieceAtLocation = _playingBoard[location.row, location.column];

            if (pieceAtLocation != null && pieceAtLocation is UserPiece)
                collision = true;

            return collision;
        }

        public void UpdatePieceLocation(GamePieceLocation oldLocation, GamePieceLocation newLocation)
        {
            var gamePiece = _playingBoard[oldLocation.row, oldLocation.column];
            _playingBoard[newLocation.row, newLocation.column] = gamePiece;
            _playingBoard[oldLocation.row, oldLocation.column] = null;
        }
    }
}
