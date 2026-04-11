using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;

namespace Figures
{
    public class LineShape : Shape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }

        public LineShape(double x1, double y1, double x2, double y2,
                         Brush stroke, double thickness)
            : base(stroke, thickness)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public LineShape() : base()
        {
            X1 = 0;
            Y1 = 0;
            X2 = 100;
            Y2 = 100;
        }

        public override void Draw(Canvas canvas)
        {
            var line = new Line
            {
                X1 = X1,
                Y1 = Y1,
                X2 = X2,
                Y2 = Y2,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(line);
        }

        public override string GetClassName() => "LineShape";

        public override void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", GetClassName());
            writer.WriteNumber("X1", X1);
            writer.WriteNumber("Y1", Y1);
            writer.WriteNumber("X2", X2);
            writer.WriteNumber("Y2", Y2);
            writer.WriteString("Stroke", Stroke.ToString());
            writer.WriteNumber("StrokeThickness", StrokeThickness);
            writer.WriteEndObject();
        }

        public override void ReadJson(JsonElement element, JsonSerializerOptions options)
        {
            if (element.TryGetProperty("X1", out JsonElement x1)) X1 = x1.GetDouble();
            if (element.TryGetProperty("Y1", out JsonElement y1)) Y1 = y1.GetDouble();
            if (element.TryGetProperty("X2", out JsonElement x2)) X2 = x2.GetDouble();
            if (element.TryGetProperty("Y2", out JsonElement y2)) Y2 = y2.GetDouble();
            if (element.TryGetProperty("Stroke", out JsonElement stroke))
                Stroke = (Brush)new BrushConverter().ConvertFromString(stroke.GetString());
            if (element.TryGetProperty("StrokeThickness", out JsonElement thickness))
                StrokeThickness = thickness.GetDouble();
        }

        public override Shape Clone()
        {
            return new LineShape(X1, Y1, X2, Y2, Stroke, StrokeThickness);
        }
    }
}