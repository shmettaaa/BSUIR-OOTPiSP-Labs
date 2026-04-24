using Figures;
using System.Windows.Media;
using System.Xml;
using System.Globalization;

public class TriangleShapeSerializer : ShapeSerializerBase<TriangleShape>
{
    protected override void WriteInternal(XmlWriter writer, TriangleShape shape)
    {
        writer.WriteAttributeString("X1", shape.X1.ToString());
        writer.WriteAttributeString("Y1", shape.Y1.ToString());
        writer.WriteAttributeString("X2", shape.X2.ToString());
        writer.WriteAttributeString("Y2", shape.Y2.ToString());
        writer.WriteAttributeString("X3", shape.X3.ToString());
        writer.WriteAttributeString("Y3", shape.Y3.ToString());
        writer.WriteAttributeString("Stroke", BrushHelper.ToArgbString(shape.Stroke as SolidColorBrush));
        writer.WriteAttributeString("Fill", BrushHelper.ToArgbString(shape.Fill as SolidColorBrush));
        writer.WriteAttributeString("StrokeThickness", shape.StrokeThickness.ToString());
    }

    protected override TriangleShape ReadInternal(XmlReader reader)
    {
        double x1 = double.Parse(reader.GetAttribute("X1"), CultureInfo.InvariantCulture);
        double y1 = double.Parse(reader.GetAttribute("Y1"), CultureInfo.InvariantCulture);
        double x2 = double.Parse(reader.GetAttribute("X2"), CultureInfo.InvariantCulture);
        double y2 = double.Parse(reader.GetAttribute("Y2"), CultureInfo.InvariantCulture);
        double x3 = double.Parse(reader.GetAttribute("X3"), CultureInfo.InvariantCulture);
        double y3 = double.Parse(reader.GetAttribute("Y3"), CultureInfo.InvariantCulture);
        var stroke = BrushHelper.FromArgbString(reader.GetAttribute("Stroke"));
        var fill = BrushHelper.FromArgbString(reader.GetAttribute("Fill"));
        double thickness = double.Parse(reader.GetAttribute("StrokeThickness"));
        return new TriangleShape(x1, y1, x2, y2, x3, y3, stroke, fill, thickness);
    }
}