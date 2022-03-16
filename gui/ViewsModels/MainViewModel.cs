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
        private Path previousPath;

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
        private ICommand rotate = null;
        private ICommand union = null;
        private ICommand intersection = null;
        private ICommand subtruct = null;

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
                createLineCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Line;
                }, param => true);
                return createLineCommand;
            }
        }

        public ICommand Rotate()
        {
            rotate = new ActionCommand(ActionRotate, param => true);
            return rotate;
        }

        void ActionRotate(object obj)
        {
            if (mode == Mode.Selection)
            {
                double ob = Convert.ToDouble(obj);
                IFigure figrotate = selectedFigures[selectedFigures.Count - 1].Rotate(ob);

                //отрисовка
            }
        }

        public ICommand Union()
        {
            union = new ActionCommand(ActionUnion, param => true);
            return union;
        }

        void ActionUnion(object obj)
        {
            if (mode == Mode.Selection)
            {
                IFigure figunion = selectedFigures[selectedFigures.Count - 1].Union(selectedFigures[selectedFigures.Count - 2]);

                //отрисовка
            }
        }

        public ICommand Intersection()
        {
            intersection = new ActionCommand(ActionIntersection, param => true);
            return intersection;
        }

        void ActionIntersection(object obj)
        {
            if (mode == Mode.Selection)
            {
                IFigure figintersection = selectedFigures[selectedFigures.Count - 1].Intersection(selectedFigures[selectedFigures.Count - 2]);

                //отрисовка
            }
        }
        public ICommand Subtruct()
        {
            subtruct = new ActionCommand(ActionSubtruct, param => true);
            return subtruct;
        }

        void ActionSubtruct(object obj)
        {
            if (mode == Mode.Selection)
            {
                IFigure figintersection = selectedFigures[selectedFigures.Count - 1].Subtruct(selectedFigures[selectedFigures.Count - 2]);

                //отрисовка
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



        private void createLine(object obj)
        {
            Path path = new Path();
            LineGrafic lineGrafic = new LineGrafic(paintingCanvas, path);
            List<GrRed.Vector> vector2 = new List<GrRed.Vector>();
            vector2.Add(new GrRed.Vector(300, 300));
            vector2.Add(new GrRed.Vector(450, 50));
            lineGrafic.AddLines(vector2);
            actionCommands.Push(createLineCommand);
        }

        private void createTriangle(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            FigureFactory figureFactory = FigureFabric.GetFactory("Triangle");
            IFigure triangle = figureFactory.GetFigure(0, start, new GrRed.Vector(0, 0));
            figureList.Add(triangle);
            actionCommands.Push(createTriangleCommand); ;
        }

        private void createRectangle(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            FigureFactory figureFactory = FigureFabric.GetFactory("Square");
            IFigure square = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            figureList.Add(square);
            actionCommands.Push(createRectangleCommand);
        }


        private void createEllipse(object obj)
        {
            GrRed.Vector start = new GrRed.Vector(50, 50);
            if (obj != null)
                start = (GrRed.Vector)obj;
            FigureFactory figureFactory = FigureFabric.GetFactory("Ellipse");
            IFigure ellipse = figureFactory.GetFigure(0, start, new GrRed.Vector(10, 10));
            figureList.Add(ellipse);
            actionCommands.Push(createEllipseCommand);
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
                    if (figureList.Count != 0)
                    {
                        //IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                        //selectedFigures.Add(selected);

                        // For test
                        selectedFigures.Add(figureList.Last());
                    }
                    break;
                case Mode.Pencil:
                    activatePen();
                    break;
                case Mode.OpenFile:
                    break;
                case Mode.Brush:
                    if (figureList.Count != 0)
                    {
                        //Path path1 = new();
                        //IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                        //IGraphic graphic = GraphicFabric.GetFactory(selected.TypeName, paintingCanvas, path1);
                        //selected.Draw(graphic);
                        //graphic.FillPolygon(currentBrush);


                        // For test
                        Path path1 = new();
                        IFigure selected = figureList.Last();
                        IGraphic graphic = GraphicFabric.GetFactory(selected.TypeName, paintingCanvas, path1);
                        selected.Draw(graphic);
                        graphic.FillPolygon(currentBrush);
                    }
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
            paintingCanvas.UseCustomCursor = true;
            // paintingCanvas.Cursor = Cursors.Wait; // Можно поставить другой курсор
        }

        private void onMouseUp(object obj)
        {
            isMouseDown = false;
            mode = Mode.Selection;
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
            switch (mode)
            {
                case Mode.Selection:
                    break;
                case Mode.Rectangle:
                    break;
                case Mode.Line:
                    break;
                case Mode.Ellipse:
                    break;
                case Mode.Triangle:
                    createTriangle(mousePos);
                    Path path = new Path();
                    TriangleGrafic triangleGrafic = new TriangleGrafic(paintingCanvas, path);
                    IFigure current = figureList.Last();
                    figureList.Clear();
                    figureList.Add(current);
                    paintingCanvas.Children.Remove(previousPath);
                    current.Draw(triangleGrafic);
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


        //private void reDraw(IFigure figure)
        //{
        //    Path path = new Path();
        //    IGraphic triangleGrafic = GraphicFabric.GetFactory(figure.TypeName, paintingCanvas, path);
        //    IFigure current = figureList.Last();
        //    figureList.Remove(figure);
        //    figureList.Add(current);
        //    paintingCanvas.Children.Remove(previousPath);
        //    current.Draw(triangleGrafic);
        //    previousPath = triangleGrafic.path;
        //}

    }
}
