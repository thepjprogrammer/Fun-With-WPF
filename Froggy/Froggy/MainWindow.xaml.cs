using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Froggy.Models;
using ReactiveUI.Legacy;
using Froggy.Characters;

namespace Froggy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FroggyGameModel _model;
        private GamePieceLocation _startLocation = new GamePieceLocation(5, 3);
        private Character _userCharacter;
        private UserPiece _userPiece;

        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

            _model = new FroggyGameModel(6,6);

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

        //apparently arrow keys aren't included in keydown ?!?
        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GamePieceLocation newLocation;

            switch(e.Key)
            {
                case Key.Up:
                    newLocation = _userPiece.Move(GamePieceMovementDirection.Up);
                    Grid.SetRow(_userCharacter, newLocation.row);
                    Grid.SetColumn(_userCharacter, newLocation.column);
                    break;
                case Key.Right:
                    newLocation = _userPiece.Move(GamePieceMovementDirection.Right);
                    Grid.SetRow(_userCharacter, newLocation.row);
                    Grid.SetColumn(_userCharacter, newLocation.column);
                    break;
                case Key.Left:
                    newLocation = _userPiece.Move(GamePieceMovementDirection.Left);
                    Grid.SetRow(_userCharacter, newLocation.row);
                    Grid.SetColumn(_userCharacter, newLocation.column);
                    break;

            }
        }

        private void AddMainCharacter(UserPiece piece, GamePieceLocation location)
        {
            var frog = new Characters.LittleFroggy();
            frog.Width = 75;
            frog.Height = 75;
            Grid.SetRow(frog, location.row);
            Grid.SetColumn(frog, location.column);
            FroggyGrid.Children.Add(frog);

            _userCharacter = frog;
            _userPiece = piece;
        }

        private void AddObstacle(MovingObstaclePiece piece)
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
            piece.Changed.Subscribe(changedArgs =>
            {
                Grid.SetRow(el, piece.CurrentLocation.row);
                Grid.SetColumn(el, piece.CurrentLocation.column);
            });

            Grid.SetRow(el, piece.CurrentLocation.row);
            Grid.SetColumn(el, piece.CurrentLocation.column);
            FroggyGrid.Children.Add(el);
        }


    }
}
