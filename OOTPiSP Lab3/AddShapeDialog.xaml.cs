using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using Figures;

namespace OOTPiSP_Lab3
{
    /// <summary>
    /// Dialog window for adding a new shape with property input
    /// </summary>
    public partial class AddShapeDialog : Window
    {
        public Shape CreatedShape { get; private set; }

        public AddShapeDialog()
        {
            InitializeComponent();
            LoadShapeTypes();
        }

        private void LoadShapeTypes()
        {
            foreach (var name in ShapeFactory.GetRegisteredShapeNames())
            {
                ShapeTypeCombo.Items.Add(name);
            }
            if (ShapeTypeCombo.Items.Count > 0)
                ShapeTypeCombo.SelectedIndex = 0;
        }

        private void OnShapeTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShapeTypeCombo.SelectedItem == null) return;

            PropertiesPanel.Children.Clear();
            string typeName = ShapeTypeCombo.SelectedItem.ToString();
            CreatedShape = ShapeFactory.CreateShape(typeName);

            switch (typeName)
            {
                case "LineShape": CreateLineEditors(); break;
                case "RectangleShape": CreateRectangleEditors(); break;
                case "EllipseShape": CreateEllipseEditors(); break;
                case "CircleShape": CreateCircleEditors(); break;
                case "TriangleShape": CreateTriangleEditors(); break;
                case "RegularPolygon": CreatePolygonEditors(); break;
            }
        }

        #region Editor Creation Methods

        private void CreateLineEditors()
        {
            AddNumericEditor("X1", "0");
            AddNumericEditor("Y1", "0");
            AddNumericEditor("X2", "200");
            AddNumericEditor("Y2", "100");
            AddColorEditor("Stroke", "Black");
            AddNumericEditor("StrokeThickness", "3");
        }

        private void CreateRectangleEditors()
        {
            AddNumericEditor("X", "50");
            AddNumericEditor("Y", "50");
            AddNumericEditor("Width", "120");
            AddNumericEditor("Height", "80");
            AddColorEditor("Stroke", "Black");
            AddColorEditor("Fill", "LightBlue");
            AddNumericEditor("StrokeThickness", "3");
        }

        private void CreateEllipseEditors()
        {
            AddNumericEditor("Cx", "150");
            AddNumericEditor("Cy", "150");
            AddNumericEditor("Width", "110");
            AddNumericEditor("Height", "70");
            AddColorEditor("Stroke", "Black");
            AddColorEditor("Fill", "Lavender");
            AddNumericEditor("StrokeThickness", "3");
        }

        private void CreateCircleEditors()
        {
            AddNumericEditor("Cx", "200");
            AddNumericEditor("Cy", "150");
            AddNumericEditor("Radius", "55");
            AddColorEditor("Stroke", "Black");
            AddColorEditor("Fill", "LightCoral");
            AddNumericEditor("StrokeThickness", "3");
        }

        private void CreateTriangleEditors()
        {
            AddNumericEditor("X1", "100"); AddNumericEditor("Y1", "50");
            AddNumericEditor("X2", "50"); AddNumericEditor("Y2", "150");
            AddNumericEditor("X3", "150"); AddNumericEditor("Y3", "150");
            AddColorEditor("Stroke", "Black");
            AddColorEditor("Fill", "LightGreen");
            AddNumericEditor("StrokeThickness", "3");
        }

        private void CreatePolygonEditors()
        {
            AddNumericEditor("Cx", "200");
            AddNumericEditor("Cy", "200");
            AddNumericEditor("Sides", "6");
            AddNumericEditor("Radius", "70");
            AddColorEditor("Stroke", "Black");
            AddColorEditor("Fill", "LightYellow");
            AddNumericEditor("StrokeThickness", "3");
        }

        private void AddNumericEditor(string label, string defaultValue)
        {
            var stack = new StackPanel { Margin = new Thickness(0, 6, 0, 6) };
            stack.Children.Add(new TextBlock { Text = label + ":", FontWeight = FontWeights.Medium });
            var tb = new TextBox { Text = defaultValue, Tag = label, Margin = new Thickness(0, 2, 0, 0) };
            stack.Children.Add(tb);
            PropertiesPanel.Children.Add(stack);
        }

        private void AddColorEditor(string label, string defaultValue)
        {
            var stack = new StackPanel { Margin = new Thickness(0, 6, 0, 6) };
            stack.Children.Add(new TextBlock { Text = label + ":", FontWeight = FontWeights.Medium });
            var cb = new ComboBox { Tag = label, Margin = new Thickness(0, 2, 0, 0) };
            string[] colors = { "Black", "Red", "Blue", "Green", "Yellow", "Orange", "Purple",
                                "Gray", "LightBlue", "LightCoral", "LightGreen", "Lavender", "LightYellow" };
            foreach (var c in colors) cb.Items.Add(c);
            cb.SelectedItem = defaultValue;
            stack.Children.Add(cb);
            PropertiesPanel.Children.Add(stack);
        }

        #endregion

        #region Helper Methods

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
            if (CreatedShape == null)
            {
                DialogResult = false;
                return;
            }

            string type = ShapeTypeCombo.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(type)) return;

            try
            {
                switch (type)
                {
                    case "LineShape": ReadLineProperties(); break;
                    case "RectangleShape": ReadRectangleProperties(); break;
                    case "EllipseShape": ReadEllipseProperties(); break;
                    case "CircleShape": ReadCircleProperties(); break;
                    case "TriangleShape": ReadTriangleProperties(); break;
                    case "RegularPolygon": ReadPolygonProperties(); break;
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to apply properties:\n{ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Read Properties (Apply to CreatedShape)

        private void ReadLineProperties()
        {
            var s = (LineShape)CreatedShape;
            if (double.TryParse(GetTextBox("X1")?.Text, out var v)) s.X1 = v;
            if (double.TryParse(GetTextBox("Y1")?.Text, out v)) s.Y1 = v;
            if (double.TryParse(GetTextBox("X2")?.Text, out v)) s.X2 = v;
            if (double.TryParse(GetTextBox("Y2")?.Text, out v)) s.Y2 = v;
            if (GetComboBox("Stroke")?.SelectedItem is string strokeStr)
                s.Stroke = (Brush)new BrushConverter().ConvertFromString(strokeStr);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadRectangleProperties()
        {
            var s = (RectangleShape)CreatedShape;
            if (double.TryParse(GetTextBox("X")?.Text, out var v)) s.X = v;
            if (double.TryParse(GetTextBox("Y")?.Text, out v)) s.Y = v;
            if (double.TryParse(GetTextBox("Width")?.Text, out v)) s.Width = v;
            if (double.TryParse(GetTextBox("Height")?.Text, out v)) s.Height = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadEllipseProperties()
        {
            var s = (EllipseShape)CreatedShape;
            if (double.TryParse(GetTextBox("Cx")?.Text, out var v)) s.Cx = v;
            if (double.TryParse(GetTextBox("Cy")?.Text, out v)) s.Cy = v;
            if (double.TryParse(GetTextBox("Width")?.Text, out v)) s.Width = v;
            if (double.TryParse(GetTextBox("Height")?.Text, out v)) s.Height = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadCircleProperties()
        {
            var s = (CircleShape)CreatedShape;
            if (double.TryParse(GetTextBox("Cx")?.Text, out var v)) s.Cx = v;
            if (double.TryParse(GetTextBox("Cy")?.Text, out v)) s.Cy = v;
            if (double.TryParse(GetTextBox("Radius")?.Text, out v)) s.Radius = v;
            if (GetComboBox("Stroke")?.SelectedItem is string str) s.Stroke = (Brush)new BrushConverter().ConvertFromString(str);
            if (GetComboBox("Fill")?.SelectedItem is string f) s.Fill = (Brush)new BrushConverter().ConvertFromString(f);
            if (double.TryParse(GetTextBox("StrokeThickness")?.Text, out v)) s.StrokeThickness = v;
        }

        private void ReadTriangleProperties()
        {
            var s = (TriangleShape)CreatedShape;
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

        private void ReadPolygonProperties()
        {
            var s = (RegularPolygon)CreatedShape;
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