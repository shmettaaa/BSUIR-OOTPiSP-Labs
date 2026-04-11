using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();

            var count = FigureRegistry.GetAllNames().Count();
            System.Diagnostics.Debug.WriteLine($"Registered shapes count: {count}");

            foreach (var name in FigureRegistry.GetAllNames())
            {
                cmbShapeTypes.Items.Add(name);
                System.Diagnostics.Debug.WriteLine($"Added: {name}");
            }

            if (cmbShapeTypes.Items.Count > 0)
                cmbShapeTypes.SelectedIndex = 0;
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
            ClearPointsAndDot();
        }
    }
}