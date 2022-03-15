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
using System.Windows.Interactivity;



namespace gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(PaintingCanvas);
        }


        private void PrimButton_Ñlick(object sender, RoutedEventArgs e)
        {
            if (Leftgrid1.Visibility == Visibility.Hidden)
                Leftgrid1.Visibility = Visibility.Visible;
            else
                Leftgrid1.Visibility = Visibility.Hidden;
        }


        private void menu_click(object sender, RoutedEventArgs e)
        {
            if (Leftgrid.Visibility == Visibility.Hidden)
            {
                Leftgrid.Visibility = Visibility.Visible;
                Left_all_grid.Visibility = Visibility.Hidden;
            }
            else
            {
                Leftgrid.Visibility = Visibility.Hidden;
                Left_all_grid.Visibility = Visibility.Visible;
            }
        }

        public InkCanvas GetCanvas()
        {
            return PaintingCanvas;
        }
    }
}
