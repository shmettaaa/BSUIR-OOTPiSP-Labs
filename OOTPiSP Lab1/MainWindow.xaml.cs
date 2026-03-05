using Figures;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOTPiSP_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShapeList _shapeList;

        public MainWindow()
        {
            InitializeComponent();

            InitializeShapes();

            _shapeList.DrawAll(DrawingCanvas);
        }

        private void InitializeShapes()
        {
            _shapeList = new ShapeList();

            _shapeList.Add(new LineShape(100, 100, 250, 150, Brushes.Red, 3));

            _shapeList.Add(new RectangleShape(100, 200, 150, 100,
                Brushes.Blue, Brushes.LightBlue, 2));

            _shapeList.Add(new EllipseShape(300, 200, 120, 80,
                Brushes.Green, Brushes.LightGreen, 2));

            _shapeList.Add(new CircleShape(500, 200, 70,
                Brushes.Orange, Brushes.Yellow, 3));

            _shapeList.Add(new SquareShape(100, 350, 80,
                Brushes.Purple, Brushes.Violet, 2));

            _shapeList.Add(new TriangleShape(
                new Point(300, 400),
                new Point(400, 400),
                new Point(350, 300),
                Brushes.Brown, Brushes.SandyBrown, 2));
        }
    }
}