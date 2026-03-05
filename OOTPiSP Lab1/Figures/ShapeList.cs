using System.Collections.Generic;
using System.Windows.Controls;

namespace Figures
{
    public class ShapeList
    {
        private List<Shape> _shapes = new List<Shape>();

        public void Add(Shape shape) => _shapes.Add(shape);
        public void Remove(Shape shape) => _shapes.Remove(shape);
        public void Clear() => _shapes.Clear();

        public void DrawAll(Canvas canvas)
        {
            canvas.Children.Clear();
            foreach (var shape in _shapes)
            {
                shape.Draw(canvas);
            }
        }
    }
}