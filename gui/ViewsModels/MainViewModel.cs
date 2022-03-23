using GrRed;
using GrRed.Geometry.Factory;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;
using System;
using GrRed.IO;
using System.Linq;

namespace gui
{
    public enum Mode
    {
        Selection,
        Square,
        Line,
        Ellipse,
        Triangle,
        Pencil,
        Brush
    }

    public partial class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<Path, IFigure> figureDict = new Dictionary<Path, IFigure>();
        public Stack<ICommand> actionCommands = new Stack<ICommand>();
        public Path previousPath;
        public Mode mode = Mode.Selection;

        private bool isMouseDown = false;
        private ICommand lastCommand;

        private InkCanvas paintingCanvas;
        private List<IFigure> selectedFigures = new List<IFigure>();
        private Brush currentBrush;
        private GrRed.Vector previousPostition;

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
        private ICommand clearCanvasCommand = null;
        private ICommand saveAsCommand;
        private ICommand helpCommand;
        private ICommand undoCommand;
        private ICommand redoCommand;
        private ICommand createNewCanvCommand;
        //private ICommand loadCommand;

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

        //public ICommand LoadCommand => loadCommand = new ActionCommand(Load, param => true);

        //private void Load(object obj)
        //{
        //    ClearCanvas(obj);
        //    Io.Load();
        //    List<IFigure> figureList = new();
        //    Dictionary<Path, IFigure> figureDict = figureList.
        //}

        public ICommand CreateNewCanvCommand => createNewCanvCommand = new ActionCommand(CreateNewCanv, param => true);

        private void CreateNewCanv(object obj)
        {
            if (paintingCanvas.Strokes != null)
            {
                SaveAs(obj);
                СlearCanvas(obj);
            }
            else
            {
                СlearCanvas(obj);
            }
        }

        public ICommand UndoCommand => undoCommand = new ActionCommand(Undo, param => true);

        private void Undo(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand RedoCommand => undoCommand = new ActionCommand(Redo, param => true);

        private void Redo(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand SaveAsCommand => saveAsCommand = new ActionCommand(SaveAs, param => true);

        private void SaveAs(object obj)
        {
            List<IFigure> ListFig = figureDict.Values.ToList();
            Io.Save(paintingCanvas, ListFig);
        }

        public ICommand HelpCommand => helpCommand = new ActionCommand(helpButton, param => true);
        

        public ICommand CreateLineCommand
        {
            get
            {
                createLineCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Line;
                    paintingCanvas.EditingMode = InkCanvasEditingMode.None;
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
                    paintingCanvas.EditingMode = InkCanvasEditingMode.None;
                }, param => true);
                return createTriangleCommand;
            }
        }


        public ICommand CreateRectangleCommand
        {
            get
            {
                createRectangleCommand = new ActionCommand(obj =>
                {
                    mode = Mode.Square;
                    paintingCanvas.EditingMode = InkCanvasEditingMode.None;
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
                    paintingCanvas.EditingMode = InkCanvasEditingMode.None;
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

        public ICommand ClearCanvasCommand
        {
            get
            {
                clearCanvasCommand = new ActionCommand(СlearCanvas, param => true);
                return clearCanvasCommand;
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
                    paintingCanvas.EditingMode = InkCanvasEditingMode.None;
                }, param => true);
                return selectionCommand;
            }
        }

        private void helpButton(object obj)
        {
            Process.Start(new ProcessStartInfo("https://gitlab.com/egorsukhinin/grred/-/wikis/Интерфейс-приложения") { UseShellExecute = true });
        }

        private void save(object obj)
        {
            List<IFigure> ListFig = figureDict.Values.ToList();
            Io.Save(paintingCanvas, ListFig);
        }
        private void СlearCanvas(object obj)
        {
            paintingCanvas.Strokes.Clear();
            actionCommands.Clear();
            figureDict.Clear();
            selectedFigures.Clear();
            paintingCanvas.Children.Clear();
        }

        private IFigure createFigure(GrRed.Vector start, GrRed.Vector scale)
        {
            switch (mode)
            {
                case Mode.Square:
                    return createRectangle(start, scale);
                case Mode.Ellipse:
                    return createEllipse(start, scale);
                case Mode.Triangle:
                    return createTriangle(start, scale);
                case Mode.Line:
                    return createLine(start, scale);
                default:
                    break;
            }
            return null;
        }
        private IFigure createLine(GrRed.Vector start, GrRed.Vector scale)
        {
            FigureFactory figureFactory = FigureFabric.GetFactory("Line");
            IFigure line = figureFactory.GetFigure(0, start, scale);
            lastCommand = createLineCommand;
            return line;
        }

        private IFigure createTriangle(GrRed.Vector start, GrRed.Vector scale)
        {
            FigureFactory figureFactory = FigureFabric.GetFactory("Triangle");
            IFigure triangle = figureFactory.GetFigure(0, start, scale);
            lastCommand = createTriangleCommand;
            return triangle;
        }

        private IFigure createRectangle(GrRed.Vector start, GrRed.Vector scale)
        {
            FigureFactory figureFactory = FigureFabric.GetFactory("Square");
            IFigure square = figureFactory.GetFigure(0, start, scale);
            lastCommand = createRectangleCommand;
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
            //GrRed.Vector mousePos = new GrRed.Vector(position.X, position.Y);
            switch (mode)
            {
                case Mode.Selection:
                    selectFigure(position);
                    break;
                case Mode.Pencil:
                    activatePen();
                    break;
                case Mode.Brush:
                    if (figureDict.Count != 0)
                    {
                        Dictionary<IFigure, Path> dictByFigure = figureDict.ToDictionary(keys => keys.Value, values => values.Key);
                        IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                        if (selected != null)
                        {
                            Path oldPath = dictByFigure.GetValueOrDefault(selected);
                            IGraphic graphic = GraphicFabric.GetFactory(selected.TypeName, paintingCanvas);
                            paintingCanvas.Children.Remove(oldPath);
                            figureDict.Remove(oldPath);
                            selected.Draw(graphic);
                            figureDict.Add((Path)graphic.path, selected);
                            graphic.FillPolygon(currentBrush);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void selectFigure(Point position)
        {
            if (figureDict.Count != 0)
            {
                Dictionary<IFigure, Path> dictByFigure = figureDict.ToDictionary(keys => keys.Value, values => values.Key);
                Path selectedPath = null;
                IFigure selected = FindFigure(new GrRed.Vector(position.X, position.Y));
                if (selected != null)
                {
                    if (!selectedFigures.Contains(selected))
                    {
                        selectedPath = dictByFigure.GetValueOrDefault(selected);
                        selectedPath.Stroke = Brushes.Aqua;
                        selectedFigures.Add(selected);
                    }
                }
                else
                {
                    foreach (var fig in selectedFigures)
                    {
                        selectedPath = dictByFigure.GetValueOrDefault(fig);
                        selectedPath.Stroke = Brushes.Black;
                    }
                    selectedFigures.Clear();
                }
            }
        }
        private IFigure FindFigure(GrRed.Vector p)
        {
            IFigure res = null;
            foreach (var fig in figureDict.Values)
                if (fig.IsIn(p, 0.1))
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
            actionCommands.Push(lastCommand);
            previousPath = null;
            previousPostition = new GrRed.Vector(-1, -1);
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
            if (previousPostition.X == -1)
                previousPostition = mousePos;
            if (previousPath != null)
            {
                paintingCanvas.Children.Remove(previousPath);
                mousePos = figureDict.GetValueOrDefault(previousPath).Center;
                figureDict.Remove(previousPath);
            }
            mouseMoveSwitch(mousePos, scale);
        }

        private void mouseMoveSwitch(GrRed.Vector mousePos, GrRed.Vector scale)
        {
            if (mode != Mode.Selection && mode != Mode.Brush && mode != Mode.Pencil)
            {
                IFigure figure;
                IGraphic graphic = GraphicFabric.GetFactory(Enum.GetName(mode), paintingCanvas);
                figure = createFigure(mousePos, scale);
                figure.Draw(graphic);
                figureDict.Add((Path)graphic.path, figure);
                previousPath = (Path)graphic.path;
            }
            else
            {
                moveSelectedFigures(mousePos);
            }
        }

        private void moveSelectedFigures(GrRed.Vector mousePos)
        {
            if (selectedFigures.Count > 0)
            {
                Dictionary<IFigure, Path> dictByFigure = figureDict.ToDictionary(keys => keys.Value, values => values.Key);
                for (int i = 0; i < selectedFigures.Count; i++)
                {
                    IFigure fig = selectedFigures[i];
                    IFigure newFig = fig.Move(mousePos - previousPostition);
                    previousPostition = mousePos;
                    Path oldPath = dictByFigure.GetValueOrDefault(fig);
                    paintingCanvas.Children.Remove(oldPath);
                    figureDict.Remove(oldPath);
                    IGraphic graphic = GraphicFabric.GetFactory(newFig.TypeName, paintingCanvas);
                    graphic.conturColor = Brushes.Aqua;
                    graphic.fillColor = oldPath.Fill;
                    newFig.Draw(graphic);
                    figureDict.Add((Path)graphic.path, newFig);
                    selectedFigures.Remove(fig);
                    selectedFigures.Add(newFig);
                }
            }
        }
    }
}