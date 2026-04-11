using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using Figures;

namespace OOTPiSP_Lab3
{
    public partial class ShapePropertyDialog : Window
    {
        private readonly Shape _shape;

        public ShapePropertyDialog(Shape shape)
        {
            InitializeComponent();
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            LoadCurrentProperties();
        }

        private void LoadCurrentProperties()
        {
            PropertiesPanel.Children.Clear();
            string type = _shape.GetClassName();

            switch (type)
            {
                case "LineShape": CreateLineEditEditors((LineShape)_shape); break;
                case "RectangleShape": CreateRectangleEditEditors((RectangleShape)_shape); break;
                case "EllipseShape": CreateEllipseEditEditors((EllipseShape)_shape); break;
                case "CircleShape": CreateCircleEditEditors((CircleShape)_shape); break;
                case "TriangleShape": CreateTriangleEditEditors((TriangleShape)_shape); break;
                case "RegularPolygon": CreatePolygonEditEditors((RegularPolygon)_shape); break;
            }
        }

        #region Edit Editor Creators (with current values)

        private void CreateLineEditEditors(LineShape s)
        {
            AddNumericEditor("X1", s.X1.ToString());
            AddNumericEditor("Y1", s.Y1.ToString());
            AddNumericEditor("X2", s.X2.ToString());
            AddNumericEditor("Y2", s.Y2.ToString());
            AddColorEditor("Stroke", s.Stroke.ToString());
            AddNumericEditor("StrokeThickness", s.StrokeThickness.ToString());
        }

        private void CreateRectangleEditEditors(RectangleShape s)
        {
            AddNumericEditor("X", s.X.ToString());
            AddNumericEditor("Y", s.Y.ToString());
            AddNumericEditor("Width", s.Width.ToString());
            AddNumericEditor("Height", s.Height.ToString());
            AddColorEditor("Stroke", s.Stroke.ToString());
            AddColorEditor("Fill", s.Fill.ToString());
            AddNumericEditor("StrokeThickness", s.StrokeThickness.ToString());
        }

        private void CreateEllipseEditEditors(EllipseShape s)
        {
            AddNumericEditor("Cx", s.Cx.ToString());
            AddNumericEditor("Cy", s.Cy.ToString());
            AddNumericEditor("Width", s.Width.ToString());
            AddNumericEditor("Height", s.Height.ToString());
            AddColorEditor("Stroke", s.Stroke.ToString());
            AddColorEditor("Fill", s.Fill.ToString());
            AddNumericEditor("StrokeThickness", s.StrokeThickness.ToString());
        }

        private void CreateCircleEditEditors(CircleShape s)
        {
            AddNumericEditor("Cx", s.Cx.ToString());
            AddNumericEditor("Cy", s.Cy.ToString());
            AddNumericEditor("Radius", s.Radius.ToString());
            AddColorEditor("Stroke", s.Stroke.ToString());
            AddColorEditor("Fill", s.Fill.ToString());
            AddNumericEditor("StrokeThickness", s.StrokeThickness.ToString());
        }

        private void CreateTriangleEditEditors(TriangleShape s)
        {
            AddNumericEditor("X1", s.X1.ToString()); AddNumericEditor("Y1", s.Y1.ToString());
            AddNumericEditor("X2", s.X2.ToString()); AddNumericEditor("Y2", s.Y2.ToString());
            AddNumericEditor("X3", s.X3.ToString()); AddNumericEditor("Y3", s.Y3.ToString());
            AddColorEditor("Stroke", s.Stroke.ToString());
            AddColorEditor("Fill", s.Fill.ToString());
            AddNumericEditor("StrokeThickness", s.StrokeThickness.ToString());
        }

        private void CreatePolygonEditEditors(RegularPolygon s)
        {
            AddNumericEditor("Cx", s.Cx.ToString());
            AddNumericEditor("Cy", s.Cy.ToString());
            AddNumericEditor("Sides", s.Sides.ToString());
            AddNumericEditor("Radius", s.Radius.ToString());
            AddColorEditor("Stroke", s.Stroke.ToString());
            AddColorEditor("Fill", s.Fill.ToString());
            AddNumericEditor("StrokeThickness", s.StrokeThickness.ToString());
        }

        private void AddNumericEditor(string label, string value)
        {
            var stack = new StackPanel { Margin = new Thickness(0, 6, 0, 6) };
            stack.Children.Add(new TextBlock { Text = label + ":", FontWeight = FontWeights.Medium });
            var tb = new TextBox { Text = value, Tag = label, Margin = new Thickness(0, 2, 0, 0) };
            stack.Children.Add(tb);
            PropertiesPanel.Children.Add(stack);
        }

        private void AddColorEditor(string label, string value)
        {
            var stack = new StackPanel { Margin = new Thickness(0, 6, 0, 6) };
            stack.Children.Add(new TextBlock { Text = label + ":", FontWeight = FontWeights.Medium });
            var cb = new ComboBox { Tag = label, Margin = new Thickness(0, 2, 0, 0) };
            string[] colors = { "Black", "Red", "Blue", "Green", "Yellow", "Orange", "Purple",
                                "Gray", "LightBlue", "LightCoral", "LightGreen", "Lavender", "LightYellow" };
            foreach (var c in colors) cb.Items.Add(c);
            cb.SelectedItem = value;
            stack.Children.Add(cb);
            PropertiesPanel.Children.Add(stack);
        }

        private TextBox GetTextBox(string tag) =>
            PropertiesPanel.Children.OfType<StackPanel>()
                .SelectMany(sp => sp.Children.OfType<TextBox>())
                .FirstOrDefault(tb => tb.Tag?.ToString() == tag);

        private ComboBox GetComboBox(string tag) =>
            PropertiesPanel.Children.OfType<StackPanel>()
                .SelectMany(sp => sp.Children.OfType<ComboBox>())
                .FirstOrDefault(cb => cb.Tag?.ToString() == tag);

        #endregion

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            string type = _shape.GetClassName();
            try
            {
                switch (type)
                {
                    case "LineShape": ReadLineEditProperties(); break;
                    case "RectangleShape": ReadRectangleEditProperties(); break;
                    case "EllipseShape": ReadEllipseEditProperties(); break;
                    case "CircleShape": ReadCircleEditProperties(); break;
                    case "TriangleShape": ReadTriangleEditProperties(); break;
                    case "RegularPolygon": ReadPolygonEditProperties(); break;
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to apply changes:\n{ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Read Edit Properties

        private void ReadLineEditProperties()
        {
            var s = (LineShape)_shape;
            if (double.TryParse(GetTextBox("X1")?.Text, out var v)) s.X1 = v;
            if (double.TryParse(GetTextBox("Y1")?.Text, out v)) s.Y1 = v;
            if (double.TryParse(GetTextBox("X2")?.Text, out v)) s.X2 = v;
            if (double.TryParse(GetTextBox("Y2")?.Text, out v)) s.Y2 = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadRectangleEditProperties()
        {
            var s = (RectangleShape)_shape;
            if (double.TryParse(GetTextBox("X")?.Text, out var v)) s.X = v;
            if (double.TryParse(GetTextBox("Y")?.Text, out v)) s.Y = v;
            if (double.TryParse(GetTextBox("Width")?.Text, out v)) s.Width = v;
            if (double.TryParse(GetTextBox("Height")?.Text, out v)) s.Height = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadEllipseEditProperties()
        {
            var s = (EllipseShape)_shape;
            if (double.TryParse(GetTextBox("Cx")?.Text, out var v)) s.Cx = v;
            if (double.TryParse(GetTextBox("Cy")?.Text, out v)) s.Cy = v;
            if (double.TryParse(GetTextBox("Width")?.Text, out v)) s.Width = v;
            if (double.TryParse(GetTextBox("Height")?.Text, out v)) s.Height = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadCircleEditProperties()
        {
            var s = (CircleShape)_shape;
            if (double.TryParse(GetTextBox("Cx")?.Text, out var v)) s.Cx = v;
            if (double.TryParse(GetTextBox("Cy")?.Text, out v)) s.Cy = v;
            if (double.TryParse(GetTextBox("Radius")?.Text, out v)) s.Radius = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadTriangleEditProperties()
        {
            var s = (TriangleShape)_shape;
            if (double.TryParse(GetTextBox("X1")?.Text, out var v)) s.X1 = v;
            if (double.TryParse(GetTextBox("Y1")?.Text, out v)) s.Y1 = v;
            if (double.TryParse(GetTextBox("X2")?.Text, out v)) s.X2 = v;
            if (double.TryParse(GetTextBox("Y2")?.Text, out v)) s.Y2 = v;
            if (double.TryParse(GetTextBox("X3")?.Text, out v)) s.X3 = v;
            if (double.TryParse(GetTextBox("Y3")?.Text, out v)) s.Y3 = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadPolygonEditProperties()
        {
            var s = (RegularPolygon)_shape;
            if (double.TryParse(GetTextBox("Cx")?.Text, out var v)) s.Cx = v;
            if (double.TryParse(GetTextBox("Cy")?.Text, out v)) s.Cy = v;
            if (int.TryParse(GetTextBox("Sides")?.Text, out var sides)) s.Sides = sides;
            if (double.TryParse(GetTextBox("Radius")?.Text, out v)) s.Radius = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        #endregion

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}