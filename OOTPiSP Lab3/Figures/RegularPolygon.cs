using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.Json;

namespace Figures
{
    public class RegularPolygon : ClosedShape
    {
        public double Cx { get; set; }
        public double Cy { get; set; }
        public int Sides { get; set; }
        public double Radius { get; set; }

        public RegularPolygon(double cx, double cy, int sides, double radius,
                              Brush stroke, Brush fill, double thickness)
            : base(stroke, fill, thickness)
        {
            Cx = cx;
            Cy = cy;
            Sides = sides;
            Radius = radius;
        }

        public RegularPolygon() : base()
        {
            Cx = 100;
            Cy = 100;
            Sides = 6;
            Radius = 80;
        }

        public override void Draw(Canvas canvas)
        {
            Point[] points = new Point[Sides];

            for (int i = 0; i < Sides; i++)
            {
                double angle = 2 * Math.PI * i / Sides - Math.PI / 2;
                points[i] = new Point(
                    Cx + Radius * Math.Cos(angle),
                    Cy + Radius * Math.Sin(angle)
                );
            }

            var polygon = new Polygon
            {
                Points = new PointCollection(points),
                Stroke = Stroke,
                Fill = Fill,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(polygon);
        }

        public override string GetClassName() => "RegularPolygon";

        public override void WriteJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", GetClassName());
            writer.WriteNumber("Cx", Cx);
            writer.WriteNumber("Cy", Cy);
            writer.WriteNumber("Sides", Sides);
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
            if (element.TryGetProperty("Sides", out JsonElement sides)) Sides = sides.GetInt32();
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
            return new RegularPolygon(Cx, Cy, Sides, Radius, Stroke, Fill, StrokeThickness);
        }

    }
}