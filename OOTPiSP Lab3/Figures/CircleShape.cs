using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;

namespace Figures
{
    public class CircleShape : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Radius { get; set; }

        public CircleShape(double cx, double cy, double radius,
                           Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Radius = radius;
        }

        public CircleShape() : base()
        {
            Cx = 100;
            Cy = 100;
            Radius = 50;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = 2 * Radius,
                Height = 2 * Radius,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, Cx - Radius);
            Canvas.SetTop(ellipse, Cy - Radius);
            canvas.Children.Add(ellipse);
        }

        public override string GetClassName() => "CircleShape";

        public override void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", GetClassName());
            writer.WriteNumber("Cx", Cx);
            writer.WriteNumber("Cy", Cy);
            writer.WriteNumber("Radius", Radius);
            writer.WriteString("Stroke", Stroke.ToString());
            writer.WriteString("Fill", Fill.ToString());
            writer.WriteNumber("StrokeThickness", StrokeThickness);
            writer.WriteEndObject();
        }

        public override void ReadJson(JsonElement element, JsonSerializerOptions options)
        {
            if (element.TryGetProperty("Cx", out JsonElement cx)) Cx = cx.GetDouble();
            if (element.TryGetProperty("Cy", out JsonElement cy)) Cy = cy.GetDouble();
            if (element.TryGetProperty("Radius", out JsonElement r)) Radius = r.GetDouble();
            if (element.TryGetProperty("Stroke", out JsonElement stroke))
                Stroke = (Brush)new BrushConverter().ConvertFromString(stroke.GetString());
            if (element.TryGetProperty("Fill", out JsonElement fill))
                Fill = (Brush)new BrushConverter().ConvertFromString(fill.GetString());
            if (element.TryGetProperty("StrokeThickness", out JsonElement thickness))
                StrokeThickness = thickness.GetDouble();
        }

        public override Shape Clone()
        {
            return new CircleShape(Cx, Cy, Radius, Stroke, Fill, StrokeThickness);
        }

    }
}