using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using GrRed;
using GrRed.Geometry.Factory;

namespace gui
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        public enum Mode
        {
            Сursor,
            Pencil,
            Line,
            Triangle,
            Ellipse,
            BrokenLine,
            Rectangle,
            Fill
        }

        Mode currentMode;
        public Mode CurrentMode
        {
            get => currentMode;
            set
            {
                PreviousIndex = (int)currentMode;
                OnPropertyChanged();
            }
        }
        public int PreviousIndex { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<IFigure> figureList = new();
        public List<IFigure> selectedFigures = new();
        public Stack<ICommand> actionCommands = new();
        public InkCanvas paintingCanvas;
        public Brush currentBrush;

        public bool penIsActive;
        public bool canExecute;
        public ICommand createLineCommand;
        public ICommand createTriangleCommand;
        public ICommand createRectangleCommand;
        public ICommand createEllipseCommand;
        public ICommand penButton;
        public ICommand mouseDown;
        public ICommand selectField;
        public ICommand selectColor;
        private ICommand clearCanvasCommand;

        //private ICommand YkazButton = null;
        //private ICommand MysorButton = null;
        //private ICommand FillButton = null;
        //private ICommand ApproximationButton = null;
        //private ICommand DistanceButton = null;
        //private ICommand NextButton = null;
        //private ICommand BackButton = null;       

        private readonly double esp = 0.01;

        public MainViewModel() { }
        public MainViewModel(InkCanvas canvas)
        {
            paintingCanvas = canvas;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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

        public ICommand ClearCanvasCommand
        {
            get
            {
                clearCanvasCommand = new ActionCommand(ClearCanvas, param => true);
                return clearCanvasCommand;
            }
        }

        private void ClearCanvas(object obj)
        {
            paintingCanvas.Strokes.Clear();
            actionCommands.Clear();
            figureList.Clear();
            selectedFigures.Clear();
        }

        public void createLine(object obj)
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

        public void createTriangle(object obj)
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

        public void createRectangle(object obj)
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


        public void createEllipse(object obj)
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

        public void activatePen(object obj)
        {
            if (paintingCanvas.EditingMode == InkCanvasEditingMode.Ink)
                paintingCanvas.EditingMode = InkCanvasEditingMode.None;
            else
                paintingCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        IFigure FindFigure(GrRed.Vector p)
        {
            foreach (var fig in figureList)
            {
                if (fig.IsIn(p,esp))
                {
                    return fig;
                }
            }

            return null;
        }

        public void mouseDownHandler(object obj)
        {
           
            canExecute = true;

            Point position = Mouse.GetPosition(paintingCanvas);
            GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);

            //switch (CurrentMode)
            //{
            //    case Mode.Сursor:
            //        GrRed.Vector p = mousePos;
            //        IFigure figure = FindFigure(p);
            //        //SelectedFigure = figure;
            //        break;

            //    case Mode.Pencil:
            //        activatePen(obj);
            //        break;

            //    case Mode.Line:
            //        createLine(obj);
            //        break;

            //    case Mode.Triangle:
            //        createTriangle(obj);
            //        break;

            //    case Mode.Rectangle:
            //        createRectangle(obj);
            //        break;

            //    case Mode.Ellipse:
            //        createEllipse(obj);
            //        break;

            //    case Mode.BrokenLine:
            //        break;

            //    case Mode.Fill:
            //        break;

            //    default:
            //        break;
            //}

            if (actionCommands.Count != 0)
                actionCommands.Peek().Execute(mousePos);
            canExecute = false;
        }


        public void changeColor(object obj)
        {
            string colorStr = obj.ToString();
            SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString(colorStr);
            currentBrush = color;
            paintingCanvas.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(colorStr);
        }
    }
}
