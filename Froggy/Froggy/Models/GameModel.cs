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
        private ReactiveUI.Legacy.ReactiveCommand _moveMainCharacterCommand;
        private GamePiece[,] playingBoard;

        public GameModel(int boardWidth, int boardHeight)
        {
            playingBoard = new GamePiece[boardWidth, boardHeight];

            //TODO: Handle moving main character around the board...
            /**_moveMainCharacterCommand = new ReactiveUI.Legacy.ReactiveCommand();
            _moveMainCharacterCommand.RegisterAsyncAction(_ =>
            {
                
            });**/

        }


        public MovingObstaclePiece AddObstacle(int speed, GamePieceLocation location)
        {
            MovingObstaclePiece newPiece = new MovingObstaclePiece(this, 1, speed, location);
            playingBoard[location.row, location.column] = newPiece;
            return newPiece;
        }

        public MovingObstaclePiece AddObstacle(GamePieceLocation location)
        {
            MovingObstaclePiece newPiece = new MovingObstaclePiece(this, 1, GamePieceMovementSpeed.SLOW, location);
            playingBoard[location.row, location.column] = newPiece;
            return newPiece;
        }

    }
}
