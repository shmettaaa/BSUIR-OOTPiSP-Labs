using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;

namespace Figures
{
    public class TriangleShape : ClosedShape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double X3 { get; set; }
        public double Y3 { get; set; }

        public TriangleShape(double x1, double y1, double x2, double y2, double x3, double y3,
                             Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }

        public TriangleShape() : base()
        {
            X1 = 100; Y1 = 50;
            X2 = 50; Y2 = 150;
            X3 = 150; Y3 = 150;
        }

        public override void Draw(Canvas canvas)
        {
            var polygon = new Polygon
            {
                Points = new PointCollection
                {
                    new Point(X1, Y1),
                    new Point(X2, Y2),
                    new Point(X3, Y3)
                },
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(polygon);
        }

        public override string GetClassName() => "TriangleShape";

        public override void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", GetClassName());
            writer.WriteNumber("X1", X1);
            writer.WriteNumber("Y1", Y1);
            writer.WriteNumber("X2", X2);
            writer.WriteNumber("Y2", Y2);
            writer.WriteNumber("X3", X3);
            writer.WriteNumber("Y3", Y3);
            writer.WriteString("Stroke", Stroke.ToString());
            writer.WriteString("Fill", Fill.ToString());
            writer.WriteNumber("StrokeThickness", StrokeThickness);
            writer.WriteEndObject();
        }

        public override void ReadJson(JsonElement element, JsonSerializerOptions options)
        {
            if (element.TryGetProperty("X1", out JsonElement x1)) X1 = x1.GetDouble();
            if (element.TryGetProperty("Y1", out JsonElement y1)) Y1 = y1.GetDouble();
            if (element.TryGetProperty("X2", out JsonElement x2)) X2 = x2.GetDouble();
            if (element.TryGetProperty("Y2", out JsonElement y2)) Y2 = y2.GetDouble();
            if (element.TryGetProperty("X3", out JsonElement x3)) X3 = x3.GetDouble();
            if (element.TryGetProperty("Y3", out JsonElement y3)) Y3 = y3.GetDouble();
            if (element.TryGetProperty("Stroke", out JsonElement stroke))
                Stroke = (Brush)new BrushConverter().ConvertFromString(stroke.GetString());
            if (element.TryGetProperty("Fill", out JsonElement fill))
                Fill = (Brush)new BrushConverter().ConvertFromString(fill.GetString());
            if (element.TryGetProperty("StrokeThickness", out JsonElement thickness))
                StrokeThickness = thickness.GetDouble();
        }

        public override Shape Clone()
        {
            return new TriangleShape(X1, Y1, X2, Y2, X3, Y3, Stroke, Fill, StrokeThickness);
        }

    }
}