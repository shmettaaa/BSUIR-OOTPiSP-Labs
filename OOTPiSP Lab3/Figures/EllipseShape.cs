using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;

namespace Figures
{
    public class EllipseShape : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public EllipseShape(double cx, double cy, double width, double height,
                            Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Width = width;
            Height = height;
        }

        public EllipseShape() : base()
        {
            Cx = 100;
            Cy = 100;
            Width = 100;
            Height = 80;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, Cx - Width / 2);
            Canvas.SetTop(ellipse, Cy - Height / 2);
            canvas.Children.Add(ellipse);
        }

        public override string GetClassName() => "EllipseShape";

        public override void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", GetClassName());
            writer.WriteNumber("Cx", Cx);
            writer.WriteNumber("Cy", Cy);
            writer.WriteNumber("Width", Width);
            writer.WriteNumber("Height", Height);
            writer.WriteString("Stroke", Stroke.ToString());
            writer.WriteString("Fill", Fill.ToString());
            writer.WriteNumber("StrokeThickness", StrokeThickness);
            writer.WriteEndObject();
        }

        public override void ReadJson(JsonElement element, JsonSerializerOptions options)
        {
            if (element.TryGetProperty("Cx", out JsonElement cx)) Cx = cx.GetDouble();
            if (element.TryGetProperty("Cy", out JsonElement cy)) Cy = cy.GetDouble();
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
            return new EllipseShape(Cx, Cy, Width, Height, Stroke, Fill, StrokeThickness);
        }

    }
}