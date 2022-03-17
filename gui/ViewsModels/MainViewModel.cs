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

        private Dictionary<Path, IFigure> figureDict = new Dictionary<Path, IFigure>();
        private List<IFigure> selectedFigures = new List<IFigure>();
        private Stack<ICommand> actionCommands = new Stack<ICommand>();
        private InkCanvas paintingCanvas;
        private Brush currentBrush;
        private Path previousPath;

        private Mode mode = Mode.Selection;
        private bool isMouseDown = false;
        private ICommand lastCommand;

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
        private ICommand selectionCommand = null;

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

        public void InOrenge(object obj)
        {

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
                createLineCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Line;
                }, param => true);
                return createLineCommand;
            }
        }

        public ICommand CreateTriangleCommand
        {
            get
            {
                createTriangleCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Triangle;
                }, param => true);
                bool res = createTriangleCommand.CanExecute(false);
                return createTriangleCommand;
            }
        }


        public ICommand CreateRectangleCommand
        {
            get
            {
                createRectangleCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Rectangle;
                }, param => true);
                return createRectangleCommand;
            }
        }


        public ICommand CreateEllipseCommand
        {
            get
            {
                createEllipseCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Ellipse;
                }, param => true);
                return createEllipseCommand;
            }
        }


        public ICommand PenButton
        {
            get
            {
                penButton = new ActionCommand(obj =>
                {
                    mode = Mode.Pencil;
                }, param => true);
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

        public ICommand SelectionCommand
        {
            get
            {
                selectionCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Selection;
                }, param => true);
                return selectionCommand;
            }
        }

        private IFigure createTriangle(GrRed.Vector start, GrRed.Vector scale)
        {
            FigureFactory figureFactory = FigureFabric.GetFactory("Triangle");
            IFigure triangle = figureFactory.GetFigure(0, start, scale);
            lastCommand = createTriangleCommand;
            return triangle;
        }

        private IFigure createRectangle(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            FigureFactory figureFactory = FigureFabric.GetFactory("Square");
            IFigure square = figureFactory.GetFigure(0, start, new GrRed.Vector(0, 0));
            lastCommand= createRectangleCommand;
            return square;
        }


        private IFigure createEllipse(GrRed.Vector start, GrRed.Vector scale)
        {
            FigureFactory figureFactory = FigureFabric.GetFactory("Ellipse");
            IFigure ellipse = figureFactory.GetFigure(0, start, scale);
            lastCommand = createEllipseCommand;
            return ellipse;
        }

        private void activatePen()
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
                    if (figureDict.Count != 0)
                    {
                        //IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                        //selectedFigures.Add(selected);

                        // For test
                        //selectedFigures.Add(figureDict.Last());
                    }
                    break;
                case Mode.Pencil:
                    activatePen();
                    break;
                case Mode.OpenFile:
                    break;
                case Mode.Brush:
                    if (figureDict.Count != 0)
                    {
                        Path path1 = new();
                        IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                        IGraphic graphic = GraphicFabric.GetFactory(selected.TypeName, paintingCanvas, path1);
                        selected.Draw(graphic);
                        graphic.FillPolygon(currentBrush);


                        // For test
                        //Path path1 = new();
                        //IFigure selected = figureDict.Last();
                        //IGraphic graphic = GraphicFabric.GetFactory(selected.TypeName, paintingCanvas, path1);
                        //selected.Draw(graphic);
                        //graphic.FillPolygon(currentBrush);
                    }
                    break;
                default:
                    break;
            }
        }

        private IFigure FindFigure(GrRed.Vector p)
        {
            IFigure res = null;
            foreach (var fig in figureDict.Values)
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


            //paintingCanvas.UseCustomCursor = true;
            // paintingCanvas.Cursor = Cursors.Wait; // Можно поставить другой курсор
        }

        private void onMouseUp(object obj)
        {
            isMouseDown = false;
            actionCommands.Push(lastCommand);
            previousPath = null;
        }

        private void onMouseMove(object obj)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                mouseMoveHandler();
            }
            else
            {
                previousPath = null;
            }
        }

        private void mouseMoveHandler()
        {
            Point position = Mouse.GetPosition(paintingCanvas);
            GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);
            GrRed.Vector scale = mousePos;
            if (previousPath != null)
            {
                paintingCanvas.Children.Remove(previousPath);
                mousePos = figureDict.GetValueOrDefault(previousPath).Center;
                figureDict.Remove(previousPath);
            }
            switch (mode)
            {
                case Mode.Selection:
                    break;
                case Mode.Rectangle:
                    IFigure square = createRectangle(mousePos);
                    Path path_square = new ();
                    RectangleGrafic squareGrafic = new RectangleGrafic(paintingCanvas, path_square);
                    square.Draw(squareGrafic);
                    figureDict.Add(path_square, square);
                    previousPath = squareGrafic.path;
                    break;
                case Mode.Line:
                    Path path_line = new();
                    
                    LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path_line);
                    List<GrRed.Vector> line = new List<GrRed.Vector>();
                    line.Add(mousePos);
                    line.Add(scale);
                    lineGrafic.AddLines(line);
                    figureDict.Add(lineGrafic.path, null);
                    previousPath = lineGrafic.path;
                    break;
                case Mode.Ellipse:
                    IFigure ellipse = createEllipse(mousePos, scale);
                    Path path_ellipse = new();
                    EllipseGrafic ellipseGrafic = new EllipseGrafic(paintingCanvas, path_ellipse);
                    ellipse.Draw(ellipseGrafic);
                    figureDict.Add(path_ellipse, ellipse);
                    previousPath = ellipseGrafic.path;
                    break;
                case Mode.Triangle:
                    IFigure triangle = createTriangle(mousePos, scale);
                    Path path_triangle = new Path();
                    TriangleGrafic triangleGrafic = new TriangleGrafic(paintingCanvas, path_triangle);
                    triangle.Draw(triangleGrafic);
                    figureDict.Add(path_triangle, triangle);
                    previousPath = triangleGrafic.path;
                    break;
                default:
                    break;
            }
        }
        private void moveSelectedFigures()
        {

            // Not working
            if (isMouseDown && selectedFigures.Count != 0)
            {
                Point position = Mouse.GetPosition(paintingCanvas);
                GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);

                foreach (var fig in selectedFigures)
                {
                    GrRed.Vector delta = fig.Center + mousePos;
                    IFigure newFig = fig.Move(delta);
                    Path path = new Path();
                    TriangleGrafic triangleGrafic = new TriangleGrafic(paintingCanvas, path);
                    newFig.Draw(triangleGrafic);
                }
            }
        }

        // Функция для отрисовки и перерисовки
        //private void reDraw(IFigure figure)
        //{
        //    Path path = new Path();
        //    IGraphic triangleGrafic = GraphicFabric.GetFactory(figure.TypeName, paintingCanvas, path);
        //    IFigure current = figureDict.Last();
        //    figureDict.Remove(figure);
        //    figureDict.Add(current);
        //    paintingCanvas.Children.Remove(previousPath);
        //    current.Draw(triangleGrafic);
        //    previousPath = triangleGrafic.path;
        //}


        // TODO 1:
        // Для дальнеший действий над фигурой нужен словарь с фигурой в качестве ключа и Path объектом в качестве значений
        // Path объект хранит в себе заливку
        // Команды добавляются в стек только если они выполнены !!!!!!!!! Для каждрй команды это индивидуально, но есть способы сделать красиво, но времени нет
        // Фигуры добавляются в словарь только если они созданы

    }
}
