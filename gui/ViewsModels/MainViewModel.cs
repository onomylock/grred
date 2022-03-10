using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
using GrRed;
using GrRed.Geometry.Factory;

namespace gui
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private List<IFigure> figureList;
        private Stack<ICommand> actionCommands;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private ICommand createLineCommand = null;
        public ICommand CreateLineCommand
        {
            get
            {
                createLineCommand = new ActionCommand(CreateLine, param => true);
                return createLineCommand;
            }
        }

        private ICommand createTriangleCommand;
        public ICommand CreateTriangleCommand
        {
            get
            {
                createLineCommand = new ActionCommand(ShowMessage, param => true);
                return createLineCommand;
            }
        }

        public void CreateLine(object obj)
        {
            Path path = new Path();
            FigureFactory triangleFabric = FigureFabric.GetFactory("triangle");
            IFigure triangle = triangleFabric.GetFigure(0, new GrRed.Vector(0, 0), new GrRed.Vector(5, 5));
            IGraphic triangleGraphic = new TriangleGrafic(PaintingCanvas, path);
            triangle.Draw(triangleGraphic);
        }


        //     if (Leftgrid.Visibility == Visibility.Hidden)
        //{
        //    Leftgrid.Visibility = Visibility.Visible;
        //    Left_all_grid.Visibility = Visibility.Hidden;
        //}
        //else
        //{
        //    Leftgrid.Visibility = Visibility.Hidden;
        //    Left_all_grid.Visibility = Visibility.Visible;
        //}



        //RectangleGrafic rectangle = new RectangleGrafic();
        //List<Vector> vector = new List<Vector>();
        //vector.Add(new Vector(50, 50));
        //vector.Add(new Vector(50, 100));
        //vector.Add(new Vector(100, 100));
        //vector.Add(new Vector(100, 50));
        //rectangle.AddLines(vector, PaintingGrid);

        //EllipseGrafic ellipse = new EllipseGrafic();
        //vector.Add(new Vector(50, 50));
        //vector.Add(new Vector(75, 60));
        //vector.Add(new Vector(100, 50));
        //ellipse.AddPolyArc(vector, PaintingGrid);

        //private int _Cliсks;
        //public int Clicks
        //{
        //    get { return _Cliсks; }
        //    set
        //    {
        //        _Cliсks = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public MainViewModel()
        //{
        //    Task.Factory.StartNew(() =>
        //    {
        //        while (true)
        //        {
        //            Task.Delay(1000).Wait();

        //            Cliсks++;
        //        }
        //    });
        //}
        //private void menu_click(object sender, RoutedEventArgs e)
        //{
        //    if (Leftgrid.Visibility == Visibility.Hidden)
        //    {
        //        Leftgrid.Visibility = Visibility.Visible;
        //        Left_all_grid.Visibility = Visibility.Hidden;
        //    }
        //    else
        //    {
        //        Leftgrid.Visibility = Visibility.Hidden;
        //        Left_all_grid.Visibility = Visibility.Visible;
        //    }
        //}

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("nazad");
        //}

        //private void NextButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("vpered");
        //}

        //private void PenButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void FillButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void ApproximationButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void DistanceButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void PrimButton_Сlick(object sender, RoutedEventArgs e)
        //{
        //    if (Leftgrid1.Visibility == Visibility.Hidden)
        //        Leftgrid1.Visibility = Visibility.Visible;
        //    else
        //        Leftgrid1.Visibility = Visibility.Hidden;
        //}

        //private void PipetteButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void ColorButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void OrangeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Orange;
        //}

        //private void RedButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Red;
        //}

        //private void WhiteButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.White;
        //}

        //private void BlackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Black;
        //}

        //private void YellowButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Yellow;
        //}

        //private void GreenButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Green;
        //}

        //private void BlueButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Blue;
        //}

        //private void DarkBlueButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.DarkBlue;
        //}

        //private void PurpleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Purple;
        //}

        //private void PinkButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorButton.Background = Brushes.Pink;
        //}

        //private void UnfButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void InterButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void SubtrButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void ContourButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void ColorContourButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void BlackContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Black;
        //}

        //private void WhiteContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.White;
        //}

        //private void RedContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Red;
        //}

        //private void OrangeContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Orange;
        //}

        //private void YellowContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Yellow;
        //}

        //private void GreenContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Green;
        //}

        //private void BlueContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Blue;
        //}

        //private void DarkBlueContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.DarkBlue;
        //}

        //private void PurpleContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Purple;
        //}

        //private void PinkContourButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ColorContourButton.Background = Brushes.Pink;
        //}
    }

}
