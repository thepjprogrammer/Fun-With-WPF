using Froggy.Characters;
using Froggy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using System.Reactive.Linq;


namespace Froggy.ViewModels
{
    public class FroggyViewModel : SinglePlayerGameViewModel
    {
        Grid _uiGrid;

        public FroggyViewModel(FroggyGameModel model, Grid uiGrid) : base(model)
        {
            _uiGrid = uiGrid;

            var froggy = _model.AddMainCharacter(_startLocation);
            var topDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.SLOW, new GamePieceLocation(1, 5));
            var secondDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.FAST, new GamePieceLocation(2, 5));
            var thirdDumperObstacle = _model.AddObstacle(1500, new GamePieceLocation(3, 5));
            var bottomDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.FAST, new GamePieceLocation(4, 5));

            AddMainCharacter(froggy, _startLocation);
            AddObstacle(topDumperObstacle);
            AddObstacle(secondDumperObstacle);
            AddObstacle(thirdDumperObstacle);
            AddObstacle(bottomDumperObstacle); 
        }

        public override void MoveMainCharacter(GamePieceMovementDirection direction)
        {
            GamePieceLocation newLocation = _userPiece.Move(direction);
            Grid.SetRow(_userCharacter, newLocation.row);
            Grid.SetColumn(_userCharacter, newLocation.column);

            CheckForGameOver(newLocation);
        }

        private void CheckForGameOver(GamePieceLocation location)
        {
            if (location.row > -1 && _model.LocationIsOccupied(location))
            {
                _userCharacter.RemoveFromBoard = true;
            }
        }

        override public void AddMainCharacter(UserPiece piece, GamePieceLocation location)
        {
            var frog = new Characters.LittleFroggy();
            frog.Width = 75;
            frog.Height = 75;
            Grid.SetRow(frog, location.row);
            Grid.SetColumn(frog, location.column);
            _uiGrid.Children.Add(frog);

            _userCharacter = frog;
            _userPiece = piece;
        }

        override public void AddObstacle(MovingObstaclePiece piece)
        {
            var bottomDumper = new Characters.Dumper();
            BindMovingObstacle(bottomDumper, piece);
        }

        private void AddPieceToUI(Character uiCharacter, MovingObstaclePiece piece)
        {
            BindMovingObstacle(uiCharacter, piece);
        }

        private void BindMovingObstacle(FrameworkElement el, MovingObstaclePiece piece)
        {
            //observe all user collisions 
            //and update displayed user character if collision occurs
            piece.ObservableForProperty(x => x.UserCollisionEminent)
            .Where(collisionChange => collisionChange.Value == true)
            .ObserveOn(ReactiveUI.RxApp.MainThreadScheduler)
            .Subscribe(userCollision =>
            {
                _userCharacter.RemoveFromBoard = true;
            });


            piece.Changed
            .SubscribeOn(ReactiveUI.RxApp.MainThreadScheduler)
            .ObserveOn(ReactiveUI.RxApp.MainThreadScheduler)
            .Subscribe(changedArgs =>
            {
                Grid.SetRow(el, piece.CurrentLocation.row);
                Grid.SetColumn(el, piece.CurrentLocation.column);

                //collision between user piece and obstacle
                if (_model.CollideWithUserPiece(piece.CurrentLocation))
                {
                    //_userCharacter.RemoveFromBoard = true;
                }
            });

            Grid.SetRow(el, piece.CurrentLocation.row);
            Grid.SetColumn(el, piece.CurrentLocation.column);
            _uiGrid.Children.Add(el);
        }

    }
}
