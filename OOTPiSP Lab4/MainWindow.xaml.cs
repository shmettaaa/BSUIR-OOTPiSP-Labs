using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Figures;
using Figures.Renderers;

namespace FiguresApp
{
    public partial class MainWindow : Window
    {
        private readonly List<Point> clickPoints = new List<Point>();
        private Ellipse startDot;
        private ObservableCollection<Figures.Shape> _shapes = new ObservableCollection<Figures.Shape>();          
        private bool _isDirty = false;
        private IDataTransformer _activeTransformer;

        public MainWindow()
        {
            InitializeComponent();

            _activeTransformer = TransformerRegistry.Instance.GetAll().FirstOrDefault();

            _shapes.CollectionChanged += Shapes_CollectionChanged;

            this.Title = $"FiguresApp - {_shapes.Count} shapes";

            foreach (var name in FigureRegistry.GetAllNames())
            {
                cmbShapeTypes.Items.Add(name);
            }
            if (cmbShapeTypes.Items.Count > 0)
                cmbShapeTypes.SelectedIndex = 0;
        }

        private void Shapes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _isDirty = true;

            var baseTitle = "FiguresApp";
            this.Title = _isDirty ? $"{baseTitle} - {_shapes.Count} shapes*" : $"{baseTitle} - {_shapes.Count} shapes";
        }

        private void cmbShapeTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            polygonPanel.Visibility =
                (cmbShapeTypes.SelectedItem?.ToString() == "Многоугольник")
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void drawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(drawingCanvas);
            clickPoints.Add(p);

            if (clickPoints.Count == 1)
                ShowStartDot(p);

            var handler = FigureRegistry.Get(cmbShapeTypes.SelectedItem?.ToString());
            if (handler == null) return;

            if (clickPoints.Count >= handler.Factory.RequiredPointCount)
            {
                DrawCurrentShape(handler);
                ClearPointsAndDot();
            }
        }

        private void DrawCurrentShape(FigureHandler handler)
        {
            var stroke = new SolidColorBrush(colorPickerStroke.SelectedColor ?? Colors.Black);
            var fill = chkFilled.IsChecked == true
                ? new SolidColorBrush(colorPickerFill.SelectedColor ?? Colors.Transparent)
                : Brushes.Transparent;

            double thickness = sldThickness.Value;
            int sides = (cmbShapeTypes.SelectedItem?.ToString() == "Многоугольник")
                ? (int)sldSides.Value
                : 0;

            var model = handler.Create(clickPoints.AsReadOnly(), stroke, fill, thickness, sides);

            if (model != null)
            {
                handler.Renderer.Render(model, drawingCanvas);
                _shapes.Add(model);   
            }
        }

        private void ShowStartDot(Point p)
        {
            startDot = new Ellipse
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Red,
                Opacity = 0.3,
                StrokeThickness = 0
            };

            Canvas.SetLeft(startDot, p.X - 4);
            Canvas.SetTop(startDot, p.Y - 4);
            drawingCanvas.Children.Add(startDot);
        }

        private void ClearPointsAndDot()
        {
            clickPoints.Clear();

            if (startDot != null)
            {
                drawingCanvas.Children.Remove(startDot);
                startDot = null;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            drawingCanvas.Children.Clear();
            _shapes.Clear();
            ClearPointsAndDot();
            _isDirty = false;
            this.Title = $"FiguresApp - {_shapes.Count} shapes";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog { Filter = "XML files|*.xml" };
            if (dialog.ShowDialog() == true)
            {
                string xml = ShapeCollectionSerializer.SaveToString(_shapes);
                if (_activeTransformer != null)
                    xml = _activeTransformer.TransformBeforeSave(xml);
                File.WriteAllText(dialog.FileName, xml);
                
                _isDirty = false;
                this.Title = $"FiguresApp - {_shapes.Count} shapes";
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog { Filter = "XML files|*.xml" };
            if (dialog.ShowDialog() == true)
            {
                string xml = File.ReadAllText(dialog.FileName);
                
                if (_activeTransformer != null)
                    xml = _activeTransformer.TransformAfterLoad(xml);
                var loadedShapes = ShapeCollectionSerializer.LoadFromString(xml);
               
                drawingCanvas.Children.Clear();
                _shapes.Clear();
                foreach (var shape in loadedShapes)
                {
                    _shapes.Add(shape);
                    var renderer = RendererRegistry.GetRenderer(shape);
                    renderer?.Render(shape, drawingCanvas);
                }
                _isDirty = false;
                this.Title = $"FiguresApp - {_shapes.Count} shapes";
            }
        }

        private void ExportHtml_Click(object sender, RoutedEventArgs e)
        {
            var htmlTransformer = _activeTransformer?.Name == "HTML Report"
                ? _activeTransformer
                : TransformerRegistry.Instance.GetAll().FirstOrDefault(t => t.Name == "HTML Report");

            if (htmlTransformer == null)
            {
                MessageBox.Show("HTML Report plugin not found.", "Export Error");
                return;
            }

            var dialog = new Microsoft.Win32.SaveFileDialog { Filter = "HTML files|*.html" };
            if (dialog.ShowDialog() == true)
            {
                string xml = ShapeCollectionSerializer.SaveToString(_shapes);
                string html = htmlTransformer.TransformBeforeSave(xml);
                File.WriteAllText(dialog.FileName, html);
                MessageBox.Show("HTML report exported.", "Export");
            }
        }

        private void ConfigureTransformers_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TransformersSettingsWindow(_activeTransformer);
            if (dialog.ShowDialog() == true)
            {
                _activeTransformer = dialog.SelectedTransformer;
            }
        }
    }
}