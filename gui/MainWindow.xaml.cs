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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("nazad");
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("vpered");
        }

        private void PrimitiveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ApproximationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DistanceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VerticallyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HorizontallyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PipetteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrangeButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Orange;
        }

        private void RedButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Red;
        }

        private void WhiteButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.White;
        }

        private void BlackButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Black;
        }

        private void YellowButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Yellow;
        }

        private void GreenButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Green;
        }

        private void BlueButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Blue;
        }

        private void DarkBlueButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.DarkBlue;
        }

        private void PurpleButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Purple;
        }

        private void PinkButton_Click(object sender, RoutedEventArgs e)
        {
            ColorButton.Background = Brushes.Pink;
        }

        private void UnfButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubtrButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContourButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColorContourButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BlackContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Black;
        }

        private void WhiteContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.White;
        }

        private void RedContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Red;
        }

        private void OrangeContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Orange;
        }

        private void YellowContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Yellow;
        }

        private void GreenContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Green;
        }

        private void BlueContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Blue;
        }

        private void DarkBlueContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.DarkBlue;
        }

        private void PurpleContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Purple;
        }

        private void PinkContourButton_Click(object sender, RoutedEventArgs e)
        {
            ColorContourButton.Background = Brushes.Pink;
        }
    }
}
