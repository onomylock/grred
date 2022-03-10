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
    public partial class MainViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private List<IFigure> figureList = new List<IFigure>();
        private List<IFigure> selectedFigures = new List<IFigure>();
        private Stack<ICommand> actionCommands = new Stack<ICommand>();
        private InkCanvas paintingCanvas;
        private GrRed.Vector startPoint;

        private bool penIsActive = false;


        public MainViewModel() { }
        public MainViewModel(InkCanvas canvas)
        {
            this.paintingCanvas = canvas;
        }

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
                createLineCommand = new ActionCommand(createLine, param => true);
                actionCommands.Push(createLineCommand);
                return createLineCommand;
            }
        }

        private ICommand createTriangleCommand = null;
        public ICommand CreateTriangleCommand
        {
            get
            {
                createTriangleCommand = new ActionCommand(createTriangle, param => true);
                actionCommands.Push(createTriangleCommand);
                return createTriangleCommand;
            }
        }


        private ICommand createRectangleCommand = null;
        public ICommand CreateRectangleCommand
        {
            get
            {
                createRectangleCommand = new ActionCommand(createRectangle, param => true);
                actionCommands.Push(createRectangleCommand);
                return createRectangleCommand;
            }
        }


        private ICommand createEllipseCommand = null;
        public ICommand CreateEllipseCommand
        {
            get
            {
                createEllipseCommand = new ActionCommand(createEllipse, param => true);
                actionCommands.Push(createEllipseCommand);
                return createEllipseCommand;
            }
        }


        private ICommand penButton = null;
        public ICommand PenButton
        {
            get
            {
                penButton = new ActionCommand(activatePen, param => true);
                actionCommands.Push(penButton);
                return penButton;
            }
        }


        private ICommand mouseDown = null;
        public ICommand MouseDown
        {
            get
            {
                mouseDown = new ActionCommand(mouseDownHandler, param => true);
                return mouseDown;
            }
        }


        private ICommand selectField = null;
        public ICommand SelectField
        {
            get
            {
                selectField = new ActionCommand(mouseDownHandler, param => true);
                return selectField;
            }
        }



        private void createLine(object obj)
        {
            Path path = new Path();
            LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path);
            List<GrRed.Vector> vector2 = new List<GrRed.Vector>();
            vector2.Add(new GrRed.Vector(300, 300));
            vector2.Add(new GrRed.Vector(450, 50));
            lineGrafic.AddLines(vector2);
            Brush brush2 = Brushes.Firebrick;
            lineGrafic.FillPolygon(brush2);
        }

        private void createTriangle(object obj)
        {
            Path path = new Path();
            TriangleGrafic triangleGrafic = new TriangleGrafic(paintingCanvas, path);
            List<GrRed.Vector> vector2 = new List<GrRed.Vector>();
            vector2.Add(new GrRed.Vector(300, 300));
            vector2.Add(new GrRed.Vector(450, 50));
            vector2.Add(new GrRed.Vector(100, 200));
            triangleGrafic.AddLines(vector2);
            Brush brush2 = Brushes.Firebrick;
            triangleGrafic.FillPolygon(brush2);

        }

        private void createRectangle(object obj)
        {
            Path path = new Path();
            RectangleGrafic rectangle = new RectangleGrafic(paintingCanvas, path);
            List<GrRed.Vector> vector = new List<GrRed.Vector>();
            vector.Add(new GrRed.Vector(50, 50));
            vector.Add(new GrRed.Vector(50, 100));
            vector.Add(new GrRed.Vector(100, 100));
            vector.Add(new GrRed.Vector(100, 50));
            rectangle.AddLines(vector);
            Brush brush = Brushes.Red;
            rectangle.FillPolygon(brush);

        }


        private void createEllipse(object obj)
        {
            Path path = new Path();
            EllipseGrafic ellipseGrafic = new EllipseGrafic(paintingCanvas, path);
            List<GrRed.Vector> vector1 = new List<GrRed.Vector>();
            vector1.Add(new GrRed.Vector(500, 500));
            vector1.Add(new GrRed.Vector(450, 450));
            vector1.Add(new GrRed.Vector(400, 400));
            ellipseGrafic.AddPolyArc(vector1);
            Brush brush1 = Brushes.BlueViolet;
            ellipseGrafic.FillPolygon(brush1);
        }

        private void activatePen(object obj)
        {
            if (!penIsActive)
            {
                penIsActive = true;
                paintingCanvas.EditingMode = InkCanvasEditingMode.Ink;
            } 
            else
            {
                penIsActive = false;
                paintingCanvas.EditingMode = InkCanvasEditingMode.None;
            }
        }


        private void mouseDownHandler(object obj)
        {
            MessageBox.Show("Privet");
        }
    }

}
