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
        private ICommand createLineCommand = null;
        private ICommand createTriangleCommand = null;
        private ICommand createRectangleCommand = null;
        private ICommand createEllipseCommand = null;
        private ICommand penButton = null;
        private ICommand mouseDown = null;
        private ICommand selectField = null;

        

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
                actionCommands.Push(createLineCommand);
                return createLineCommand;
            }
        }

        public ICommand CreateTriangleCommand
        {
            get
            {
                createTriangleCommand = new ActionCommand(createTriangle, param => true);
                actionCommands.Push(createTriangleCommand);
                return createTriangleCommand;
            }
        }


        public ICommand CreateRectangleCommand
        {
            get
            {
                createRectangleCommand = new ActionCommand(createRectangle, param => true);
                actionCommands.Push(createRectangleCommand);
                return createRectangleCommand;
            }
        }


        public ICommand CreateEllipseCommand
        {
            get
            {
                createEllipseCommand = new ActionCommand(createEllipse, param => true);
                actionCommands.Push(createEllipseCommand);
                return createEllipseCommand;
            }
        }


        public ICommand PenButton
        {
            get
            {
                penButton = new ActionCommand(activatePen, param => true);
                actionCommands.Push(penButton);
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
            FigureFactory figureFactory = FigureFabric.GetFactory("Triangle");
            IFigure triangle = figureFactory.GetFigure(0, new GrRed.Vector(50, 50), new GrRed.Vector(10, 10));
            triangle.Draw(triangleGrafic);
        }

        private void createRectangle(object obj)
        {
            Path path = new Path();
            RectangleGrafic rectangle = new RectangleGrafic(paintingCanvas, path);
            FigureFactory figureFactory = FigureFabric.GetFactory("Square");
            IFigure square = figureFactory.GetFigure(0, new GrRed.Vector(50, 50), new GrRed.Vector(10, 10));
        }


        private void createEllipse(object obj)
        {
            Path path = new Path();
            EllipseGrafic ellipseGrafic = new EllipseGrafic(paintingCanvas, path);
            FigureFactory figureFactory = FigureFabric.GetFactory("Ellipse");
            IFigure ellipse = figureFactory.GetFigure(0, new GrRed.Vector(50, 50), new GrRed.Vector(10, 10));
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
