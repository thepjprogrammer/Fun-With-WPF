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
using Froggy.ViewModels;

namespace Froggy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FroggyGameModel _model;
        private FroggyViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

            //model setup
            _model = new FroggyGameModel(6,6);

            //will need to be pulled from the current level
            var froggy = _model.AddMainCharacter(new GamePieceLocation(5, 3));
            var topDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.SLOW, new GamePieceLocation(1, 5));
            var secondDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.FAST, new GamePieceLocation(2, 5));
            var thirdDumperObstacle = _model.AddObstacle(1500, new GamePieceLocation(3, 5));
            var bottomDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.FAST, new GamePieceLocation(4, 5));

            //vm setup
            _vm = new FroggyViewModel(_model, FroggyGrid);
        }

        //apparently arrow keys aren't included in keydown ?!?
        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GamePieceLocation newLocation = new GamePieceLocation(-1, -1);

            switch(e.Key)
            {
                case Key.Up:
                    _vm.MoveMainCharacter(GamePieceMovementDirection.Up);
                    break;
                case Key.Right:
                    _vm.MoveMainCharacter(GamePieceMovementDirection.Right);
                    break;
                case Key.Left:
                    _vm.MoveMainCharacter(GamePieceMovementDirection.Left);
                    break;
            }

        }



    }
}
