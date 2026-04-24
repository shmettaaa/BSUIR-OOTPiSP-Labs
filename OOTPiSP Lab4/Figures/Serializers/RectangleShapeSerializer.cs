using Figures;
using System.Windows.Media;
using System.Xml;
using System.Globalization;

public class RectangleShapeSerializer : ShapeSerializerBase<RectangleShape>
{
    protected override void WriteInternal(XmlWriter writer, RectangleShape shape)
    {
        writer.WriteAttributeString("X", shape.X.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Y", shape.Y.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Width", shape.Width.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Height", shape.Height.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Stroke", BrushHelper.ToArgbString(shape.Stroke as SolidColorBrush));
        writer.WriteAttributeString("Fill", BrushHelper.ToArgbString(shape.Fill as SolidColorBrush));
        writer.WriteAttributeString("StrokeThickness", shape.StrokeThickness.ToString(CultureInfo.InvariantCulture));
    }

    protected override RectangleShape ReadInternal(XmlReader reader)
    {
        double x = double.Parse(reader.GetAttribute("X"), CultureInfo.InvariantCulture);
        double y = double.Parse(reader.GetAttribute("Y"), CultureInfo.InvariantCulture);
        double w = double.Parse(reader.GetAttribute("Width"), CultureInfo.InvariantCulture);
        double h = double.Parse(reader.GetAttribute("Height"), CultureInfo.InvariantCulture);
        var stroke = BrushHelper.FromArgbString(reader.GetAttribute("Stroke"));
        var fill = BrushHelper.FromArgbString(reader.GetAttribute("Fill"));
        double thickness = double.Parse(reader.GetAttribute("StrokeThickness"), CultureInfo.InvariantCulture);
        return new RectangleShape(x, y, w, h, stroke, fill, thickness);
    }
}