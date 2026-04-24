using Figures;
using System.Windows.Media;
using System.Xml;
using System.Globalization;

public class CircleShapeSerializer : ShapeSerializerBase<CircleShape>
{
    protected override void WriteInternal(XmlWriter writer, CircleShape shape)
    {
        writer.WriteAttributeString("Cx", shape.Cx.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Cy", shape.Cy.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Radius", shape.Radius.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("Stroke", BrushHelper.ToArgbString(shape.Stroke as SolidColorBrush));
        writer.WriteAttributeString("Fill", BrushHelper.ToArgbString(shape.Fill as SolidColorBrush));
        writer.WriteAttributeString("StrokeThickness", shape.StrokeThickness.ToString(CultureInfo.InvariantCulture));
    }

    protected override CircleShape ReadInternal(XmlReader reader)
        {
        double cx = double.Parse(reader.GetAttribute("Cx"), CultureInfo.InvariantCulture);
        double cy = double.Parse(reader.GetAttribute("Cy"), CultureInfo.InvariantCulture);
        double r = double.Parse(reader.GetAttribute("Radius"), CultureInfo.InvariantCulture);
        var stroke = BrushHelper.FromArgbString(reader.GetAttribute("Stroke"));
        var fill = BrushHelper.FromArgbString(reader.GetAttribute("Fill"));
        double thickness = double.Parse(reader.GetAttribute("StrokeThickness"), CultureInfo.InvariantCulture);
        return new CircleShape(cx, cy, r, stroke, fill, thickness);
    }
}