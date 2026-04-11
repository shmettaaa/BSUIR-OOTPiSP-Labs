using System.Text.Json;
using System.Windows.Media;

namespace Figures
{
    public abstract class ClosedShape : Shape
    {
        public Brush Fill { get; set; }

        protected ClosedShape(Brush stroke, Brush fill, double thickness)
            : base(stroke, thickness)
        {
            Fill = fill;
        }

        protected ClosedShape() : base()
        {
            Fill = Brushes.Transparent;
        }

        protected void WriteBrush(Utf8JsonWriter writer, string propertyName, Brush brush)
        {
            writer.WriteString(propertyName, brush.ToString());
        }

        protected Brush ReadBrush(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out JsonElement brushElement))
            {
                string brushString = brushElement.GetString();
                return (Brush)new BrushConverter().ConvertFromString(brushString);
            }
            return Brushes.Black;
        }
    }
}