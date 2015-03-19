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

namespace Froggy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FroggyGameModel _model;

        public MainWindow()
        {
            InitializeComponent();

            _model = new FroggyGameModel(5,6);
            var topDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.SLOW, new GamePieceLocation(1, 5));
            var bottomDumperObstacle = _model.AddObstacle(GamePieceMovementSpeed.FAST, new GamePieceLocation(4, 5));

            AddObstacleToUI(topDumperObstacle);
            AddObstacleToUI(bottomDumperObstacle);
        }

        private void AddObstacleToUI(MovingObstaclePiece piece)
        {
            var bottomDumper = new Characters.Dumper();
            BindMovingObstacle(bottomDumper, piece);
        }

        private void BindMovingObstacle(FrameworkElement el, MovingObstaclePiece piece)
        {
            DataObject myRowBindingData = new DataObject(piece.CurrentLocation.row);
            Binding myRowBinding = new Binding("Grid.Row");
            myRowBinding.Source = myRowBindingData;
            el.SetBinding(Grid.RowProperty, myRowBinding);

            DataObject myColBindingData = new DataObject(piece.CurrentLocation.column);
            Binding myColBinding = new Binding("Grid.Column");
            myColBinding.Source = myColBindingData;
            el.SetBinding(Grid.ColumnProperty, myColBinding);

            piece.Changed.Subscribe(changedArgs =>
            {
                Grid.SetRow(el, piece.CurrentLocation.row);
                Grid.SetColumn(el, piece.CurrentLocation.column);
            });

            Grid.SetRow(el, piece.CurrentLocation.row);
            Grid.SetColumn(el, piece.CurrentLocation.column);
            FroggyGrid.Children.Add(el);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }


    }
}
