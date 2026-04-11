using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using Figures;

namespace OOTPiSP_Lab3
{
    public partial class MainWindow : Window
    {
        private readonly ShapesViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ShapesViewModel();
            DataContext = _viewModel;

            AddSampleShapes();
            RefreshDrawing();
        }

        private void AddSampleShapes()
        {
            _viewModel.AddShape(new RectangleShape(50, 50, 120, 80, Brushes.Blue, Brushes.LightBlue, 3));
            _viewModel.AddShape(new CircleShape(280, 140, 55, Brushes.Red, Brushes.LightCoral, 3));
            _viewModel.AddShape(new TriangleShape(480, 60, 430, 160, 530, 160, Brushes.Green, Brushes.LightGreen, 3));
            _viewModel.AddShape(new EllipseShape(180, 320, 110, 70, Brushes.Purple, Brushes.Lavender, 3));
            _viewModel.AddShape(new LineShape(40, 280, 190, 380, Brushes.Black, 4));
            _viewModel.AddShape(new RegularPolygon(520, 340, 6, 65, Brushes.Orange, Brushes.LightYellow, 3));
        }

        private void RefreshDrawing()
        {
            DrawingCanvas.Children.Clear();
            foreach (var shape in _viewModel.Shapes)
            {
                shape.Draw(DrawingCanvas);
            }
        }

        private void OnAddShapeClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddShapeDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true && dialog.CreatedShape != null)
            {
                _viewModel.AddShape(dialog.CreatedShape);
                RefreshDrawing();
            }
        }

        private void OnRemoveShapeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.RemoveSelectedShape();
            RefreshDrawing();
        }

        private void OnEditShapeClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.EditShapeProperties(_viewModel.SelectedShape, this))
            {
                RefreshDrawing();
            }
        }

        private void OnRefreshDrawingClick(object sender, RoutedEventArgs e)
        {
            RefreshDrawing();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                FileName = "shapes.json"
            };

            if (saveDialog.ShowDialog() == true)
            {
                _viewModel.SerializeToFile(saveDialog.FileName);
            }
        }

        private void OnLoadClick(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json"
            };

            if (openDialog.ShowDialog() == true)
            {
                _viewModel.DeserializeFromFile(openDialog.FileName);
                RefreshDrawing();
            }
        }
    }
}