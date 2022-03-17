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
        public Brush curConturBrush;
        public Brush curFillBrush;
        public double curThickness;
        public MainWindow()
        {
            curConturBrush = Brushes.Black;
            curFillBrush = Brushes.White;
            curThickness = 1;
            InitializeComponent();
            DataContext = new MainViewModel(PaintingCanvas);

            //List<GrRed.Vector> lines = new List<GrRed.Vector>();

            //LineGrafic lineGrafic = new LineGrafic(PaintingCanvas, curConturBrush, curThickness);
            //lineGrafic.AddLines(lines);

            //EllipseGrafic ellipseGeometry = new EllipseGrafic(PaintingCanvas, curConturBrush, curFillBrush, curThickness);
            //ellipseGeometry.AddPolyArc(lines);
        }
    }
}
