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
        private Brush currentBrush;

        private bool penIsActive = false;
        private bool canExecute = false;
        private ICommand createLineCommand = null;
        private ICommand createTriangleCommand = null;
        private ICommand createRectangleCommand = null;
        private ICommand createEllipseCommand = null;
        private ICommand penButton = null;
        private ICommand mouseDown = null;
        private ICommand selectField = null;
        private ICommand selectColor = null;

        //private ICommand YkazButton = null;
        //private ICommand MysorButton = null;
        //private ICommand FillButton = null;
        //private ICommand ApproximationButton = null;
        //private ICommand DistanceButton = null;
        //private ICommand NextButton = null;
        //private ICommand BackButton = null;       


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

        public ICommand CreateLineCommand
        {
            get
            {
                createLineCommand = new ActionCommand(createLine, param => true);
                return createLineCommand;
            }
        }

        public ICommand CreateTriangleCommand
        {
            get
            {
                createTriangleCommand = new ActionCommand(createTriangle, param => true);
                bool res = createTriangleCommand.CanExecute(false);
                return createTriangleCommand;
            }
        }


        public ICommand CreateRectangleCommand
        {
            get
            {
                createRectangleCommand = new ActionCommand(createRectangle, param => true);
                return createRectangleCommand;
            }
        }


        public ICommand CreateEllipseCommand
        {
            get
            {
                createEllipseCommand = new ActionCommand(createEllipse, param => true);
                return createEllipseCommand;
            }
        }


        public ICommand PenButton
        {
            get
            {
                penButton = new ActionCommand(activatePen, param => true);
                return penButton;
            }
        }


        public ICommand MouseDown
        {
            get
            {
                mouseDown = new ActionCommand(mouseDownHandler, param => true);
                return mouseDown;
            }
        }


        public ICommand SelectField
        {
            get
            {
                selectField = new ActionCommand(mouseDownHandler, param => true);
                return selectField;
            }
        }


        public ICommand SelectColor
        {
            get
            {
                selectColor = new ActionCommand(changeColor, param => true);
                return selectColor;
            }
        }



        private void createLine(object obj)
        {
            if (canExecute)
            {
                Path path = new Path();
                LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path);
                List<GrRed.Vector> vector2 = new List<GrRed.Vector>();
                vector2.Add(new GrRed.Vector(300, 300));
                vector2.Add(new GrRed.Vector(450, 50));
                lineGrafic.AddLines(vector2);
                Brush brush2 = Brushes.Firebrick;
                lineGrafic.FillPolygon(brush2);
            } else 
                actionCommands.Push(createLineCommand);
        }

        private void createTriangle(object obj)
        {
            if (canExecute)
            {
                GrRed.Vector start = new GrRed.Vector(50, 50);
                if (obj != null)
                    start = (GrRed.Vector)obj;
                Path path = new Path();
                TriangleGrafic triangleGrafic = new TriangleGrafic(paintingCanvas, path);
                FigureFactory figureFactory = FigureFabric.GetFactory("Triangle");
                IFigure triangle = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
                triangle.Draw(triangleGrafic);
            } else
                actionCommands.Push(createTriangleCommand);
        }

        private void createRectangle(object obj)
        {
            if (canExecute)
            {
                GrRed.Vector start = new GrRed.Vector(50, 50);
                if (obj != null)
                    start = (GrRed.Vector)obj;
                Path path = new Path();
                RectangleGrafic rectangle = new RectangleGrafic(paintingCanvas, path);
                FigureFactory figureFactory = FigureFabric.GetFactory("Square");
                IFigure square = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            } else
                actionCommands.Push(createRectangleCommand);
        }


        private void createEllipse(object obj)
        {
            if (canExecute)
            {
                GrRed.Vector start = new GrRed.Vector(50, 50);
                if (obj != null)
                    start = (GrRed.Vector)obj;
                Path path = new Path();
                EllipseGrafic ellipseGrafic = new EllipseGrafic(paintingCanvas, path);
                FigureFactory figureFactory = FigureFabric.GetFactory("Ellipse");
                IFigure ellipse = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            } else
                actionCommands.Push(createEllipseCommand);
        }

        private void activatePen(object obj)
        {
            if (paintingCanvas.EditingMode == InkCanvasEditingMode.Ink)
                paintingCanvas.EditingMode = InkCanvasEditingMode.None;
            else
                paintingCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }


        private void mouseDownHandler(object obj)
        {
            canExecute = true;
            Point position = Mouse.GetPosition(paintingCanvas);
            GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);
            if (actionCommands.Count != 0)
                actionCommands.Peek().Execute(mousePos);
            canExecute = false;
        }


        private void changeColor(object obj)
        {
            string colorStr = obj.ToString();
            SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString(colorStr);
            currentBrush = color;
            paintingCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(colorStr);
        }
    }
}
