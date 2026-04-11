using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;

namespace Figures
{
    public class RectangleShape : ClosedShape
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleShape(double x, double y, double width, double height,
                              Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleShape() : base()
        {
            X = 0;
            Y = 0;
            Width = 100;
            Height = 100;
        }

        public override void Draw(Canvas canvas)
        {
            var rect = new Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
            canvas.Children.Add(rect);
        }

        public override string GetClassName() => "RectangleShape";

        public override void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", GetClassName());
            writer.WriteNumber("X", X);
            writer.WriteNumber("Y", Y);
            writer.WriteNumber("Width", Width);
            writer.WriteNumber("Height", Height);
            writer.WriteString("Stroke", Stroke.ToString());
            writer.WriteString("Fill", Fill.ToString());
            writer.WriteNumber("StrokeThickness", StrokeThickness);
            writer.WriteEndObject();
        }

        public override void ReadJson(JsonElement element, JsonSerializerOptions options)
        {
            if (element.TryGetProperty("X", out JsonElement x)) X = x.GetDouble();
            if (element.TryGetProperty("Y", out JsonElement y)) Y = y.GetDouble();
            if (element.TryGetProperty("Width", out JsonElement w)) Width = w.GetDouble();
            if (element.TryGetProperty("Height", out JsonElement h)) Height = h.GetDouble();
            if (element.TryGetProperty("Stroke", out JsonElement stroke))
                Stroke = (Brush)new BrushConverter().ConvertFromString(stroke.GetString());
            if (element.TryGetProperty("Fill", out JsonElement fill))
                Fill = (Brush)new BrushConverter().ConvertFromString(fill.GetString());
            if (element.TryGetProperty("StrokeThickness", out JsonElement thickness))
                StrokeThickness = thickness.GetDouble();
        }

        public override Shape Clone()
        {
            return new RectangleShape(X, Y, Width, Height, Stroke, Fill, StrokeThickness);
        }

    }
}