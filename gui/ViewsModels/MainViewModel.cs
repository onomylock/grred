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
using System.Windows.Ink;

namespace gui
{

    public enum Mode
    {
        Selection,
        Rectangle,
        Line,
        Ellipse,
        Triangle,
        Pencil,
        OpenFile,
        Brush
    }

    public partial class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private List<IFigure> figureList = new List<IFigure>();
        private List<IFigure> selectedFigures = new List<IFigure>();
        private Stack<ICommand> actionCommands = new Stack<ICommand>();
        private InkCanvas paintingCanvas;
        private Brush currentBrush;

        private IFigure drawingObject = null;
        private Mode mode = Mode.Selection;
        private bool isMouseDown = false;

        private ICommand createLineCommand = null;
        private ICommand createTriangleCommand = null;
        private ICommand createRectangleCommand = null;
        private ICommand createEllipseCommand = null;
        private ICommand penButton = null;
        private ICommand mouseDown = null;
        private ICommand selectField = null;
        private ICommand selectColor = null;
        private ICommand mouseUp = null;
        private ICommand mouseMove = null;


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

        public ICommand MouseUp
        {
            get
            {
                mouseUp = new ActionCommand(onMouseUp, param => true);
                return mouseUp;
            }
        }

        public ICommand MouseMove
        {
            get
            {
                mouseMove = new ActionCommand(onMouseMove, param => true);
                return mouseMove;
            }
        }



        private void createLine(object obj)
        {
            //Path path = new Path();
            //LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path);
            //List<GrRed.Vector> vector2 = new List<GrRed.Vector>();
            //vector2.Add(new GrRed.Vector(300, 300));
            //vector2.Add(new GrRed.Vector(450, 50));
            //lineGrafic.AddLines(vector2);
            //Brush brush2 = Brushes.Firebrick;
            //lineGrafic.FillPolygon(brush2);
            //actionCommands.Push(createLineCommand);
            mode = Mode.Line;
        }

        private void createTriangle(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            FigureFactory figureFactory = FigureFabric.GetFactory("Triangle");
            IFigure triangle = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            figureList.Add(triangle);
            actionCommands.Push(createTriangleCommand);
            mode = Mode.Triangle;
        }

        private void createRectangle(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            Path path = new Path();
            RectangleGrafic rectangle = new RectangleGrafic(paintingCanvas, path);
            FigureFactory figureFactory = FigureFabric.GetFactory("Square");
            IFigure square = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            actionCommands.Push(createRectangleCommand);
            mode = Mode.Rectangle;
        }


        private void createEllipse(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            Path path = new Path();
            EllipseGrafic ellipseGrafic = new EllipseGrafic(paintingCanvas, path);
            FigureFactory figureFactory = FigureFabric.GetFactory("Ellipse");
            IFigure ellipse = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            actionCommands.Push(createEllipseCommand);
            mode = Mode.Ellipse;
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
            isMouseDown = true;
            Point position = Mouse.GetPosition(paintingCanvas);
            GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);
            switch (mode)
            {
                case Mode.Selection:
                    //IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                    //selectedFigures.Add(selected);
                    selectedFigures.Add(figureList.Last());
                    break;
                case Mode.Rectangle:
                    break;
                case Mode.Line:
                    break;
                case Mode.Ellipse:
                    break;
                case Mode.Triangle:
                    Path path = new Path();
                    TriangleGrafic triangleGrafic = new TriangleGrafic(paintingCanvas, path);
                    IFigure current = figureList.Last();
                    current.Draw(triangleGrafic);
                    mode = Mode.Selection;
                    break;
                case Mode.Pencil:
                    break;
                case Mode.OpenFile:
                    break;
                default:
                    break;
            }
        }

        private IFigure FindFigure(GrRed.Vector p)
        {
            IFigure res = null;
            foreach (var fig in figureList)
                if (fig.IsIn(p, 1E-2))
                    res = fig;
            return res;
        }

        private void changeColor(object obj)
        {
            mode = Mode.Brush;
            string colorStr = obj.ToString();
            SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString(colorStr);
            currentBrush = color;
            paintingCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(colorStr);
        }

        private void onMouseUp(object obj)
        {
            isMouseDown = false;
        }

        private void onMouseMove(object obj)
        {
            Point position = Mouse.GetPosition(paintingCanvas);
            GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);
            if (selectedFigures.Count != 0)
            {
                foreach (var fig in selectedFigures)
                {
                    GrRed.Vector delta = fig.Center;
                    fig.Move(new GrRed.Vector(0.1, 0.1));
                }
            }
        }
    }
}
