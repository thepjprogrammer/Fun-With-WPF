using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Froggy.Characters
{
    public class Character : UserControl
    {
        public static readonly DependencyProperty _removeFromBoard = DependencyProperty.Register("RemoveFromBoard", typeof(bool), typeof(Character));

        public Character(){ }

        public bool RemoveFromBoard
        {
            get { return (bool) GetValue(_removeFromBoard);}
            set { SetValue(_removeFromBoard, value);  }
        }

    }
}
