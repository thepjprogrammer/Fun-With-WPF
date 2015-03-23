using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Legacy;
using ReactiveUI;

namespace Froggy.Models
{
    public class MovingObstaclePiece : ObstaclePiece
    {
        //ReactiveUI 6.0 code
        //private ReactiveCommand<object> _moveCommand;
        
        //old school reactive ui code for registering a function
        private ReactiveUI.Legacy.ReactiveCommand _moveCommand;
        readonly ObservableAsPropertyHelper<GamePieceLocation> _locationResults;
        private GamePieceLocation _lastLocation;
        private bool _userCollisionEminent;

        public MovingObstaclePiece(GameModel myModel, int moveRate, int moveSpeed, GamePieceLocation myLocation) : base(myModel, moveRate, moveSpeed, myLocation)
        {
            //RxUI 6.0 code
            //_moveCommand = ReactiveCommand.Create();
            //_moveCommand.


            //old school RxUI code
            MoveCommand = new ReactiveUI.Legacy.ReactiveCommand();
            var newLocation = MoveCommand.RegisterAsyncFunction(_ =>
            {
                if (_currentLocation.column > 0)
                {
                    LastLocation = new GamePieceLocation(_currentLocation.row, _currentLocation.column);
                    _currentLocation.column--;
                }

                if (!_model.LocationIsOccupied(_currentLocation))
                    _model.UpdatePieceLocation(LastLocation, _currentLocation);
                else if (_model.CollideWithUserPiece(_currentLocation))
                    UserCollisionEminent = true;

                return _currentLocation;
            }).ToProperty(this, x => x._currentLocation, out _locationResults, _currentLocation);

            _updateLocationTimer = new System.Timers.Timer(moveSpeed);
            _updateLocationTimer.Elapsed += _updateLocationTimer_Elapsed;
            _updateLocationTimer.Enabled = true;

        }

        public GamePieceLocation CurrentLocation {
            get { return _locationResults.Value; }
        }

        public void Move()
        {
            MoveCommand.Execute(null);
        }

        private void _updateLocationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Move();
        }

        public bool UserCollisionEminent
        {
            get { return _userCollisionEminent; }
            set { this.RaiseAndSetIfChanged(ref _userCollisionEminent, value); }
        }

        public GamePieceLocation LastLocation
        {
            get { return _lastLocation; }
            set { this.RaiseAndSetIfChanged(ref _lastLocation, value);  }
        }

        public ReactiveUI.Legacy.ReactiveCommand MoveCommand
        {
            get { return _moveCommand;  }
            set { this.RaiseAndSetIfChanged(ref _moveCommand, value);   }
        }
    }
}
