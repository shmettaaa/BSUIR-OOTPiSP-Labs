using Figures;
using System.Windows.Media;
using System.Xml;

public class LineShapeSerializer : ShapeSerializerBase<LineShape>
{
    protected override void WriteInternal(XmlWriter writer, LineShape shape)
    {
        writer.WriteAttributeString("X1", shape.X1.ToString());
        writer.WriteAttributeString("Y1", shape.Y1.ToString());
        writer.WriteAttributeString("X2", shape.X2.ToString());
        writer.WriteAttributeString("Y2", shape.Y2.ToString());
        writer.WriteAttributeString("Stroke", shape.Stroke.ToString());
        writer.WriteAttributeString("StrokeThickness", shape.StrokeThickness.ToString());
    }

    protected override LineShape ReadInternal(XmlReader reader)
    {
        double x1 = double.Parse(reader.GetAttribute("X1"));
        double y1 = double.Parse(reader.GetAttribute("Y1"));
        double x2 = double.Parse(reader.GetAttribute("X2"));
        double y2 = double.Parse(reader.GetAttribute("Y2"));
        var stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(reader.GetAttribute("Stroke")));
        double thickness = double.Parse(reader.GetAttribute("StrokeThickness"));
        return new LineShape(x1, y1, x2, y2, stroke, thickness);
    }
}