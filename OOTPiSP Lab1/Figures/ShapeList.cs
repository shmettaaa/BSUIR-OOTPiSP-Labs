using System.Collections.Generic;
using System.Windows.Controls;

namespace Figures
{
    // Container for storing multiple shapes
    public class ShapeList
    {
        // Internal list to store shapes
        private List<Shape> _shapes = new List<Shape>();

        // Add a shape to the list
        public void Add(Shape shape) => _shapes.Add(shape);

        // Remove a shape from the list
        public void Remove(Shape shape) => _shapes.Remove(shape);

        // Remove all shapes
        public void Clear() => _shapes.Clear();

        // Draw all shapes on the given canvas
        // Clears canvas first, then draws each shape
        public void DrawAll(Canvas canvas)
        {
            canvas.Children.Clear(); // Remove previous drawings

            // Polymorphic call: each shape draws itself
            foreach (var shape in _shapes)
            {
                shape.Draw(canvas);
            }
        }
    }
}