using System.Windows.Media;
using System.Windows.Controls;
using System.Text.Json;

namespace Figures
{
    public abstract class Shape
    {
        public Brush Stroke { get; set; }

        public double StrokeThickness { get; set; }

        protected Shape(Brush stroke, double thickness)
        {
            Stroke = stroke;
            StrokeThickness = thickness;
        }

        protected Shape()
        {
            Stroke = Brushes.Black;
            StrokeThickness = 1.0;
        }

        public abstract void Draw(Canvas canvas);

        public abstract string GetClassName();

        public abstract void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options);

        public abstract void ReadJson(JsonElement element, JsonSerializerOptions options);

        public abstract Shape Clone();
    }
}