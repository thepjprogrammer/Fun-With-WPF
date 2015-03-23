using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace Froggy.Models
{
    public enum GamePieceMovementDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class GamePieceMovementSpeed
    {
        public const int FAST = 1000;
        public const int SLOW = 3000;
    }

    public class GamePiece : ReactiveObject { }

    public struct GamePieceLocation
    {
        public int row;
        public int column;

        public GamePieceLocation(int modelRow, int modelColumn)
        {
            row = modelRow;
            column = modelColumn;
        }
    }
}
