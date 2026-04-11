using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;

namespace Figures
{
    public class ShapesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Shape> _shapes;
        private Shape _selectedShape;

        public ObservableCollection<Shape> Shapes
        {
            get => _shapes;
            set
            {
                _shapes = value;
                OnPropertyChanged(nameof(Shapes));
            }
        }

        public Shape SelectedShape
        {
            get => _selectedShape;
            set
            {
                _selectedShape = value;
                OnPropertyChanged(nameof(SelectedShape));
                OnPropertyChanged(nameof(HasSelectedShape));
            }
        }

        public bool HasSelectedShape => SelectedShape != null;

        public ShapesViewModel()
        {
            Shapes = new ObservableCollection<Shape>();
        }

        public void AddShape(Shape shape)
        {
            if (shape != null)
            {
                Shapes.Add(shape);
            }
        }

        public void RemoveSelectedShape()
        {
            if (SelectedShape != null)
            {
                Shapes.Remove(SelectedShape);
                SelectedShape = null;
            }
        }

        public void SerializeToFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true }))
                {
                    writer.WriteStartArray();
                    foreach (var shape in Shapes)
                    {
                        shape.WriteJson(writer, null);
                    }
                    writer.WriteEndArray();
                }
                MessageBox.Show($"Successfully saved {Shapes.Count} shapes to {filePath}", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving shapes: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeserializeFromFile(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                JsonDocument document = JsonDocument.Parse(jsonContent);
                JsonElement root = document.RootElement;

                var newShapes = new ObservableCollection<Shape>();

                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement shapeElement in root.EnumerateArray())
                    {
                        if (shapeElement.TryGetProperty("type", out JsonElement typeElement))
                        {
                            string typeName = typeElement.GetString();
                            if (ShapeFactory.IsShapeRegistered(typeName))
                            {
                                Shape shape = ShapeFactory.CreateShape(typeName);
                                shape.ReadJson(shapeElement, null);
                                newShapes.Add(shape);
                            }
                        }
                    }
                }

                Shapes = newShapes;
                MessageBox.Show($"Successfully loaded {Shapes.Count} shapes from {filePath}", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shapes: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool EditShapeProperties(Shape shape, Window owner)
        {
            if (shape == null) return false;

            var dialog = new OOTPiSP_Lab3.ShapePropertyDialog(shape);
            dialog.Owner = owner;

            if (dialog.ShowDialog() == true)
            {
                OnPropertyChanged(nameof(Shapes));
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}